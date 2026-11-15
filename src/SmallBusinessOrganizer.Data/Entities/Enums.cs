namespace SmallBusinessOrganizer.Data.Entities;

public enum UserRole { Admin, WarehouseWorker, Manager, Viewer }

public enum StockMovementType { Receive, Issue, Transfer, Adjustment }

public enum PurchaseOrderStatus { New, Confirmed, Received, Cancelled }

public enum SalesOrderStatus { New, Confirmed, Picked, Shipped, Delivered, Cancelled }
