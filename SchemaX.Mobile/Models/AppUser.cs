using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Models
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Username { get; set; } = "";
        public string FullName => FirstName + " " + Surname;
        public string Email { get; set; } = "";
    }
}
