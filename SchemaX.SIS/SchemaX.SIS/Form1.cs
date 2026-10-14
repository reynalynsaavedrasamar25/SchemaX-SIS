
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SchemaX.SIS.Helpers;

namespace SchemaX.SIS
{
    public partial class Form1 : Form
    {
        // Win32 API setup to set cue banner (placeholder) text
        private const int CB_SETCUEBANNER = 0x1703;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam
        );

        public Form1()
        {
            InitializeComponent();

            this.BackColor = ThemeColors.Background;
            btnAdd.BackColor = ThemeColors.Accent;
            btnAdd.ForeColor = ThemeColors.TextLight;
            btnDelete.BackColor = ThemeColors.Danger;
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = ThemeColors.Primary;
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = ThemeColors.TextLight;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = ThemeColors.GridAlternateRow;

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a student before deleting.",
                    "No Student Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a student before editing.",
                    "No Student Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Student ID validation
            string studentId = txtStudentID.Text.Trim();

            if (!Validator.IsRequiredFieldValid(studentId))
            {
                MessageBox.Show(
                    "Please enter a Student ID.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!Validator.IsStudentIdValid(studentId))
            {
                MessageBox.Show(
                    "Student ID must follow the format YYYY-XXXXX (e.g., 2026-00001).",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Student Name validation
            string studentName = txtName.Text.Trim();

            if (!Validator.IsRequiredFieldValid(studentName))
            {
                MessageBox.Show(
                    "Please enter the student's name.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!Validator.IsNameValid(studentName))
            {
                MessageBox.Show(
                    "Student name can only contain letters, spaces, hyphens, and apostrophes.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Contact Number validation
            string contactNumber = txtContactNo.Text.Trim();

            if (!Validator.IsRequiredFieldValid(contactNumber))
            {
                MessageBox.Show(
                    "Please enter a contact number.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!Validator.IsContactNumberValid(contactNumber))
            {
                MessageBox.Show(
                    "Contact number must be 7 to 15 characters and contain only numbers, spaces, hyphens, or a plus sign.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Program validation
            string program = txtProgram.Text.Trim();

            if (!Validator.IsRequiredFieldValid(program))
            {
                MessageBox.Show(
                    "Please enter the student's program.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Gender validation
            string gender = cmbGender.Text.Trim();

            if (!Validator.IsRequiredFieldValid(gender))
            {
                MessageBox.Show(
                    "Please select a gender.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!cmbGender.Items.Cast<object>()
                .Any(item => string.Equals(
                    item?.ToString(),
                    gender,
                    StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(
                    "Please select a valid gender from the list.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Year Level validation
            string yearLevel = cmbYrLevel.Text.Trim();

            if (!Validator.IsRequiredFieldValid(yearLevel))
            {
                MessageBox.Show(
                    "Please select a year level.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!cmbYrLevel.Items.Cast<object>()
                .Any(item => string.Equals(
                    item?.ToString(),
                    yearLevel,
                    StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(
                    "Please select a valid year level from the list.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
        }
    }
}
