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
            <!-- Store Image -->
            <asp:Image ID="imgStoreImage" runat="server" CssClass="store-image" />
            <!-- Store Name -->
            <asp:Label ID="lblStoreName" runat="server" CssClass="store-name"></asp:Label>
            <!-- Store Address -->
            <asp:Label ID="lblAddress" runat="server" CssClass="store-address"></asp:Label>

            <!-- Menu Section -->
            <h2>Menu</h2>
            <asp:Repeater ID="MenuItemsRepeater" runat="server">
                <ItemTemplate>
                    <!-- 메뉴 아이템 디스플레이 로직 -->
                    <div class="menu-item">
                        <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("MenuName") %>' class="menu-item-image" />
                        <span class="menu-name"><%# Eval("MenuName") %></span>
                        <span class="menu-description"><%# Eval("Description") %></span>
                        <div class="price-info">
                            <span class="original-price">Was: $<%# Eval("OriginalPrice", "{0:F2}") %></span>
                            <span class="discounted-price">Now: $<%# Eval("DiscountedPrice", "{0:F2}") %></span>
                        </div>
                        <div class="stock-info">
                            Available Quantity: <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%# Eval("Stock") %>'></asp:Label>
                        </div>
                           <!-- 장바구니 버튼 -->
                        <asp:Button ID="btnAddToCart" runat="server" CommandArgument='<%# Eval("MenuItemID") %>' Text="Add to Cart" OnClick="AddToCart" CssClass="add-to-cart-button" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- 재고 메시지와 수량 표시 -->
            <div class="stock-info">
                <asp:Label ID="lblStockMessage" runat="server" CssClass="stock-message" Visible="false"></asp:Label>
            </div>

            <!-- Total cost and quantity display -->
            <div class="cart-totals">
                <p>Total Cost: <asp:Label ID="lblTotalCost" runat="server" /></p>
                <p>Total Quantity: <asp:Label ID="lblTotalQuantity" runat="server" /></p>
            </div>

            <!-- Shopping Cart Button -->
            <asp:Button ID="btnShoppingCart" runat="server" Text="Shopping Cart" PostBackUrl="~/ShoppingCart.aspx" CssClass="shopping-cart-button" />
        </div>
    </form>
</body>
</html>