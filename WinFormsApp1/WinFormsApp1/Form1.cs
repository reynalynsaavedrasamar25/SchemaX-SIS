using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SchemaX.SIS
{
    public partial class Form1 : Form
    {
        // Win32 API setup to set cue banner (placeholder) text
        private const int CB_SETCUEBANNER = 0x1703;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public Form1()
        {
            InitializeComponent();

            // Apply placeholder to your ComboBox (must be DropDown style, not DropDownList)
            cmbGender.DropDownStyle = ComboBoxStyle.DropDown;
            cmbYrLevel.DropDownStyle = ComboBoxStyle.DropDown;
            SendMessage(cmbGender.Handle, CB_SETCUEBANNER, 0, "Gender");
            SendMessage(cmbYrLevel.Handle, CB_SETCUEBANNER, 0, "Year Level");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
