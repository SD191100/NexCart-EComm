
SELECT * FROM Users;
SELECT * FROM Sellers;
SELECT * FROM Products;
SELECT * FROM Orders;
SELECT * FROM OrderDetails;
SELECT * FROM Payments;
SELECT * FROM Carts;
SELECT * FROM cartitems;
SELECT * FROM Categories;
SELECT * FROM ProductInventories;
SELECT * FROM Addresses;


INSERT INTO Categories (Name, Description) VALUES
('Skin Care','this catergory is for skin care products'),
('Men''s Fashion', 'Stylish and trendy outfits for men.'),
('Women''s Fashion', 'Elegant and fashionable clothing for women.'),
('Groceries', 'Everyday essentials and food items.'),
('Packed Food', 'Ready-to-eat and packaged food items.'),
('Beverages', 'Refreshing drinks and beverages for all occasions.'),
('Electronics', 'Latest gadgets and electronic appliances.'),
('Motors', 'Motor accessories and related products.'),
('Toys', 'Fun and educational toys for kids of all ages.');



delete from Products where ProductId = 11

INSERT INTO Products (Name, Description, Price, Stock, CategoryId, SellerId, MainImage, SecondImage, ThirdImage, Details) VALUES
('face Vitamin C Serum' ,'this is a Vitamin C Serum For Good Skin ',500 , 100 , 9 , 2 , 'https://images.unsplash.com/photo-1608571423902-eed4a5ad8108?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D' , 'https://images.unsplash.com/photo-1723951174326-2a97221d3b7f?q=80&w=1780&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D' , 'https://images.unsplash.com/photo-1585652757173-57de5e9fab42?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Designed with care to provide the perfect fit for all skin types.'),
('Elegant Party Dress', 'Beautiful sleeveless evening gown for women. Lightweight and breathable material, perfect for parties and events.', 2499, 10, 3, 2, 'https://m.media-amazon.com/images/I/71EeEhEWMXL._UL1500_.jpg', 'https://m.media-amazon.com/images/I/61Wby2Uy5mL._UL1500_.jpg', 'https://m.media-amazon.com/images/I/61NTeH3ADpL._UL1500_.jpg', 'Designed with care to provide the perfect fit and style for evening events and casual outings.'),
('Men''s Casual Shirt', 'Stylish men''s cotton casual shirt with full sleeves. Ideal for office wear or casual outings.', 899, 25, 2, 2, 'https://m.media-amazon.com/images/I/81A0pQoyB2L._UL1500_.jpg', 'https://m.media-amazon.com/images/I/71qH6BcoEIL._UL1500_.jpg', 'https://m.media-amazon.com/images/I/81JkclyQUhL._UL1500_.jpg', 'Made from premium cotton for all-day comfort. Available in various sizes and colors.'),
('Organic Basmati Rice', 'High-quality, long-grain organic basmati rice. Perfect for biryanis, pulao, or everyday meals.', 199, 50, 4, 2, 'https://m.media-amazon.com/images/I/71HZ1MdRqpL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81JvnSu3e8L._SL1500_.jpg', 'https://m.media-amazon.com/images/I/91jxLtbAhZL._SL1500_.jpg', 'Certified organic basmati rice sourced from sustainable farms. Ideal for health-conscious families.'),
('Mixed Nuts Gift Pack', 'Premium assorted nuts including almonds, cashews, and pistachios. Perfect for gifting or snacking.', 499, 100, 5, 2, 'https://m.media-amazon.com/images/I/81kUEmJZ4cL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/71xXCcWaUYL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81XTGA3+MwL._SL1500_.jpg', 'Packed in airtight containers to retain freshness. A healthy snack option for all ages.'),
('Herbal Green Tea', 'Pack of 100 herbal green tea bags. Infused with natural flavors to promote health and wellness.', 350, 40, 6, 2, 'https://m.media-amazon.com/images/I/61ZyBhA8MYL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/71kfnoyVpXL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81TiwN6yBtL._SL1500_.jpg', 'Naturally sourced ingredients. Contains no artificial flavors or additives.'),
('Wireless Earbuds', 'Noise-canceling wireless earbuds with a sleek charging case. Compatible with iOS and Android devices.', 2999, 30, 7, 2, 'https://m.media-amazon.com/images/I/61nFNheIhyL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/71eP4yR7igL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81G3uODjqsL._SL1500_.jpg', 'High-quality sound with active noise cancellation. Comes with customizable ear tips for added comfort.'),
('Motorbike Helmet', 'Full-face motorbike helmet with advanced protection and stylish design. Ideal for long rides.', 2599, 15, 8, 2, 'https://m.media-amazon.com/images/I/71YqEx23HzL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81ObPppV-5L._SL1500_.jpg', 'https://m.media-amazon.com/images/I/81Ha5QKkONL._SL1500_.jpg', 'Tested for durability and safety. Lightweight and comfortable to wear for extended periods.'),
('Silk Saree', 'Elegant traditional silk saree for special occasions. Soft and smooth texture with intricate designs.', 3499, 5, 3, 2, 'https://m.media-amazon.com/images/I/81PbC23wWNL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/91fSeVXPCEL._SL1500_.jpg', 'https://m.media-amazon.com/images/I/91zxSaZcRFL._SL1500_.jpg', 'A perfect blend of tradition and style. Available in multiple colors.');


INSERT INTO Users (Email, PasswordHash, Role, FirstName, LastName, ContactNumber ,IsActive) VALUES
('sellerAccount1@gmail.com', 'seller@1234', 'Seller', 'Seller', 'Test', '1800-168-187',1);

INSERT INTO Sellers(CompanyName, GstNumber, BankAccountNumber, IFSC, UserID) 
VALUES ('Aggarwal Store', '878867545675', '123456789012', 'IFSC1234', 3);
