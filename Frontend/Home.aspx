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
                    <input type="search" placeholder="Search FreshSaver" />
                </div>
            </div>

            <div class="category-container">
                <a href="#" class="category-icon">Grocery</a>
                <a href="#" class="category-icon">Convenience</a>
                <a href="#" class="category-icon">Restaurant</a>
            </div>

            <div class="filter-container">
                <button class="filter-button">Pick-up time</button>
                <button class="filter-button">Food types</button>
            </div>

            <div class="recommended-section">
                <h2>Recommended for you</h2>
                <div class="store-item">
                    <img src="images\owl.jpeg" alt="Store Item" class="store-image"/>
                    <div class="store-description">
                        <span class="store-name">The Owl of Minerva - 255 King St</span>
                        <span class="original-price">$17.99</span>
                        <span class="price">$8.99</span>
                    </div>
                </div>
            </div>

            <div class="new-stores-section">
                <h2>New stores</h2>
                <div class="store-item">
                    <img src="images\seven-eleven.jpg" alt="Store Item" class="store-image"/>
                    <div class="store-description">
                        <span class="store-name">7-Eleven - 256 King St</span>
                        <span class="original-price">$12.99</span>
                        <span class="price">$4.99</span>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>