<!--
* Filename: ForgotPassword.aspx
* Author:   Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date:     16/12/2023
* Description: This HTML markup file creates the user interface for the 'Forgot Password' feature of the website. 
*              It includes input fields for the user's username and email, and two buttons - one for submitting a 
*              request to retrieve the password and another to return to the login page. 
-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Namespace.ForgotPassword" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link href="forgotPassword.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />s
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Input field for username -->
            <label for="username">Username:</label>
            <input type="text" id="username" runat="server" />
        </div>
        <div>
            <!-- Input field for email -->
            <label for="email">Email:</label>
            <input type="text" id="email" runat="server" />
        </div>
        <!-- Button to trigger password retrieval -->
        <button type="submit" runat="server" onserverclick="RetrievePassword_Click">Retrieve Password</button>
        <br />
        <!-- Button to redirect back to the login page -->
        <button type="submit" runat="server" onserverclick="btnRedirectToLogin_Click" >Return to Login</button>
    </form>
    <br />
    <!-- Area where the password or related messages will be displayed -->
    <div runat="server" id="passwordDisplay"></div>
    <div>
        

    </div>
</body>
</html>
