﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="FreshSaver.SignUp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sign-Up</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
</head>
<body>
    <form id="signupForm" runat="server">
        <asp:TextBox runat="server" ID="username" placeholder="Username" />
        <asp:TextBox runat="server" ID="email" TextMode="Email" placeholder="Email" />
        <asp:TextBox runat="server" ID="password" TextMode="Password" placeholder="Password" CssClass="password-input" onkeyup="checkPasswordStrength();" />
        <div id="passwordStrengthIndicator" style="display:none;">
            <label>Password Strength: </label>
            <span id="passwordStrength"></span>
        </div>
        <asp:Button runat="server" ID="revealPasswordButton" Text="Show" OnClientClick="togglePasswordVisibility(); return false;" CssClass="reveal-password-button" />
        <asp:Label runat="server" ID="errorLabel" CssClass="error-message" Visible="false"></asp:Label>
        <asp:Button runat="server" Text="Sign Up" OnClick="Submit_Click" />
        <asp:Button runat="server" Text="Return to Login" OnClick="ReturnToLogin_Click" CssClass="return-to-login-button" />
    </form>
</body>
        <script type="text/javascript">
            function togglePasswordVisibility()
            {
                let passwordInput = document.getElementById('<%= password.ClientID %>');
                let toggleButton = document.getElementById('<%= revealPasswordButton.ClientID %>');

                if (passwordInput.type === 'password') {
                    passwordInput.type = 'text';
                    toggleButton.textContent = 'Hide';
                }
                else {
                    passwordInput.type = 'password';
                    toggleButton.textContent = 'Show';
                }
            }

            function checkPasswordStrength()
            {
                let password = document.getElementById('<%= password.ClientID %>').value;
                let strengthIndicator = document.getElementById('passwordStrength');
                let indicatorContainer = document.getElementById('passwordStrengthIndicator');

                let strength = 0;
                if (password.length >= 8) strength++;
                if (password.match(/[A-Z]/)) strength++;
                if (password.match(/[\W]/)) strength++;

                switch (strength) {
                    case 0:
                    case 1:
                        strengthIndicator.textContent = 'Weak';
                        indicatorContainer.style.display = 'block';
                        break;
                    case 2:
                        strengthIndicator.textContent = 'The common';
                        indicatorContainer.style.display = 'block';
                        break;
                    case 3:
                        strengthIndicator.textContent = 'Strong';
                        indicatorContainer.style.display = 'block';
                        break;
                    default:
                        indicatorContainer.style.display = 'none';
                        break;
                }
            }
        </script>
</html>
