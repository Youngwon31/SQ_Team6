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
    ImageURL VARCHAR(255),
    Stock INT,
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID)
);

INSERT INTO Stores (StoreID, StoreName, Address, Category, ImageURL, Rating, PickUpTimes, RegistrationDate) VALUES 
(1, 'The Owl of Minerva', '255 King St', 'Restaurant', 'images/owl.jpeg', 4.8, '9:00 PM - 11:00 PM', NOW() - INTERVAL 4 MONTH),
(2, '7-Eleven', '256 King St', 'Convenience', 'images/seven-eleven.jpg', 4.5, '24 Hours', NOW() - INTERVAL 4 MONTH),
(3, 'Walmart', '70 Bridgeport Rd E', 'Grocery', 'images/walmart.png', 4.7, '8:00 AM - 10:00 PM', NOW() - INTERVAL 4 MONTH),
(4, 'McDonalds', '335 Farmers Market Rd', 'Restaurant', 'images/mcdonalds.jpeg', 4.7, '6:00 PM - 1:00 AM', NOW() - INTERVAL 3 DAY),
(5, 'Neighbours Market', '278 King St', 'Convenience', 'images/neighbours.jpeg', 4.5, '7:00 AM - 11:00 PM', NOW() - INTERVAL 1 WEEK),
(6, 'Nofrills', '24 Forwell Creek Rd', 'Grocery', 'images/nofrills.png', 4.6, '9:00 AM - 9:00 PM', NOW());

INSERT INTO MenuItems (MenuItemID, StoreID, MenuName, Description, OriginalPrice, DiscountedPrice, ImageURL, Stock) VALUES 
(100, 1, 'Kamjatang', 'Pork bone soup', 17.99, 8.99, 'images/kamjatang.jpg', 3),
(200, 2, 'Bakery', 'Brownie', 3.50, 1.50, 'images/brownie.jpeg', 7),
(300, 3, 'Dairy goods', 'Skimmed 2% milk', 5.18, 2.48, 'images/milk.jpg', 15),
(400, 4, 'Bakery', '6 pack of donuts', 8.99, 4.99, 'images/donuts.jpg', 6),
(500, 5, 'Energy Bar', 'Chocolate energy bar', 1.99, 0.99, 'images/energy-bar.jpg', 20),
(600, 6, 'Fresh Tomatoes', 'Locally sourced tomatoes', 4.99, 2.99, 'images/tomatoes.jpg', 10);