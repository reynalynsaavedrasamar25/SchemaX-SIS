using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace SchemaX.SIS.Helpers
{
    public static class InputStyler
    {
        private class InputState
        {
            public bool Hover;
            public bool Focused;
            public Color BaseBorder = Color.FromArgb(186, 200, 222);
            public Color FocusBorder = ThemeColors.Accent; // Matches your theme accent color
        }

        private static readonly ConditionalWeakTable<Control, InputState> States = new();

        /// <summary>
        /// Applies rounded styling, clean padding, and custom borders to a TextBox, ComboBox, or DateTimePicker.
        /// </summary>
        public static void Apply(Control control)
        {
            if (control == null) return;
            if (States.TryGetValue(control, out _)) return; // Prevent double-wrapping

            var parent = control.Parent;
            if (parent == null) return;

            int originalTabIndex = control.TabIndex;

            // 1. Force a custom wrapper height (e.g., 36px or 38px) to make it thicker 
            // independently of the inner control's font/height limit.
            int targetHeight = Math.Max(control.Height + 10, 36); 

            var wrapper = new Panel
            {
                Name = control.Name + "_Wrapper",
                Size = new Size(control.Width, targetHeight),
                Location = control.Location,
                BackColor = Color.White,
                Cursor = control.Cursor,
                TabIndex = originalTabIndex,
                TabStop = true
            };

            var state = States.GetOrCreateValue(control);

            // 2. Center the inner control vertically inside the thicker wrapper
            control.Location = new Point(12, (wrapper.Height - control.Height) / 2);
            control.Width = wrapper.Width - 24; // 12px padding left/right
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            control.TabIndex = 0;

            if (control is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.None;
                tb.Font = new Font("Segoe UI", 9.5F);
            }
            else if (control is ComboBox cb)
            {
                cb.FlatStyle = FlatStyle.Flat;
                cb.Font = new Font("Segoe UI", 9.5F);
            }
            else if (control is DateTimePicker dtp)
            {
                dtp.Font = new Font("Segoe UI", 9.5F);
            }

            parent.Controls.Add(wrapper);
            parent.Controls.Remove(control);
            wrapper.Controls.Add(control);

            // Wire up events
            wrapper.Paint += (s, e) => DrawWrapper(wrapper, e, state);
            wrapper.Resize += (s, e) => {
                control.Location = new Point(12, (wrapper.Height - control.Height) / 2);
                control.Width = wrapper.Width - 24;
                wrapper.Invalidate();
            };

            wrapper.GotFocus += (s, e) => control.Focus();

            control.MouseEnter += (s, e) => { state.Hover = true; wrapper.Invalidate(); };
            control.MouseLeave += (s, e) => { state.Hover = false; wrapper.Invalidate(); };
            control.GotFocus += (s, e) => { state.Focused = true; wrapper.Invalidate(); };
            control.LostFocus += (s, e) => { state.Focused = false; wrapper.Invalidate(); };

            wrapper.MouseEnter += (s, e) => { state.Hover = true; wrapper.Invalidate(); };
            wrapper.MouseLeave += (s, e) => { state.Hover = false; wrapper.Invalidate(); };
            wrapper.MouseDown += (s, e) => { control.Focus(); };

            wrapper.Invalidate();
        }

        private static void DrawWrapper(Panel wrapper, PaintEventArgs e, InputState state)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(wrapper.Parent?.BackColor ?? SystemColors.Control);

            Color fill = Color.White;
            Color border = state.Focused ? state.FocusBorder 
                         : state.Hover ? Color.FromArgb(140, 160, 190) 
                         : state.BaseBorder;
            float borderWidth = state.Focused ? 2f : 1.5f;

            // FIX CLIP: Inset the rectangle slightly (by 1-2 pixels) so thick pens 
            // and anti-aliasing aren't cut off at the top and right edges.
            var rect = new Rectangle(1, 1, wrapper.Width - 3, wrapper.Height - 3);

            using (var path = RoundedRect(rect, 10)) // Radius 10-12 fits nicely with a thicker box
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(border, borderWidth))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}