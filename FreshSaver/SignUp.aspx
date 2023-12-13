<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="FreshSaver.SignUp" %>

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
        <asp:Label runat="server" AssociatedControlID="username" Text="Username:" />
        <asp:TextBox runat="server" ID="username" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="username" ErrorMessage="Username is required" />

        <asp:Label runat="server" AssociatedControlID="email" Text="Email:" />
        <asp:TextBox runat="server" ID="email" TextMode="Email" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="email" ErrorMessage="Email is required" />

        <asp:Label runat="server" AssociatedControlID="password" Text="Password:" />
        <asp:TextBox runat="server" ID="password" TextMode="Password" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="password" ErrorMessage="Password is required" />

        <asp:Button runat="server" Text="Sign Up" OnClick="Submit_Click" />
    </form>
</body>
</html>
