using MySqlConnector;
using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SchemaX.SIS
{
    public partial class studentRecordForm : Form
    {
        private readonly StudentRepository _students = new();
        private readonly AcademicRepository _academic = new();
        private readonly Bitmap _placeholder = MakePlaceholder();

        private List<Student> _list = new();
        private Student? _selected;
        private bool _alphabetical;
        private bool _loading;                 // true while we fill grids in code

        public studentRecordForm()
        {
            InitializeComponent();
            BuildGrids();
            StyleControls();

            cmbYear.SelectedIndex = 0;
            cmbProgram.SelectedIndex = 0;

            txtSearch.TextChanged += (s, e) => LoadStudentList(_selected?.StudentId);
            cmbYear.SelectedIndexChanged += (s, e) => LoadStudentList(_selected?.StudentId);
            cmbProgram.SelectedIndexChanged += (s, e) => LoadStudentList(_selected?.StudentId);
            btnSort.Click += btnSort_Click;
            dgvStudentList.SelectionChanged += dgvStudentList_SelectionChanged;
            dgvAcademic.SelectionChanged += dgvAcademic_SelectionChanged;
            btnAddSub.Click += btnAddSub_Click;
            btnEditSub.Click += btnEditSub_Click;
            btnDeleteSub.Click += btnDeleteSub_Click;
            btnClearSub.Click += (s, e) => ClearSubjectFields();
            btnUpload.Click += btnUpload_Click;
            btnBack.Click += (s, e) => Close();
            txtSubjectCode.Leave += txtSubjectCode_Leave;

            Load += (s, e) => LoadStudentList(null);
        }

        private void BuildGrids()
        {
            dgvStudentList.Columns.Add("colId", "Student ID");
            dgvStudentList.Columns.Add("colName", "Name");
            dgvStudentList.Columns[0].Width = 105;
            dgvStudentList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvAcademic.Columns.Add("colCode", "Subject Code");
            dgvAcademic.Columns.Add("colTitle", "Subject");
            dgvAcademic.Columns.Add("colUnits", "Units");
            dgvAcademic.Columns.Add("colGrade", "Grade");
            dgvAcademic.Columns[0].Width = 110;
            dgvAcademic.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAcademic.Columns[2].Width = 60;
            dgvAcademic.Columns[3].Width = 70;
            dgvAcademic.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAcademic.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridView g in new[] { dgvStudentList, dgvAcademic })
            {
                g.ReadOnly = true;
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.AllowUserToResizeRows = false;
                g.MultiSelect = false;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.RowHeadersVisible = false;
                g.ScrollBars = ScrollBars.Vertical;            // the scrollbar beside the list
                g.BackgroundColor = ThemeColors.Surface;
                g.BorderStyle = BorderStyle.FixedSingle;
                g.EnableHeadersVisualStyles = false;
                g.ColumnHeadersDefaultCellStyle.BackColor = ThemeColors.Primary;
                g.ColumnHeadersDefaultCellStyle.ForeColor = ThemeColors.TextLight;
                g.AlternatingRowsDefaultCellStyle.BackColor = ThemeColors.GridAlternateRow;
                g.DefaultCellStyle.SelectionBackColor = ThemeColors.Accent;
                g.DefaultCellStyle.SelectionForeColor = ThemeColors.TextLight;
            }
        }

        private void StyleControls()
        {
            BackColor = ThemeColors.Background;

            pnlTop.BackColor = ThemeColors.Primary;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = ThemeColors.TextLight;

            pnlAddSubject.BackColor = ThemeColors.GridAlternateRow;
            lblAddSubject.BackColor = ThemeColors.Primary;
            lblAddSubject.ForeColor = ThemeColors.TextLight;
            lblAddSubject.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblSubjectHint.ForeColor = Color.DimGray;

            StyleButton(btnAddSub, ThemeColors.Accent);
            StyleButton(btnEditSub, ThemeColors.Accent);
            StyleButton(btnDeleteSub, ThemeColors.Danger);
            StyleButton(btnClearSub, ThemeColors.Primary);
            StyleButton(btnBack, ThemeColors.Primary);
            StyleButton(btnSort, ThemeColors.Accent);
            StyleButton(btnUpload, ThemeColors.Accent);

            lblProfileTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProfileTitle.ForeColor = ThemeColors.Primary;
            lblFullName.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblFullName.ForeColor = ThemeColors.TextDark;
            foreach (Label l in new[] { lblProfileLeft, lblProfileRight, lblProfileEmail })
            {
                l.Font = new Font("Segoe UI", 10F);
                l.ForeColor = ThemeColors.TextDark;
            }
            lblAcademicTitle.BackColor = ThemeColors.GridAlternateRow;
            lblAcademicTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAcademicTitle.ForeColor = ThemeColors.Primary;
            lblGwa.BackColor = ThemeColors.GridAlternateRow;
            lblGwa.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblGwa.ForeColor = ThemeColors.Primary;
            lblEmpty.Font = new Font("Segoe UI", 13F);
            lblEmpty.ForeColor = Color.DimGray;
        }

        private static void StyleButton(Button b, Color back)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = back;
            b.ForeColor = ThemeColors.TextLight;
            b.Cursor = Cursors.Hand;
        }

        private static Bitmap MakePlaceholder()
        {
            var bmp = new Bitmap(150, 150);
            using var g = Graphics.FromImage(bmp);
            g.Clear(ThemeColors.GridAlternateRow);
            using var font = new Font("Segoe UI", 16F);
            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString("Photo", font, Brushes.Gray, new RectangleF(0, 0, 150, 150), sf);
            return bmp;
        }

        private static void Warn(string message) =>
            MessageBox.Show(message, "Check your input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void LoadStudentList(string? keepId)
        {
            try
            {
                int year = cmbYear.SelectedIndex > 0 ? cmbYear.SelectedIndex : 0;
                string program = cmbProgram.SelectedIndex > 0 ? cmbProgram.Text : "";
                _list = _students.GetList(txtSearch.Text, year, program, _alphabetical);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not load the student list. Check that MySQL is running.");
                return;
            }

            _loading = true;
            dgvStudentList.Rows.Clear();
            foreach (Student s in _list)
            {
                string name = $"{s.Surname}, {s.FirstName} {s.MiddleName}".Trim();
                int i = dgvStudentList.Rows.Add(s.StudentId, name);
                dgvStudentList.Rows[i].Tag = s;
            }

            int index = -1;
            if (_list.Count > 0)
            {
                index = keepId == null ? 0 : _list.FindIndex(s => s.StudentId == keepId);
                if (index < 0) index = 0;                       // default: first student
                dgvStudentList.ClearSelection();
                dgvStudentList.CurrentCell = dgvStudentList.Rows[index].Cells[0];
                dgvStudentList.Rows[index].Selected = true;
            }
            _loading = false;

            ShowStudent(index >= 0 ? _list[index] : null);
        }

        private void btnSort_Click(object? sender, EventArgs e)
        {
            _alphabetical = !_alphabetical;
            btnSort.Text = _alphabetical ? "Sort: A-Z (surname)" : "Sort: by ID";
            LoadStudentList(_selected?.StudentId);
        }

        private void dgvStudentList_SelectionChanged(object? sender, EventArgs e)
        {
            if (_loading || dgvStudentList.SelectedRows.Count == 0) return;
            ShowStudent((Student)dgvStudentList.SelectedRows[0].Tag!);
        }

        // -------------------------------------------- profile + academic record (right)
        private void ShowStudent(Student? s)
        {
            _selected = s;
            pnlDetails.Visible = s != null;
            lblEmpty.Visible = s == null;

            if (s == null)
            {
                bool filtering = txtSearch.Text.Trim().Length > 0
                                 || cmbYear.SelectedIndex > 0 || cmbProgram.SelectedIndex > 0;
                lblEmpty.Text = filtering
                    ? "No students match your search."
                    : "No students yet. Add some in Enrolled Students.";
                ClearSubjectFields();
                return;
            }

            lblFullName.Text = s.FullName;
            lblProfileLeft.Text =
                "Student No.: " + s.StudentId + "\n" +
                "First Name: " + s.FirstName + "\n" +
                "Middle Name: " + (string.IsNullOrWhiteSpace(s.MiddleName) ? "-" : s.MiddleName) + "\n" +
                "Surname: " + s.Surname + "\n" +
                "Gender: " + s.Gender;
            lblProfileRight.Text =
                "Year Level: " + s.YearLevel + "\n" +
                "Program: " + s.Program + "\n" +
                "Bloc: " + s.Bloc + "\n" +
                "Birthday: " + s.Birthday.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "\n" +
                "Contact: " + s.ContactNo;
            lblProfileEmail.Text = "BU Email: " + s.BuEmail;

            SetPhoto(s.PhotoPath);
            LoadAcademic();
        }

        private void SetPhoto(string? path)
        {
            Image? old = picPhoto.Image;
            picPhoto.Image = PhotoStore.Load(path) ?? _placeholder;
            if (old != null && old != _placeholder) old.Dispose();
        }

        private void LoadAcademic()
        {
            if (_selected == null) return;

            List<EnrolledSubject> subjects;
            try
            {
                subjects = _academic.GetEnrolled(_selected.StudentId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not load the academic record.");
                return;
            }

            _loading = true;
            dgvAcademic.Rows.Clear();
            foreach (EnrolledSubject x in subjects)
            {
                int i = dgvAcademic.Rows.Add(
                    x.SubjectCode, x.Title, x.Units,
                    x.Grade.HasValue ? x.Grade.Value.ToString("0.00", CultureInfo.InvariantCulture) : "");
                dgvAcademic.Rows[i].Tag = x;
            }
            _loading = false;

            int graded = subjects.Count(x => x.Grade.HasValue);
            lblGwa.Text = "GWA: " + GwaCalculator.Format(GwaCalculator.Compute(subjects)) +
                          (subjects.Count > 0 ? $"     ({graded} of {subjects.Count} subjects graded)" : "");

            ClearSubjectFields();
        }
        private void dgvAcademic_SelectionChanged(object? sender, EventArgs e)
        {
            if (_loading) return;
            if (dgvAcademic.SelectedRows.Count == 0) { SetMode(false); return; }

            var x = (EnrolledSubject)dgvAcademic.SelectedRows[0].Tag!;
            txtSubject.Text = x.Title;
            txtSubjectCode.Text = x.SubjectCode;
            txtUnits.Text = x.Units.ToString();
            txtGrade.Text = x.Grade.HasValue
                ? x.Grade.Value.ToString("0.00", CultureInfo.InvariantCulture) : "";
            SetMode(true);
        }

        // rowSelected = true  -> edit mode (only the grade can be changed)
        // rowSelected = false -> add mode
        private void SetMode(bool rowSelected)
        {
            bool has = _selected != null;

            foreach (TextBox t in new[] { txtSubject, txtSubjectCode, txtUnits, txtGrade })
                t.Enabled = has;
            txtSubject.ReadOnly = rowSelected;
            txtSubjectCode.ReadOnly = rowSelected;
            txtUnits.ReadOnly = rowSelected;

            btnAddSub.Enabled = has && !rowSelected;
            btnEditSub.Enabled = has && rowSelected;
            btnDeleteSub.Enabled = has && rowSelected;
            btnClearSub.Enabled = has;
            btnUpload.Enabled = has;

            lblSubjectHint.Text = !has
                ? "Select a student first."
                : rowSelected
                    ? "Editing the selected subject. Only the grade can be changed. " +
                      "Leave it blank to remove the grade. Click Clear to add a new subject."
                    : "Fill in the subject, code and units, then click Add. " +
                      "The grade can be left blank until it is released.";
        }

        private void ClearSubjectFields()
        {
            txtSubject.Clear();
            txtSubjectCode.Clear();
            txtUnits.Clear();
            txtGrade.Clear();

            _loading = true;
            dgvAcademic.ClearSelection();
            dgvAcademic.CurrentCell = null;
            _loading = false;

            SetMode(false);
        }

        private bool TryReadGrade(out decimal? grade)
        {
            grade = null;
            string t = txtGrade.Text.Trim();
            if (t.Length == 0) return true;                     // blank is allowed

            if (!decimal.TryParse(t, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal g)
                || !GwaCalculator.IsValidGrade(g))
            {
                Warn("Grade must be a number from 1.00 to 5.00 (up to 2 decimals), or left blank.");
                txtGrade.Focus();
                return false;
            }
            grade = g;
            return true;
        }

        private void btnAddSub_Click(object? sender, EventArgs e)
        {
            if (_selected == null) return;

            string title = txtSubject.Text.Trim();
            string code = txtSubjectCode.Text.Trim().ToUpperInvariant();

            if (title.Length == 0 || title.Length > 100)
            { Warn("Enter the subject name (up to 100 characters)."); txtSubject.Focus(); return; }

            if (code.Length == 0 || !Regex.IsMatch(code, @"^[A-Z0-9 \-]{1,15}$"))
            { Warn("Enter the subject code (letters, numbers, space or hyphen, up to 15 characters)."); txtSubjectCode.Focus(); return; }

            if (!int.TryParse(txtUnits.Text.Trim(), out int units) || units < 1 || units > 6)
            { Warn("Units must be a whole number from 1 to 6."); txtUnits.Focus(); return; }

            if (!TryReadGrade(out decimal? grade)) return;

            try
            {
                _academic.AddSubjectToStudent(_selected.StudentId, code, title, units, grade);
                LoadAcademic();
            }
            catch (InvalidOperationException ex)               // code exists with other name/units
            {
                Warn(ex.Message);
            }
            catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                Warn("This student already has that subject.");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not add the subject. Please try again.");
            }
        }

        private void btnEditSub_Click(object? sender, EventArgs e)
        {
            if (dgvAcademic.SelectedRows.Count == 0) return;
            var x = (EnrolledSubject)dgvAcademic.SelectedRows[0].Tag!;

            if (!TryReadGrade(out decimal? grade)) return;

            try
            {
                if (grade.HasValue) _academic.SetGrade(x.Id, grade.Value);
                else _academic.ClearGrade(x.Id);
                LoadAcademic();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not save the grade. Please try again.");
            }
        }

        private void btnDeleteSub_Click(object? sender, EventArgs e)
        {
            if (_selected == null || dgvAcademic.SelectedRows.Count == 0) return;
            var x = (EnrolledSubject)dgvAcademic.SelectedRows[0].Tag!;

            var answer = MessageBox.Show(
                $"Remove {x.SubjectCode} - {x.Title} from {_selected.FullName}'s academic record?\n" +
                "Its grade will be removed too.",
                "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            if (answer != DialogResult.Yes) return;

            try
            {
                _academic.RemoveSubject(x.Id);
                LoadAcademic();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not remove the subject. Please try again.");
            }
        }

        // If the code already exists, fill in its name and units automatically.
        private void txtSubjectCode_Leave(object? sender, EventArgs e)
        {
            if (dgvAcademic.SelectedRows.Count > 0) return;
            string code = txtSubjectCode.Text.Trim();
            if (code.Length == 0) return;

            try
            {
                Subject? sub = _academic.GetSubjectByCode(code);
                if (sub != null)
                {
                    txtSubject.Text = sub.Title;
                    txtUnits.Text = sub.Units.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);                          // not worth a popup
            }
        }

        // --------------------------------------------------------------- photo
        private void btnUpload_Click(object? sender, EventArgs e)
        {
            if (_selected == null) return;

            using var dlg = new OpenFileDialog
            {
                Title = "Choose a student photo",
                Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            if (new FileInfo(dlg.FileName).Length > PhotoStore.MaxBytes)
            { Warn("That photo is too large. Please choose an image under 5 MB."); return; }

            try
            {
                using (var test = Image.FromFile(dlg.FileName)) { }   // throws if not a real image

                string saved = PhotoStore.Save(dlg.FileName, _selected.StudentId);
                _students.UpdatePhotoPath(_selected.StudentId, saved);
                _selected.PhotoPath = saved;
                SetPhoto(saved);
            }
            catch (OutOfMemoryException)
            {
                Warn("That file is not a valid image.");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not save the photo. Please try again.");
            }
        }
    }
}
