<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Namespace.ForgotPassword" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link href="forgotPassword.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="username">Username:</label>
            <input type="text" id="username" runat="server" />
        </div>
        <div>
            <label for="email">Email:</label>
            <input type="text" id="email" runat="server" />
        </div>
        <button type="submit" runat="server" onserverclick="RetrievePassword_Click">Retrieve Password</button>
        <br />
        <button type="submit" runat="server" onserverclick="btnRedirectToLogin_Click" >Return to Login</button>
    </form>
    <br />
    <div runat="server" id="passwordDisplay"></div>
    <div>
        

    </div>
</body>
</html>
