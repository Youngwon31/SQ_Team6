<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FreshSaver.Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="homePage.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="homeForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Top bar with profile image on the right -->
        <div class="top-bar">
            <asp:ImageButton ID="imgbtnProfile" runat="server" ImageUrl="~/images/profile-icon.png" PostBackUrl="~/account.aspx" CssClass="profile-button" />
        </div>

        <!-- Search bar section -->
        <div class="search-bar">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search FreshSaver" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rptSearchResults" runat="server" OnItemCommand="rptSearchResults_ItemCommand">
                        <ItemTemplate>
                            <div class="search-result-item">
                                <asp:LinkButton CommandName="SelectStore" CommandArgument='<%# Eval("StoreID") %>' Text='<%# Eval("StoreName") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- Address container with pin icon -->
        <div class="address-container">
            <img src="~/images/maps-and-flags.png" runat="server" alt="Location Pin" class="pin-icon" />
            <div class="address">108 University Ave</div>
                </div>
 

            <div class="category-container">
                    <!--Grocery-->
                <asp:LinkButton ID="lnkGrocery" runat="server" CssClass="category-icon" CommandArgument="Grocery" OnCommand="CategorySelected">
                    <img src="images/grocery-icon.png" alt="Grocery" />
                    <span class="category-text">Groceries</span>
                    <!--Convenience-->
                </asp:LinkButton>
                <asp:LinkButton ID="lnkConvenience" runat="server" CssClass="category-icon" CommandArgument="Convenience" OnCommand="CategorySelected">
                    <img src="images/convenience-icon.png" alt="Convenience" />
                    <span class="category-text">Convenience</span>
                    <!--Restaurant-->
                </asp:LinkButton>
                <asp:LinkButton ID="lnkRestaurant" runat="server" CssClass="category-icon selected-category" CommandArgument="Restaurant" OnCommand="CategorySelected">
                    <img src="images/restaurant-icon.png" alt="Restaurant" />
                    <span class="category-text">Restaurant</span>
                    
                </asp:LinkButton>
            </div>



            <div class="store-section">
                <asp:Repeater ID="StoresRepeater" runat="server">
                    <HeaderTemplate>
                        <h2 class="tag">Recommended for you</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="store-item">
                            <asp:LinkButton ID="lnkStore" runat="server" CommandArgument='<%# Eval("StoreID") %>' OnCommand="StoreSelected">
                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("StoreName") %>' class="store-image"/>
                            </asp:LinkButton>
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
                        <h2 class="tag">New stores</h2>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="store-item">
                            <asp:LinkButton ID="lnkStore" runat="server" CommandArgument='<%# Eval("StoreID") %>' OnCommand="StoreSelected">
                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("StoreName") %>' class="store-image"/>
                            </asp:LinkButton>
                            <div class="store-description">
                                <span class="store-name"><%# Eval("StoreName") %> - <%# Eval("Address") %></span>
                                <span class="menu-item-name"><%# Eval("MenuName") %></span>
                                <span class="original-price">Was: $<%# Eval("OriginalPrice") %></span>
                                <span class="discounted-price">Now: $<%# Eval("DiscountedPrice") %></span>
                            </div>
                            <div class="bottom-nav">
                                
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    </form>
</body>
</html>
