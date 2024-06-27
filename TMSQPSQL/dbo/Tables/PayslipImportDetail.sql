CREATE TABLE [dbo].[PayslipImportDetail] (
    [PINo]         SMALLINT       NOT NULL,
    [HeaderNo]     SMALLINT       NOT NULL,
    [DetailNo]     SMALLINT       NOT NULL,
    [DetailTypeNo] TINYINT        NOT NULL,
    [RowNo]        INT            NULL,
    [ItemName]     VARCHAR (48)   NULL,
    [Amount]       DECIMAL (8, 2) NULL,
    CONSTRAINT [PK_PayslipImportDetail] PRIMARY KEY CLUSTERED ([PINo] ASC, [HeaderNo] ASC, [DetailNo] ASC, [DetailTypeNo] ASC),
    CONSTRAINT [FK_PayslipImportDetail_PayslipImportHead_PINo_HeaderNo] FOREIGN KEY ([PINo], [HeaderNo]) REFERENCES [dbo].[PayslipImportHead] ([PINo], [HeaderNo]) ON DELETE CASCADE
);

