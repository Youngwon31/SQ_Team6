﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using MySql.Data.MySqlClient;

namespace Namespace
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to a different page if the user is already authenticated
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // Extracting user input
            string username = usernameInput.Value;
            string password = passwordInput.Value;

            if (ValidateUser(username, password))
            {
                // Check if the user has selected "Remember Me"
                bool persist = rememberMeInput.Checked;

                // Create an authentication ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, // Ticket version
                    username, // Username
                    DateTime.Now, // Issuance time
                    DateTime.Now.AddMinutes(30), // Expiration time
                    persist, // Persistent cookies
                    string.Empty, // User data
                    FormsAuthentication.FormsCookiePath); // Cookie path

                // Ticket encryption
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Create a cookie
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                if (persist)
                {
                    authCookie.Expires = ticket.Expiration;
                }

                // Adding cookies to responses
                Response.Cookies.Add(authCookie);

                // Redirect users to the page they originally requested
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "HomePage.aspx";
                }

                Response.Redirect(returnUrl);
            }
            else
            {
                // Display an error message if the credentials are invalid
                errorMessage.InnerText = "Invalid username or password.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = "Server=localhost; Database=UserDB; Uid=root; Pwd=0000;";
            using (var connection = new MySqlConnection(connectionString))
            {
                // Create a user validation query with username and password
                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

                // Open a database connection
                connection.Open();

                //  Generate SQL commands
                using (var command = new MySqlCommand(query, connection))
                {
                    // Set SQL parameters
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Run a query and see the results
                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    return userCount > 0;
                }
            }
        }
    }
}