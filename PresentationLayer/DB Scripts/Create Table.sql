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

--drop table [dbo].[Aircrafts]
CREATE TABLE [dbo].[Aircrafts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [varchar](200) NULL,
	[TailNo] [varchar](30) NOT NULL,
	[Year] [varchar](4) NULL,
	[AircraftMakeId] [int] NOT NULL,
	[AircraftModelId] [int] NOT NULL,
	[AircraftCategoryId] [int] NOT NULL,
	[AircraftClassId] [int] NULL,
	[FlightSimulatorClassId] [int] NULL,
	[NoofEngines] [int] NULL,
	[NoofPropellers] [int] NULL,
	[IsEngineshavePropellers] [bit] NOT NULL,
	[IsEnginesareTurbines] [bit] NOT NULL,
	[TrackOilandFuel] [bit] NOT NULL,
	[IsIdentifyMeterMismatch] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [PK__Aircraft__3214EC07B0337CE8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF_Aircrafts_NoofPropellers]  DEFAULT ((0)) FOR [NoofPropellers]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__IsEng__38996AB5]  DEFAULT ((0)) FOR [IsEngineshavePropellers]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__IsEng__398D8EEE]  DEFAULT ((0)) FOR [IsEnginesareTurbines]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__Track__3A81B327]  DEFAULT ((0)) FOR [TrackOilandFuel]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__IsIde__3B75D760]  DEFAULT ((0)) FOR [IsIdentifyMeterMismatch]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__IsAct__3C69FB99]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Aircrafts] ADD  CONSTRAINT [DF__Aircrafts__IsDel__3D5E1FD2]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_AircraftCategory_Aircraft] FOREIGN KEY([AircraftCategoryId])
REFERENCES [dbo].[AircraftCategories] ([Id])
GO

ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_AircraftCategory_Aircraft]
GO

ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_AircraftClass_Aircraft] FOREIGN KEY([AircraftClassId])
REFERENCES [dbo].[AircraftClasses] ([Id])
GO

ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_AircraftClass_Aircraft]
GO

ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_AircraftMake_Aircraft] FOREIGN KEY([AircraftMakeId])
REFERENCES [dbo].[AircraftMakes] ([Id])
GO

ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_AircraftMake_Aircraft]
GO

ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_AircraftModel_Aircraft] FOREIGN KEY([AircraftModelId])
REFERENCES [dbo].[AircraftModels] ([Id])
GO

ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_AircraftModel_Aircraft]
GO

ALTER TABLE [dbo].[Aircrafts]  WITH CHECK ADD  CONSTRAINT [FK_FlightSimulatorClass_Aircraft] FOREIGN KEY([FlightSimulatorClassId])
REFERENCES [dbo].[FlightSimulatorClasses] ([Id])
GO

ALTER TABLE [dbo].[Aircrafts] CHECK CONSTRAINT [FK_FlightSimulatorClass_Aircraft]
GO


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

--drop table UserRolePermissions

Create Table [Permissions]
(
Id int primary key Identity (1,1),
PermissionType varchar(100),
)
GO

Create Table ModuleDetails
(
Id int primary key Identity (1,1),
Name varchar(100),
ControllerName varchar(100),
ActionName varchar(100),
DisplayName varchar(100),
Icon varchar(50)
)


Create Table UserRolePermissions
(
Id int primary key Identity (1,1),
RoleId int,
PermissionId int,
ModuleId int,
IsAllowed bit not null default(0),
CONSTRAINT UK_RoleId_PermissionId_ModuleId UNIQUE (RoleId, PermissionId,ModuleId),
CONSTRAINT FK_UserRole_UserRolePermission Foreign Key (RoleId) References UserRoles(Id),
CONSTRAINT FK_Permission_UserRolePermission Foreign Key (PermissionId) References Permissions(Id),
CONSTRAINT FK_ModuleDetail_UserRolePermission Foreign Key (ModuleId) References ModuleDetails(Id),
)
Go

