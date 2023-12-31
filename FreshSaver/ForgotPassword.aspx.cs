﻿/*
* Filename: ForgotPassword.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the Forgot Password page of the website.
*              It allows users to retrieve their password by entering their username and email.
*              The code checks the user's credentials against the database and, if found,
*              displays the password. It also includes functionality to redirect users back to the login page.
*/

using System;
using System.Configuration;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace Namespace
{
    public partial class ForgotPassword : Page
    {
        // Database connection string
        private string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        protected void RetrievePassword_Click(object sender, EventArgs e)
        {
            //getting user input
            string username = this.username.Value;
            string email = this.email.Value;
            
            //connecting to SQL database
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT password FROM users WHERE username = @username AND email = @email";//query to get password based off the username and email given

                connection.Open();

                using (var command = new MySqlCommand(query, connection))//sending query to our database
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        passwordDisplay.InnerText = $"Your password is: {result.ToString()}";//display user password that was found
                    }
                    else
                    {
                        passwordDisplay.InnerText = "No matching user found.";//we were unable to find a user with those paramaters
                    }
                }
            }
        }
        protected void btnRedirectToLogin_Click(object sender, EventArgs e)
        {
            // Redirect the user to Login.aspx when the button is clicked
            Response.Redirect("Login.aspx");
        }


    }
}
