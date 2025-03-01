CREATE TABLE [dbo].[JobPost] (
    [PostID]          INT      IDENTITY (1, 1) NOT NULL,
    [JobID]           CHAR (6) NULL,
    [DateOfPosting]   DATETIME NULL,
    [LastDateToApply] DATETIME NULL,
    [NoOfVacancies]   INT      NULL,
    PRIMARY KEY CLUSTERED ([PostID] ASC),
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID])
);

