<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="FreshSaver.ShoppingCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shopping Cart</title>
    <link href="shoppingCart.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="shopping-cart">
            <h2>Shopping Cart</h2>
            <asp:Label ID="lblEmptyShop" runat="server"></asp:Label>
            <asp:Repeater ID="CartItemsRepeater" runat="server">
                <ItemTemplate>
                    <div class="cart-item">
                        <span class="item-name"><%# Eval("MenuName") %></span>
                        <br />
                        <span class="item-quantity"> Quantity: <%# Eval("Quantity") %></span>
                        <br />
                        <span class="item-price">Price: $<%# Eval("ItemTotal", "{0:F2}") %></span>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="cart-totals">
                <p>Total Cost: $<asp:Label ID="lblTotalCost" runat="server"/></p>
            </div>

            <div class="cart-buttons">
                <asp:Button ID="btnBackToHome" runat="server" Text="Back to Home" PostBackUrl="~/Home.aspx" CssClass="back-to-store-button" />
                <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" CssClass="checkout-button"/>
            </div>
        </div>
    </form>
</body>
</html>
