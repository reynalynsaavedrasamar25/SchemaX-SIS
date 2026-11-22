using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SchemaX.SIS.Helpers;

namespace SchemaX.SIS.Helpers
{
    public static class PasswordHasher
    {
        //hashes the password using SHA256
        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(bytes);
            }
        }

        //compares a plain password agianst the hashed password
        public static bool Verify(string password, string hash)
        {
            return string.Equals(Hash(password), hash, StringComparison.OrdinalIgnoreCase);
        }

        // lowercase letters, numbers, underscore, period; up to 50 characters
        public static bool IsValidUsername(string username) =>
            Regex.IsMatch(username, @"^[a-z0-9_.]{1,50}$");

        public static bool IsValidPassword(string password) => password.Length >= 8;

        // letters only (single spaces between words), no special characters
        public static bool IsValidName(string name) =>
            name.Length <= 50 && Regex.IsMatch(name, @"^\p{L}+( \p{L}+)*$");

        public static bool IsValidEmail(string email) =>
            email.Length <= 100 &&
            Regex.IsMatch(email, @"^[a-z0-9._%+\-]+@bicol-u\.edu\.ph$", RegexOptions.IgnoreCase);

        // returns an error message, or null when everything is valid
        public static string? ValidateSignUp(string firstName, string surname, string email,
                                             string username, string password, string confirm)
        {
            if (firstName == "") return "First name is required.";
            if (!IsValidName(firstName))
                return "First name may only contain letters (no special characters).";

            if (surname == "") return "Surname is required.";
            if (!IsValidName(surname))
                return "Surname may only contain letters (no special characters).";

            if (email == "") return "Bicol University email is required.";
            if (!IsValidEmail(email))
                return "Email must be a valid address ending with @bicol-u.edu.ph.";

            if (username == "") return "Username is required.";
            if (!IsValidUsername(username))
                return "Username may only use lowercase letters, numbers, \"_\" and \".\" (up to 50 characters).";

            if (password == "") return "Password is required.";
            if (!IsValidPassword(password)) return "Password must be at least 8 characters.";

            if (confirm != password) return "Passwords do not match.";
            return null;
        }
    }

}
