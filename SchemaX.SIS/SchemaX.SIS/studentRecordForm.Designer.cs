namespace SchemaX.SIS
{
    partial class studentRecordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblTitle = new Label();
            pnlAddSubject = new Panel();
            lblAddSubject = new Label();
            txtSubject = new TextBox();
            txtSubjectCode = new TextBox();
            txtUnits = new TextBox();
            txtGrade = new TextBox();
            btnAddSub = new Button();
            btnEditSub = new Button();
            btnDeleteSub = new Button();
            btnClearSub = new Button();
            lblSubjectHint = new Label();
            btnBack = new Button();
            txtSearch = new TextBox();
            cmbYear = new ComboBox();
            cmbProgram = new ComboBox();
            btnSort = new Button();
            dgvStudentList = new DataGridView();
            lblEmpty = new Label();
            pnlDetails = new Panel();
            lblProfileTitle = new Label();
            picPhoto = new PictureBox();
            btnUpload = new Button();
            lblFullName = new Label();
            lblProfileLeft = new Label();
            lblProfileRight = new Label();
            lblProfileEmail = new Label();
            lblAcademicTitle = new Label();
            dgvAcademic = new DataGridView();
            lblGwa = new Label();
            pnlTop.SuspendLayout();
            pnlAddSubject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudentList).BeginInit();
            pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPhoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAcademic).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1300, 50);
            pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(88, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Student Record";
            // 
            // pnlAddSubject
            // 
            pnlAddSubject.Controls.Add(lblAddSubject);
            pnlAddSubject.Controls.Add(txtSubject);
            pnlAddSubject.Controls.Add(txtSubjectCode);
            pnlAddSubject.Controls.Add(txtUnits);
            pnlAddSubject.Controls.Add(txtGrade);
            pnlAddSubject.Controls.Add(btnAddSub);
            pnlAddSubject.Controls.Add(btnEditSub);
            pnlAddSubject.Controls.Add(btnDeleteSub);
            pnlAddSubject.Controls.Add(btnClearSub);
            pnlAddSubject.Controls.Add(lblSubjectHint);
            pnlAddSubject.Controls.Add(btnBack);
            pnlAddSubject.Location = new Point(15, 65);
            pnlAddSubject.Name = "pnlAddSubject";
            pnlAddSubject.Size = new Size(240, 600);
            pnlAddSubject.TabIndex = 1;
            // 
            // lblAddSubject
            // 
            lblAddSubject.Location = new Point(0, 0);
            lblAddSubject.Name = "lblAddSubject";
            lblAddSubject.Size = new Size(240, 36);
            lblAddSubject.TabIndex = 0;
            lblAddSubject.Text = "Add Subject";
            lblAddSubject.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(15, 55);
            txtSubject.MaxLength = 100;
            txtSubject.Name = "txtSubject";
            txtSubject.PlaceholderText = "Subject";
            txtSubject.Size = new Size(210, 23);
            txtSubject.TabIndex = 0;
            // 
            // txtSubjectCode
            // 
            txtSubjectCode.Location = new Point(15, 93);
            txtSubjectCode.MaxLength = 15;
            txtSubjectCode.Name = "txtSubjectCode";
            txtSubjectCode.PlaceholderText = "Subject Code";
            txtSubjectCode.Size = new Size(210, 23);
            txtSubjectCode.TabIndex = 1;
            // 
            // txtUnits
            // 
            txtUnits.Location = new Point(15, 131);
            txtUnits.MaxLength = 1;
            txtUnits.Name = "txtUnits";
            txtUnits.PlaceholderText = "Units";
            txtUnits.Size = new Size(210, 23);
            txtUnits.TabIndex = 2;
            // 
            // txtGrade
            // 
            txtGrade.Location = new Point(15, 169);
            txtGrade.MaxLength = 4;
            txtGrade.Name = "txtGrade";
            txtGrade.PlaceholderText = "Grade (optional)";
            txtGrade.Size = new Size(210, 23);
            txtGrade.TabIndex = 3;
            // 
            // btnAddSub
            // 
            btnAddSub.Location = new Point(15, 212);
            btnAddSub.Name = "btnAddSub";
            btnAddSub.Size = new Size(210, 40);
            btnAddSub.TabIndex = 4;
            btnAddSub.Text = "Add";
            btnAddSub.UseVisualStyleBackColor = true;
            // 
            // btnEditSub
            // 
            btnEditSub.Location = new Point(15, 258);
            btnEditSub.Name = "btnEditSub";
            btnEditSub.Size = new Size(210, 40);
            btnEditSub.TabIndex = 5;
            btnEditSub.Text = "Edit";
            btnEditSub.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSub
            // 
            btnDeleteSub.Location = new Point(15, 304);
            btnDeleteSub.Name = "btnDeleteSub";
            btnDeleteSub.Size = new Size(210, 40);
            btnDeleteSub.TabIndex = 6;
            btnDeleteSub.Text = "Delete";
            btnDeleteSub.UseVisualStyleBackColor = true;
            // 
            // btnClearSub
            // 
            btnClearSub.Location = new Point(15, 350);
            btnClearSub.Name = "btnClearSub";
            btnClearSub.Size = new Size(210, 40);
            btnClearSub.TabIndex = 7;
            btnClearSub.Text = "Clear / New subject";
            btnClearSub.UseVisualStyleBackColor = true;
            // 
            // lblSubjectHint
            // 
            lblSubjectHint.Location = new Point(15, 393);
            lblSubjectHint.Name = "lblSubjectHint";
            lblSubjectHint.Size = new Size(210, 90);
            lblSubjectHint.TabIndex = 9;
            lblSubjectHint.Text = "Select a student first.";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(15, 548);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(210, 40);
            btnBack.TabIndex = 8;
            btnBack.Text = "Back to Dashboard";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(270, 65);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search name or student ID";
            txtSearch.Size = new Size(400, 23);
            txtSearch.TabIndex = 0;
            // 
            // cmbYear
            // 
            cmbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYear.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" });
            cmbYear.Location = new Point(270, 100);
            cmbYear.Name = "cmbYear";
            cmbYear.Size = new Size(125, 23);
            cmbYear.TabIndex = 1;
            // 
            // cmbProgram
            // 
            cmbProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProgram.Items.AddRange(new object[] { "All Programs", "BSCS", "BSIT" });
            cmbProgram.Location = new Point(402, 100);
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new Size(125, 23);
            cmbProgram.TabIndex = 2;
            // 
            // btnSort
            // 
            btnSort.Location = new Point(534, 98);
            btnSort.Name = "btnSort";
            btnSort.Size = new Size(136, 32);
            btnSort.TabIndex = 3;
            btnSort.Text = "Sort: by ID";
            btnSort.UseVisualStyleBackColor = true;
            // 
            // dgvStudentList
            // 
            dgvStudentList.Location = new Point(270, 138);
            dgvStudentList.Name = "dgvStudentList";
            dgvStudentList.Size = new Size(400, 527);
            dgvStudentList.TabIndex = 4;
            // 
            // lblEmpty
            // 
            lblEmpty.Location = new Point(685, 300);
            lblEmpty.Name = "lblEmpty";
            lblEmpty.Size = new Size(600, 40);
            lblEmpty.TabIndex = 7;
            lblEmpty.Text = "Select a student from the list.";
            lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDetails
            // 
            pnlDetails.Controls.Add(lblProfileTitle);
            pnlDetails.Controls.Add(picPhoto);
            pnlDetails.Controls.Add(btnUpload);
            pnlDetails.Controls.Add(lblFullName);
            pnlDetails.Controls.Add(lblProfileLeft);
            pnlDetails.Controls.Add(lblProfileRight);
            pnlDetails.Controls.Add(lblProfileEmail);
            pnlDetails.Controls.Add(lblAcademicTitle);
            pnlDetails.Controls.Add(dgvAcademic);
            pnlDetails.Controls.Add(lblGwa);
            pnlDetails.Location = new Point(685, 60);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(600, 610);
            pnlDetails.TabIndex = 8;
            pnlDetails.Visible = false;
            // 
            // lblProfileTitle
            // 
            lblProfileTitle.Location = new Point(0, 0);
            lblProfileTitle.Name = "lblProfileTitle";
            lblProfileTitle.Size = new Size(600, 32);
            lblProfileTitle.TabIndex = 0;
            lblProfileTitle.Text = "Student Profile";
            // 
            // picPhoto
            // 
            picPhoto.BorderStyle = BorderStyle.FixedSingle;
            picPhoto.Location = new Point(0, 40);
            picPhoto.Name = "picPhoto";
            picPhoto.Size = new Size(150, 150);
            picPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            picPhoto.TabIndex = 1;
            picPhoto.TabStop = false;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(0, 198);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(150, 40);
            btnUpload.TabIndex = 0;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // lblFullName
            // 
            lblFullName.AutoEllipsis = true;
            lblFullName.Location = new Point(160, 40);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(440, 30);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "Full Name";
            // 
            // lblProfileLeft
            // 
            lblProfileLeft.Location = new Point(160, 78);
            lblProfileLeft.Name = "lblProfileLeft";
            lblProfileLeft.Size = new Size(215, 110);
            lblProfileLeft.TabIndex = 4;
            // 
            // lblProfileRight
            // 
            lblProfileRight.Location = new Point(385, 78);
            lblProfileRight.Name = "lblProfileRight";
            lblProfileRight.Size = new Size(215, 110);
            lblProfileRight.TabIndex = 5;
            // 
            // lblProfileEmail
            // 
            lblProfileEmail.AutoEllipsis = true;
            lblProfileEmail.Location = new Point(160, 198);
            lblProfileEmail.Name = "lblProfileEmail";
            lblProfileEmail.Size = new Size(440, 24);
            lblProfileEmail.TabIndex = 6;
            // 
            // lblAcademicTitle
            // 
            lblAcademicTitle.Location = new Point(0, 240);
            lblAcademicTitle.Name = "lblAcademicTitle";
            lblAcademicTitle.Size = new Size(600, 32);
            lblAcademicTitle.TabIndex = 7;
            lblAcademicTitle.Text = "Academic Record";
            lblAcademicTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvAcademic
            // 
            dgvAcademic.Location = new Point(0, 278);
            dgvAcademic.Name = "dgvAcademic";
            dgvAcademic.Size = new Size(600, 270);
            dgvAcademic.TabIndex = 1;
            // 
            // lblGwa
            // 
            lblGwa.Location = new Point(0, 556);
            lblGwa.Name = "lblGwa";
            lblGwa.Size = new Size(600, 40);
            lblGwa.TabIndex = 9;
            lblGwa.Text = "GWA: N/A";
            lblGwa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // studentRecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 680);
            Controls.Add(pnlDetails);
            Controls.Add(lblEmpty);
            Controls.Add(dgvStudentList);
            Controls.Add(btnSort);
            Controls.Add(cmbProgram);
            Controls.Add(cmbYear);
            Controls.Add(txtSearch);
            Controls.Add(pnlAddSubject);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "studentRecordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SchemaX SIS - Student Record";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlAddSubject.ResumeLayout(false);
            pnlAddSubject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudentList).EndInit();
            pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPhoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAcademic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTop;
        private Label lblTitle;
        private Panel pnlAddSubject;
        private Label lblAddSubject;
        private TextBox txtSubject;
        private TextBox txtSubjectCode;
        private TextBox txtUnits;
        private TextBox txtGrade;
        private Button btnAddSub;
        private Button btnEditSub;
        private Button btnDeleteSub;
        private Button btnClearSub;
        private Label lblSubjectHint;
        private Button btnBack;
        private TextBox txtSearch;
        private ComboBox cmbYear;
        private ComboBox cmbProgram;
        private Button btnSort;
        private DataGridView dgvStudentList;
        private Label lblEmpty;
        private Panel pnlDetails;
        private Label lblProfileTitle;
        private PictureBox picPhoto;
        private Button btnUpload;
        private Label lblFullName;
        private Label lblProfileLeft;
        private Label lblProfileRight;
        private Label lblProfileEmail;
        private Label lblAcademicTitle;
        private DataGridView dgvAcademic;
        private Label lblGwa;
    }
}
