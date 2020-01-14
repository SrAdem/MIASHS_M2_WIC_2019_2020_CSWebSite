CREATE TABLE [dbo].[Item] (
    [id_item]     INT          IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [description] TEXT         NULL,
    [available]   BIT          DEFAULT ((0)) NOT NULL,
    [price]       FLOAT (53)   NOT NULL,
    [picture]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_item] ASC)
);

CREATE TABLE [dbo].[Category] (
    [id_category]    INT          IDENTITY (1, 1) NOT NULL,
    [title]          VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_category] ASC)
);

CREATE TABLE [dbo].[MasterCategory] (
    [id_mastercategory] INT          IDENTITY (1, 1) NOT NULL,
    [title]             VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_mastercategory] ASC)
);

CREATE TABLE [dbo].[CategoriesLinks] (
    [id_mastercategory] INT NOT NULL,
    [id_subcategory]    INT NOT NULL,
    CONSTRAINT [PK_CategoriesLink] PRIMARY KEY CLUSTERED ([id_mastercategory] ASC, [id_subcategory] ASC),
    CONSTRAINT [FK_Mastercategory_ID] FOREIGN KEY ([id_mastercategory]) REFERENCES [dbo].[MasterCategory] ([id_mastercategory]),
    CONSTRAINT [FK_Subcategory_ID] FOREIGN KEY ([id_subcategory]) REFERENCES [dbo].[Category] ([id_category])
);

CREATE TABLE [dbo].[ItemCategory] (
    [id_item]     INT NOT NULL,
    [id_category] INT NOT NULL,
    CONSTRAINT [PK_item_Category] PRIMARY KEY CLUSTERED ([id_item] ASC, [id_category] ASC),
    CONSTRAINT [FK_Item_identifier] FOREIGN KEY ([id_item]) REFERENCES [dbo].[Item] ([id_item]),
    CONSTRAINT [FK_Item_Category] FOREIGN KEY ([id_category]) REFERENCES [dbo].[Category] ([id_category])
);


CREATE TABLE [dbo].[Address] (
    [id_address] INT          IDENTITY (1, 1) NOT NULL,
    [number]     VARCHAR (10) NOT NULL,
    [title]      VARCHAR (50) NOT NULL,
    [city]       VARCHAR (50) NOT NULL,
    [postcode]   INT          NOT NULL,
    [country]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_address] ASC)
);

CREATE TABLE [dbo].[Users] (
    [id_user]    INT          IDENTITY (1, 1) NOT NULL,
    [id_address] INT          NULL,
    [first_name] VARCHAR (50) NOT NULL,
    [last_name]  VARCHAR (50) NOT NULL,
    [email]      VARCHAR (50) NOT NULL,
    [pass_word]  VARCHAR (50) NOT NULL,
    [superUser]  BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_user] ASC),
    CONSTRAINT [FK_User_Address] FOREIGN KEY ([id_address]) REFERENCES [dbo].[Address] ([id_address])
);

CREATE TABLE [dbo].[Order] (
    [id_order]  INT  IDENTITY (1, 1) NOT NULL,
    [id_user]   INT  NOT NULL,
    [date]      DATE NOT NULL,
    [delivered] BIT  NOT NULL,
    PRIMARY KEY CLUSTERED ([id_order] ASC),
    CONSTRAINT [FK_Order_Userid] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([id_user])
);

CREATE TABLE [dbo].[OrderItems] (
    [id_order] INT NOT NULL,
    [id_item]  INT NOT NULL,
    [quantity] INT NOT NULL,
    CONSTRAINT [PK_Order_Items] PRIMARY KEY CLUSTERED ([id_order] ASC, [id_item] ASC),
    CONSTRAINT [FK_Order_Item] FOREIGN KEY ([id_item]) REFERENCES [dbo].[Item] ([id_item]),
    CONSTRAINT [FK_Order_Order] FOREIGN KEY ([id_order]) REFERENCES [dbo].[Order] ([id_order])
);
