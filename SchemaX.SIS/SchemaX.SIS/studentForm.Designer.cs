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
            cmbYrLevel = new ComboBox();
            cmbGender = new ComboBox();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            txtContactNo = new TextBox();
            txtProgram = new TextBox();
            txtName = new TextBox();
            txtStudentID = new TextBox();
            pnlSearch = new Panel();
            btnViewSearch = new Button();
            txtSearch = new TextBox();
            pnlDGV = new Panel();
            dgvStudents = new DataGridView();
            StudentID = new DataGridViewTextBoxColumn();
            studName = new DataGridViewTextBoxColumn();
            Gender = new DataGridViewTextBoxColumn();
            YearLevel = new DataGridViewTextBoxColumn();
            Program = new DataGridViewTextBoxColumn();
            ContactNumber = new DataGridViewTextBoxColumn();
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
            pnlControl.Controls.Add(cmbYrLevel);
            pnlControl.Controls.Add(cmbGender);
            pnlControl.Controls.Add(btnDelete);
            pnlControl.Controls.Add(btnEdit);
            pnlControl.Controls.Add(btnAdd);
            pnlControl.Controls.Add(txtContactNo);
            pnlControl.Controls.Add(txtProgram);
            pnlControl.Controls.Add(txtName);
            pnlControl.Controls.Add(txtStudentID);
            pnlControl.Dock = DockStyle.Left;
            pnlControl.Location = new Point(0, 0);
            pnlControl.Name = "pnlControl";
            pnlControl.Size = new Size(300, 601);
            pnlControl.TabIndex = 0;
            // 
            // cmbYrLevel
            // 
            cmbYrLevel.FormattingEnabled = true;
            cmbYrLevel.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbYrLevel.Location = new Point(150, 154);
            cmbYrLevel.Name = "cmbYrLevel";
            cmbYrLevel.Size = new Size(134, 23);
            cmbYrLevel.TabIndex = 10;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Non-Binary", "Other" });
            cmbGender.Location = new Point(12, 154);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(134, 23);
            cmbGender.TabIndex = 9;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(197, 241);
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
            btnEdit.Location = new Point(105, 241);
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
            btnAdd.Location = new Point(12, 241);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 35);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtContactNo
            // 
            txtContactNo.Location = new Point(12, 212);
            txtContactNo.Name = "txtContactNo";
            txtContactNo.PlaceholderText = "Contact Number";
            txtContactNo.Size = new Size(272, 23);
            txtContactNo.TabIndex = 5;
            // 
            // txtProgram
            // 
            txtProgram.Location = new Point(12, 183);
            txtProgram.Name = "txtProgram";
            txtProgram.PlaceholderText = "Program";
            txtProgram.Size = new Size(272, 23);
            txtProgram.TabIndex = 4;
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 125);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Student Name";
            txtName.Size = new Size(272, 23);
            txtName.TabIndex = 1;
            // 
            // txtStudentID
            // 
            txtStudentID.Location = new Point(12, 96);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.PlaceholderText = "Student ID";
            txtStudentID.Size = new Size(273, 23);
            txtStudentID.TabIndex = 0;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnViewSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(300, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(5);
            pnlSearch.Size = new Size(708, 42);
            pnlSearch.TabIndex = 1;
            // 
            // btnViewSearch
            // 
            btnViewSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewSearch.Location = new Point(571, 5);
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
            txtSearch.Location = new Point(5, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search students... (leave empty to display all)";
            txtSearch.Size = new Size(560, 32);
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
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Columns.AddRange(new DataGridViewColumn[] { StudentID, studName, Gender, YearLevel, Program, ContactNumber });
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.Location = new Point(5, 5);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.Size = new Size(698, 549);
            dgvStudents.TabIndex = 0;
            // 
            // StudentID
            // 
            StudentID.FillWeight = 20F;
            StudentID.HeaderText = "Student ID";
            StudentID.Name = "StudentID";
            // 
            // studName
            // 
            studName.FillWeight = 30F;
            studName.HeaderText = "Name";
            studName.Name = "studName";
            // 
            // Gender
            // 
            Gender.FillWeight = 5F;
            Gender.HeaderText = "Gender";
            Gender.MinimumWidth = 15;
            Gender.Name = "Gender";
            // 
            // YearLevel
            // 
            YearLevel.FillWeight = 5F;
            YearLevel.HeaderText = "Year Level";
            YearLevel.MinimumWidth = 15;
            YearLevel.Name = "YearLevel";
            // 
            // Program
            // 
            Program.FillWeight = 15F;
            Program.HeaderText = "Program";
            Program.MinimumWidth = 15;
            Program.Name = "Program";
            // 
            // ContactNumber
            // 
            ContactNumber.FillWeight = 20F;
            ContactNumber.HeaderText = "Contact No.";
            ContactNumber.MinimumWidth = 45;
            ContactNumber.Name = "ContactNumber";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 601);
            Controls.Add(mainPanel);
            Name = "Form1";
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
        private DataGridViewTextBoxColumn StudentID;
        private DataGridViewTextBoxColumn studName;
        private DataGridViewTextBoxColumn Gender;
        private DataGridViewTextBoxColumn YearLevel;
        private DataGridViewTextBoxColumn Program;
        private DataGridViewTextBoxColumn ContactNumber;
        private TextBox txtName;
        private TextBox txtStudentID;
        private TextBox txtContactNo;
        private TextBox txtProgram;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnViewSearch;
        private Button btnDelete;
        private ComboBox cmbYrLevel;
        private ComboBox cmbGender;
    }
}
