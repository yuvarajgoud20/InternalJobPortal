CREATE TABLE [dbo].[Employee] (
    [EmployeeID]      CHAR (6)     NOT NULL,
    [EmployeeName]    VARCHAR (30) NULL,
    [EmailID]         VARCHAR (40) NULL,
    [PhoneNo]         CHAR (10)    NULL,
    [TotalExperience] INT          NULL,
    [JobID]           CHAR (6)     NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID])
);