INSERT INTO Permissions Values('Create');
INSERT INTO Permissions Values('View');
INSERT INTO Permissions Values('Edit');
INSERT INTO Permissions Values('Delete');
GO

INSERT INTO ModuleDetails VALUES ('User','User','Index','User','far fa-user nav-icon')
INSERT INTO ModuleDetails VALUES ('InstructorType','InstructorType','Index','Instructor Type','far fa-user nav-icon')
INSERT INTO ModuleDetails VALUES ('Aircraft','Aircraft','Index','Aircraft','fas fa-plane nav-icon')
INSERT INTO ModuleDetails VALUES ('UserRolePermission','UserRolePermission','Index','User Role Permission','fas fa-key nav-icon')
GO



INSERT INTO UserRolePermissions VALUES(1,1,1,1)
INSERT INTO UserRolePermissions VALUES(1,2,1,1)
INSERT INTO UserRolePermissions VALUES(1,3,1,1)
INSERT INTO UserRolePermissions VALUES(1,4,1,1)
INSERT INTO UserRolePermissions VALUES(1,1,2,1)
INSERT INTO UserRolePermissions VALUES(1,2,2,1)
INSERT INTO UserRolePermissions VALUES(1,3,2,1)
INSERT INTO UserRolePermissions VALUES(1,4,2,1)
INSERT INTO UserRolePermissions VALUES(1,1,3,1)
INSERT INTO UserRolePermissions VALUES(1,2,3,1)
INSERT INTO UserRolePermissions VALUES(1,3,3,1)
INSERT INTO UserRolePermissions VALUES(1,4,3,1)
INSERT INTO UserRolePermissions VALUES(1,1,4,1)
INSERT INTO UserRolePermissions VALUES(1,2,4,1)
INSERT INTO UserRolePermissions VALUES(1,3,4,1)
INSERT INTO UserRolePermissions VALUES(1,4,4,1)

INSERT INTO UserRolePermissions VALUES(2,1,1,0)
INSERT INTO UserRolePermissions VALUES(2,2,1,0)
INSERT INTO UserRolePermissions VALUES(2,3,1,0)
INSERT INTO UserRolePermissions VALUES(2,4,1,0)
INSERT INTO UserRolePermissions VALUES(2,1,2,0)
INSERT INTO UserRolePermissions VALUES(2,2,2,0)
INSERT INTO UserRolePermissions VALUES(2,3,2,0)
INSERT INTO UserRolePermissions VALUES(2,4,2,0)
INSERT INTO UserRolePermissions VALUES(2,1,3,0)
INSERT INTO UserRolePermissions VALUES(2,2,3,0)
INSERT INTO UserRolePermissions VALUES(2,3,3,0)
INSERT INTO UserRolePermissions VALUES(2,4,3,0)
INSERT INTO UserRolePermissions VALUES(2,1,4,0)
INSERT INTO UserRolePermissions VALUES(2,2,4,0)
INSERT INTO UserRolePermissions VALUES(2,3,4,0)
INSERT INTO UserRolePermissions VALUES(2,4,4,0)

INSERT INTO UserRolePermissions VALUES(3,1,1,0)
INSERT INTO UserRolePermissions VALUES(3,2,1,0)
INSERT INTO UserRolePermissions VALUES(3,3,1,0)
INSERT INTO UserRolePermissions VALUES(3,4,1,0)
INSERT INTO UserRolePermissions VALUES(3,1,2,0)
INSERT INTO UserRolePermissions VALUES(3,2,2,0)
INSERT INTO UserRolePermissions VALUES(3,3,2,0)
INSERT INTO UserRolePermissions VALUES(3,4,2,0)
INSERT INTO UserRolePermissions VALUES(3,1,3,0)
INSERT INTO UserRolePermissions VALUES(3,2,3,0)
INSERT INTO UserRolePermissions VALUES(3,3,3,0)
INSERT INTO UserRolePermissions VALUES(3,4,3,0)
INSERT INTO UserRolePermissions VALUES(3,1,4,0)
INSERT INTO UserRolePermissions VALUES(3,2,4,0)
INSERT INTO UserRolePermissions VALUES(3,3,4,0)
INSERT INTO UserRolePermissions VALUES(3,4,4,0)

