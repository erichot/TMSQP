CREATE TABLE [dbo].[BaseWebPage] (
    [PageId]         CHAR (7)      NOT NULL,
    [SubFolder1]     VARCHAR (16)  NOT NULL,
    [SubFolder2]     VARCHAR (24)  NOT NULL,
    [PageName]       VARCHAR (32)  NOT NULL,
    [MenuitemNodeId] CHAR (5)      NULL,
    [ParentPageId]   CHAR (7)      NULL,
    [PageTitle]      NVARCHAR (32) NULL,
    [PageDescripion] NVARCHAR (32) NOT NULL,
    CONSTRAINT [PK_BaseWebPage] PRIMARY KEY CLUSTERED ([PageId] ASC),
    CONSTRAINT [FK_BaseWebPage_BaseWebMenuitem_MenuitemNodeId] FOREIGN KEY ([MenuitemNodeId]) REFERENCES [dbo].[BaseWebMenuitem] ([NodeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_BaseWebPage_MenuitemNodeId]
    ON [dbo].[BaseWebPage]([MenuitemNodeId] ASC);

