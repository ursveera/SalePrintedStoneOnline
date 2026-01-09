CREATE TABLE Orders (
    OrderId INT IDENTITY PRIMARY KEY,
    UserId INT,
    ProductId INT,
    ColorId INT,
    PrintedLabel NVARCHAR(50), -- text printed on stone
    Quantity INT DEFAULT 1,
    TotalPrice DECIMAL(10,2),
    OrderStatus NVARCHAR(20),  -- Pending, Paid, Shipped
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (ColorId) REFERENCES ProductColors(ColorId)
);
