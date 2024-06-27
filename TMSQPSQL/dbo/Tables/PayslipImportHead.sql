CREATE TABLE [dbo].[PayslipImportHead] (
    [PINo]              SMALLINT        NOT NULL,
    [HeaderNo]          SMALLINT        NOT NULL,
    [RowNo]             INT             NULL,
    [Period]            VARCHAR (16)    NULL,
    [EmployeeName]      VARCHAR (52)    NULL,
    [EmployeeId]        CHAR (16)       NULL,
    [EmployeeNo]        SMALLINT        NULL,
    [DepartmentName]    VARCHAR (48)    NULL,
    [PaymentMethod]     VARCHAR (16)    NULL,
    [BankAccount]       VARCHAR (32)    NULL,
    [FooterRowNo]       INT             NULL,
    [IncomeSubTotal]    DECIMAL (18, 2) NULL,
    [DeductionSubTotal] DECIMAL (18, 2) NULL,
    [EPFAmount]         DECIMAL (18, 2) NULL,
    [SOCSOAmount]       DECIMAL (18, 2) NULL,
    [EISAmount]         DECIMAL (18, 2) NULL,
    [NettPay]           DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_PayslipImportHead] PRIMARY KEY CLUSTERED ([PINo] ASC, [HeaderNo] ASC),
    CONSTRAINT [FK_PayslipImportHead_Employee_EmployeeNo] FOREIGN KEY ([EmployeeNo]) REFERENCES [dbo].[Employee] ([EmployeeNo]),
    CONSTRAINT [FK_PayslipImportHead_PayslipImport_PINo] FOREIGN KEY ([PINo]) REFERENCES [dbo].[PayslipImport] ([PINo]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PayslipImportHead_EmployeeNo]
    ON [dbo].[PayslipImportHead]([EmployeeNo] ASC);

