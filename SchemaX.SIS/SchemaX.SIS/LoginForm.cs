using SchemaX.SIS.Data;
using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;

namespace SchemaX.SIS
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _users = new();
        private bool _hasError;
        private static readonly Color ErrorBack = Color.FromArgb(253, 222, 222);

        public LoginForm()
        {
            InitializeComponent();
            txtUsername.TextChanged += ClearErrorState;    // typing again goes back to default colors
            txtPassword.TextChanged += ClearErrorState;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter your username and password.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                AppUser? user = null;
                // only ask the database when both inputs follow the rules
                if (PasswordHasher.IsValidUsername(username) && PasswordHasher.IsValidPassword(password))
                    user = _users.Authenticate(username, password);

                if (user == null)
                {
                    SetErrorState(true);                   // both fields turn red
                    MessageBox.Show("The username and password did not match. Please try again.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Session.CurrentUser = user;
                DialogResult = DialogResult.OK;            // closes this form; the dashboard opens next
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex,
                    "Could not log in. Check that MySQL is running and appsettings.json is correct.");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            using var signUp = new SignUpForm();
            if (signUp.ShowDialog(this) == DialogResult.OK)
                DialogResult = DialogResult.OK;            // account created and signed in -> dashboard
        }

        private void SetErrorState(bool on)
        {
            _hasError = on;
            foreach (TextBox box in new[] { txtUsername, txtPassword })
            {
                box.BackColor = on ? ErrorBack : SystemColors.Window;
                box.ForeColor = on ? ThemeColors.Danger : SystemColors.WindowText;
            }
        }

        private void ClearErrorState(object? sender, EventArgs e)
        {
            if (_hasError) SetErrorState(false);
        }
    }
}
