using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace SchemaX.SIS.Helpers
{
    public enum ButtonKind { Menu, Danger, Solid }

    public static class ButtonStyler
    {
        private class State
        {
            public bool Hover, Down, Active, Focus; //added Focus field for keyboard users
            public string Glyph = "";
            public ButtonKind Kind;
            public Color Base = ThemeColors.Danger;
        }

        private static readonly ConditionalWeakTable<Button, State> States = new();

        public static void Apply(Button b, string glyph, ButtonKind kind, Color? solidColor = null)
        {
            var s = States.GetOrCreateValue(b);
            s.Glyph = glyph;
            s.Kind = kind;
            if (solidColor.HasValue) s.Base = solidColor.Value;

            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.UseVisualStyleBackColor = false;
            b.Cursor = Cursors.Hand;
            b.Font = new Font("Segoe UI Semibold", 9F);

            b.Paint -= OnPaint; b.Paint += OnPaint;
            b.MouseEnter -= OnEnter; b.MouseEnter += OnEnter;
            b.MouseLeave -= OnLeave; b.MouseLeave += OnLeave;
            b.MouseDown -= OnDown; b.MouseDown += OnDown;
            b.MouseUp -= OnUp; b.MouseUp += OnUp;

            b.GotFocus -= OnFocus; b.GotFocus += OnFocus;
            b.LostFocus -= OnBlur; b.LostFocus += OnBlur;

            b.Invalidate();
        }

        // highlights a button as "selected" (solid blue)
        public static void SetActive(Button b, bool active)
        {
            if (States.TryGetValue(b, out var s)) { s.Active = active; b.Invalidate(); }
        }

        private static void Change(object? sender, Action<State> change)
        {
            if (sender is Button b && States.TryGetValue(b, out var s)) { change(s); b.Invalidate(); }
        }
        private static void OnEnter(object? sender, EventArgs e) => Change(sender, s => s.Hover = true);
        private static void OnLeave(object? sender, EventArgs e) => Change(sender, s => { s.Hover = false; s.Down = false; });
        private static void OnDown(object? sender, MouseEventArgs e) => Change(sender, s => s.Down = true);
        private static void OnUp(object? sender, MouseEventArgs e) => Change(sender, s => s.Down = false);

        // add focus and blur events for keyboard users
        private static void OnFocus(object? sender, EventArgs e) => Change(sender, s => s.Focus = true);
        private static void OnBlur(object? sender, EventArgs e) => Change(sender, s => s.Focus = false);

        private static void OnPaint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button b || !States.TryGetValue(b, out var s)) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(b.Parent?.BackColor ?? SystemColors.Control);

            bool isHighlighted = s.Hover || s.Focus; // highlight on hover or focus

            Color fill, border, textColor, circle, glyphColor;
            switch (s.Kind)
            {
                case ButtonKind.Danger:
                    fill = s.Down ? Color.FromArgb(248, 215, 211)
                         : s.Hover ? Color.FromArgb(253, 236, 234) : Color.White;
                    border = Color.FromArgb(235, 190, 185);
                    textColor = ThemeColors.Danger;
                    circle = ThemeColors.Danger;
                    glyphColor = Color.White;
                    break;
                case ButtonKind.Solid:
                    fill = s.Down ? ControlPaint.Dark(s.Base, 0.2f)
                         : s.Hover ? ControlPaint.Dark(s.Base, 0.1f) : s.Base;
                    border = fill;
                    textColor = Color.White;
                    circle = Color.White;
                    glyphColor = s.Base;
                    break;
                default: // Menu
                    fill = s.Down ? Color.FromArgb(169, 182, 204)
                         : s.Hover ? Color.FromArgb(186, 200, 222) : Color.White;
                    border = Color.FromArgb(186, 200, 222);
                    textColor = ThemeColors.TextDark;
                    circle = ThemeColors.Accent;
                    glyphColor = Color.White;
                    break;
            }
            if (s.Active)
            {
                fill = ThemeColors.Accent; border = ThemeColors.Accent;
                textColor = Color.White; circle = Color.White; glyphColor = ThemeColors.Accent;
            }

            var rect = new Rectangle(0, 0, b.Width - 1, b.Height - 1);
            using (var path = RoundedRect(rect, 12))
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(border))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            int x = 14;
            if (s.Glyph.Length > 0)
            {
                int d = Math.Min(34, b.Height - 16);
                var circleRect = new Rectangle(x, (b.Height - d) / 2, d, d);
                using (var cb = new SolidBrush(circle)) g.FillEllipse(cb, circleRect);
                using var iconFont = new Font("Segoe MDL2 Assets", 12F);
                TextRenderer.DrawText(g, s.Glyph, iconFont, circleRect, glyphColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                x += d + 12;
            }

            var textRect = new Rectangle(x, 0, b.Width - x - 10, b.Height);
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis |
                        (s.Glyph.Length > 0 ? TextFormatFlags.Left : TextFormatFlags.HorizontalCenter);
            TextRenderer.DrawText(g, b.Text, b.Font, textRect, textColor, flags);
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
