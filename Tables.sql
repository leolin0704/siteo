CREATE TABLE TNotice(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Title NVARCHAR(200) NOT NULL,
	Content NVARCHAR(4000) NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

CREATE TABLE TBanner(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Title NVARCHAR(200) NOT NULL,
	LinkUrl NVARCHAR(500) NULL,
	ImgSrc NVARCHAR(500) NOT NULL,
	[Order] INT NOT NULL DEFAULT(1),
	PositionKey VARCHAR(30) NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)


CREATE TABLE TModule(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	ParentModuleID INT NULL,
	[Key] CHAR(50) NOT NULL,
	[Order] INT NOT NULL DEFAULT(1),
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

ALTER TABLE TModule
ADD CONSTRAINT FK_TModule_ParentModuleID
FOREIGN KEY (ParentModuleID) REFERENCES TModule(ID)

CREATE TABLE TPermission(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

CREATE TABLE TPermissionModule(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PermissionID INT NOT NULL,
	ModuleID INT NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

ALTER TABLE TPermissionModule
ADD CONSTRAINT FK_TPermissionModule_PermissionID
FOREIGN KEY (PermissionID) REFERENCES TPermission(ID)

ALTER TABLE TPermissionModule
ADD CONSTRAINT FK_TPermissionModule_ModuleID
FOREIGN KEY (ModuleID) REFERENCES TModule(ID)

CREATE TABLE TRole(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)


CREATE TABLE TRolePermission(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PermissionID INT NOT NULL,
	RoleID INT NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

ALTER TABLE TRolePermission
ADD CONSTRAINT FK_TRolePermission_PermissionID
FOREIGN KEY (PermissionID) REFERENCES TPermission(ID)

ALTER TABLE TRolePermission
ADD CONSTRAINT FK_TRolePermission_RoleID
FOREIGN KEY (RoleID) REFERENCES TRole(ID)

CREATE TABLE TAdminUser(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Account NVARCHAR(50) NOT NULL,
	[Password] VARBINARY(100) NOT NULL,
	[Status] CHAR(1) NOT NULL, -- A active D disable
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

CREATE TABLE TAdminUserRole(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	AdminUserID INT NOT NULL,
	RoleID INT NOT NULL,
	IsDeleted INT NOT NULL DEFAULT(0),
	LastUpdateDate DATETIME NULL,
	LastUpdateBy NVARCHAR(20) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
	CreateBy NVARCHAR(20) NOT NULL
)

ALTER TABLE TAdminUserRole
ADD CONSTRAINT FK_TAdminUserRole_AdminUserID
FOREIGN KEY (AdminUserID) REFERENCES TAdminUser(ID)

ALTER TABLE TAdminUserRole
ADD CONSTRAINT FK_TAdminUserRole_RoleID
FOREIGN KEY (RoleID) REFERENCES TRole(ID)



insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'banner管理','banner',1,0,GETDATE(),'lily');
insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'新闻管理','news',1,0,GETDATE(),'lily');
insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'用户反馈管理','feedback',1,0,GETDATE(),'lily');
insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'联系我们','contact',1,0,GETDATE(),'lily');
insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'关于我们','aboutUs',1,0,GETDATE(),'lily');
insert TModule(Name,[Key],[Order],IsDeleted,CreateDate,CreateBy) values(N'公告管理','publicManage',1,0,GETDATE(),'lily');

select * from TModule;

--delete from TModule where ID<13;


insert TRole(Name,IsDeleted,CreateDate,CreateBy) values('data manager',0,GETDATE(),'lily');
insert TRole(Name,IsDeleted,CreateDate,CreateBy) values('admin',0,GETDATE(),'lily');

select * from TRole;
--delete from TRole where ID=2;


insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'banner管理',0,GETDATE(),'lily');
insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'新闻管理',0,GETDATE(),'lily');
insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'用户反馈管理',0,GETDATE(),'lily');
insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'联系我们',0,GETDATE(),'lily');
insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'关于我们',0,GETDATE(),'lily');
insert TPermission(Name,IsDeleted,CreateDate,CreateBy) values(N'公告管理',0,GETDATE(),'lily');

select * from TPermission;


insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(1,13,0,GETDATE(),'lily');
insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(2,14,0,GETDATE(),'lily');
insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(3,15,0,GETDATE(),'lily');
insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(4,16,0,GETDATE(),'lily');
insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(5,17,0,GETDATE(),'lily');
insert TPermissionModule(PermissionID,ModuleID,IsDeleted,CreateDate,CreateBy)values(6,18,0,GETDATE(),'lily');

select * from TPermissionModule;

insert TRolePermission(PermissionID,RoleID,IsDeleted,CreateDate,CreateBy) SELECT P.ID,R.ID,0,GETDATE(),'LEO' FROM TRole R,TPermission P
DELETE TRolePermission WHERE RoleID = 3 AND PermissionID = 3

select * from TRolePermission;


create table TNews(
ID int identity(1,1) PRIMARY KEY,
Title nvarchar(50),
Content nvarchar(4000),
ReleaseDate DATETIME null,
[Status] int NOT NULL,
SetToTop int NULL,
TypeID int not null
)
GO

create table TNewsType(
	ID int identity(1,1) PRIMARY KEY,
	[Name] nvarchar(50),
	ParentID int NULL
)

GO

ALTER TABLE TNews
ADD CONSTRAINT FK_TNews_TypeID
FOREIGN KEY (TypeID) REFERENCES TNewsType(ID)
GO


ALTER TABLE TNewsType
ADD CONSTRAINT FK_TNewsType_ParentID
FOREIGN KEY (ParentID) REFERENCES TNewsType(ID)
GO

create table TNewsPictures(
	ID int identity(1,1) PRIMARY KEY,
	NewsID int not null,
	Title nvarchar(50),
	ImgSrc varchar(300),
	LinkUrl varchar(300),
	[Order] int NOT NULL
)
GO

ALTER TABLE TNewsPictures
ADD CONSTRAINT FK_TNewsPictures_NewsID
FOREIGN KEY (NewsID) REFERENCES TNews(ID)
GO