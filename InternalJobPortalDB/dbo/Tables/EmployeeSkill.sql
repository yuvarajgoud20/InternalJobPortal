CREATE TABLE [dbo].[EmployeeSkill] (
    [EmployeeID]      CHAR (6) NOT NULL,
    [SkillID]         CHAR (6) NOT NULL,
    [SkillExperience] INT      NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [SkillID] ASC),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]),
    FOREIGN KEY ([SkillID]) REFERENCES [dbo].[Skill] ([SkillID])
);

