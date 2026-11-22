using MySqlConnector;
using SchemaX.SIS.Models;
using SchemaX.SIS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipelines;

namespace SchemaX.SIS.Data
{
    public class UserRepository
    {
        public bool UsernameExists(string username) => Exists("username", username);
        public bool EmailExists(string email) => Exists("bu_email", email);

        private static bool Exists(string column, string value)
        {
            using var con = Db.Open();
            using var cmd = new MySqlCommand(
                "SELECT COUNT(*) FROM users WHERE " + column + " = @v", con);
            cmd.Parameters.AddWithValue("@v", value);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        // stores the new account; returns the new user_id
        public int Create(string firstName, string surname, string email,
                          string username, string password)
        {
            using var con = Db.Open();
            using var cmd = new MySqlCommand(@"
                INSERT INTO users (first_name, surname, bu_email, username, password_hash)
                VALUES (@fn, @sn, @em, @un, @pw)", con);
            cmd.Parameters.AddWithValue("@fn", firstName);
            cmd.Parameters.AddWithValue("@sn", surname);
            cmd.Parameters.AddWithValue("@em", email);
            cmd.Parameters.AddWithValue("@un", username);
            cmd.Parameters.AddWithValue("@pw", PasswordHasher.Hash(password));
            cmd.ExecuteNonQuery();
            return (int)cmd.LastInsertedId;
        }

        // look up the username; if found, check the password. Returns the user or null.
        public AppUser? Authenticate(string username, string password)
        {
            using var con = Db.Open();
            using var cmd = new MySqlCommand(@"
                SELECT user_id, first_name, surname, bu_email, password_hash
                FROM users WHERE username = @un", con);
            cmd.Parameters.AddWithValue("@un", username);
            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;                       // username does not exist
            if (!PasswordHasher.Verify(password, reader.GetString("password_hash")))
                return null;                                       // password is wrong

            return new AppUser
            {
                UserId = reader.GetInt32("user_id"),
                FirstName = reader.GetString("first_name"),
                Surname = reader.GetString("surname"),
                Username = username,
                Email = reader.GetString("bu_email")
            };
        }
    }

}
