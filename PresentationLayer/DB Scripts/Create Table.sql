Create Table UserRoles
(
Id int primary key Identity (1,1),
Name varchar(100)
)
Go

Insert into UserRoles
Values
 ('SuperAdmin')
,('Admin')
,('Office Staff')
,('Instructors')
,('Mechanic')
,('Pilot (Renter)')
,('Owner')
GO

Create Table InstructorTypes
(
Id int primary key Identity (1,1),
Name varchar(100)
)
Go

Insert into InstructorTypes
Values ('None')
,('Chief Flight Instructor')
,('Asst. Chief Instructor')
,('Check Instructor')
GO


Create Table Countries
(
Id int primary key Identity (1,1),
Name varchar(100),
TwoCharCountryCode CHAR(2),
ThreeCharCountryCode CHAR(3) 
)
Go

INSERT INTO Countries VALUES 
 ('Aghanistan','AF','AFG')
,('Aland Islands','AX','ALA')
,('Albania','AL','ALB')
,('Algeria','DZ','DZA')
,('American Samoa','AS','ASM')
,('Andorra','AD','AND')
,('Angola','AO','AGO')
,('Anguilla','AI','AIA')
,('Antarctica','AQ','ATA')
,('Antigua and Barbuda','AG','ATG')
,('Argentina','AR','ARG')
,('Armenia','AM','ARM')
,('Aruba','AW','ABW')
,('Australia','AU','AUS')
,('Austria','AT','AUT')
,('Azerbaijan','AZ','AZE')
,('Bahamas','BS','BHS')
,('Bahrain','BH','BHR')
,('Bangladesh','BD','BGD')
,('Barbados','BB','BRB')
,('Belarus','BY','BLR')
,('Belgium','BE','BEL')
,('Belize','BZ','BLZ')
,('Benin','BJ','BEN')
,('Bermuda','BM','BMU')
,('Bhutan','BT','BTN')
,('Bolivia','BO','BOL')
,('Bonaire, Sint Eustatius and Saba','BQ','BES')
,('Bosnia and Herzegovina','BA','BIH')
,('Botswana','BW','BWA')
,('Bouvet Island','BV','BVT')
,('Brazil','BR','BRA')
,('British Indian Ocean Territory','IO','IOT')
,('Brunei','BN','BRN')
,('Bulgaria','BG','BGR')
,('Burkina Faso','BF','BFA')
,('Burundi','BI','BDI')
,('Cambodia','KH','KHM')
,('Cameroon','CM','CMR')
,('Canada','CA','CAN')
,('Cape Verde','CV','CPV')
,('Cayman Islands','KY','CYM')
,('Central African Republic','CF','CAF')
,('Chad','TD','TCD')
,('Chile','CL','CHL')
,('China','CN','CHN')
,('Christmas Island','CX','CXR')
,('Cocos (Keeling) Islands','CC','CCK')
,('Colombia','CO','COL')
,('Comoros','KM','COM')
,('Congo','CG','COG')
,('Cook Islands','CK','COK')
,('Costa Rica','CR','CRI')
,('Ivory Coast','CI','CIV')
,('Croatia','HR','HRV')
,('Cuba','CU','CUB')
,('Curacao','CW','CUW')
,('Cyprus','CY','CYP')
,('Czech Republic','CZ','CZE')
,('Democratic Republic of the Congo','CD','COD')
,('Denmark','DK','DNK')
,('Djibouti','DJ','DJI')
,('Dominica','DM','DMA')
,('Dominican Republic','DO','DOM')
,('Ecuador','EC','ECU')
,('Egypt','EG','EGY')
,('El Salvador','SV','SLV')
,('Equatorial Guinea','GQ','GNQ')
,('Eritrea','ER','ERI')
,('Estonia','EE','EST')
,('Ethiopia','ET','ETH')
,('Falkland Islands (Malvinas)','FK','FLK')
,('Faroe Islands','FO','FRO')
,('Fiji','FJ','FJI')
,('Finland','FI','FIN')
,('France','FR','FRA')
,('French Guiana','GF','GUF')
,('French Polynesia','PF','PYF')
,('French Southern Territories','TF','ATF')
,('Gabon','GA','GAB')
,('Gambia','GM','GMB')
,('Georgia','GE','GEO')
,('Germany','DE','DEU')
,('Ghana','GH','GHA')
,('Gibraltar','GI','GIB')
,('Greece','GR','GRC')
,('Greenland','GL','GRL')
,('Grenada','GD','GRD')
,('Guadaloupe','GP','GLP')
,('Guam','GU','GUM')
,('Guatemala','GT','GTM')
,('Guernsey','GG','GGY')
,('Guinea','GN','GIN')
,('Guinea-Bissau','GW','GNB')
,('Guyana','GY','GUY')
,('Haiti','HT','HTI')
,('Heard Island and McDonald Islands','HM','HMD')
,('Honduras','HN','HND')
,('Hong Kong','HK','HKG')
,('Hungary','HU','HUN')
,('Iceland','IS','ISL')
,('India','IN','IND')
,('Indonesia','ID','IDN')
,('Iran','IR','IRN')
,('Iraq','IQ','IRQ')
,('Ireland','IE','IRL')
,('Isle of Man','IM','IMN')
,('Israel','IL','ISR')
,('Italy','IT','ITA')
,('Jamaica','JM','JAM')
,('Japan','JP','JPN')
,('Jersey','JE','JEY')
,('Jordan','JO','JOR')
,('Kazakhstan','KZ','KAZ')
,('Kenya','KE','KEN')
,('Kiribati','KI','KIR')
,('Kosovo','XK','---')
,('Kuwait','KW','KWT')
,('Kyrgyzstan','KG','KGZ')
,('Laos','LA','LAO')
,('Latvia','LV','LVA')
,('Lebanon','LB','LBN')
,('Lesotho','LS','LSO')
,('Liberia','LR','LBR')
,('Libya','LY','LBY')
,('Liechtenstein','LI','LIE')
,('Lithuania','LT','LTU')
,('Luxembourg','LU','LUX')
,('Macao','MO','MAC')
,('Macedonia','MK','MKD')
,('Madagascar','MG','MDG')
,('Malawi','MW','MWI')
,('Malaysia','MY','MYS')
,('Maldives','MV','MDV')
,('Mali','ML','MLI')
,('Malta','MT','MLT')
,('Marshall Islands','MH','MHL')
,('Martinique','MQ','MTQ')
,('Mauritania','MR','MRT')
,('Mauritius','MU','MUS')
,('Mayotte','YT','MYT')
,('Mexico','MX','MEX')
,('Micronesia','FM','FSM')
,('Moldava','MD','MDA')
,('Monaco','MC','MCO')
,('Mongolia','MN','MNG')
,('Montenegro','ME','MNE')
,('Montserrat','MS','MSR')
,('Morocco','MA','MAR')
,('Mozambique','MZ','MOZ')
,('Myanmar (Burma)','MM','MMR')
,('Namibia','NA','NAM')
,('Nauru','NR','NRU')
,('Nepal','NP','NPL')
,('Netherlands','NL','NLD')
,('New Caledonia','NC','NCL')
,('New Zealand','NZ','NZL')
,('Nicaragua','NI','NIC')
,('Niger','NE','NER')
,('Nigeria','NG','NGA')
,('Niue','NU','NIU')
,('Norfolk Island','NF','NFK')
,('North Korea','KP','PRK')
,('Northern Mariana Islands','MP','MNP')
,('Norway','NO','NOR')
,('Oman','OM','OMN')
,('Pakistan','PK','PAK')
,('Palau','PW','PLW')
,('Palestine','PS','PSE')
,('Panama','PA','PAN')
,('Papua New Guinea','PG','PNG')
,('Paraguay','PY','PRY')
,('Peru','PE','PER')
,('Phillipines','PH','PHL')
,('Pitcairn','PN','PCN')
,('Poland','PL','POL')
,('Portugal','PT','PRT')
,('Puerto Rico','PR','PRI')
,('Qatar','QA','QAT')
,('Reunion','RE','REU')
,('Romania','RO','ROU')
,('Russia','RU','RUS')
,('Rwanda','RW','RWA')
,('Saint Barthelemy','BL','BLM')
,('Saint Helena','SH','SHN')
,('Saint Kitts and Nevis','KN','KNA')
,('Saint Lucia','LC','LCA')
,('Saint Martin','MF','MAF')
,('Saint Pierre and Miquelon','PM','SPM')
,('Saint Vincent and the Grenadines','VC','VCT')
,('Samoa','WS','WSM')
,('San Marino','SM','SMR')
,('Sao Tome and Principe','ST','STP')
,('Saudi Arabia','SA','SAU')
,('Senegal','SN','SEN')
,('Serbia','RS','SRB')
,('Seychelles','SC','SYC')
,('Sierra Leone','SL','SLE')
,('Singapore','SG','SGP')
,('Sint Maarten','SX','SXM')
,('Slovakia','SK','SVK')
,('Slovenia','SI','SVN')
,('Solomon Islands','SB','SLB')
,('Somalia','SO','SOM')
,('South Africa','ZA','ZAF')
,('South Georgia and the South Sandwich Islands','GS','SGS')
,('South Korea','KR','KOR')
,('South Sudan','SS','SSD')
,('Spain','ES','ESP')
,('Sri Lanka','LK','LKA')
,('Sudan','SD','SDN')
,('Suriname','SR','SUR')
,('Svalbard and Jan Mayen','SJ','SJM')
,('Swaziland','SZ','SWZ')
,('Sweden','SE','SWE')
,('Switzerland','CH','CHE')
,('Syria','SY','SYR')
,('Taiwan','TW','TWN')
,('Tajikistan','TJ','TJK')
,('Tanzania','TZ','TZA')
,('Thailand','TH','THA')
,('Timor-Leste (East Timor)','TL','TLS')
,('Togo','TG','TGO')
,('Tokelau','TK','TKL')
,('Tonga','TO','TON')
,('Trinidad and Tobago','TT','TTO')
,('Tunisia','TN','TUN')
,('Turkey','TR','TUR')
,('Turkmenistan','TM','TKM')
,('Turks and Caicos Islands','TC','TCA')
,('Tuvalu','TV','TUV')
,('Uganda','UG','UGA')
,('Ukraine','UA','UKR')
,('United Arab Emirates','AE','ARE')
,('United Kingdom','GB','GBR')
,('United States','US','USA')
,('United States Minor Outlying Islands','UM','UMI')
,('Uruguay','UY','URY')
,('Uzbekistan','UZ','UZB')
,('Vanuatu','VU','VUT')
,('Vatican City','VA','VAT')
,('Venezuela','VE','VEN')
,('Vietnam','VN','VNM')
,('Virgin Islands, British','VG','VGB')
,('Virgin Islands, US','VI','VIR')
,('Wallis and Futuna','WF','WLF')
,('Western Sahara','EH','ESH')
,('Yemen','YE','YEM')
,('Zambia','ZM','ZMB')
,('Zimbabwe','ZW','ZWE');
GO

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
	[NoofPropellers] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [IsEngineshavePropellers]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [IsEnginesareTurbines]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [TrackOilandFuel]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [IsIdentifyMeterMismatch]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Aircrafts] ADD  DEFAULT ((0)) FOR [IsDeleted]
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

