namespace SchemaX.SIS
{
    partial class SignUpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtFirstName = new TextBox();
            lblSurname = new Label();
            txtSurname = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            txtHint = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            btnSignUp = new Button();
            lblTitle = new Label();
            label3 = new Label();
            txtConfirm = new TextBox();
            btnBack = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 7F);
            label1.Location = new Point(96, 74);
            label1.Name = "label1";
            label1.Size = new Size(51, 12);
            label1.TabIndex = 0;
            label1.Text = "First Name";
            // 
            // txtFirstName
            // 
            txtFirstName.ForeColor = Color.Gray;
            txtFirstName.Location = new Point(96, 89);
            txtFirstName.MaxLength = 50;
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(185, 23);
            txtFirstName.TabIndex = 1;
            // 
            // lblSurname
            // 
            lblSurname.AutoSize = true;
            lblSurname.Font = new Font("Segoe UI", 7F);
            lblSurname.Location = new Point(96, 124);
            lblSurname.Name = "lblSurname";
            lblSurname.Size = new Size(44, 12);
            lblSurname.TabIndex = 2;
            lblSurname.Text = "Surname";
            // 
            // txtSurname
            // 
            txtSurname.ForeColor = Color.Gray;
            txtSurname.Location = new Point(96, 139);
            txtSurname.MaxLength = 50;
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(185, 23);
            txtSurname.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 7F);
            lblEmail.Location = new Point(96, 181);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(97, 12);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Bicol University Email";
            // 
            // txtEmail
            // 
            txtEmail.CharacterCasing = CharacterCasing.Lower;
            txtEmail.ForeColor = Color.Gray;
            txtEmail.Location = new Point(96, 196);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(185, 23);
            txtEmail.TabIndex = 5;
            // 
            // txtHint
            // 
            txtHint.AutoSize = true;
            txtHint.Font = new Font("Segoe UI", 6.5F);
            txtHint.ForeColor = SystemColors.ControlDarkDark;
            txtHint.Location = new Point(96, 222);
            txtHint.Name = "txtHint";
            txtHint.Size = new Size(128, 12);
            txtHint.TabIndex = 6;
            txtHint.Text = "Must end with @bicol-u.edu.ph";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 7F);
            lblUsername.Location = new Point(96, 245);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(49, 12);
            lblUsername.TabIndex = 7;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.CharacterCasing = CharacterCasing.Lower;
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Location = new Point(96, 260);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(185, 23);
            txtUsername.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 7F);
            label2.Location = new Point(96, 295);
            label2.Name = "label2";
            label2.Size = new Size(46, 12);
            label2.TabIndex = 9;
            label2.Text = "Password";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(96, 310);
            textBox1.MaxLength = 100;
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(185, 23);
            textBox1.TabIndex = 10;
            // 
            // btnSignUp
            // 
            btnSignUp.Location = new Point(96, 445);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(75, 23);
            btnSignUp.TabIndex = 11;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = true;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(145, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(85, 28);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Sign Up";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 7F);
            label3.Location = new Point(96, 347);
            label3.Name = "label3";
            label3.Size = new Size(84, 12);
            label3.TabIndex = 13;
            label3.Text = "Confirm password";
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(96, 362);
            txtConfirm.MaxLength = 100;
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '*';
            txtConfirm.Size = new Size(185, 23);
            txtConfirm.TabIndex = 14;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(177, 445);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(104, 23);
            btnBack.TabIndex = 15;
            btnBack.Text = "Back to Log In";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.FromArgb(192, 57, 43);
            lblError.Location = new Point(32, 347);
            lblError.Name = "lblError";
            lblError.Size = new Size(320, 40);
            lblError.TabIndex = 16;
            // 
            // SignUpForm
            // 
            AcceptButton = btnSignUp;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 581);
            Controls.Add(btnBack);
            Controls.Add(txtConfirm);
            Controls.Add(label3);
            Controls.Add(lblTitle);
            Controls.Add(btnSignUp);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(txtHint);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtSurname);
            Controls.Add(lblSurname);
            Controls.Add(txtFirstName);
            Controls.Add(label1);
            Controls.Add(lblError);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SignUpForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SchemaX SIS - Sign Up";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFirstName;
        private Label lblSurname;
        private TextBox txtSurname;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label txtHint;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label label2;
        private TextBox textBox1;
        private Button btnSignUp;
        private Label lblTitle;
        private Label label3;
        private TextBox txtConfirm;
        private Button btnBack;
        private Label lblError;
    }
}