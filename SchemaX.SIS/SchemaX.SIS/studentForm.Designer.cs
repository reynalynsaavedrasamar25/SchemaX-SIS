namespace SchemaX.SIS
{
    partial class studentForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlControl = new Panel();
            labelFilterTitle = new Label();
            cmbProgram = new ComboBox();
            labelStudentIDHint = new Label();
            btnClearSelection = new Button();
            dateBirthDate = new DateTimePicker();
            btnBack = new Button();
            btnExportCSV = new Button();
            txtBUEmail = new TextBox();
            txtBloc = new TextBox();
            txtSurname = new TextBox();
            txtFirstName = new TextBox();
            labelStudentsShown = new Label();
            cmbProgramFilter = new ComboBox();
            cmbYrFilter = new ComboBox();
            cmbYrLevel = new ComboBox();
            cmbGender = new ComboBox();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            txtContactNo = new TextBox();
            txtMidName = new TextBox();
            txtStudentID = new TextBox();
            pnlSearch = new Panel();
            btnViewClear = new Button();
            btnViewSearch = new Button();
            txtSearch = new TextBox();
            pnlDGV = new Panel();
            dgvStudents = new DataGridView();
            StudentID = new DataGridViewTextBoxColumn();
            studName = new DataGridViewTextBoxColumn();
            Gender = new DataGridViewTextBoxColumn();
            YearLevel = new DataGridViewTextBoxColumn();
            Program = new DataGridViewTextBoxColumn();
            ContactNo = new DataGridViewTextBoxColumn();
            mainPanel = new Panel();
            tooltipStudentID = new ToolTip(components);
            pnlControl.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlDGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pnlControl
            // 
            pnlControl.Controls.Add(labelFilterTitle);
            pnlControl.Controls.Add(cmbProgram);
            pnlControl.Controls.Add(labelStudentIDHint);
            pnlControl.Controls.Add(btnClearSelection);
            pnlControl.Controls.Add(dateBirthDate);
            pnlControl.Controls.Add(btnBack);
            pnlControl.Controls.Add(btnExportCSV);
            pnlControl.Controls.Add(txtBUEmail);
            pnlControl.Controls.Add(txtBloc);
            pnlControl.Controls.Add(txtSurname);
            pnlControl.Controls.Add(txtFirstName);
            pnlControl.Controls.Add(labelStudentsShown);
            pnlControl.Controls.Add(cmbProgramFilter);
            pnlControl.Controls.Add(cmbYrFilter);
            pnlControl.Controls.Add(cmbYrLevel);
            pnlControl.Controls.Add(cmbGender);
            pnlControl.Controls.Add(btnDelete);
            pnlControl.Controls.Add(btnEdit);
            pnlControl.Controls.Add(btnAdd);
            pnlControl.Controls.Add(txtContactNo);
            pnlControl.Controls.Add(txtMidName);
            pnlControl.Controls.Add(txtStudentID);
            pnlControl.Dock = DockStyle.Left;
            pnlControl.Location = new Point(0, 0);
            pnlControl.Name = "pnlControl";
            pnlControl.Size = new Size(439, 601);
            pnlControl.TabIndex = 0;
            // 
            // labelFilterTitle
            // 
            labelFilterTitle.AutoSize = true;
            labelFilterTitle.Font = new Font("Segoe UI", 14F);
            labelFilterTitle.Location = new Point(12, 21);
            labelFilterTitle.Name = "labelFilterTitle";
            labelFilterTitle.Size = new Size(91, 25);
            labelFilterTitle.TabIndex = 23;
            labelFilterTitle.Text = "Filter by...";
            // 
            // cmbProgram
            // 
            cmbProgram.FormattingEnabled = true;
            cmbProgram.Items.AddRange(new object[] { "BSCS", "BSIT" });
            cmbProgram.Location = new Point(293, 253);
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new Size(135, 23);
            cmbProgram.TabIndex = 10;
            // 
            // labelStudentIDHint
            // 
            labelStudentIDHint.Font = new Font("Segoe UI", 7F);
            labelStudentIDHint.Location = new Point(11, 111);
            labelStudentIDHint.Name = "labelStudentIDHint";
            labelStudentIDHint.Size = new Size(277, 25);
            labelStudentIDHint.TabIndex = 22;
            labelStudentIDHint.Text = "Student ID can't be changed while editing. Press Clear Selection to add a new student.";
            labelStudentIDHint.Visible = false;
            // 
            // btnClearSelection
            // 
            btnClearSelection.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClearSelection.FlatStyle = FlatStyle.Flat;
            btnClearSelection.Location = new Point(11, 443);
            btnClearSelection.Name = "btnClearSelection";
            btnClearSelection.Size = new Size(417, 35);
            btnClearSelection.TabIndex = 15;
            btnClearSelection.Text = "Clear Selection";
            btnClearSelection.UseVisualStyleBackColor = true;
            btnClearSelection.Click += btnClearSelection_Click;
            // 
            // dateBirthDate
            // 
            dateBirthDate.Format = DateTimePickerFormat.Short;
            dateBirthDate.Location = new Point(293, 166);
            dateBirthDate.MaxDate = new DateTime(2026, 10, 16, 0, 0, 0, 0);
            dateBirthDate.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateBirthDate.Name = "dateBirthDate";
            dateBirthDate.Size = new Size(135, 23);
            dateBirthDate.TabIndex = 7;
            dateBirthDate.Value = new DateTime(2026, 10, 16, 0, 0, 0, 0);
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(11, 525);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(417, 65);
            btnBack.TabIndex = 17;
            btnBack.Text = "Back to Dashboard";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnExportCSV
            // 
            btnExportCSV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExportCSV.FlatStyle = FlatStyle.Flat;
            btnExportCSV.Location = new Point(11, 484);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new Size(417, 35);
            btnExportCSV.TabIndex = 16;
            btnExportCSV.Text = "Export CSV";
            btnExportCSV.UseVisualStyleBackColor = true;
            btnExportCSV.Click += btnExportCSV_Click;
            // 
            // txtBUEmail
            // 
            txtBUEmail.Location = new Point(10, 253);
            txtBUEmail.Name = "txtBUEmail";
            txtBUEmail.PlaceholderText = "BU E-mail Address";
            txtBUEmail.Size = new Size(275, 23);
            txtBUEmail.TabIndex = 4;
            // 
            // txtBloc
            // 
            txtBloc.Location = new Point(251, 282);
            txtBloc.Name = "txtBloc";
            txtBloc.PlaceholderText = "Bloc";
            txtBloc.Size = new Size(34, 23);
            txtBloc.TabIndex = 6;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(10, 224);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(275, 23);
            txtSurname.TabIndex = 3;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(10, 166);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PlaceholderText = "First Name";
            txtFirstName.Size = new Size(275, 23);
            txtFirstName.TabIndex = 1;
            // 
            // labelStudentsShown
            // 
            labelStudentsShown.AutoSize = true;
            labelStudentsShown.Location = new Point(10, 74);
            labelStudentsShown.Name = "labelStudentsShown";
            labelStudentsShown.Size = new Size(160, 15);
            labelStudentsShown.TabIndex = 11;
            labelStudentsShown.Text = "Showing _ out of _ student(s)";
            // 
            // cmbProgramFilter
            // 
            cmbProgramFilter.FormattingEnabled = true;
            cmbProgramFilter.Items.AddRange(new object[] { "All Programs", "BSCS", "BSIT" });
            cmbProgramFilter.Location = new Point(223, 48);
            cmbProgramFilter.Name = "cmbProgramFilter";
            cmbProgramFilter.Size = new Size(205, 23);
            cmbProgramFilter.TabIndex = 17;
            cmbProgramFilter.Text = "All Programs";
            // 
            // cmbYrFilter
            // 
            cmbYrFilter.FormattingEnabled = true;
            cmbYrFilter.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            cmbYrFilter.Location = new Point(10, 48);
            cmbYrFilter.Name = "cmbYrFilter";
            cmbYrFilter.Size = new Size(205, 23);
            cmbYrFilter.TabIndex = 16;
            cmbYrFilter.Text = "All Years";
            // 
            // cmbYrLevel
            // 
            cmbYrLevel.FormattingEnabled = true;
            cmbYrLevel.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbYrLevel.Location = new Point(293, 224);
            cmbYrLevel.Name = "cmbYrLevel";
            cmbYrLevel.Size = new Size(135, 23);
            cmbYrLevel.TabIndex = 9;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbGender.Location = new Point(293, 195);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(135, 23);
            cmbGender.TabIndex = 8;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(293, 384);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(135, 53);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Location = new Point(152, 384);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(135, 53);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(12, 384);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(133, 53);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtContactNo
            // 
            txtContactNo.Location = new Point(10, 282);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.PlaceholderText = "Contact Number";
            txtContactNo.Size = new Size(235, 23);
            txtContactNo.TabIndex = 5;
            // 
            // txtMidName
            // 
            txtMidName.Location = new Point(10, 195);
            txtMidName.Name = "txtMidName";
            txtMidName.PlaceholderText = "Middle Name (optional)";
            txtMidName.Size = new Size(275, 23);
            txtMidName.TabIndex = 2;
            // 
            // txtStudentID
            // 
            txtStudentID.Location = new Point(293, 282);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.PlaceholderText = "Student ID";
            txtStudentID.Size = new Size(135, 23);
            txtStudentID.TabIndex = 11;
            tooltipStudentID.SetToolTip(txtStudentID, "Student ID can't be changed while editing. Press Clear Selection to add a new student.");
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnViewClear);
            pnlSearch.Controls.Add(btnViewSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(439, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(5);
            pnlSearch.Size = new Size(569, 46);
            pnlSearch.TabIndex = 1;
            // 
            // btnViewClear
            // 
            btnViewClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewClear.Location = new Point(431, 5);
            btnViewClear.Name = "btnViewClear";
            btnViewClear.Size = new Size(132, 35);
            btnViewClear.TabIndex = 20;
            btnViewClear.Text = "Clear";
            btnViewClear.UseVisualStyleBackColor = true;
            btnViewClear.Click += btnViewClear_Click;
            // 
            // btnViewSearch
            // 
            btnViewSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewSearch.Location = new Point(296, 5);
            btnViewSearch.Name = "btnViewSearch";
            btnViewSearch.Size = new Size(132, 35);
            btnViewSearch.TabIndex = 19;
            btnViewSearch.Text = "Search";
            btnViewSearch.UseVisualStyleBackColor = true;
            btnViewSearch.Click += btnViewSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("Segoe UI", 15.5F);
            txtSearch.Location = new Point(8, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search students... (leave empty to display all)";
            txtSearch.Size = new Size(282, 35);
            txtSearch.TabIndex = 18;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // pnlDGV
            // 
            pnlDGV.Controls.Add(dgvStudents);
            pnlDGV.Dock = DockStyle.Fill;
            pnlDGV.Location = new Point(439, 46);
            pnlDGV.Name = "pnlDGV";
            pnlDGV.Padding = new Padding(5);
            pnlDGV.Size = new Size(569, 555);
            pnlDGV.TabIndex = 2;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Columns.AddRange(new DataGridViewColumn[] { StudentID, studName, Gender, YearLevel, Program, ContactNo });
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.Location = new Point(5, 5);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(559, 545);
            dgvStudents.TabIndex = 0;
            dgvStudents.CellClick += dgvStudents_CellClick;
            // 
            // StudentID
            // 
            StudentID.FillWeight = 20F;
            StudentID.HeaderText = "Student ID";
            StudentID.Name = "StudentID";
            StudentID.ReadOnly = true;
            // 
            // studName
            // 
            studName.FillWeight = 30F;
            studName.HeaderText = "Name";
            studName.Name = "studName";
            studName.ReadOnly = true;
            // 
            // Gender
            // 
            Gender.FillWeight = 5F;
            Gender.HeaderText = "Gender";
            Gender.MinimumWidth = 15;
            Gender.Name = "Gender";
            Gender.ReadOnly = true;
            // 
            // YearLevel
            // 
            YearLevel.FillWeight = 5F;
            YearLevel.HeaderText = "Year Level";
            YearLevel.MinimumWidth = 15;
            YearLevel.Name = "YearLevel";
            YearLevel.ReadOnly = true;
            // 
            // Program
            // 
            Program.FillWeight = 15F;
            Program.HeaderText = "Program";
            Program.MinimumWidth = 15;
            Program.Name = "Program";
            Program.ReadOnly = true;
            // 
            // ContactNo
            // 
            ContactNo.FillWeight = 20F;
            ContactNo.HeaderText = "Contact No.";
            ContactNo.MinimumWidth = 45;
            ContactNo.Name = "ContactNo";
            ContactNo.ReadOnly = true;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(pnlDGV);
            mainPanel.Controls.Add(pnlSearch);
            mainPanel.Controls.Add(pnlControl);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1008, 601);
            mainPanel.TabIndex = 2;
            // 
            // studentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 601);
            Controls.Add(mainPanel);
            Name = "studentForm";
            Text = "Student Information System";
            WindowState = FormWindowState.Maximized;
            Load += studentForm_Load;
            pnlControl.ResumeLayout(false);
            pnlControl.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlDGV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlControl;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Panel pnlDGV;
        private DataGridView dgvStudents;
        private Panel mainPanel;
        private TextBox txtMidName;
        private TextBox txtStudentID;
        private TextBox txtContactNo;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnViewSearch;
        private Button btnDelete;
        private ComboBox cmbYrLevel;
        private ComboBox cmbGender;
        private Button btnViewClear;
        private ComboBox cmbProgramFilter;
        private ComboBox cmbYrFilter;
        private Label labelStudentsShown;
        private TextBox txtFirstName;
        private TextBox txtSurname;
        private TextBox txtBloc;
        private TextBox txtBUEmail;
        private DataGridViewTextBoxColumn StudentID;
        private DataGridViewTextBoxColumn studName;
        private DataGridViewTextBoxColumn Gender;
        private DataGridViewTextBoxColumn YearLevel;
        private DataGridViewTextBoxColumn Program;
        private DataGridViewTextBoxColumn ContactNo;
        private Button btnExportCSV;
        private Button btnBack;
        private DateTimePicker dateBirthDate;
        private ToolTip tooltipStudentID;
        private Button btnClearSelection;
        private Label labelStudentIDHint;
        private ComboBox cmbProgram;
        private Label labelFilterTitle;
    }
}
