CREATE TABLE [dbo].[JobSkill] (
    [JobID]      CHAR (6) NOT NULL,
    [SkillID]    CHAR (6) NOT NULL,
    [Experience] INT      NULL,
    PRIMARY KEY CLUSTERED ([JobID] ASC, [SkillID] ASC),
    FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job] ([JobID]),
    FOREIGN KEY ([SkillID]) REFERENCES [dbo].[Skill] ([SkillID])
);

