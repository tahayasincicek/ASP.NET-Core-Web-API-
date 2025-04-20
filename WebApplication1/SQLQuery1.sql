CREATE DATABASE UrunDb;
GO

USE UrunDb;
GO

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Price DECIMAL(18,2),
    Stock INT
);
