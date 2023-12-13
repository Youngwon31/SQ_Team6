<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Namespace.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
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
                <input type="text" id="usernameInput" placeholder="username" runat="server" />
                <input type="password" id="passwordInput" placeholder="password" runat="server" />
                <button type="button" onclick="togglePassword()">Show</button>
                    <div class="remember-me-container">
                        <input id="rememberMeInput" type="checkbox" runat="server" />
                        <label for="rememberMeInput">Remember me</label>
                    </div>
            <button type="submit" runat="server" onserverclick="Login_Click">Login</button>
                <div id="errorMessage" runat="server"></div>
            </div>
            <div class="signup-link">
                Don’t have an account? <a href="SignUp.aspx">Sign up</a>
            </div>
            <div class="signup-link">
                Forgot Password? <a href="ForgotPassword.aspx">Find password</a>
            </div>
       </div>
    </form>
</body>
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