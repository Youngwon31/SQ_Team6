<!--
 * Filename: Login.aspx
 * Author:   Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
 * Date:     16/12/2023
 * Description: This file holds the client logic of our program taking user input 
 *              and sending it to the server to validate and process against our SQL database
 *              If the user is in the database the user continues to the home page. If not
 *              they are asked to try again, or signup, or use the forgot password function
 *
-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Namespace.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="login.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="logo-container">
                <img src="images/logo.png" alt="Logo" class="responsive-image" />
            </div>
            <h1>FRESHSAVER</h1>
            <div class="input-group">
                <!-- User inputs -->
                <input type="text" id="usernameInput" placeholder="username" runat="server" />
                <input type="password" id="passwordInput" placeholder="password" runat="server" />
                <button type="button" onclick="togglePassword()">Show Password</button>
                    <div class="remember-me-container">
                        <!-- remember user input -->
                        <input id="rememberMeInput" type="checkbox" runat="server" />
                        <label for="rememberMeInput">Remember me</label>
                    </div>
            <button type="submit" runat="server" onserverclick="Login_Click">Login</button>
                <div id="errorMessage" runat="server"></div>
            </div>
            <!-- sign up and forgot password links-->
            <div class="signup-link">
                Don’t have an account? <a href="SignUp.aspx">Sign up</a>
            </div>
            <div class="signup-link">
                Forgot Password? <a href="ForgotPassword.aspx">Find password</a>
            </div>
       </div>
    </form>
</body>
    <!-- show and hide user input -->
    <script type="text/javascript">
        function togglePassword()
        {
            let passwordInput = document.getElementById('<%= passwordInput.ClientID %>');
            if (passwordInput.type === 'password')
            {
                passwordInput.type = 'text';
            }
            else
            {
                passwordInput.type = 'password';
            }
        }
    </script>
</html>