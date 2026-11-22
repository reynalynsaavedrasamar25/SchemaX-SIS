using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySqlConnector;
using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class SignUpForm : Form
    {
        private readonly UserRepository _users = new();
        public SignUpForm()
        {
            InitializeComponent();
            StyleForm();
            ArrangeFields();
            ButtonStyler.Apply(btnSignUp, "", ButtonKind.Solid, ThemeColors.Accent);
            ButtonStyler.Apply(btnBack, "", ButtonKind.Menu);
        }

        private void StyleForm()
        {
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            AcceptButton = btnSignUp;
            CancelButton = btnBack;
            foreach (Label l in Controls.OfType<Label>())
            {
                if (l == lblError)
                {
                    l.ForeColor = ThemeColors.Danger;
                    l.Font = new Font("Segoe UI", 9F);
                }
                else if (l.Font.Size >= 14)
                {
                    l.ForeColor = ThemeColors.Primary;
                    l.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
                }
                else if (l.Text.StartsWith("Must end", StringComparison.OrdinalIgnoreCase))
                {
                    l.ForeColor = Color.Gray;
                    l.Font = new Font("Segoe UI", 8.5F);
                }
                else
                {
                    l.ForeColor = ThemeColors.TextDark;        
                    l.Font = new Font("Segoe UI Semibold", 9.5F);
                }
            }

            foreach (TextBox t in new[] { txtFirstName, txtSurname, txtEmail, txtUsername, textBox1, txtConfirm })
            {
                t.BorderStyle = BorderStyle.FixedSingle;
                t.Font = new Font("Segoe UI", 10.5F);
                t.BackColor = Color.White;
            }

            ButtonStyler.Apply(btnSignUp, "", ButtonKind.Solid, ThemeColors.Accent); 
            ButtonStyler.Apply(btnBack, "", ButtonKind.Menu);                          
        }

        // Stacks everything in one centered column with equal spacing.
        private void ArrangeFields()
        {
            const int fieldW = 280;
            int x = (ClientSize.Width - fieldW) / 2;
            int y = 24;

            // heading, centered
            Label? title = Controls.OfType<Label>().FirstOrDefault(l => l.Font.Size >= 19);
            if (title != null)
            {
                title.Top = y;
                title.Left = (ClientSize.Width - title.Width) / 2;
                y += title.Height + 18;
            }

            void Place(TextBox box, string labelText)
            {
                Label? lbl = Controls.OfType<Label>().FirstOrDefault(
                    l => l.Text.Equals(labelText, StringComparison.OrdinalIgnoreCase));
                if (lbl != null) lbl.Location = new Point(x, y);
                box.Location = new Point(x, y + 22);
                box.Width = fieldW;
                y += 22 + box.Height + 12;
            }

            Place(txtFirstName, "First Name");
            Place(txtSurname, "Surname");
            Place(txtEmail, "Bicol University Email");

            // email hint sits right under the email box
            Label? hint = Controls.OfType<Label>().FirstOrDefault(
                l => l.Text.StartsWith("Must end", StringComparison.OrdinalIgnoreCase));
            if (hint != null)
            {
                hint.Location = new Point(x, txtEmail.Bottom + 2);
                y = hint.Bottom + 10;
            }

            Place(txtUsername, "Username");
            Place(textBox1, "Password");
            Place(txtConfirm, "Confirm password");

            // "Show passwords" checkbox (small extra)
            var chkShow = new CheckBox
            {
                Text = "Show passwords",
                AutoSize = true,
                ForeColor = ThemeColors.TextDark,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(x, y - 4)
            };
            chkShow.CheckedChanged += (s, e) =>
            {
                char c = chkShow.Checked ? '\0' : '*';
                textBox1.PasswordChar = c;
                txtConfirm.PasswordChar = c;
            };
            Controls.Add(chkShow);
            y += 28;

            // error message line
            lblError.AutoSize = false;
            lblError.Location = new Point(x, y);
            lblError.Size = new Size(fieldW, 36);
            y += 40;

            // buttons, same width as the fields
            btnSignUp.Location = new Point(x, y);
            btnSignUp.Size = new Size(fieldW, 42);
            y += 52;
            btnBack.Location = new Point(x, y);
            btnBack.Size = new Size(fieldW, 38);
            y += 38 + 24;

            ClientSize = new Size(ClientSize.Width, y);     // form height fits the content
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string email = txtEmail.Text.Trim().ToLowerInvariant();
            string username = txtUsername.Text.Trim();

            string? error = PasswordHasher.ValidateSignUp(firstName, surname, email, username, textBox1.Text, txtConfirm.Text);
            if (error != null)
            {
                lblError.Text = error;
                return;
            }

            try
            {
                if(_users.UsernameExists(username))
                {
                    lblError.Text = "That username is already taken.";
                    return;
                }
                if (_users.EmailExists(email))
                {
                    lblError.Text = "That email is already  registered.";
                    return;
                }

                int id = _users.Create(firstName, surname, email, username, textBox1.Text);

                Session.CurrentUser = new AppUser
                {
                    UserId = id,
                    FirstName = firstName,
                    Surname = surname,
                    Username = username,
                    Email = email
                };
                DialogResult = DialogResult.OK;
            }
            catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                lblError.Text = "That username or email is already registered.";
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "Could not create the account. Please try again.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e) =>
        DialogResult = DialogResult.Cancel;
    }
}
