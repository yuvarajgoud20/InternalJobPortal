USE InternalJobPortalDB;

CREATE TABLE Job(
	JobID CHAR(6) PRIMARY KEY,
	JobTitle VARCHAR(20),
	JobDescription VARCHAR(300),
	Salary MONEY
);

CREATE TABLE Employee(
	EmployeeID CHAR(6) PRIMARY KEY,
	EmployeeName VARCHAR(30),
	EmailID VARCHAR(20),
	PhoneNo CHAR(10),
	TotalExperience INT,
	JobID CHAR(6) REFERENCES Job(JobID)
);

CREATE TABLE Skill(
	SkillID CHAR(6) PRIMARY KEY,
	SkillName VARCHAR(20),
	SkillLevel CHAR(1)
);

CREATE TABLE JobSkill(
	JobID CHAR(6) REFERENCES Job(JobID),
	SkillID CHAR(6) REFERENCES Skill(SkillID),
	Experience INT,
	PRIMARY KEY(JobID,SkillID)
);

CREATE TABLE EmployeeSkill(
	EmployeeID CHAR(6) REFERENCES Employee(EmployeeID),
	SkillID CHAR(6) REFERENCES Skill(SkillID),
	SkillExperience INT,
	PRIMARY KEY(EmployeeID,SkillID)
);

CREATE TABLE JobPost(
	PostID INT IDENTITY PRIMARY KEY,
	JobID CHAR(6) REFERENCES Job(JobID),
	DateOfPosting DATETIME,
	LastDateToApply DATETIME,
	NoOfVacancies INT
)

CREATE TABLE ApplyJob(
	PostID INT REFERENCES JobPost(PostID),
	EmployeeID CHAR(6) REFERENCES Employee(EmployeeID),
	AppliedDate DATETIME,
	ApplicationStatus CHAR(1),
	PRIMARY KEY(PostID,EmployeeID)
);