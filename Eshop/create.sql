
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8C148BEB7B03A54C]') AND parent_object_id = OBJECT_ID('address'))
alter table address  drop constraint FK8C148BEB7B03A54C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6482B44BD1CE6EA]') AND parent_object_id = OBJECT_ID('category'))
alter table category  drop constraint FK6482B44BD1CE6EA


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE44CC044ECFE2B50]') AND parent_object_id = OBJECT_ID('category_product'))
alter table category_product  drop constraint FKE44CC044ECFE2B50


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE44CC0448B5A8603]') AND parent_object_id = OBJECT_ID('category_product'))
alter table category_product  drop constraint FKE44CC0448B5A8603


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK30EBA8FFF33A266A]') AND parent_object_id = OBJECT_ID('image'))
alter table image  drop constraint FK30EBA8FFF33A266A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD7390C657B03A54C]') AND parent_object_id = OBJECT_ID('[order]'))
alter table [order]  drop constraint FKD7390C657B03A54C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD7390C65AC24DD13]') AND parent_object_id = OBJECT_ID('[order]'))
alter table [order]  drop constraint FKD7390C65AC24DD13


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD7390C65727F4899]') AND parent_object_id = OBJECT_ID('[order]'))
alter table [order]  drop constraint FKD7390C65727F4899


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2DCFAA6F179C83DF]') AND parent_object_id = OBJECT_ID('order_item'))
alter table order_item  drop constraint FK2DCFAA6F179C83DF


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2DCFAA6FECFE2B50]') AND parent_object_id = OBJECT_ID('order_item'))
alter table order_item  drop constraint FK2DCFAA6FECFE2B50


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5E47AA97F33A266A]') AND parent_object_id = OBJECT_ID('page'))
alter table page  drop constraint FK5E47AA97F33A266A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1F94D48AF33A266A]') AND parent_object_id = OBJECT_ID('product'))
alter table product  drop constraint FK1F94D48AF33A266A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5E3BB4B37B03A54C]') AND parent_object_id = OBJECT_ID('user_role'))
alter table user_role  drop constraint FK5E3BB4B37B03A54C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5E3BB4B3245A2EF5]') AND parent_object_id = OBJECT_ID('user_role'))
alter table user_role  drop constraint FK5E3BB4B3245A2EF5


    if exists (select * from dbo.sysobjects where id = object_id(N'address') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table address

    if exists (select * from dbo.sysobjects where id = object_id(N'category') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table category

    if exists (select * from dbo.sysobjects where id = object_id(N'category_product') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table category_product

    if exists (select * from dbo.sysobjects where id = object_id(N'gallery') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table gallery

    if exists (select * from dbo.sysobjects where id = object_id(N'image') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table image

    if exists (select * from dbo.sysobjects where id = object_id(N'[order]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [order]

    if exists (select * from dbo.sysobjects where id = object_id(N'order_item') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table order_item

    if exists (select * from dbo.sysobjects where id = object_id(N'page') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table page

    if exists (select * from dbo.sysobjects where id = object_id(N'product') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table product

    if exists (select * from dbo.sysobjects where id = object_id(N'role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table role

    if exists (select * from dbo.sysobjects where id = object_id(N'user_role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table user_role

    if exists (select * from dbo.sysobjects where id = object_id(N'[user]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [user]

    create table address (
        id INT IDENTITY NOT NULL,
       [type] INT null,
       [primary] BIT null,
       user_id INT null,
       created DATETIME null,
       name NVARCHAR(255) null,
       surname NVARCHAR(255) null,
       street NVARCHAR(255) null,
       street_code NVARCHAR(255) null,
       city NVARCHAR(255) null,
       zip_code NVARCHAR(255) null,
       primary key (id)
    )

    create table category (
        id INT IDENTITY NOT NULL,
       parent_id INT null,
       name NVARCHAR(255) null,
       description NVARCHAR(255) null,
       primary key (id)
    )

    create table category_product (
        category_id INT not null,
       product_id INT not null,
       primary key (product_id, category_id)
    )

    create table gallery (
        id INT IDENTITY NOT NULL,
       primary key (id)
    )

    create table image (
        id INT IDENTITY NOT NULL,
       name NVARCHAR(255) null,
       description NVARCHAR(255) null,
       visible BIT null,
       gallery_id INT null,
       primary key (id)
    )

    create table [order] (
        id INT IDENTITY NOT NULL,
       user_id INT null,
       name DATETIME null,
       description DATETIME null,
       order_state INT null,
       delivery_address INT null,
       billing_address INT null,
       shipping_cost FLOAT(53) null,
       primary key (id)
    )

    create table order_item (
        id INT IDENTITY NOT NULL,
       order_id INT null,
       name NVARCHAR(255) null,
       price FLOAT(53) null,
       amount INT null,
       product_id INT null,
       primary key (id)
    )

    create table page (
        id INT IDENTITY NOT NULL,
       name NVARCHAR(255) null,
       content NVARCHAR(255) null,
       gallery_id INT null,
       primary key (id)
    )

    create table product (
        id INT IDENTITY NOT NULL,
       name NVARCHAR(255) null,
       description NVARCHAR(255) null,
       full_description NVARCHAR(255) null,
       price FLOAT(53) null,
       visible BIT null,
       highlighted BIT null,
       gallery_id INT null,
       stock_quantity INT null,
       stock_status INT null,
       primary key (id)
    )

    create table role (
        id INT IDENTITY NOT NULL,
       identificator NVARCHAR(255) null,
       description NVARCHAR(255) null,
       primary key (id)
    )

    create table user_role (
        id INT not null,
       user_id INT not null,
       role_id INT IDENTITY NOT NULL,
       primary key (user_id, role_id)
    )

    create table [user] (
        id INT IDENTITY NOT NULL,
       email NVARCHAR(255) null,
       password NVARCHAR(255) null,
       name NVARCHAR(255) null,
       surname NVARCHAR(255) null,
       primary key (id)
    )

    alter table address 
        add constraint FK8C148BEB7B03A54C 
        foreign key (user_id) 
        references [user]

    alter table category 
        add constraint FK6482B44BD1CE6EA 
        foreign key (parent_id) 
        references category

    alter table category_product 
        add constraint FKE44CC044ECFE2B50 
        foreign key (product_id) 
        references product

    alter table category_product 
        add constraint FKE44CC0448B5A8603 
        foreign key (category_id) 
        references category

    alter table image 
        add constraint FK30EBA8FFF33A266A 
        foreign key (gallery_id) 
        references gallery

    alter table [order] 
        add constraint FKD7390C657B03A54C 
        foreign key (user_id) 
        references [user]

    alter table [order] 
        add constraint FKD7390C65AC24DD13 
        foreign key (delivery_address) 
        references address

    alter table [order] 
        add constraint FKD7390C65727F4899 
        foreign key (billing_address) 
        references address

    alter table order_item 
        add constraint FK2DCFAA6F179C83DF 
        foreign key (order_id) 
        references [order]

    alter table order_item 
        add constraint FK2DCFAA6FECFE2B50 
        foreign key (product_id) 
        references product

    alter table page 
        add constraint FK5E47AA97F33A266A 
        foreign key (gallery_id) 
        references gallery

    alter table product 
        add constraint FK1F94D48AF33A266A 
        foreign key (gallery_id) 
        references gallery

    alter table user_role 
        add constraint FK5E3BB4B37B03A54C 
        foreign key (user_id) 
        references [user]

    alter table user_role 
        add constraint FK5E3BB4B3245A2EF5 
        foreign key (role_id) 
        references role
