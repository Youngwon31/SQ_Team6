using System;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace Namespace
{
    public partial class ForgotPassword : Page
    {
        protected void RetrievePassword_Click(object sender, EventArgs e)
        {
            string username = this.username.Value;
            string email = this.email.Value;

            string connectionString = "Server=localhost; Database=UserDB; Uid=root; Pwd=0000;";
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT password FROM users WHERE username = @username AND email = @email";

                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        passwordDisplay.InnerText = $"Your password is: {result.ToString()}";
                    }
                    else
                    {
                        passwordDisplay.InnerText = "No matching user found.";
                    }
                }
            }
        }
    }
}
