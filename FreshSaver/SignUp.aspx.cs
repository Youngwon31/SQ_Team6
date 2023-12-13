using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreshSaver
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string email = this.email.Text;
            string password = this.password.Text;

            // Password hashing
            // string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            string connectionString = "Server=localhost; Database=UserDB; Uid=root; Pwd=0000;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO users (username, email, password) VALUES (@username, @email, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        // Successful signup message
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationSuccess", "alert('Registration successful!');", true);
                    }
                    catch (MySqlException ex)
                    {
                        // Handling database errors
                        ClientScript.RegisterStartupScript(this.GetType(), "registrationError", $"alert('An error occurred: {ex.Message}');", true);
                    }
                }
            }

        }
    }
}