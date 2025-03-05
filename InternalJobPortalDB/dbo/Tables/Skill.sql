CREATE TABLE [dbo].[Skill] (
    [SkillID]    CHAR (6)     NOT NULL,
    [SkillName]  VARCHAR (40) NULL,
    [SkillLevel] CHAR (1) NULL, 
    PRIMARY KEY CLUSTERED ([SkillID] ASC), 
    CONSTRAINT [CK_SkillLevel_Column] CHECK (SkillLevel IN ('B','I','A')),
);

