CREATE TABLE [dbo].[PayslipImport] (
    [PINo]              SMALLINT      IDENTITY (1, 1) NOT NULL,
    [SalaryYearMonth]   DATE          NOT NULL,
    [SalaryYear]        AS            (CONVERT([smallint],datepart(year,[SalaryYearMonth]))),
    [SalaryMonth]       AS            (CONVERT([tinyint],datepart(month,[SalaryYearMonth]))),
    [NumberOfEmployees] INT           NOT NULL,
    [HasImported]       BIT           DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT           DEFAULT ((0)) NOT NULL,
    [Remark]            NVARCHAR (80) NOT NULL,
    [CreatedDate]       DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [ImportedDate]      DATETIME2 (7) NULL,
    CONSTRAINT [PK_PayslipImport] PRIMARY KEY CLUSTERED ([PINo] ASC)
);

