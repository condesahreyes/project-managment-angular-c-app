USE [master]
GO
/****** Object:  Database [OBL2]    Script Date: 07/10/2021 15:22:09 ******/
CREATE DATABASE [OBL2]
 CONTAINMENT = NONE
GO
ALTER DATABASE [OBL2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OBL2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OBL2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OBL2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OBL2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OBL2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OBL2] SET ARITHABORT OFF 
GO
ALTER DATABASE [OBL2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OBL2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OBL2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OBL2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OBL2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OBL2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OBL2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OBL2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OBL2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OBL2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OBL2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OBL2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OBL2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OBL2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OBL2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OBL2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [OBL2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OBL2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OBL2] SET  MULTI_USER 
GO
ALTER DATABASE [OBL2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OBL2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OBL2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OBL2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OBL2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OBL2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OBL2] SET QUERY_STORE = OFF
GO
USE [OBL2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Domain] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[SolvedById] [uniqueidentifier] NULL,
	[StateName] [nvarchar](450) NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [uniqueidentifier] NOT NULL,
	[TotalBugs] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rols]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rols](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Rols] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Name] [nvarchar](450) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[RolId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersProject]    Script Date: 07/10/2021 15:22:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersProject](
	[ProjectsId] [uniqueidentifier] NOT NULL,
	[UsersId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UsersProject] PRIMARY KEY CLUSTERED 
(
	[ProjectsId] ASC,
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210924014508_Create-Database', N'5.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211001212204_DataBaseWithConnectionString', N'5.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211003214740_Agregado de token a modelo user', N'5.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211004214634_Eliminando tabla de bug para cambiar su comportamiento', N'5.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211004215246_Nueva migración estable sin tabla bug', N'5.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211004215400_Creando nueva tabla bug', N'5.0.10')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (1000, N'Bug 1.1', N'Dominio 1.1', N'1.1', N'b1d3d60b-8857-41b1-b4b2-fce444bcccdc', NULL, N'activo')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (1001, N'Bug 1.2', N'Dominio 1.2', N'1.2', N'b1d3d60b-8857-41b1-b4b2-fce444bcccdc', N'f1e3179f-d371-463d-b61c-08d989afbe0e', N'resuelto')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (2000, N'Bug 2.1', N'Dominio 2.1', N'2.1', N'ec6ed451-0c47-4237-aca7-37cb6cc1b55b', NULL, N'resuelto')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (3000, N'Bug 3.1', N'Dominio 3.1', N'3.1', N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', NULL, N'activo')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (3001, N'Bug 3.2', N'Dominio 3.2', N'3.2', N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', NULL, N'activo')
GO
INSERT [dbo].[Bugs] ([Id], [Name], [Domain], [Version], [ProjectId], [SolvedById], [StateName]) VALUES (3002, N'Bug 3.3', N'Dominio 3.3', N'3.3', N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', NULL, N'activo')
GO
INSERT [dbo].[Projects] ([Id], [TotalBugs], [Name]) VALUES (N'ec6ed451-0c47-4237-aca7-37cb6cc1b55b', 0, N'Proyecto 2')
GO
INSERT [dbo].[Projects] ([Id], [TotalBugs], [Name]) VALUES (N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', 1, N'Proyecto 3')
GO
INSERT [dbo].[Projects] ([Id], [TotalBugs], [Name]) VALUES (N'b1d3d60b-8857-41b1-b4b2-fce444bcccdc', 2, N'Proyecto 1')
GO
INSERT [dbo].[Rols] ([Id], [Name]) VALUES (N'8e1c2470-fa28-4a02-b159-88c5674ec602', N'Administrador')
GO
INSERT [dbo].[Rols] ([Id], [Name]) VALUES (N'8e1c2470-fa28-4a02-b159-88c5674ec603', N'Tester')
GO
INSERT [dbo].[Rols] ([Id], [Name]) VALUES (N'8e1c2470-fa28-4a02-b159-88c5674ec604', N'Desarrollador')
GO
INSERT [dbo].[States] ([Name], [Id]) VALUES (N'activo', N'8e1c2470-fa28-4a02-b159-88c5674ec603')
GO
INSERT [dbo].[States] ([Name], [Id]) VALUES (N'resuelto', N'8e1c2470-fa28-4a02-b159-88c5674ec602')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'c68728c6-073f-4bf2-b619-08d989afbe0e', N'Tester 1', N'Tester 1', N'Tester 1', N'tester', N'tester@tester.com', N'Tester-4e02230b-ef0f-46a7-9051-0442bb0117a0', N'8e1c2470-fa28-4a02-b159-88c5674ec603')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'a9e2076a-5c12-41a8-b61a-08d989afbe0e', N'Tester 2', N'Tester 2', N'Tester 2', N'tester', N'tester2@tester.com', N'Tester-7a24fd81-1698-4be0-a431-18dea61c70e6', N'8e1c2470-fa28-4a02-b159-88c5674ec603')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'7ea4c11e-4eb2-43f4-b61b-08d989afbe0e', N'Admin', N'Admin', N'Admin', N'Admin', N'admin@admin.com', NULL, N'8e1c2470-fa28-4a02-b159-88c5674ec602')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'f1e3179f-d371-463d-b61c-08d989afbe0e', N'Desarrollador 1', N'Desarrollador 1', N'Desarrollador 1', N'desarrollador', N'desarrollador@desarrollador.com', N'Desarrollador-58721e37-df41-45a6-9504-5bb2f33b9efd', N'8e1c2470-fa28-4a02-b159-88c5674ec604')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'a0140b88-738c-4075-b61d-08d989afbe0e', N'Desarrollador 2', N'Desarrollador 2', N'Desarrollador 2', N'desarrollador', N'desarrollador2@desarrollador.com', N'Desarrollador-9b6425a2-2695-4730-bf87-80e626dc30c4', N'8e1c2470-fa28-4a02-b159-88c5674ec604')
GO
INSERT [dbo].[Users] ([Id], [Name], [LastName], [UserName], [Password], [Email], [Token], [RolId]) VALUES (N'8e1c2470-fa28-4a02-b159-88c5674ec602', N'SuperAdmin', N'SuperAdmin', N'SuperAdmin', N'SuperAdmin', N'super@admin.com', N'Administrador-866e12c3-7656-43c3-bf19-01141ce8cb82', N'8e1c2470-fa28-4a02-b159-88c5674ec602')
GO
INSERT [dbo].[UsersProject] ([ProjectsId], [UsersId]) VALUES (N'b1d3d60b-8857-41b1-b4b2-fce444bcccdc', N'c68728c6-073f-4bf2-b619-08d989afbe0e')
GO
INSERT [dbo].[UsersProject] ([ProjectsId], [UsersId]) VALUES (N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', N'a9e2076a-5c12-41a8-b61a-08d989afbe0e')
GO
INSERT [dbo].[UsersProject] ([ProjectsId], [UsersId]) VALUES (N'ec6ed451-0c47-4237-aca7-37cb6cc1b55b', N'f1e3179f-d371-463d-b61c-08d989afbe0e')
GO
INSERT [dbo].[UsersProject] ([ProjectsId], [UsersId]) VALUES (N'60dcd2f9-c9e3-4e48-bf64-db84d4b0ed0f', N'f1e3179f-d371-463d-b61c-08d989afbe0e')
GO
INSERT [dbo].[UsersProject] ([ProjectsId], [UsersId]) VALUES (N'b1d3d60b-8857-41b1-b4b2-fce444bcccdc', N'f1e3179f-d371-463d-b61c-08d989afbe0e')
GO
/****** Object:  Index [IX_Bugs_ProjectId]    Script Date: 07/10/2021 15:22:09 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_ProjectId] ON [dbo].[Bugs]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bugs_SolvedById]    Script Date: 07/10/2021 15:22:09 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_SolvedById] ON [dbo].[Bugs]
(
	[SolvedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Bugs_StateName]    Script Date: 07/10/2021 15:22:09 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_StateName] ON [dbo].[Bugs]
(
	[StateName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RolId]    Script Date: 07/10/2021 15:22:09 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RolId] ON [dbo].[Users]
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsersProject_UsersId]    Script Date: 07/10/2021 15:22:09 ******/
CREATE NONCLUSTERED INDEX [IX_UsersProject_UsersId] ON [dbo].[UsersProject]
(
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_States_StateName] FOREIGN KEY([StateName])
REFERENCES [dbo].[States] ([Name])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_States_StateName]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Users_SolvedById] FOREIGN KEY([SolvedById])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Users_SolvedById]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Rols_RolId] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Rols_RolId]
GO
ALTER TABLE [dbo].[UsersProject]  WITH CHECK ADD  CONSTRAINT [FK_UsersProject_Projects_ProjectsId] FOREIGN KEY([ProjectsId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersProject] CHECK CONSTRAINT [FK_UsersProject_Projects_ProjectsId]
GO
ALTER TABLE [dbo].[UsersProject]  WITH CHECK ADD  CONSTRAINT [FK_UsersProject_Users_UsersId] FOREIGN KEY([UsersId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersProject] CHECK CONSTRAINT [FK_UsersProject_Users_UsersId]
GO
USE [master]
GO
ALTER DATABASE [OBL2] SET  READ_WRITE 
GO
