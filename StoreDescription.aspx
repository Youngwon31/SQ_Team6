<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreDescription.aspx.cs" Inherits="FreshSaver.StoreDescription" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store Description</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="store-details">
            <h1 id="lblStoreName" runat="server"></h1>
            <asp:Image id="imgStoreImage" runat="server" />
            <p id="lblAddress" runat="server"></p>
            <h2>Menu</h2>
            <div class="menu-item">
                <h3 id="lblMenuName" runat="server"></h3>
                <p id="lblDescription" runat="server"></p>
                <p>Was: $<span id="lblOriginalPrice" runat="server"></span></p>
                <p>Now: $<span id="lblDiscountedPrice" runat="server"></span></p>
            </div>
        </div>
    </form>
</body>
</html>
