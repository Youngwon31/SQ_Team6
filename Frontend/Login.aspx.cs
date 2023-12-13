using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;

namespace Namespace
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If the user is already authenticated, redirect them to a different page.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // Extract the submitted username and password
            string username = usernameInput.Value;
            string password = passwordInput.Value;

            // Validate the credentials with your method
            if (ValidateUser(username, password))
            {
                // If the user wants to be remembered, set the ticket to be persistent
                bool persist = rememberMeInput.Checked;

                // Create the authentication ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, // Ticket version
                    username, // User name for whom the ticket is issued
                    DateTime.Now, // Date/time issued
                    DateTime.Now.AddMinutes(30), // Date/time to expire
                    persist, // "true" for a persistent user cookie
                    string.Empty, // User-data, in this case, the roles
                    FormsAuthentication.FormsCookiePath); // Path cookie valid for

                // Encrypt the ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                if (ticket.IsPersistent)
                {
                    authCookie.Expires = ticket.Expiration;
                }

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(authCookie);

                // Redirect the user to the originally requested page
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null) returnUrl = "HomePage.aspx";

                Response.Redirect(returnUrl);
            }
            else
            {
                // If the user's credentials are invalid
                // You might want to log this incident
                errorMessage.InnerText = "Invalid username or password.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            // Here you should write the logic to validate the user.
            // This could be against a database or any other data store.

            // This is just a placeholder for your actual validation logic
            return (username == "testuser" && password == "testpassword");
        }
    }
}
