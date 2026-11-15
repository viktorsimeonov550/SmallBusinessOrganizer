-- SmallBusinessOrganizer: начална версия на базата данни (Етап 1)
-- Target: Microsoft SQL Server
-- Enum колоните се пазят като INT (виж enum-ите в Entities/Enums.cs).

IF DB_ID(N'SmallBusinessOrganizer') IS NULL
    CREATE DATABASE SmallBusinessOrganizer;
GO

USE SmallBusinessOrganizer;
GO

CREATE TABLE Users (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Email         NVARCHAR(256)  NOT NULL,
    PasswordHash  NVARCHAR(512)  NOT NULL,
    FullName      NVARCHAR(200)  NOT NULL,
    Role          INT            NOT NULL,          -- 0 Admin, 1 WarehouseWorker, 2 Manager, 3 Viewer
    IsActive      BIT            NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT 1,
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);

CREATE TABLE Categories (
    Id    INT IDENTITY(1,1) PRIMARY KEY,
    Name  NVARCHAR(100) NOT NULL,
    CONSTRAINT UQ_Categories_Name UNIQUE (Name)
);

CREATE TABLE Products (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Sku             NVARCHAR(50)   NOT NULL,
    Name            NVARCHAR(200)  NOT NULL,
    CategoryId      INT            NOT NULL,
    Unit            NVARCHAR(20)   NOT NULL,
    MinStockLevel   INT            NOT NULL,
    UnitPrice       DECIMAL(18,2)  NOT NULL,
    CONSTRAINT UQ_Products_Sku UNIQUE (Sku),
    CONSTRAINT CK_Product_MinStock CHECK (MinStockLevel >= 0),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE Warehouses (
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Name     NVARCHAR(200) NOT NULL,
    Address  NVARCHAR(300) NOT NULL
);

CREATE TABLE Locations (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    WarehouseId  INT           NOT NULL,
    Code         NVARCHAR(50)  NOT NULL,
    CONSTRAINT UQ_Locations_Code UNIQUE (Code),
    CONSTRAINT FK_Locations_Warehouses FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id)
);

-- Текуща наличност по артикул и локация (съставен ключ).
CREATE TABLE StockLevels (
    ProductId   INT NOT NULL,
    LocationId  INT NOT NULL,
    Quantity    INT NOT NULL,
    CONSTRAINT PK_StockLevels PRIMARY KEY (ProductId, LocationId),
    CONSTRAINT CK_StockLevel_Quantity CHECK (Quantity >= 0),
    CONSTRAINT FK_StockLevels_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_StockLevels_Locations FOREIGN KEY (LocationId) REFERENCES Locations(Id)
);

-- История на движенията на стока.
CREATE TABLE StockMovements (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    ProductId       INT           NOT NULL,
    FromLocationId  INT           NULL,
    ToLocationId    INT           NULL,
    Quantity        INT           NOT NULL,
    Type            INT           NOT NULL,       -- 0 Receive, 1 Issue, 2 Transfer, 3 Adjustment
    Reason          NVARCHAR(300) NOT NULL,
    UserId          INT           NOT NULL,
    CreatedAt       DATETIME2     NOT NULL,
    CONSTRAINT CK_StockMovement_Quantity CHECK (Quantity > 0),
    CONSTRAINT FK_StockMovements_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_StockMovements_FromLocations FOREIGN KEY (FromLocationId) REFERENCES Locations(Id),
    CONSTRAINT FK_StockMovements_ToLocations FOREIGN KEY (ToLocationId) REFERENCES Locations(Id),
    CONSTRAINT FK_StockMovements_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Suppliers (
    Id     INT IDENTITY(1,1) PRIMARY KEY,
    Name   NVARCHAR(200) NOT NULL,
    Email  NVARCHAR(256) NOT NULL
);

CREATE TABLE PurchaseOrders (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId  INT       NOT NULL,
    Status      INT       NOT NULL,                -- 0 New, 1 Confirmed, 2 Received, 3 Cancelled
    CreatedAt   DATETIME2 NOT NULL,
    CONSTRAINT FK_PurchaseOrders_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);

CREATE TABLE PurchaseOrderItems (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    PurchaseOrderId  INT           NOT NULL,
    ProductId        INT           NOT NULL,
    Quantity         INT           NOT NULL,
    UnitCost         DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_PurchaseOrderItems_Orders FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrders(Id),
    CONSTRAINT FK_PurchaseOrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

CREATE TABLE Customers (
    Id     INT IDENTITY(1,1) PRIMARY KEY,
    Name   NVARCHAR(200) NOT NULL,
    Email  NVARCHAR(256) NOT NULL
);

CREATE TABLE SalesOrders (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId  INT       NOT NULL,
    Status      INT       NOT NULL,                -- 0 New, 1 Confirmed, 2 Picked, 3 Shipped, 4 Delivered, 5 Cancelled
    CreatedAt   DATETIME2 NOT NULL,
    CONSTRAINT FK_SalesOrders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE SalesOrderItems (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    SalesOrderId  INT           NOT NULL,
    ProductId     INT           NOT NULL,
    Quantity      INT           NOT NULL,
    UnitPrice     DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_SalesOrderItems_Orders FOREIGN KEY (SalesOrderId) REFERENCES SalesOrders(Id),
    CONSTRAINT FK_SalesOrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO
