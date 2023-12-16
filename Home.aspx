<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FreshSaver.Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="homeForm" runat="server">
        <div class="home-container">
            <div class="address-search-container">
                <div class="address">108 University Ave</div>
                <div class="search-bar">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search FreshSaver"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchStores" CssClass="search-button" />
                </div>
            </div>

            <div class="category-container">
                <asp:LinkButton ID="lnkGrocery" runat="server" CssClass="category-icon" CommandArgument="Grocery" OnCommand="CategorySelected">Grocery</asp:LinkButton>
                <asp:LinkButton ID="lnkConvenience" runat="server" CssClass="category-icon" CommandArgument="Convenience" OnCommand="CategorySelected">Convenience</asp:LinkButton>
                <asp:LinkButton ID="lnkRestaurant" runat="server" CssClass="category-icon selected-category" CommandArgument="Restaurant" OnCommand="CategorySelected">Restaurant</asp:LinkButton>
            </div>

            <div class="store-section">
                <asp:Repeater ID="StoresRepeater" runat="server">
                    <HeaderTemplate>
                        <h2>Recommended for you</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="store-item">
                            <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("StoreName") %>' class="store-image"/>
                            <div class="store-description">
                                <span class="store-name"><%# Eval("StoreName") %> - <%# Eval("Address") %></span>
                                <span class="menu-item-name"><%# Eval("MenuName") %></span>
                                <span class="original-price">Was: $<%# Eval("OriginalPrice") %></span>
                                <span class="discounted-price">Now: $<%# Eval("DiscountedPrice") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="new-stores-section">
                <asp:Repeater ID="NewStoresRepeater" runat="server">
                    <HeaderTemplate>
                        <h2>New stores</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="store-item">
                            <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("StoreName") %>' class="store-image"/>
                            <div class="store-description">
                                <span class="store-name"><%# Eval("StoreName") %> - <%# Eval("Address") %></span>
                                <span class="menu-item-name"><%# Eval("MenuName") %></span>
                                <span class="original-price">Was: $<%# Eval("OriginalPrice") %></span>
                                <span class="discounted-price">Now: $<%# Eval("DiscountedPrice") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