Create Table [Permissions]
(
Id int primary key Identity (1,1),
PermissionType varchar(100),
)
GO

INSERT INTO Permissions Values('Create');
INSERT INTO Permissions Values('View');
INSERT INTO Permissions Values('Edit');
INSERT INTO Permissions Values('Delete');
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
Go

Create Table UserRolePermissions
(
Id int primary key Identity (1,1),
RoleId int,
PermissionId int,
ModuleId int,
CompanyId int,
IsAllowed bit not null default(0),
CONSTRAINT UK_RoleId_PermissionId_ModuleId UNIQUE (RoleId, PermissionId,ModuleId),
CONSTRAINT FK_UserRole_UserRolePermission Foreign Key (RoleId) References UserRoles(Id),
CONSTRAINT FK_Permission_UserRolePermission Foreign Key (PermissionId) References Permissions(Id),
CONSTRAINT FK_ModuleDetail_UserRolePermission Foreign Key (ModuleId) References ModuleDetails(Id),
CONSTRAINT FK_Companies_UserRolePermission Foreign Key (CompanyId) References Companies(Id),
)
GO



--INSERT INTO UserRolePermissions VALUES(2,1,1,0)
--INSERT INTO UserRolePermissions VALUES(2,2,1,0)
--INSERT INTO UserRolePermissions VALUES(2,3,1,0)
--INSERT INTO UserRolePermissions VALUES(2,4,1,0)
--INSERT INTO UserRolePermissions VALUES(2,1,2,0)
--INSERT INTO UserRolePermissions VALUES(2,2,2,0)
--INSERT INTO UserRolePermissions VALUES(2,3,2,0)
--INSERT INTO UserRolePermissions VALUES(2,4,2,0)
--INSERT INTO UserRolePermissions VALUES(2,1,3,0)
--INSERT INTO UserRolePermissions VALUES(2,2,3,0)
--INSERT INTO UserRolePermissions VALUES(2,3,3,0)
--INSERT INTO UserRolePermissions VALUES(2,4,3,0)
--INSERT INTO UserRolePermissions VALUES(2,1,4,0)
--INSERT INTO UserRolePermissions VALUES(2,2,4,0)
--INSERT INTO UserRolePermissions VALUES(2,3,4,0)
--INSERT INTO UserRolePermissions VALUES(2,4,4,0)
--INSERT INTO UserRolePermissions VALUES(2,1,5,0)
--INSERT INTO UserRolePermissions VALUES(2,2,5,0)
--INSERT INTO UserRolePermissions VALUES(2,3,5,0)
--INSERT INTO UserRolePermissions VALUES(2,4,5,0)

