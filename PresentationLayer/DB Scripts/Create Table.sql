Create Table UserRoles
(
Id int primary key Identity (1,1),
Name varchar(100)
)
Go

Create Table InstructorTypes
(
Id int primary key Identity (1,1),
Name varchar(100)
)
Go



Create Table Countries
(
Id int primary key Identity (1,1),
Name varchar(100),
TwoCharCountryCode CHAR(2),
ThreeCharCountryCode CHAR(3) 
)
Go

--drop table users
Create Table [Users] 
(
Id int primary key Identity (1,1),
FirstName varchar(150) Not null,
LastName varchar(150) Not null,
Email varchar(150) unique Not null,
IsSendEmailInvite bit,
Phone varchar(20),
RoleId Int Not null,
IsIntructor bit,
InstructorTypeId int,
CompanyName varchar(500),
ExternalId varchar(50),
DateofBirth Datetime,
Gender varchar(6),
CountryId int,
[Password] varchar(100),
IsActive bit,
IsDeleted bit,
CreatedOn Datetime,
CreatedBy int,
UpdatedOn Datetime,
UpdatedBy int,
CONSTRAINT FK_UserRole_User Foreign Key (RoleId) References UserRoles(Id),
CONSTRAINT FK_InstructorType_User Foreign Key (InstructorTypeId) References InstructorTypes(Id),
CONSTRAINT FK_Country_User Foreign Key (CountryId) References Countries(Id),
)
Go

--drop table EmailConfirmation
Create Table EmailConfirmation
(
Id int primary key Identity (1,1),
ActivationLink varchar(500),
EmailSentOn Datetime,
ExpireOn Datetime,
UserId int,
CONSTRAINT FK_User_EmailConfirmation Foreign Key (UserId) References Users(Id),
)