namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Primary = c.Boolean(nullable: false),
                        UserId = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Street = c.String(),
                        StreedCode = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                        TotalLogins = c.Int(nullable: false),
                        Phone = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identificator = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FullDescription = c.String(),
                        Price = c.Double(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Highlighted = c.Boolean(nullable: false),
                        GalleryId = c.Int(),
                        FileStorageId = c.Int(),
                        StockQuantity = c.Int(nullable: false),
                        StockStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileStorages", t => t.FileStorageId)
                .ForeignKey("dbo.Galleries", t => t.GalleryId)
                .Index(t => t.GalleryId)
                .Index(t => t.FileStorageId);
            
            CreateTable(
                "dbo.FileStorages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContentType = c.String(),
                        Description = c.String(),
                        Visible = c.Boolean(nullable: false),
                        FileStorageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileStorages", t => t.FileStorageId)
                .Index(t => t.FileStorageId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Visible = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        RealWidth = c.Int(nullable: false),
                        RealHeight = c.Int(nullable: false),
                        ContentType = c.String(),
                        GalleryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.GalleryId, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.GalleryId);
            
            CreateTable(
                "dbo.DeliveryPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        Created = c.DateTime(),
                        Completed = c.DateTime(),
                        OrderState = c.Int(nullable: false),
                        DeliveryPrice = c.Int(nullable: false),
                        PaymentPrice = c.Int(nullable: false),
                        BillingAddress_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Delivery_Id = c.Int(),
                        DeliveryAddress_Id = c.Int(),
                        Payment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.BillingAddress_Id)
                .ForeignKey("dbo.Users", t => t.Customer_Id)
                .ForeignKey("dbo.DeliveryPayments", t => t.Delivery_Id)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddress_Id)
                .ForeignKey("dbo.DeliveryPayments", t => t.Payment_Id)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Delivery_Id)
                .Index(t => t.DeliveryAddress_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.OrderHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Message = c.String(),
                        OrderState = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ChangedBy_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ChangedBy_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.ChangedBy_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identificator = c.String(),
                        Name = c.String(),
                        Content = c.String(),
                        GalleryId = c.Int(),
                        FileStorageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileStorages", t => t.FileStorageId)
                .ForeignKey("dbo.Galleries", t => t.GalleryId)
                .Index(t => t.GalleryId)
                .Index(t => t.FileStorageId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.Pages", "FileStorageId", "dbo.FileStorages");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "Payment_Id", "dbo.DeliveryPayments");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderHistories", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderHistories", "ChangedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "DeliveryAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "Delivery_Id", "dbo.DeliveryPayments");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "BillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Products", "GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.Images", "ParentId", "dbo.Images");
            DropForeignKey("dbo.Images", "GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.Products", "FileStorageId", "dbo.FileStorages");
            DropForeignKey("dbo.Files", "FileStorageId", "dbo.FileStorages");
            DropForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.ProductCategories", new[] { "Category_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.Pages", new[] { "FileStorageId" });
            DropIndex("dbo.Pages", new[] { "GalleryId" });
            DropIndex("dbo.OrderHistories", new[] { "Order_Id" });
            DropIndex("dbo.OrderHistories", new[] { "ChangedBy_Id" });
            DropIndex("dbo.Orders", new[] { "Payment_Id" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.Orders", new[] { "Delivery_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "BillingAddress_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.Images", new[] { "GalleryId" });
            DropIndex("dbo.Images", new[] { "ParentId" });
            DropIndex("dbo.Files", new[] { "FileStorageId" });
            DropIndex("dbo.Products", new[] { "FileStorageId" });
            DropIndex("dbo.Products", new[] { "GalleryId" });
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.ProductCategories");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Pages");
            DropTable("dbo.OrderHistories");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.DeliveryPayments");
            DropTable("dbo.Images");
            DropTable("dbo.Galleries");
            DropTable("dbo.Files");
            DropTable("dbo.FileStorages");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
        }
    }
}
