
using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

            // Delete the student from the database
            try
            {
                StudentRepository repository = new StudentRepository();
                bool deleted = repository.Delete(studentId);

                if (deleted)
                {
                    // Remove the student from the grid
                    dgvStudents.Rows.Remove(row);

                    MessageBox.Show(
                        "Student deleted successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "No student record was deleted.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to delete student.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

            string? id = dgvStudents.CurrentRow.Cells["StudentID"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please select a student first.", "No selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EnterEditMode(id);
        }

        private bool _isEdit;

        // fill the fields, lock the Student ID, change the title
        private void EnterEditMode(string studentId)
        {
            try
            {
                StudentRepository repository = new StudentRepository();

                Student? s = repository.GetById(studentId);
                if (s == null)
                {
                    MessageBox.Show("That student no longer exists.", "Not found",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _isEdit = true;

                txtStudentID.Text = s.StudentId;
                txtFirstName.Text = s.FirstName;
                txtMidName.Text = s.MiddleName ?? "";
                txtSurname.Text = s.Surname;
                cmbGender.Text = s.Gender;
                cmbYrLevel.Text = s.YearLevel.ToString();
                txtProgram.Text = s.Program;
                txtBloc.Text = s.Bloc;
                txtBirthdate.Text = s.Birthday.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                txtBUEmail.Text = s.BuEmail;
                txtContactNo.Text = s.ContactNo;

                txtStudentID.ReadOnly = true;                         // lock Student ID
                Text = "Student Information System - EDIT STUDENT";   // title changes
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database. Please check that MySQL is running.",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Returns the form to Add mode 
        private void ExitEditMode()
        {
            _isEdit = false;
            txtStudentID.ReadOnly = false;
            Text = "Student Information System";

            foreach (var tb in new[] { txtStudentID, txtFirstName, txtMidName, txtSurname,
                               txtProgram, txtBloc, txtBirthdate, txtBUEmail, txtContactNo })
                tb.Clear();

            cmbGender.SelectedIndex = -1; cmbGender.Text = "";
            cmbYrLevel.SelectedIndex = -1; cmbYrLevel.Text = "";
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

            // Check for duplicate Student ID only when adding a new student
            if (!_isEdit)
            {
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

            string middleName = txtMidName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string bloc = txtBloc.Text.Trim();
            string buEmail = txtBUEmail.Text.Trim();

            if(!DateTime.TryParse(txtBirthdate.Text.Trim(), out DateTime birthday))
            {
                MessageBox.Show("Please enter a valid birthday.");
                return;
            }

            // Create a Student object
            Student student = new Student
            {
                StudentId = studentId,
                FirstName = studentName,
                MiddleName = string.IsNullOrWhiteSpace(middleName) ? null : middleName,
                Surname = surname,
                Gender = gender,
                YearLevel = year,
                Program = program,
                Bloc = bloc,
                Birthday = birthday,
                BuEmail = buEmail,
                ContactNo = contactNumber,
                PhotoPath = null
            };

            // save the student to MySQL
            try
            {
                StudentRepository repository = new StudentRepository();

                if (_isEdit)
                {
                    bool updated = repository.Update(student);

                    if (updated)
                    {
                        // Update the selected student's information in the grid
                        foreach (DataGridViewRow row in dgvStudents.Rows)
                        {
                            if (row.IsNewRow)
                                continue;

                            if (string.Equals(
                                row.Cells[0].Value?.ToString(),
                                student.StudentId,
                                StringComparison.OrdinalIgnoreCase))
                            {
                                row.Cells[1].Value = student.FullName;
                                row.Cells[2].Value = student.Gender;
                                row.Cells[3].Value = student.YearLevel;
                                row.Cells[4].Value = student.Program;
                                row.Cells[5].Value = student.ContactNo;
                                break;
                            }
                        }

                        MessageBox.Show(
                            "Student updated successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        ExitEditMode();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No student record was updated.",
                            "Update Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
                else
                {
                    repository.Insert(student);

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
                        "Student saved successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save student to the database.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

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
