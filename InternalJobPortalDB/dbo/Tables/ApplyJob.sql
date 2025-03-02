CREATE TABLE [dbo].[ApplyJob] (
    [PostID]            INT      NOT NULL,
    [EmployeeID]        CHAR (6) NOT NULL,
    [AppliedDate]       DATETIME NULL,
    [ApplicationStatus] CHAR (1) NULL,
    PRIMARY KEY CLUSTERED ([PostID] ASC, [EmployeeID] ASC),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]),
    FOREIGN KEY ([PostID]) REFERENCES [dbo].[JobPost] ([PostID]), 
    CONSTRAINT [ApplicationStatus] CHECK (ApplicationStatus IN ('P','A','R'))
);

