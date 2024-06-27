CREATE TABLE [dbo].[NotificationDetail] (
    [NotificationNo] INT           NOT NULL,
    [ItemNo]         SMALLINT      NOT NULL,
    [EmployeeNo]     SMALLINT      NOT NULL,
    [EmployeeId]     CHAR (12)     NOT NULL,
    [Attachment]     VARCHAR (36)  NOT NULL,
    [Remark]         VARCHAR (64)  NOT NULL,
    [HasNotified]    BIT           NOT NULL,
    [NotifiedDate]   DATETIME2 (7) NULL,
    CONSTRAINT [PK_NotificationDetail] PRIMARY KEY CLUSTERED ([NotificationNo] ASC, [ItemNo] ASC),
    CONSTRAINT [FK_NotificationDetail_NotificationHead_NotificationNo] FOREIGN KEY ([NotificationNo]) REFERENCES [dbo].[NotificationHead] ([NotificationNo]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_NotificationDetail_EmployeeNo]
    ON [dbo].[NotificationDetail]([EmployeeNo] ASC);

