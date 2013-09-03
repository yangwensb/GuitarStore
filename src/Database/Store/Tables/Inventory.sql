CREATE TABLE [Store].[Inventory] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [TypeId]   UNIQUEIDENTIFIER NULL,
    [Builder]  VARCHAR (50)     NULL,
    [Model]    VARCHAR (80)     NULL,
    [QOH]      INT              NOT NULL,
    [Cost]     DECIMAL (10, 2)  NOT NULL,
    [Price]    DECIMAL (10, 2)  NOT NULL,
    [Received] DATETIME         NULL,
    [ts]       ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inventory_Guitar] FOREIGN KEY ([TypeId]) REFERENCES [Store].[Guitar] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