--INSERT INTO UserRolePermissions VALUES(3,1,1,0)
--INSERT INTO UserRolePermissions VALUES(3,2,1,0)
--INSERT INTO UserRolePermissions VALUES(3,3,1,0)
--INSERT INTO UserRolePermissions VALUES(3,4,1,0)
--INSERT INTO UserRolePermissions VALUES(3,1,2,0)
--INSERT INTO UserRolePermissions VALUES(3,2,2,0)
--INSERT INTO UserRolePermissions VALUES(3,3,2,0)
--INSERT INTO UserRolePermissions VALUES(3,4,2,0)
--INSERT INTO UserRolePermissions VALUES(3,1,3,0)
--INSERT INTO UserRolePermissions VALUES(3,2,3,0)
--INSERT INTO UserRolePermissions VALUES(3,3,3,0)
--INSERT INTO UserRolePermissions VALUES(3,4,3,0)
--INSERT INTO UserRolePermissions VALUES(3,1,4,0)
--INSERT INTO UserRolePermissions VALUES(3,2,4,0)
--INSERT INTO UserRolePermissions VALUES(3,3,4,0)
--INSERT INTO UserRolePermissions VALUES(3,4,4,0)
--INSERT INTO UserRolePermissions VALUES(3,1,5,0)
--INSERT INTO UserRolePermissions VALUES(3,2,5,0)
--INSERT INTO UserRolePermissions VALUES(3,3,5,0)
--INSERT INTO UserRolePermissions VALUES(3,4,5,0)

