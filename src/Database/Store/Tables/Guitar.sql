CREATE TABLE [Store].[Guitar] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Type] VARCHAR (50)     NULL,
    [ts]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Guitar] PRIMARY KEY CLUSTERED ([Id] ASC)
);



