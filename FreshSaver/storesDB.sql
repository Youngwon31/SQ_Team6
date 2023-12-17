CREATE DATABASE IF NOT EXISTS StoresDB;
USE StoresDB;
CREATE TABLE Stores (
    StoreID INT AUTO_INCREMENT PRIMARY KEY,
    StoreName VARCHAR(255),
    Address VARCHAR(255),
    Category ENUM('Grocery', 'Convenience', 'Restaurant'),
    ImageURL VARCHAR(255),
    Rating DECIMAL(3, 1),
    PickUpTimes VARCHAR(255),
    RegistrationDate DATETIME
);

CREATE TABLE MenuItems (
    MenuItemID INT AUTO_INCREMENT PRIMARY KEY,
    StoreID INT,
    MenuName VARCHAR(255),
	Description TEXT,
    OriginalPrice DECIMAL(10, 2),
    DiscountedPrice DECIMAL(10, 2),
    ItemURL VARCHAR(255),
    Stock INT,
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID)
);

INSERT INTO Stores (StoreID, StoreName, Address, Category, ImageURL, Rating, PickUpTimes, RegistrationDate) VALUES 
(1, 'The Owl of Minerva', '255 King St', 'Restaurant', 'images/owl.jpeg', 4.8, '9:00 PM - 11:00 PM', NOW() - INTERVAL 4 MONTH),
(2, '7-Eleven', '256 King St', 'Convenience', 'images/seven-eleven.jpg', 4.5, '24 Hours', NOW() - INTERVAL 1 WEEK),
(3, 'Walmart', '70 Bridgeport Rd E', 'Grocery', 'images/walmart.png', 4.7, '8:00 AM - 10:00 PM', NOW() - INTERVAL 4 MONTH),
(4, 'McDonalds', '335 Farmers Market Rd', 'Restaurant', 'images/mcdonalds.jpg', 4.7, '6:00 PM - 1:00 AM', NOW() - INTERVAL 3 DAY),
(5, 'Neighbours Market', '278 King St', 'Convenience', 'images/neighbours.png', 4.5, '7:00 AM - 11:00 PM', NOW() - INTERVAL 4 MONTH),
(6, 'Nofrills', '24 Forwell Creek Rd', 'Grocery', 'images/nofrills.jpg', 4.6, '9:00 AM - 9:00 PM', NOW());

INSERT INTO MenuItems (MenuItemID, StoreID, MenuName, Description, OriginalPrice, DiscountedPrice, ItemURL, Stock) VALUES 
(100, 1, 'Soup', 'kamjatang', 17.99, 8.99, 'images/kamjatang.png', 3),
(200, 2, 'Bakery', 'Brownie', 3.50, 1.50, 'images/brownie.jpg', 7),
(300, 3, 'Dairy goods', 'Skimmed 2% milk', 5.18, 2.48, 'images/milk.png', 15),
(400, 4, 'Bakery', '6 pack of donuts', 8.99, 4.99, 'images/donuts.png', 6),
(500, 5, 'Energy Bar', 'Chocolate energy bar', 1.99, 0.99, 'images/energy-bar.png', 20),
(600, 6, 'Produce', 'Locally sourced tomatoes', 4.99, 2.99, 'images/tomatoes.png', 10),
(700, 6, 'Produce', 'Locally sourced Cucumbers', 3.99, 1.50, 'images/cucumber.jpg', 16),
(800, 5, 'Fruit Snack', 'Nutritious Fruit Cup', 5.99, 2.50, 'images/FruitCup.jpg', 4),
(900, 4, 'Protein', 'Chicken Nuggets', 7.99, 5.50, 'images/chickenNugget.jpg', 2),
(1000, 3, 'Produce', 'Celery Sticks', 3.99, 1.49, 'images/CelerySticks.jpg', 18),
(1100, 2, 'Protein', 'Jumbo Hot Dogs', 1.50, 0.99, 'images/JumboHotDog.jpg', 9),
(1200, 1, 'Fried Good', 'Kimchi Pancake', 10.00, 6.50, 'images/kimchiPancake.jpg', 2);

