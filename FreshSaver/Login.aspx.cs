/*
* Filename: Login.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the Login page of the website.
*              It handles user authentication, including validating user credentials against 
*              a database, managing authentication tickets, and logging login attempts. 
*              Users are redirected to their originally requested page upon successful authentication.
*/

using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.IO;
using System.Configuration;

namespace Namespace
{
    public partial class Login : Page
    {
        // Database connection string
        private string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;

        // Executes when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to a different page if the user is already authenticated
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // Extracting user input
            string username = usernameInput.Value;
            string password = passwordInput.Value;

            // Log the login attempt before validating the user
            LogLoginAttempt(username);

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
                    returnUrl = "Home.aspx";
                }

                Response.Redirect(returnUrl);
            }
            else
            {
                // Display an error message if the credentials are invalid
                errorMessage.InnerText = "Invalid username or password.";
            }
        }

        // Validates user credentials against the database
        private bool ValidateUser(string username, string password)
        {
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

        // Logs each login attempt to a file
        private void LogLoginAttempt(string username)
        {
            string filePath = Server.MapPath("~/logs/LoginLogs.txt");
            string logText = $"Username: {username}, Timestamp: {DateTime.Now}\n";

            try
            {
                // Append the log text to the file
                File.AppendAllText(filePath, logText);
            }
            catch (Exception ex)
            {
                // Handle any file writing errors
                Console.WriteLine("Error writing to log file: " + ex.Message);
            }
        }
    }
}
