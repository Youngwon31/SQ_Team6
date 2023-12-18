<!--
* Filename: ShoppingCart.aspx
* Author:   Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date:     16/12/2023
* Description: This HTML markup file creates the user interface for the Shopping Cart page of the FreshSaver website.
*              It displays the items added to the shopping cart, including names, quantities, and prices.
*              The page provides options for users to proceed to checkout or return to the Home page.
*              It dynamically displays the total cost of items in the cart.
-->

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
         <!-- Shopping Cart Section -->
        <div class="shopping-cart">
            <h2>Shopping Cart</h2>
            <!-- Label for empty shopping cart message -->
            <asp:Label ID="lblEmptyShop" runat="server"></asp:Label>
            <asp:Repeater ID="CartItemsRepeater" runat="server">
                <ItemTemplate>
                     <!-- Individual cart item display -->
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

            <!-- Display of total cost in the cart -->
            <div class="cart-totals">
                <p>Total Cost: $<asp:Label ID="lblTotalCost" runat="server"/></p>
            </div>

             <!-- Buttons for navigation -->
            <div class="cart-buttons">
                <asp:Button ID="btnBackToHome" runat="server" Text="Back to Home" PostBackUrl="~/Home.aspx" CssClass="back-to-store-button" />
                <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" CssClass="checkout-button"/>
            </div>
        </div>
    </form>
</body>
</html>
