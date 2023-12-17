using System;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace Namespace
{
    public partial class ForgotPassword : Page
    {
        protected void RetrievePassword_Click(object sender, EventArgs e)
        {
            //getting user input
            string username = this.username.Value;
            string email = this.email.Value;

            //creating string to where our database is
            string connectionString = "Server=localhost; Database=UserDB; Uid=root; Pwd=@Yj7788794439;";
            
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
