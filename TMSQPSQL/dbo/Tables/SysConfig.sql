CREATE TABLE [dbo].[SysConfig] (
    [scSysConfigNo] SMALLINT       NOT NULL,
    [scSystemID]    CHAR (16)      NOT NULL,
    [scClass]       CHAR (8)       NOT NULL,
    [scSysValue]    VARCHAR (64)   NOT NULL,
    [scSysNotes]    NVARCHAR (128) NOT NULL,
    [scSysType]     INT            NOT NULL,
    CONSTRAINT [PK_SysConfig] PRIMARY KEY CLUSTERED ([scSysConfigNo] ASC)
);

