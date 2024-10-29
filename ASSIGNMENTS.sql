-- Drop existing tables if they exist to avoid conflicts
DROP TABLE IF EXISTS Employee_Audit;
DROP TABLE IF EXISTS Employee;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Order_Audit;
DROP TABLE IF EXISTS Stock;
DROP TABLE IF EXISTS Customers;
DROP TABLE IF EXISTS Department;

-- Create Department Table
CREATE TABLE Department (
    ID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Populate Department Table with Test Data
INSERT INTO Department (ID, Name) VALUES
(1, 'Human Resources'),
(2, 'IT'),
(3, 'Sales'),
(4, 'Marketing');

-- Create Employee Table
CREATE TABLE Employee (
    ID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')),
    DOB DATE NOT NULL,
    DeptId INT,
    FOREIGN KEY (DeptId) REFERENCES Department(ID)
);

-- Populate Employee Table with Test Data
INSERT INTO Employee (ID, Name, Gender, DOB, DeptId) VALUES
(1, 'Alice', 'F', '1985-06-15', 1),
(2, 'Bob', 'M', '1990-09-25', 2),
(3, 'Charlie', 'M', '1987-12-05', 3),
(4, 'Diana', 'F', '1992-03-30', 1),
(5, 'Eve', 'F', '1980-01-20', 2);

-- Create Stock Table
CREATE TABLE Stock (
    ProductID INT PRIMARY KEY,
    Quantity INT NOT NULL
);

-- Insert Test Data into Stock Table
INSERT INTO Stock (ProductID, Quantity) VALUES
(1, 100),
(2, 200);

-- Create Customers Table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL
);

-- Insert Test Data into Customers Table
INSERT INTO Customers (CustomerID, CustomerName) VALUES
(1, 'Prateek'),
(2, 'Kunal');

-- Create Orders Table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    ProductID INT,
    OrderQuantity INT,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (ProductID) REFERENCES Stock(ProductID)
);

-- Create Order_Audit Table
CREATE TABLE Order_Audit (
    AuditID INT IDENTITY PRIMARY KEY,
    OrderID INT,
    CustomerID INT,
    OrderDate DATE,
    AuditDate DATETIME DEFAULT GETDATE(),
    AuditInfo VARCHAR(MAX)
);

-- Create Employee_Audit Table
CREATE TABLE Employee_Audit (
    AuditID INT IDENTITY PRIMARY KEY,
    EmployeeID INT,
    ChangeTime DATETIME DEFAULT GETDATE(),
    OldName VARCHAR(100),
    NewName VARCHAR(100),
    OldGender CHAR(1),
    NewGender CHAR(1),
    OldDOB DATE,
    NewDOB DATE,
    OldDeptId INT,
    NewDeptId INT
);

-- Drop procedures if they already exist
IF OBJECT_ID('dbo.UpdateEmployeeDetails', 'P') IS NOT NULL
    DROP PROCEDURE dbo.UpdateEmployeeDetails;

IF OBJECT_ID('dbo.GetEmployeeByGenderAndDept', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GetEmployeeByGenderAndDept;

IF OBJECT_ID('dbo.GetEmployeeCountByGender', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GetEmployeeCountByGender;

-- Procedure to update employee details
CREATE PROCEDURE dbo.UpdateEmployeeDetails
    @EmployeeID INT,
    @Name VARCHAR(255),
    @Gender CHAR(1),
    @DOB DATE,
    @DeptId INT
AS
BEGIN
    UPDATE Employee
    SET 
        Name = @Name,
        Gender = @Gender,
        DOB = @DOB,
        DeptId = @DeptId
    WHERE 
        ID = @EmployeeID;
END;
GO

-- Procedure to get employee information by gender and department
CREATE PROCEDURE dbo.GetEmployeeByGenderAndDept
    @Gender CHAR(1),
    @DeptId INT
AS
BEGIN
    SELECT 
        E.ID AS EmployeeID,
        E.Name AS EmployeeName,
        E.Gender,
        E.DOB,
        D.Name AS DepartmentName
    FROM 
        Employee E
    INNER JOIN 
        Department D ON E.DeptId = D.ID
    WHERE 
        E.Gender = @Gender
        AND E.DeptId = @DeptId;
END;
GO

-- Procedure to get the count of employees based on gender
CREATE PROCEDURE dbo.GetEmployeeCountByGender
    @Gender CHAR(1)
AS
BEGIN
    SELECT 
        COUNT(*) AS EmployeeCount
    FROM 
        Employee
    WHERE 
        Gender = @Gender;
END;
GO

-- Create Trigger to log changes to the Employee table into Employee_Audit table
CREATE TRIGGER LogEmployeeChanges
ON Employee
AFTER UPDATE
AS
BEGIN
    INSERT INTO Employee_Audit (EmployeeID, OldName, NewName, OldGender, NewGender, OldDOB, NewDOB, OldDeptId, NewDeptId)
    SELECT 
        inserted.ID, 
        deleted.Name, 
        inserted.Name, 
        deleted.Gender, 
        inserted.Gender, 
        deleted.DOB, 
        inserted.DOB, 
        deleted.DeptId, 
        inserted.DeptId
    FROM 
        inserted
    INNER JOIN 
        deleted ON inserted.ID = deleted.ID;
END;
GO

-- Create Trigger to update Stock table after a new order is placed
CREATE TRIGGER UpdateStockAfterOrder
ON Orders
AFTER INSERT
AS
BEGIN
    UPDATE Stock
    SET Quantity = Quantity - inserted.OrderQuantity
    FROM inserted
    WHERE Stock.ProductID = inserted.ProductID;
END;
GO

-- Trigger to prevent deletion of a customer with existing orders
CREATE TRIGGER PreventCustomerDeletion
ON Customers
BEFORE DELETE
AS
BEGIN
    DECLARE @order_count INT;
    SELECT @order_count = COUNT(*) FROM Orders WHERE CustomerID IN (SELECT CustomerID FROM deleted);

    IF @order_count > 0
    BEGIN
        RAISERROR('Cannot delete customer with existing orders', 16, 1);
        ROLLBACK;
    END
END;
GO

-- Create Trigger to log Order changes in Order_Audit table
CREATE TRIGGER trgAfterInsertOrder
ON Orders
AFTER INSERT
AS
BEGIN
    INSERT INTO Order_Audit (OrderID, CustomerID, OrderDate, AuditInfo)
    SELECT OrderID, CustomerID, OrderDate, 'Order Inserted'
    FROM inserted;
END;
GO

-- Insert Test Data into Orders Table
INSERT INTO Orders (OrderID, CustomerID, OrderDate, ProductID, OrderQuantity) VALUES
(1, 1, '2024-10-01', 1, 5),
(2, 2, '2024-10-02', 2, 3);

-- Example of updating an employee to test logging
UPDATE Employee SET Name = 'Alice Johnson', Gender = 'F', DOB = '1990-05-10', DeptId = 2 WHERE ID = 1;

-- Select data for verification
SELECT * FROM Stock;
SELECT * FROM Orders;
SELECT * FROM Customers;
SELECT * FROM Employee;
SELECT * FROM Employee_Audit;
SELECT * FROM Order_Audit;
