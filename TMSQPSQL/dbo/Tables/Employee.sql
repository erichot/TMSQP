CREATE TABLE [dbo].[Employee] (
    [EmployeeNo]   SMALLINT      IDENTITY (1, 1) NOT NULL,
    [EmployeeId]   CHAR (16)     NULL,
    [EmployeeName] VARCHAR (52)  NULL,
    [Email]        VARCHAR (64)  NOT NULL,
    [Remark]       NVARCHAR (40) NULL,
    [CreatePerson] SMALLINT      NOT NULL,
    [CreateDate]   DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [UpdatePerson] SMALLINT      NULL,
    [UpdateDate]   DATETIME2 (7) NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeNo] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_EmployeeId]
    ON [dbo].[Employee]([EmployeeId] ASC) WHERE ([EmployeeId] IS NOT NULL);

