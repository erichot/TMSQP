CREATE TABLE [dbo].[BaseWebMenuitem] (
    [NodeID]                   CHAR (5)      NOT NULL,
    [HasChildNode]             BIT           NOT NULL,
    [ParentNodeID]             CHAR (5)      NOT NULL,
    [NodeName]                 NVARCHAR (22) NOT NULL,
    [AspPage]                  VARCHAR (48)  NOT NULL,
    [DataFeather]              VARCHAR (16)  NOT NULL,
    [ItemDescription]          NVARCHAR (64) NULL,
    [ThresholdPermissionValue] SMALLINT      NOT NULL,
    [RoleTypeFlag]             TINYINT       NOT NULL,
    [OrderNo]                  SMALLINT      NOT NULL,
    [Version]                  VARCHAR (8)   NOT NULL,
    [Remark]                   NVARCHAR (32) NULL,
    [InActive]                 BIT           DEFAULT ((0)) NOT NULL,
    [CreateDate]               DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [UpdateDate]               DATETIME2 (7) NULL,
    CONSTRAINT [PK_BaseWebMenuitem] PRIMARY KEY CLUSTERED ([NodeID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_BaseWebMenuitem_ParentNodeID]
    ON [dbo].[BaseWebMenuitem]([ParentNodeID] ASC);

