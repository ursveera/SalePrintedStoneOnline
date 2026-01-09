/* ================================
   DATABASE
================================ */
CREATE DATABASE StoneShopDb;
GO
USE StoneShopDb;
GO

/* ================================
   PRODUCTS
================================ */
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(300) NULL,
    BasePrice DECIMAL(10,2) NOT NULL,
    DefaultImageUrl NVARCHAR(300) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

/* ================================
   PRODUCT COLORS (VARIANTS)
================================ */
CREATE TABLE ProductColors (
    ColorId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    ColorName NVARCHAR(50) NOT NULL,
    ExtraPrice DECIMAL(10,2) NOT NULL DEFAULT 0,
    ImageUrl NVARCHAR(300) NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

/* ================================
   PRODUCT IMAGES (OPTIONAL)
================================ */
CREATE TABLE ProductImages (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    ImageUrl NVARCHAR(300) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

/* ================================
   USERS (CUSTOMERS / GUEST)
================================ */
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(300) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive bit
);

ALTER TABLE Users
ADD IsActive BIT;
/* ================================
   CART
================================ */
CREATE TABLE Carts (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    SessionId NVARCHAR(50) NOT NULL,
    UserId INT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

/* ================================
   CART ITEMS
================================ */
CREATE TABLE CartItems (
    CartItemId INT IDENTITY(1,1) PRIMARY KEY,
    CartId INT NOT NULL,
    ProductId INT NOT NULL,
    ColorId INT NOT NULL,
    PrintedLabel NVARCHAR(50) NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CartId) REFERENCES Carts(CartId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (ColorId) REFERENCES ProductColors(ColorId)
);

/* ================================
   ORDERS
================================ */
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    OrderStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

/* ================================
   ORDER ITEMS
================================ */
CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ColorId INT NOT NULL,
    PrintedLabel NVARCHAR(50) NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (ColorId) REFERENCES ProductColors(ColorId)
);

/* ================================
   PAYMENTS
================================ */
CREATE TABLE Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    PaymentMethod NVARCHAR(30) NOT NULL,
    TransactionId NVARCHAR(100) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentStatus NVARCHAR(20) NOT NULL,
    PaidAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
);

/* ================================
   SAMPLE DATA
================================ */
INSERT INTO Products (Name, Description, BasePrice, DefaultImageUrl)
VALUES (
    'Printed Stone',
    'Custom printed stone with personalized message',
    299.00,
    'https://yourcdn.com/images/stone_default.png'
);

INSERT INTO ProductColors (ProductId, ColorName, ExtraPrice, ImageUrl)
VALUES
(1, 'Black', 0, 'https://yourcdn.com/images/stone_black.png'),
(1, 'White', 20, 'https://yourcdn.com/images/stone_white.png'),
(1, 'Gold', 50, 'https://yourcdn.com/images/stone_gold.png');