INSERT INTO UserRolePermissions VALUES(4,1,1,0)
INSERT INTO UserRolePermissions VALUES(4,2,1,0)
INSERT INTO UserRolePermissions VALUES(4,3,1,0)
INSERT INTO UserRolePermissions VALUES(4,4,1,0)
INSERT INTO UserRolePermissions VALUES(4,1,2,0)
INSERT INTO UserRolePermissions VALUES(4,2,2,0)
INSERT INTO UserRolePermissions VALUES(4,3,2,0)
INSERT INTO UserRolePermissions VALUES(4,4,2,0)
INSERT INTO UserRolePermissions VALUES(4,1,3,0)
INSERT INTO UserRolePermissions VALUES(4,2,3,0)
INSERT INTO UserRolePermissions VALUES(4,3,3,0)
INSERT INTO UserRolePermissions VALUES(4,4,3,0)
INSERT INTO UserRolePermissions VALUES(4,1,4,0)
INSERT INTO UserRolePermissions VALUES(4,2,4,0)
INSERT INTO UserRolePermissions VALUES(4,3,4,0)
INSERT INTO UserRolePermissions VALUES(4,4,4,0)

INSERT INTO UserRolePermissions VALUES(5,1,1,0)
INSERT INTO UserRolePermissions VALUES(5,2,1,0)
INSERT INTO UserRolePermissions VALUES(5,3,1,0)
INSERT INTO UserRolePermissions VALUES(5,4,1,0)
INSERT INTO UserRolePermissions VALUES(5,1,2,0)
INSERT INTO UserRolePermissions VALUES(5,2,2,0)
INSERT INTO UserRolePermissions VALUES(5,3,2,0)
INSERT INTO UserRolePermissions VALUES(5,4,2,0)
INSERT INTO UserRolePermissions VALUES(5,1,3,0)
INSERT INTO UserRolePermissions VALUES(5,2,3,0)
INSERT INTO UserRolePermissions VALUES(5,3,3,0)
INSERT INTO UserRolePermissions VALUES(5,4,3,0)
INSERT INTO UserRolePermissions VALUES(5,1,4,0)
INSERT INTO UserRolePermissions VALUES(5,2,4,0)
INSERT INTO UserRolePermissions VALUES(5,3,4,0)
INSERT INTO UserRolePermissions VALUES(5,4,4,0)

INSERT INTO UserRolePermissions VALUES(6,1,1,0)
INSERT INTO UserRolePermissions VALUES(6,2,1,0)
INSERT INTO UserRolePermissions VALUES(6,3,1,0)
INSERT INTO UserRolePermissions VALUES(6,4,1,0)
INSERT INTO UserRolePermissions VALUES(6,1,2,0)
INSERT INTO UserRolePermissions VALUES(6,2,2,0)
INSERT INTO UserRolePermissions VALUES(6,3,2,0)
INSERT INTO UserRolePermissions VALUES(6,4,2,0)
INSERT INTO UserRolePermissions VALUES(6,1,3,0)
INSERT INTO UserRolePermissions VALUES(6,2,3,0)
INSERT INTO UserRolePermissions VALUES(6,3,3,0)
INSERT INTO UserRolePermissions VALUES(6,4,3,0)
INSERT INTO UserRolePermissions VALUES(6,1,4,0)
INSERT INTO UserRolePermissions VALUES(6,2,4,0)
INSERT INTO UserRolePermissions VALUES(6,3,4,0)
INSERT INTO UserRolePermissions VALUES(6,4,4,0)


