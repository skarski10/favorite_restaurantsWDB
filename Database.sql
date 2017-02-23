CREATE DATABASE [restaurant]
GO
USE [restaurant]
GO
/****** Object:  Table [dbo].[cuisine]    Script Date: 2/22/2017 5:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 2/22/2017 5:01:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cuisine] ON 

INSERT [dbo].[cuisine] ([id], [type]) VALUES (103, N'asdf')
INSERT [dbo].[cuisine] ([id], [type]) VALUES (104, N'sde.hfawe/')
INSERT [dbo].[cuisine] ([id], [type]) VALUES (105, N'asfasdfdasvc')
INSERT [dbo].[cuisine] ([id], [type]) VALUES (106, N'asdfasd')
INSERT [dbo].[cuisine] ([id], [type]) VALUES (107, N'asdfasd')
INSERT [dbo].[cuisine] ([id], [type]) VALUES (108, N'sadfasdcvz')
SET IDENTITY_INSERT [dbo].[cuisine] OFF
