CREATE TABLE ProductColors (
    ColorId INT IDENTITY PRIMARY KEY,
    ProductId INT,
    ColorName NVARCHAR(50),  -- Red, Black, White
    ExtraPrice DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
