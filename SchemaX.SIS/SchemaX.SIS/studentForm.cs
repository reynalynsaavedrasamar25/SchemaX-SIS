
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class studentForm : Form
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

        public studentForm()
        {
            InitializeComponent();

            this.BackColor = ThemeColors.Background;
            btnAdd.BackColor = ThemeColors.Accent;
            btnAdd.ForeColor = ThemeColors.TextLight;
            btnDelete.BackColor = ThemeColors.Danger;
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = ThemeColors.Primary;
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = ThemeColors.TextLight;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = ThemeColors.GridAlternateRow;

            // placeholder text for ComboBoxes
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYrLevel.DropDownStyle = ComboBoxStyle.DropDownList;
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

            // Get the selected student's information
            DataGridViewRow row = dgvStudents.SelectedRows[0];

            string studentId = row.Cells[0].Value?.ToString() ?? "";
            string studentName = row.Cells[1].Value?.ToString() ?? "";

            // Ask for confirmation before deleting
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete {studentName} ({studentId})?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Stop if the user clicks No
            if (result == DialogResult.No)
            {
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

            // Check if the Student ID already exists in the grid
            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (string.Equals(
                    row.Cells[0].Value?.ToString(),
                    studentId,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "This Student ID already exists.",
                        "Duplicate Student ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }
            }

            // Student Name validation
            string studentName = txtFirstName.Text.Trim();

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

            // Convert the year level to a number
            byte year = byte.Parse(yearLevel);

            // Create a Student object
            Student student = new Student
            {
                StudentId = studentId,
                FirstName = studentName,
                ContactNo = contactNumber,
                Program = program,
                Gender = gender,
                YearLevel = year
            };

            dgvStudents.Rows.Add(
                student.StudentId,
                student.FullName,
                student.Gender,
                student.YearLevel,
                student.Program,
                student.ContactNo
            );

            // Clear the input fields
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtMidName.Clear();
            txtSurname.Clear();
            txtContactNo.Clear();
            txtProgram.Clear();
            txtBloc.Clear();
            txtBirthdate.Clear();
            txtBUEmail.Clear();

            cmbGender.SelectedIndex = -1;
            cmbGender.Text = "";

            cmbYrLevel.SelectedIndex = -1;
            cmbYrLevel.Text = "";

            txtStudentID.Focus();

            MessageBox.Show(
                "Student added to the table!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnViewClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
