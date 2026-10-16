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
            pnlControl = new Panel();
            txtBUEmail = new TextBox();
            txtBirthdate = new TextBox();
            txtBloc = new TextBox();
            txtProgram = new TextBox();
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
            pnlControl.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlDGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pnlControl
            // 
            pnlControl.Controls.Add(txtBUEmail);
            pnlControl.Controls.Add(txtBirthdate);
            pnlControl.Controls.Add(txtBloc);
            pnlControl.Controls.Add(txtProgram);
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
            pnlControl.Size = new Size(300, 601);
            pnlControl.TabIndex = 0;
            // 
            // txtBUEmail
            // 
            txtBUEmail.Location = new Point(11, 340);
            txtBUEmail.Name = "txtBUEmail";
            txtBUEmail.PlaceholderText = "BU E-mail Address";
            txtBUEmail.Size = new Size(272, 23);
            txtBUEmail.TabIndex = 17;
            // 
            // txtBirthdate
            // 
            txtBirthdate.Location = new Point(11, 311);
            txtBirthdate.Name = "txtBirthdate";
            txtBirthdate.PlaceholderText = "Birth Date (MM/DD/YYYY)";
            txtBirthdate.Size = new Size(273, 23);
            txtBirthdate.TabIndex = 16;
            // 
            // txtBloc
            // 
            txtBloc.Location = new Point(150, 272);
            txtBloc.Name = "txtBloc";
            txtBloc.PlaceholderText = "Bloc";
            txtBloc.Size = new Size(134, 23);
            txtBloc.TabIndex = 15;
            // 
            // txtProgram
            // 
            txtProgram.Location = new Point(12, 272);
            txtProgram.Name = "txtProgram";
            txtProgram.PlaceholderText = "Program";
            txtProgram.Size = new Size(135, 23);
            txtProgram.TabIndex = 14;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(12, 202);
            txtSurname.Name = "txtSurname";
            txtSurname.PlaceholderText = "Surname";
            txtSurname.Size = new Size(272, 23);
            txtSurname.TabIndex = 13;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(11, 144);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PlaceholderText = "First Name";
            txtFirstName.Size = new Size(273, 23);
            txtFirstName.TabIndex = 12;
            // 
            // labelStudentsShown
            // 
            labelStudentsShown.AutoSize = true;
            labelStudentsShown.Location = new Point(12, 38);
            labelStudentsShown.Name = "labelStudentsShown";
            labelStudentsShown.Size = new Size(160, 15);
            labelStudentsShown.TabIndex = 11;
            labelStudentsShown.Text = "Showing _ out of _ student(s)";
            // 
            // cmbProgramFilter
            // 
            cmbProgramFilter.FormattingEnabled = true;
            cmbProgramFilter.Items.AddRange(new object[] { "All Programs", "BSCS", "BSIT" });
            cmbProgramFilter.Location = new Point(149, 12);
            cmbProgramFilter.Name = "cmbProgramFilter";
            cmbProgramFilter.Size = new Size(135, 23);
            cmbProgramFilter.TabIndex = 3;
            cmbProgramFilter.Text = "All Programs";
            // 
            // cmbYrFilter
            // 
            cmbYrFilter.FormattingEnabled = true;
            cmbYrFilter.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            cmbYrFilter.Location = new Point(12, 12);
            cmbYrFilter.Name = "cmbYrFilter";
            cmbYrFilter.Size = new Size(135, 23);
            cmbYrFilter.TabIndex = 4;
            cmbYrFilter.Text = "All Years";
            // 
            // cmbYrLevel
            // 
            cmbYrLevel.FormattingEnabled = true;
            cmbYrLevel.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbYrLevel.Location = new Point(150, 243);
            cmbYrLevel.Name = "cmbYrLevel";
            cmbYrLevel.Size = new Size(134, 23);
            cmbYrLevel.TabIndex = 10;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(12, 243);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(134, 23);
            cmbGender.TabIndex = 9;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(197, 400);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 35);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Location = new Point(105, 400);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(88, 35);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(12, 400);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 35);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtContactNo
            // 
            txtContactNo.Location = new Point(12, 369);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.PlaceholderText = "Contact Number";
            txtContactNo.Size = new Size(272, 23);
            txtContactNo.TabIndex = 5;
            // 
            // txtMidName
            // 
            txtMidName.Location = new Point(11, 173);
            txtMidName.Name = "txtMidName";
            txtMidName.PlaceholderText = "Middle Name (optional)";
            txtMidName.Size = new Size(272, 23);
            txtMidName.TabIndex = 1;
            // 
            // txtStudentID
            // 
            txtStudentID.Location = new Point(11, 115);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.PlaceholderText = "Student ID";
            txtStudentID.Size = new Size(273, 23);
            txtStudentID.TabIndex = 0;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnViewClear);
            pnlSearch.Controls.Add(btnViewSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(300, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(5);
            pnlSearch.Size = new Size(708, 42);
            pnlSearch.TabIndex = 1;
            // 
            // btnViewClear
            // 
            btnViewClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewClear.Location = new Point(570, 5);
            btnViewClear.Name = "btnViewClear";
            btnViewClear.Size = new Size(132, 32);
            btnViewClear.TabIndex = 9;
            btnViewClear.Text = "Clear";
            btnViewClear.UseVisualStyleBackColor = true;
            btnViewClear.Click += btnViewClear_Click;
            // 
            // btnViewSearch
            // 
            btnViewSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewSearch.Location = new Point(435, 5);
            btnViewSearch.Name = "btnViewSearch";
            btnViewSearch.Size = new Size(132, 32);
            btnViewSearch.TabIndex = 8;
            btnViewSearch.Text = "Search";
            btnViewSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("Segoe UI", 14F);
            txtSearch.Location = new Point(8, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search students... (leave empty to display all)";
            txtSearch.Size = new Size(421, 32);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // pnlDGV
            // 
            pnlDGV.Controls.Add(dgvStudents);
            pnlDGV.Dock = DockStyle.Fill;
            pnlDGV.Location = new Point(300, 42);
            pnlDGV.Name = "pnlDGV";
            pnlDGV.Padding = new Padding(5);
            pnlDGV.Size = new Size(708, 559);
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
            dgvStudents.Size = new Size(698, 549);
            dgvStudents.TabIndex = 0;
            dgvStudents.CellContentClick += dgvStudents_CellContentClick;
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
            Load += Form1_Load;
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
        private Panel pnlDashboardFilter;
        private Label labelStudentsShown;
        private TextBox txtFirstName;
        private TextBox txtSurname;
        private TextBox txtProgram;
        private TextBox txtBirthdate;
        private TextBox txtBloc;
        private TextBox txtBUEmail;
        private DataGridViewTextBoxColumn StudentID;
        private DataGridViewTextBoxColumn studName;
        private DataGridViewTextBoxColumn Gender;
        private DataGridViewTextBoxColumn YearLevel;
        private DataGridViewTextBoxColumn Program;
        private DataGridViewTextBoxColumn ContactNo;
    }
}
