CREATE TABLE [dbo].[NotificationRecipient] (
    [NotificationNo] INT           NOT NULL,
    [ItemNo]         SMALLINT      NOT NULL,
    [RecipientNo]    SMALLINT      NOT NULL,
    [RecipientName]  NVARCHAR (52) NOT NULL,
    [Email]          VARCHAR (64)  NOT NULL,
    [HasNotified]    BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_NotificationRecipient] PRIMARY KEY CLUSTERED ([NotificationNo] ASC, [ItemNo] ASC, [RecipientNo] ASC),
    CONSTRAINT [FK_NotificationRecipient_NotificationDetail_NotificationNo_ItemNo] FOREIGN KEY ([NotificationNo], [ItemNo]) REFERENCES [dbo].[NotificationDetail] ([NotificationNo], [ItemNo]) ON DELETE CASCADE
);