--INSERT INTO UserRolePermissions VALUES(4,1,1,0)
--INSERT INTO UserRolePermissions VALUES(4,2,1,0)
--INSERT INTO UserRolePermissions VALUES(4,3,1,0)
--INSERT INTO UserRolePermissions VALUES(4,4,1,0)
--INSERT INTO UserRolePermissions VALUES(4,1,2,0)
--INSERT INTO UserRolePermissions VALUES(4,2,2,0)
--INSERT INTO UserRolePermissions VALUES(4,3,2,0)
--INSERT INTO UserRolePermissions VALUES(4,4,2,0)
--INSERT INTO UserRolePermissions VALUES(4,1,3,0)
--INSERT INTO UserRolePermissions VALUES(4,2,3,0)
--INSERT INTO UserRolePermissions VALUES(4,3,3,0)
--INSERT INTO UserRolePermissions VALUES(4,4,3,0)
--INSERT INTO UserRolePermissions VALUES(4,1,4,0)
--INSERT INTO UserRolePermissions VALUES(4,2,4,0)
--INSERT INTO UserRolePermissions VALUES(4,3,4,0)
--INSERT INTO UserRolePermissions VALUES(4,4,4,0)
--INSERT INTO UserRolePermissions VALUES(4,1,5,0)
--INSERT INTO UserRolePermissions VALUES(4,2,5,0)
--INSERT INTO UserRolePermissions VALUES(4,3,5,0)
--INSERT INTO UserRolePermissions VALUES(4,4,5,0)

