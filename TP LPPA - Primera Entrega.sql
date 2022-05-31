USE [master]
GO
/****** Object:  Database [VeterinariaLPPA]    Script Date: 25/5/2022 23:12:55 ******/
CREATE DATABASE [VeterinariaLPPA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VeterinariaLPPA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\VeterinariaLPPA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VeterinariaLPPA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\VeterinariaLPPA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VeterinariaLPPA] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VeterinariaLPPA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VeterinariaLPPA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET ARITHABORT OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VeterinariaLPPA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VeterinariaLPPA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VeterinariaLPPA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VeterinariaLPPA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET RECOVERY FULL 
GO
ALTER DATABASE [VeterinariaLPPA] SET  MULTI_USER 
GO
ALTER DATABASE [VeterinariaLPPA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VeterinariaLPPA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VeterinariaLPPA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VeterinariaLPPA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VeterinariaLPPA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VeterinariaLPPA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VeterinariaLPPA', N'ON'
GO
ALTER DATABASE [VeterinariaLPPA] SET QUERY_STORE = OFF
GO
USE [VeterinariaLPPA]
GO
/****** Object:  Table [dbo].[Accion]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accion](
	[id] [int] NULL,
	[detalle] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id] [int] NULL,
	[detalle] [varchar](500) NULL,
	[fecha] [varchar](500) NULL,
	[id_usuario] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Usuario]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Usuario](
	[id] [int] NULL,
	[tipo_usuario] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario_Accion]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario_Accion](
	[id_tipousuario] [int] NULL,
	[id_accion] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] NULL,
	[usuario] [varchar](100) NULL,
	[contraseña] [varchar](100) NULL,
	[nombre] [varchar](100) NULL,
	[id_tipo_usuario] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (3, N'Comprar')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (4, N'Consultar')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (5, N'Agregar Item')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (6, N'Borrar Item')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (7, N'Cancelar Compra')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (1, N'Backup')
INSERT [dbo].[Accion] ([id], [detalle]) VALUES (2, N'Restore')
GO
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (1, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 21:45:51.8732877', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (2, N'Inicio de Sesion - Usuario: prueba', N'2022-05-25 21:46:06.4888442', 2)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (3, N'Inicio de Sesion - Usuario: prueba', N'2022-05-25 22:08:16.1166694', 2)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (4, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 22:43:33.2172470', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (5, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 22:44:28.3325447', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (6, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 23:04:36.3406422', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (7, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 23:05:15.5521243', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (8, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 23:07:14.4795832', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (9, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 23:07:57.7731771', 1)
INSERT [dbo].[Bitacora] ([id], [detalle], [fecha], [id_usuario]) VALUES (10, N'Inicio de Sesion - Usuario: admin', N'2022-05-25 23:08:11.5608372', 1)
GO
INSERT [dbo].[Tipo_Usuario] ([id], [tipo_usuario]) VALUES (1, N'Webmaster')
INSERT [dbo].[Tipo_Usuario] ([id], [tipo_usuario]) VALUES (2, N'Cliente')
GO
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (1, 1)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (1, 2)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (2, 3)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (2, 4)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (2, 5)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (2, 6)
INSERT [dbo].[TipoUsuario_Accion] ([id_tipousuario], [id_accion]) VALUES (2, 7)
GO
INSERT [dbo].[Usuario] ([id], [usuario], [contraseña], [nombre], [id_tipo_usuario]) VALUES (1, N'admin', N'21232F297A57A5A743894A0E4A801FC3', N'Juancito', 1)
INSERT [dbo].[Usuario] ([id], [usuario], [contraseña], [nombre], [id_tipo_usuario]) VALUES (2, N'cliente', N'4983A0AB83ED86E0E7213C8783940193', N'Patito', 2)
GO
/****** Object:  StoredProcedure [dbo].[listar_acciones]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[listar_acciones]
@id_tipo_usu varchar(50)
as 
begin
select b.id, b.detalle
from TipoUsuario_Accion a 
inner join Accion b 
on a.id_accion= b.id 
where a.id_tipousuario = @id_tipo_usu
end 
GO
/****** Object:  StoredProcedure [dbo].[llenar_bitacora]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[llenar_bitacora]
@id_usu varchar(100), @detalle varchar(500)
as 
begin
declare @id int
set @id = isnull((Select max(id) from Bitacora),0 ) +1

INSERT INTO [dbo].[Bitacora]
           ([id]
           ,[detalle]
           ,[fecha]
           ,[id_usuario])
     VALUES
           (@id,
           @detalle,
           SYSDATETIME(),
           @id_usu)
end
GO
/****** Object:  StoredProcedure [dbo].[verificar_usuario]    Script Date: 25/5/2022 23:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[verificar_usuario]
@usu varchar(100), @pass varchar(100)
as 
begin
select a.id, a.nombre, a.usuario, a.contraseña, a.id_tipo_usuario, b.tipo_usuario
from Usuario a 
inner join Tipo_Usuario b 
on a.id_tipo_usuario = b.id 
where a.usuario = @usu and a.contraseña = @pass
end 
GO
USE [master]
GO
ALTER DATABASE [VeterinariaLPPA] SET  READ_WRITE 
GO
