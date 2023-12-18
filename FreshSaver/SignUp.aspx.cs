/*
* Filename: SignUp.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the SignUp page of the FreshSaver website.
*              It includes methods for handling user registration, including input validation, 
*              database interaction for storing user details, and logging signup attempts.
*              The user inputs are validated for format and completeness before being inserted
*              into the UserDB database. The page also provides a method to redirect users 
*              to the Login page.
*/

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

namespace FreshSaver
{
    public partial class SignUp : System.Web.UI.Page
    {
        // Database connection string
        private string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        // This method handles the event when the submit button is clicked
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Retrieving user inputs from the form
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
   
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // SQL command to insert new user data into the database
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
                    // Log the signup attempt
                    LogSignupAttempt(username);
                }
                catch (MySqlException ex)
                {
                    // Display error message in case of an exception
                    errorLabel.Text = $"An error occurred: {ex.Message}";
                    errorLabel.Visible = true;
                }
            }
            
        }

        // Method to redirect the user to the login page
        protected void ReturnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        // Method to log the signup attempt
        private void LogSignupAttempt(string username)
        {
            // Define the path and content of the log file
            string filePath = Server.MapPath("~/logs/SignupLogs.txt");
            string logText = $"Username: {username}, Timestamp: {DateTime.Now}\n";

            try
            {
                // Append the log text to the file
                File.AppendAllText(filePath, logText);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during file writing
                Console.WriteLine("Error writing to log file: " + ex.Message);
            }
        }
    }
}