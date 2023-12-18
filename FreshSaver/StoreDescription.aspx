<!--
* Filename: StoreDescription.aspx
* Author:   Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date:     16/12/2023
* Description: This HTML markup file creates the user interface for the Store Description page of the FreshSaver website.
*              It displays detailed information about a specific store, including its image, name, address, and pick-up times.
*              The page also features a menu section where users can view menu items, adjust quantities, and see pricing details.
*              It includes functionality for adding or removing items from the shopping cart and displays total cost and cart quantity.
*              A button is provided for navigating to the ShoppingCart page for further actions.
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreDescription.aspx.cs" Inherits="FreshSaver.StoreDescription" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store Description</title>
    <link href="storeDescription.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="store-details">
            <!-- Store Image -->
            <asp:Image ID="imgStoreImage" runat="server" CssClass="store-image" />
            <br />
            <!-- Store Name -->
            <asp:Label ID="lblStoreName" runat="server" CssClass="store-name"></asp:Label>
            <br />
            <!-- Store Address -->
            <asp:Label ID="lblAddress" runat="server" CssClass="store-address"></asp:Label>
            <br />
            <!-- Store Pick up times -->
            <asp:Label ID="lblPickUpTimes" runat="server" CssClass="store-pickup"></asp:Label>

            <!-- Menu Section -->
            <h2>Menu</h2>
            <asp:Repeater ID="MenuItemsRepeater" runat="server">
                <ItemTemplate>
                    <!-- Menu Item Display Logic -->
                    <div class="menu-item">
                        <div class="quantity-controls">
                            <asp:Button ID="btnRemoveFromCart" runat="server" CommandArgument='<%# Eval("MenuItemID") %>' Text="-" OnClick="RemoveFromCart" CssClass="quantity-btn remove-btn" />
                            
                            <asp:Button ID="Button1" runat="server" CommandArgument='<%# Eval("MenuItemID") %>' Text="+" OnClick="AddToCart" CssClass="quantity-btn add-btn" />
                        </div>
                        <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("MenuName") %>' class="menu-item-image" />
                        <br />
                        <span class="menu-name"><%# Eval("MenuName") %></span>
                        <br />
                        <span class="menu-description"><%# Eval("Description") %></span>
                        <br />
                        <div class="price-info">
                            <span class="original-price">Was: $<%# Eval("OriginalPrice", "{0:F2}") %></span>
                            <span class="discounted-price">Now: $<%# Eval("DiscountedPrice", "{0:F2}") %></span>
                        </div>
                        <div class="stock-info">
                            Available Quantity: <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%# Eval("Stock") %>'></asp:Label>
                        </div>
                           
                        
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- Displaying inventory messages and quantities -->
            <div class="stock-info">
                <asp:Label ID="lblStockMessage" runat="server" CssClass="stock-message" Visible="false"></asp:Label>
            </div>

            <!-- Total cost and quantity display -->
            <div class="cart-totals">
                <p>Total Cost: <asp:Label ID="lblTotalCost" runat="server" /></p>
                <p>Current Quantity in Cart: <asp:Label ID="lblTotalQuantity" runat="server" /></p>
            </div>

            <!-- Shopping Cart Button -->
            <asp:Button ID="btnShoppingCart" runat="server" Text="Shopping Cart" PostBackUrl="~/ShoppingCart.aspx" CssClass="shopping-cart-button" />
        </div>
    </form>
</body>
</html>