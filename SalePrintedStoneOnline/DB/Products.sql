CREATE TABLE Products (
    ProductId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),      -- e.g. "Printed Stone"
    BasePrice DECIMAL(10,2), -- base price
    IsActive BIT DEFAULT 1
);
