CREATE TABLE [dbo].[Job] (
    [JobID]          CHAR (6)      NOT NULL,
    [JobTitle]       VARCHAR (40)  NULL,
    [JobDescription] VARCHAR (300) NULL,
    [Salary]         MONEY         NULL,
    PRIMARY KEY CLUSTERED ([JobID] ASC)
);

