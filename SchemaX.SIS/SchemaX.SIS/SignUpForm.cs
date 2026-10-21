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
