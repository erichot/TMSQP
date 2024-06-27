CREATE TABLE [dbo].[NotificationHead] (
    [NotificationNo] INT            IDENTITY (1, 1) NOT NULL,
    [PINo]           SMALLINT       NOT NULL,
    [HeaderNo]       SMALLINT       NOT NULL,
    [SenderAddress]  VARCHAR (64)   NOT NULL,
    [SenderName]     NVARCHAR (52)  NOT NULL,
    [Subject]        NVARCHAR (80)  NOT NULL,
    [MailContent]    NVARCHAR (MAX) NOT NULL,
    [CreatePerson]   SMALLINT       NOT NULL,
    [CreateDate]     DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [HasNotified]    BIT            DEFAULT ((0)) NOT NULL,
    [NotifiedDate]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_NotificationHead] PRIMARY KEY CLUSTERED ([NotificationNo] ASC),
    CONSTRAINT [FK_NotificationHead_PayslipImport_PINo] FOREIGN KEY ([PINo]) REFERENCES [dbo].[PayslipImport] ([PINo]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_NotificationHead_PINo]
    ON [dbo].[NotificationHead]([PINo] ASC);

