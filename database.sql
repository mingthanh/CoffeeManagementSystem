Create database QuanLyQuanCafe
go

use QuanLyQuanCafe
go

--Food
--Table
--FoodCategory
--Account
--Bill
--BillInfo

CREATE TABLE TableFood
(
	id INT PRIMARY KEY,
	name NVARCHAR(100),  
	status NVARCHAR(100) 	--Trống|| Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CS_AS PRIMARY KEY ,
	DisplayName NVARCHAR(100),
	PassWord NVARCHAR(1000) COLLATE SQL_Latin1_General_CP1_CS_AS ,
	Type INT  --1 admin 0:staff
)
GO

CREATE TABLE FoodCategory
(
	id INT PRIMARY KEY,
	name NVARCHAR(100) 
)
GO

CREATE TABLE Food
(
	id INT PRIMARY KEY,
	name NVARCHAR(100) ,
	idCategory INT ,
	price FLOAT ,

	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE BILL
(
	id INT PRIMARY KEY IDENTITY(1, 1),
	DateCheckIn DATE, 
	DateCheckOut DATE,
	idTable INT ,
	status int  --1: đã thanh toán, 0: chưa thanh toán
	
	FOREIGN KEY(idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT PRIMARY KEY IDENTITY(1, 1),
	idBill INT,
	idFood INT ,
	count INT ,

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

DROP TABLE FOOD
DROP TABLE FOODCATEGORY
DROP TABLE BillInfo
DROP TABLE BILL
DROP TABLE TableFood

INSERT INTO FOODCATEGORY VALUES (1,N'Rice')
Select * from Bill
Select * from Billinfo
Select * from TableFood
DELETE FROM BILL WHERE id = 0;
Select * from food

INSERT INTO Account VALUES (N'ngoc20022607@gmail.com',N'Nguyen',123,1)
INSERT INTO FoodCategory VALUES (2,N'Wine')
INSERT INTO FoodCategory VALUES (3,N'Coffee')


CREATE TABLE Customers (
	
    CustomerGmail nvarchar(100) COLLATE SQL_Latin1_General_CP1_CS_AS Primary Key,
    FullName NVARCHAR(100) NOT NULL,
	Password NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
	Status BIT NOT NULL DEFAULT 1,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    Points INT NOT NULL DEFAULT 0, 
);

drop table Customers
Insert into Customers values (1,Taylor@gmail.com,N'Taylor Swift',N'123','0123456789') 
INSERT INTO Customers (CustomerGmail, FullName, Password, PhoneNumber)
VALUES (N'customer@example.com', N'Nguyễn Văn A', N'123', N'0912345678')
INSERT INTO Customers (CustomerGmail, FullName, Password, PhoneNumber)
VALUES (N'Taylor@gmail.com',N'Taylor Swift',N'123',N'0123456789') 
select* from customers

ALTER TABLE Customers
ADD ID INT Primary Key;

ALTER TABLE Customers
DROP CONSTRAINT CustomerGmail;

ALTER TABLE ForbiddenAdmins
ADD ID INT IDENTITY(1,1) NOT NULL;

ALTER TABLE ForbiddenAdmins
ADD CONSTRAINT PK_ForbiddenAdmins_ID PRIMARY KEY (ID);