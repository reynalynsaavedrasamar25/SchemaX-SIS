using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Helpers
{
    public static class Validator
    {
        public static bool IsStudentIdValid(string studentId)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                studentId,
                @"^\d{4}-\d{5}$"
            );
        }
        public static bool IsRequiredFieldValid(string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNameValid(string? name)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   System.Text.RegularExpressions.Regex.IsMatch(
                       name,
                       @"^[A-Za-z\s'-]+$"
                   );
        }
        public static bool IsBirthdayValid(DateTime birthday)
        {
            return birthday.Date < DateTime.Today;
        }
        public static bool IsBuEmailValid(string? email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   System.Text.RegularExpressions.Regex.IsMatch(
                       email.Trim(),
                       @"^[A-Za-z0-9._%+-]+@bu\.edu\.ph$",
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase
                   );
        }
        public static bool IsContactNumberValid(string? contactNumber)
        {
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                return false;
            }

            string number = contactNumber.Trim();

            return number.Length >= 7 &&
                   number.Length <= 15 &&
                   System.Text.RegularExpressions.Regex.IsMatch(
                       number,
                       @"^[0-9+\-\s]+$"
                   );
        }

    }
}
    
