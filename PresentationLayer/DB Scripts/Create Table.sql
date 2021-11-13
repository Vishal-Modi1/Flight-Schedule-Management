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

 
--drop table [dbo].[Statuses]
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




INSERT INTO [dbo].[Statuses] ([Name],[IsActive])
VALUES('Installed',1),('Repaired',1),('Removed',1)

--drop table [dbo].[Classifications]
 CREATE TABLE [dbo].[Classifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Classifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Classifications]([Name],[IsActive])
VALUES('Aircraft',1),('Engine',1),('Propeller',1),('Appliance',1),('Avionics',1)
 
--drop table [dbo].[AirCraftEquipments]
CREATE TABLE [dbo].[AirCraftEquipments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NOT NULL,
	[AirCraftId] [int] NOT NULL,
	[ClassificationId] [int] NOT NULL,
	[Item] [nvarchar](500) NOT NULL,
	[Model] [nvarchar](500) NULL,
	[Make] [nvarchar](500) NULL,
	[Manufacturer] [nvarchar](500) NULL,
	[PartNumber] [nvarchar](500) NULL,
	[SerialNumber] [nvarchar](500) NULL,
	[ManufacturerDate] [datetime2](7) NULL,
	[LogEntryDate] [datetime2](7) NULL,
	[AircraftTTInstall] [int] NULL,
	[PartTTInstall] [int] NULL,
	[Notes] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [int] NULL,
	[DeletedOn] [datetime2](7) NULL,
	[DeletedBy] [int] NULL,
	[UpdatedOn] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_AirCraftEquipments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AirCraftEquipments] ADD  CONSTRAINT [DF_AirCraftEquipments_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[AirCraftEquipments]  WITH CHECK ADD  CONSTRAINT [FK_AirCraftEquipments_Classifications] FOREIGN KEY([ClassificationId])
REFERENCES [dbo].[Classifications] ([Id])
GO

ALTER TABLE [dbo].[AirCraftEquipments] CHECK CONSTRAINT [FK_AirCraftEquipments_Classifications]
GO

ALTER TABLE [dbo].[AirCraftEquipments]  WITH CHECK ADD  CONSTRAINT [FK_AirCraftEquipments_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO

ALTER TABLE [dbo].[AirCraftEquipments] CHECK CONSTRAINT [FK_AirCraftEquipments_Statuses]
GO