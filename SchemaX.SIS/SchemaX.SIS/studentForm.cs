
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
        // set up repository for database operations
        private readonly StudentRepository _studentRepository = new StudentRepository();

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

            dateBirthDate.MaxDate = DateTime.Today;

            this.BackColor = ThemeColors.Background;
            btnAdd.BackColor = ThemeColors.Accent;
            btnAdd.ForeColor = ThemeColors.TextLight;
            btnDelete.BackColor = ThemeColors.Danger;
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = ThemeColors.Primary;
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = ThemeColors.TextLight;
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = ThemeColors.GridAlternateRow;

            // placeholder text for ComboBoxes
            cmbProgramFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYrFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            SendMessage(cmbProgramFilter.Handle, CB_SETCUEBANNER, 0, "Program");
            SendMessage(cmbYrFilter.Handle, CB_SETCUEBANNER, 0, "Year Level");

            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYrLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            SendMessage(cmbGender.Handle, CB_SETCUEBANNER, 0, "Gender");
            SendMessage(cmbYrLevel.Handle, CB_SETCUEBANNER, 0, "Year Level");
            SendMessage(cmbProgram.Handle, CB_SETCUEBANNER, 0, "Program");
        }


        public void LoadStudents()
        {
            // Clear the DGV and load all students
            List<Student> students = _studentRepository.GetAll();
            dgvStudents.Rows.Clear();
            
            string year = cmbYrFilter.SelectedIndex > 0
                ? cmbYrFilter.SelectedIndex.ToString()
                : "All";

            string program = cmbProgramFilter.SelectedIndex > 0
                ? cmbProgramFilter.Text
                : "All";

            try
            {
                var students = _studentRepository.Search(
                    txtSearch.Text, year, program);

                dgvStudents.Rows.Clear();

                foreach (System.Data.DataRow row in students.Rows)
                {
                    dgvStudents.Rows.Add(
                        row["Student ID"],
                        row["Name"],
                        row["Gender"],
                        row["Year"],
                        row["Program"],
                        row["Contact"]
                    );
                }

                labelStudentsShown.Text =
                    $"Students shown: {students.Rows.Count}";
            }
            catch (Exception ex)
            {
                SchemaX.SIS.Helpers.ExceptionHandler.Handle(
                    ex,
                    "Unable to load students. Please check your database connection and try again."
                );
            }
            
            UnlockIDClearSlction();
        }
            

        public void UnlockIDClearSlction()
        {
            dgvStudents.ClearSelection();

            labelStudentIDHint.Visible = false;
            txtStudentID.ReadOnly = false;
            txtStudentID.BackColor = ThemeColors.Surface; //change the background color to indicate it's read-only
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
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
                $"Are you sure you want to delete {studentName} ({studentId})?\n\n" +
                "Warning: Deleting this student may also delete related records, " +
                "such as enrolled subjects and attendance records. This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }
            bool deleted = false;
            try
            {
                deleted = _studentRepository.Delete(studentId);
                
                MessageBox.Show(
                    deleted ? "Student deleted successfully!" : "No student record was deleted.",
                    deleted ? "Success" : "Deleted failed", MessageBoxButtons.OK,
                    deleted ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not delete the student. Please try again.");
            }
            LoadStudents();
            if (deleted)
                ExitEditMode();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0 || !_isEdit)
            {
                MessageBox.Show(
                    "Please select a student before editing.",
                    "No Student Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string? id = dgvStudents.SelectedRows[0]
                .Cells["StudentID"].Value?.ToString();

            SaveStudent(sender, e);
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
                cmbProgram.Text = s.Program;
                txtBloc.Text = s.Bloc;
                dateBirthDate.Value = s.Birthday;
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
                               txtBloc, txtBUEmail, txtContactNo })
                tb.Clear();

            dateBirthDate.Value = DateTime.Today;

            cmbGender.SelectedIndex = -1; cmbGender.Text = "";
            cmbYrLevel.SelectedIndex = -1; cmbYrLevel.Text = "";
            cmbProgram.SelectedIndex = -1; cmbProgram.Text = "";

            UnlockIDClearSlction();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                MessageBox.Show(
                    "You are editing an existing student. Click Edit to save changes.",
                    "Edit Mode",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            SaveStudent(sender, e);
        }

        private void SaveStudent(object sender, EventArgs e)
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
                    "Student ID must follow the format YYYY-XX-XXXXX (e.g., 2025-01-06767).",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            } if (!_isEdit)
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

            // Surname validation
            string surname = txtSurname.Text.Trim();

            if (!Validator.IsRequiredFieldValid(surname))
            {
                MessageBox.Show(
                    "Please enter a valid surname (letters, spaces, hypens, apostrophes).",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!Validator.IsNameValid(surname))
            {
                MessageBox.Show(
                    "Surname can only contain letters, spaces, hyphens, and apostrophes.",
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
            string program = cmbProgram.Text.Trim();

            if (!Validator.IsRequiredFieldValid(program))
            {
                MessageBox.Show(
                    "Please select the student's program.",
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

            // Year Level validation
            string buEmail = txtBUEmail.Text.Trim();

            if (!Validator.IsBuEmailValid(buEmail))
            {
                MessageBox.Show(
                    "Please input the BU Email, ensuring it ends with '@bicol-u.edu.ph'.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Convert the year level to a number
            byte year = byte.Parse(yearLevel);
            DateTime birthday = dateBirthDate.Value;

            string middleName = txtMidName.Text.Trim();
            // Middle name validation (optional)
            if (middleName.Length > 0 && !Validator.IsNameValid(middleName))
            {
                Warn("Middle name can only contain letters, spaces, hypens, and apostrophes.");
                return;
            }

            string bloc = txtBloc.Text.Trim();
            // Bloc validation (required)
            if (!Validator.IsRequiredFieldValid(bloc))
            {
                Warn("Please enter the student's bloc.");
                return;
            }

            string buEmail = txtBUEmail.Text.Trim();
            // BU Email validation
            if (!Validator.IsBuEmailValid(buEmail))
            {
                Warn("Email must be a valid @bicol-u.edu.ph address.");
                return;
            }

            // Birthday validation
            if (!Validator.IsBirthdayValid(dateBirthDate.Value))
            {
                Warn("Please select a valid birthday (it can't be today or in the future).");
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
                Birthday = dateBirthDate.Value.Date,
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
                        LoadStudents();
                        ExitEditMode();
                        MessageBox.Show(
                            "Student updated successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
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

                    LoadStudents();
                    ClearStudentFields();
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
                if (ex is MySqlConnector.MySqlException mysqlEx &&
                    mysqlEx.Number == 1062)
                {
                    MessageBox.Show(
                        "That Student ID or BU email already exists.",
                        "Duplicate Student",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    SchemaX.SIS.Helpers.ExceptionHandler.Handle(
                        ex,
                        "Failed to save student. Please check your database connection and try again."
                    );
                }
            }

        }

        private void btnViewClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbYrFilter.SelectedIndex = 0;
            cmbProgramFilter.SelectedIndex = 0;
            LoadStudents();
        }

        private void btnViewSearch_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string studentId =
                dgvStudents.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(studentId))
                return;

            EnterEditMode(studentId);

            labelStudentIDHint.Visible = true;
            txtStudentID.ReadOnly = true;
            txtStudentID.BackColor = ThemeColors.Danger;
            btnAdd.Text = "Update";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            ExitEditMode();
            LoadStudents();
        }

        private void studentForm_Load(object sender, EventArgs e)
        {
            cmbYrFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgramFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYrFilter.SelectedIndex = 0;
            cmbProgramFilter.SelectedIndex = 0;
            cmbYrFilter.SelectedIndexChanged += (s, e) => LoadStudents();
            cmbProgramFilter.SelectedIndexChanged += (s, e) => LoadStudents();
            LoadStudents();
            UnlockIDClearSlction();
            ExitEditMode();
            btnAdd.Text = "Add";
        }


        private void ClearStudentFields()
        {
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtMidName.Clear();
            txtSurname.Clear();
            txtContactNo.Clear();
            cmbProgram.SelectedIndex = -1;
            txtBloc.Clear();
            txtBUEmail.Clear();

            cmbGender.SelectedIndex = -1;
            cmbGender.Text = "";

            cmbYrLevel.SelectedIndex = -1;
            cmbYrLevel.Text = "";

            dateBirthDate.Value = DateTime.Today;
        }

        private void studentForm_Load(object sender, EventArgs e)
        {   
            LoadStudents();
            UnlockIDClearSlction();
        }
        
        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    FileName = "Student_Report.csv",
                    Title = "Export Students to CSV"
                };

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                using var writer = new System.IO.StreamWriter(
                    dialog.FileName, false, System.Text.Encoding.UTF8);

                var columns = dgvStudents.Columns
                    .Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .OrderBy(c => c.DisplayIndex)
                    .ToList();

                string EscapeCsv(object? value)
                {
                    string text = Convert.ToString(value) ?? "";
                    return "\"" + text.Replace("\"", "\"\"") + "\"";
                }

                writer.WriteLine(string.Join(",",
                    columns.Select(c => EscapeCsv(c.HeaderText))));

                foreach (DataGridViewRow row in dgvStudents.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    writer.WriteLine(string.Join(",",
                        columns.Select(c => EscapeCsv(
                            row.Cells[c.Index].FormattedValue))));
                }

                MessageBox.Show(
                    "Students exported successfully!",
                    "Export Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "CSV export failed: " + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void Warn(string message) => 
            MessageBox.Show(message,
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
    }
}
