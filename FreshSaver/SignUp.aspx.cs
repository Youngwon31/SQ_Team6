using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace FreshSaver
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Submit_Click(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string email = this.email.Text;
            string password = this.password.Text;

            // Server-side validation
            if (string.IsNullOrWhiteSpace(username))
            {
                errorLabel.Text = "Username is required.";
                errorLabel.Visible = true;
                return;
            }
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]*$"))
            {
                errorLabel.Text = "Username can only contain letters and numbers.";
                errorLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                errorLabel.Text = "Email is required.";
                errorLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                errorLabel.Text = "Password is required.";
                errorLabel.Visible = true;
                return;
            }
            if (!Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$"))
            {
                errorLabel.Text = "Password must contain at least one letter, one number, and one special character.";
                errorLabel.Visible = true;
                return;
            }

            //Attempt to connect to database and save user information
            string connectionString = "Server=localhost; Database=UserDB; Uid=root; Pwd=0000;";
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO users (username, email, password) VALUES (@username, @email, @password)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password); // Consider hashing the password

                try
                {
                    cmd.ExecuteNonQuery();
                    // Registration success message
                    errorLabel.Text = "Registration successful!";
                    errorLabel.Visible = true;
                    
                }
                catch (MySqlException ex)
                {
                    errorLabel.Text = $"An error occurred: {ex.Message}";
                    errorLabel.Visible = true;
                }
            }
        }
        //return to log in page
        protected void ReturnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}