--INSERT INTO UserRolePermissions VALUES(5,1,1,0)
--INSERT INTO UserRolePermissions VALUES(5,2,1,0)
--INSERT INTO UserRolePermissions VALUES(5,3,1,0)
--INSERT INTO UserRolePermissions VALUES(5,4,1,0)
--INSERT INTO UserRolePermissions VALUES(5,1,2,0)
--INSERT INTO UserRolePermissions VALUES(5,2,2,0)
--INSERT INTO UserRolePermissions VALUES(5,3,2,0)
--INSERT INTO UserRolePermissions VALUES(5,4,2,0)
--INSERT INTO UserRolePermissions VALUES(5,1,3,0)
--INSERT INTO UserRolePermissions VALUES(5,2,3,0)
--INSERT INTO UserRolePermissions VALUES(5,3,3,0)
--INSERT INTO UserRolePermissions VALUES(5,4,3,0)
--INSERT INTO UserRolePermissions VALUES(5,1,4,0)
--INSERT INTO UserRolePermissions VALUES(5,2,4,0)
--INSERT INTO UserRolePermissions VALUES(5,3,4,0)
--INSERT INTO UserRolePermissions VALUES(5,4,4,0)
--INSERT INTO UserRolePermissions VALUES(5,1,5,0)
--INSERT INTO UserRolePermissions VALUES(5,2,5,0)
--INSERT INTO UserRolePermissions VALUES(5,3,5,0)
--INSERT INTO UserRolePermissions VALUES(5,4,5,0)

--INSERT INTO UserRolePermissions VALUES(6,1,1,0)
--INSERT INTO UserRolePermissions VALUES(6,2,1,0)
--INSERT INTO UserRolePermissions VALUES(6,3,1,0)
--INSERT INTO UserRolePermissions VALUES(6,4,1,0)
--INSERT INTO UserRolePermissions VALUES(6,1,2,0)
--INSERT INTO UserRolePermissions VALUES(6,2,2,0)
--INSERT INTO UserRolePermissions VALUES(6,3,2,0)
--INSERT INTO UserRolePermissions VALUES(6,4,2,0)
--INSERT INTO UserRolePermissions VALUES(6,1,3,0)
--INSERT INTO UserRolePermissions VALUES(6,2,3,0)
--INSERT INTO UserRolePermissions VALUES(6,3,3,0)
--INSERT INTO UserRolePermissions VALUES(6,4,3,0)
--INSERT INTO UserRolePermissions VALUES(6,1,4,0)
--INSERT INTO UserRolePermissions VALUES(6,2,4,0)
--INSERT INTO UserRolePermissions VALUES(6,3,4,0)
--INSERT INTO UserRolePermissions VALUES(6,4,4,0)
--INSERT INTO UserRolePermissions VALUES(6,1,5,0)
--INSERT INTO UserRolePermissions VALUES(6,2,5,0)
--INSERT INTO UserRolePermissions VALUES(6,3,5,0)
--INSERT INTO UserRolePermissions VALUES(6,4,5,0)

--INSERT INTO UserRolePermissions VALUES(7,1,1,0)
--INSERT INTO UserRolePermissions VALUES(7,2,1,0)
--INSERT INTO UserRolePermissions VALUES(7,3,1,0)
--INSERT INTO UserRolePermissions VALUES(7,4,1,0)
--INSERT INTO UserRolePermissions VALUES(7,1,2,0)
--INSERT INTO UserRolePermissions VALUES(7,2,2,0)
--INSERT INTO UserRolePermissions VALUES(7,3,2,0)
--INSERT INTO UserRolePermissions VALUES(7,4,2,0)
--INSERT INTO UserRolePermissions VALUES(7,1,3,0)
--INSERT INTO UserRolePermissions VALUES(7,2,3,0)
--INSERT INTO UserRolePermissions VALUES(7,3,3,0)
--INSERT INTO UserRolePermissions VALUES(7,4,3,0)
--INSERT INTO UserRolePermissions VALUES(7,1,4,0)
--INSERT INTO UserRolePermissions VALUES(7,2,4,0)
--INSERT INTO UserRolePermissions VALUES(7,3,4,0)
--INSERT INTO UserRolePermissions VALUES(7,4,4,0)
--INSERT INTO UserRolePermissions VALUES(7,1,5,0)
--INSERT INTO UserRolePermissions VALUES(7,2,5,0)
--INSERT INTO UserRolePermissions VALUES(7,3,5,0)
--INSERT INTO UserRolePermissions VALUES(7,4,5,0)


select * from UserRolePermissions

GO