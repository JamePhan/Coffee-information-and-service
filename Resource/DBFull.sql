IF EXISTS (SELECT * FROM sys.databases WHERE name='CoffeehouseSystem')
BEGIN
DROP DATABASE [CoffeehouseSystem]
CREATE DATABASE [CoffeehouseSystem] COLLATE Latin1_General_100_CI_AI_SC_UTF8
END
ELSE
BEGIN 
CREATE DATABASE [CoffeehouseSystem] COLLATE Latin1_General_100_CI_AI_SC_UTF8
END
GO
USE [CoffeehouseSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](20) NULL,
	[password] [nvarchar](20) NULL,
	[is_banned] [bit] NULL,
	[forget_code] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[name] [nvarchar](30) NULL,
	[phone] [nvarchar](15) NULL,
	[address] [nvarchar](255) NULL,
	[email] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[banner_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[image_url] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[banner_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NULL,
	[phone] [nvarchar](15) NULL,
	[address] [nvarchar](150) NULL,
	[email] [nvarchar](30) NULL,
	[account_id] [int] NULL,
	[avatar] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[location_id] [int] NULL,
	[date] [date] NULL,
	[groupImage_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[seat_count] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Following]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Following](
	[following_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[customer_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[following_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupImage]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupImage](
	[groupImage_id] [int] IDENTITY(1,1) NOT NULL,
	[image_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[groupImage_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[image_id] [int] IDENTITY(1,1) NOT NULL,
	[image] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[location_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [nvarchar](max) NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[news_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[title] [nvarchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[groupImage_id] [int] NULL,
	[created_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[news_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [int] NULL,
	[customer_id] [int] NULL,
	[ticket_count] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[service_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[user_id] [int] NULL,
	[groupImage_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [nvarchar](150) NULL,
	[email] [nvarchar](30) NULL,
	[phone] [nvarchar](15) NULL,
	[coffeeShopName] [nvarchar](50) NULL,
	[account_id] [int] NULL,
	[avatar] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Waiting]    Script Date: 8/17/2023 12:42:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Waiting](
	[customer_id] [int] NULL,
	[waiting_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [nvarchar](150) NULL,
	[email] [nvarchar](30) NULL,
	[phone] [nvarchar](15) NULL,
	[coffeeShopName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[waiting_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (1, N'cuong0012', N'118', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (2, N'trancong', N'29092000', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (3, N'ad', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (4, N'jame', N'jame', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (5, N'qwer', N'qwer', 1, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (6, N'asdf', N'asdf', 1, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (7, N'monmon', N'12345', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (8, N'zinzin', N'zzzzz', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (9, N'cus1', N'zxc', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (10, N'cus2', N'zxc', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (11, N'cus3', N'zxc', 1, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (12, N'user3', N'asd', 1, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (13, N'user4', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (14, N'user5', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (15, N'cus10', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (16, N'cus11', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (17, N'cus12', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (18, N'cus13', N'asd', 0, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [forget_code]) VALUES (19, N'cus14', N'asd', 1, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([admin_id], [account_id], [name], [phone], [address], [email]) VALUES (1, 4, N'Jame', N'0999299999', N'Hanoi', N'ptt@gmail.com')
INSERT [dbo].[Admin] ([admin_id], [account_id], [name], [phone], [address], [email]) VALUES (2, 7, N'MonMon', N'1234566677', N'Hanoi', N'mon@gmail.com')
INSERT [dbo].[Admin] ([admin_id], [account_id], [name], [phone], [address], [email]) VALUES (3, 8, N'ZinZin', N'0019121444', N'TPHCM', N'asdf@gmail.com')
INSERT [dbo].[Admin] ([admin_id], [account_id], [name], [phone], [address], [email]) VALUES (4, 1, N'Cuong', N'0123441323', N'Hanoi', N'cuong@gmail.com')
INSERT [dbo].[Admin] ([admin_id], [account_id], [name], [phone], [address], [email]) VALUES (5, 2, N'TranCuong', N'0132113567', N'Hanoi', N'cuongx2@gmail.com')
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Banner] ON 

INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (1, 1, N'https://file.hstatic.net/1000075078/file/_kh_9431__1__e19a7a49963245b39b280271da3cd9fb_master.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (2, 2, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT91ZThxE5QJgeWxNOsWMhPkzd-Dud6ewjlrTFuoYVOWfw1R0FJThVX3T1FLuppOQ4XUHU&usqp=CAU')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (3, 3, N'https://file.hstatic.net/1000075078/file/grandview2_a48a6f2b6495492180138bfd09d22bb3_master.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (4, 4, N'https://file.hstatic.net/1000075078/file/_kh_9431__1__e19a7a49963245b39b280271da3cd9fb_master.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (5, 5, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT91ZThxE5QJgeWxNOsWMhPkzd-Dud6ewjlrTFuoYVOWfw1R0FJThVX3T1FLuppOQ4XUHU&usqp=CAU')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (6, 6, N'https://danviet.mediacdn.vn/2021/4/13/the-coffee-house-khong-nhuong-nguyen-1618324271848672760365-crop-1618324294521246672359.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (7, 7, N'https://freedesignfile.com/upload/2019/05/coffee-latte-banners-vector-02.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (20, 1, N'https://i.pinimg.com/originals/07/7c/f6/077cf6fa95ad43686d492a150d428322.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (21, 1, N'https://image.freepik.com/free-vector/two-retro-coffee-banners_1114-128.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (22, 3, N'https://4.bp.blogspot.com/-WX8A9rnsXns/XJUnt5uQdPI/AAAAAAAAk_M/u33CPMq50rA6KahS09sK4vXWka9dtpfeACLcBGAs/s1600/preview%2B%25281%2529.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (23, 4, N'https://th.bing.com/th/id/OIP.dm1j4RvQDBYBt2zCdOru7QHaFj?pid=ImgDet&w=800&h=600&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (24, 4, N'https://th.bing.com/th/id/OIP.Qc0wgdN59ill403cVF4nEAHaHa?pid=ImgDet&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (25, 3, N'https://images.template.net/wp-content/uploads/2017/02/01190027/Coffee-Shop-Flyer.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (26, 5, N'https://img.freepik.com/free-psd/coffee-shop-concept-banner-template_23-2148565405.jpg?size=626&ext=jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (27, 6, N'https://image.freepik.com/free-vector/coffee-shop-squared-flyer-template_23-2149000900.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (28, 7, N'https://i.pinimg.com/originals/ee/0b/81/ee0b811d8e4b8688435309fe9caa44eb.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (29, 1, N'https://img.freepik.com/premium-psd/coffee-time-social-media-story-post_700532-29.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (30, 6, N'https://th.bing.com/th/id/OIP.0SVTvfFbpSgWy0UKzg78swHaHa?pid=ImgDet&w=626&h=626&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (31, 6, N'https://th.bing.com/th/id/OIP.5qeVP5T5NvjlpbiUYhwQWgHaHa?pid=ImgDet&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (32, 6, N'https://th.bing.com/th/id/OIP.1pUbTLkUKhBm6n2kKADDYwHaHa?pid=ImgDet&w=664&h=664&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (33, 6, N'https://th.bing.com/th/id/OIP.RH8zE-rHZBKp127GebGrfQHaQd?pid=ImgDet&w=486&h=1080&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (34, 7, N'https://th.bing.com/th/id/R.b7b05569566af52e91caee0a45131b21?rik=LIbSYJoPQqhNPA&riu=http%3a%2f%2fiamdesigning.com%2fblog%2fwp-content%2fuploads%2f2014%2f10%2f1620.jpg&ehk=EOSXUuwpb6Iy72m39D3C%2fTxB%2b0H%2fyQqp92DZZ5rdUoA%3d&risl=&pid=ImgRaw&r=0')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (35, 7, N'https://th.bing.com/th/id/OIP.lDyry-lSiMalIlz4MOo15QHaFj?pid=ImgDet&w=900&h=675&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (36, 7, N'https://th.bing.com/th/id/OIP.m3UbeVfOaP9Zu1TutiXYCgHaGd?pid=ImgDet&w=480&h=419&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (37, 3, N'https://th.bing.com/th/id/OIP.MMK5-amIrVnFbg_5r6AwgwHaGr?pid=ImgDet&w=400&h=361&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (38, 5, N'https://th.bing.com/th/id/OIP.d3KKgt_1caglJCi-Sylm0AHaFj?pid=ImgDet&w=800&h=600&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (39, 5, N'https://thumbs.dreamstime.com/b/design-menu-cafe-shops-coffee-blurred-backg-template-shop-restaurant-shop-background-color-top-view-cup-78772678.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (40, 2, N'https://th.bing.com/th/id/OIP.Tte01fUJ_HATdwHupixNKAHaGu?pid=ImgDet&w=500&h=454&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (41, 2, N'https://i.pinimg.com/736x/13/49/46/1349467a8ea965ad3d83752699f1b121.jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (42, 2, N'https://th.bing.com/th/id/OIP.deeLOklDCuS3XfluTCAGggAAAA?pid=ImgDet&w=420&h=238&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (43, 4, N'https://th.bing.com/th/id/OIP.d_yRMMDYHkHkUQT_Q_72yAAAAA?pid=ImgDet&rs=1')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (44, 4, N'https://img.freepik.com/free-psd/international-coffee-day-banner-concept-template_23-2148652747.jpg?size=626&ext=jpg')
INSERT [dbo].[Banner] ([banner_id], [user_id], [image_url]) VALUES (45, 4, N'https://image.freepik.com/free-psd/international-coffee-day-flyer-concept-template_23-2148652751.jpg')
SET IDENTITY_INSERT [dbo].[Banner] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (1, N'Customer9', N'0392222222', N'Hanoi', N'asd@gmail.com', 9, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (2, N'Customer10', N'0392221109', N'TPHCM', N'mii@gmail.com', 10, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (7, N'Customer15', N'0392398138', N'Hanoi', N'dada2@gmail.com', 15, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (8, N'Custa', N'0392398132', N'DaNang', N'dada@gmail.com', 16, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (9, N'Oreon', N'0392398131', N'Hanoi', N'da3da@gmail.com', 17, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (10, N'March', N'0392398182', N'TPHCM', N'dad4a@gmail.com', 18, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id], [avatar]) VALUES (11, N'Lici', N'0392398132', N'Hanoi', N'da1da@gmail.com', 19, N'https://www.publicdomainpictures.net/pictures/270000/nahled/avatar-people-person-business-u.jpg')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (1, N'The heaven coffee', 1, CAST(N'2023-08-22' AS Date), 4, N'A San Diego roasting startup called Talitha Coffee is cranking out colorfully packaged, fresh-roasted, specialty-grade beans. Yet behind each bag is a deeper commitment to creating opportunities and support systems for survivors of sex trafficking. Talitha Coffee Co-Founder Jenny Barber has been working to combat human trafficking through nonprofit work for more than a decade, witnessing survivors running into similar barriers to education and employment.', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 50, CAST(10.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (2, N'The heaven coffee', 2, CAST(N'2023-08-22' AS Date), 5, N'The company has created what it calls the Talitha Survivor Support Network, which involves partnerships with numerous nonprofits. When a position is available within Talitha Coffee, the company will notify the network, then get referrals. Barber said the young company has helped provide career opportunities for three survivors so far, while the goal is to scale bigger to support more people. ', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 40, CAST(12.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (3, N'Go to the moon', 3, CAST(N'2023-08-22' AS Date), 6, N'“We have connections with support groups, counseling, tangible needs, housing, helping with budgeting, medical care, even as far as scholarships, if they want to go down that route to go to school,” she said. “We’re making sure they have a successful support system.” Robert Barber, an on-staff Q Grader and the rest of a small production team are currently overseeing roasting operations at a roastery near downtown San Diego, according to Jenny Barber. Talitha was just named one of the host venues of the U.S. Coffee Championships preliminary events.', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 70, CAST(15.00 AS Decimal(10, 2)), 4)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (4, N'Coffee moon', 4, CAST(N'2023-08-22' AS Date), 7, N'Jenny Barber said the business has focused on teamwork and collaboration, especially given its dual focuses of quality coffee and support services. “It’s not just about what you bring to the table,” she said. “It’s about surrounding yourself with people who are excellent in what they do who can fill in the gaps where you have weakness.” Talitha Coffee is launching with multiple core blends, single-origin beans and nitro cold brew kegs. They plan to reach the channels of wholesale, grocery and hospitality, while also offering monthly subscriptions direct to consumers. “We want to give people an easy way to get involved with combatting trafficking,” Barber said. “It’s as simple as, you can be drinking a cup of coffee and making the difference in the lives of survivors.”', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 90, CAST(20.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (5, N'Mar Coffee', 2, CAST(N'2023-08-22' AS Date), 7, N'Go to the Sky to drink coffee', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 200, CAST(30.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (7, N'Slow coffee ', 2, CAST(N'2023-08-06' AS Date), 9, N'Slow Coffee: Our Favourite Brewing Methods Embracing slow coffee can transform the way you enjoy your favourite brew every day. If you’re unfamiliar with the concept of slow coffee or are looking for the best way to brew your coffee at home, here are the three different slow coffee methods that we love.', CAST(N'2023-08-06T05:10:30.217' AS DateTime), CAST(N'2023-08-06T05:10:30.217' AS DateTime), 11, CAST(11.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (8, N'Artsy coffee mugs ', 2, CAST(N'2023-08-06' AS Date), 13, N'Since their inception in 1511, coffee houses provided a space in which communities could come together. In modern times, however, these businesses more commonly offer just a means of caffeination. This leads to missed opportunities to earn some extra income and boost customer engagement by hosting special events.', CAST(N'2023-08-06T15:36:56.067' AS DateTime), CAST(N'2023-08-06T15:36:56.067' AS DateTime), 12, CAST(110.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (9, N'Board Game Nights', 7, CAST(N'2023-08-02' AS Date), 14, N'This fun and interactive event concept encourages customers to stop by with friends or family to participate in an unusual coffee shop activity while enjoying tasty beverages.', CAST(N'2023-08-02T15:36:56.067' AS DateTime), CAST(N'2023-08-02T15:36:56.067' AS DateTime), 500, CAST(100.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (10, N'Public Cuppings', 3, CAST(N'2023-09-02' AS Date), 15, N'Engage your customers with a public cupping of several coffees you currently serve. This type of event provides a great way to share your product while encouraging interested coffee consumers to dive into a process used worldwide in the coffee community. Another key benefit of this type of event is the open invitation, which means you don’t need to close the shop to normal walk-in business. Customers coming in to simply buy a coffee may even join your cupping event and learn more about your product.', CAST(N'2023-09-02T15:36:56.067' AS DateTime), CAST(N'2023-09-02T15:36:56.067' AS DateTime), 100, CAST(110.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (11, N'Open Mic Performances', 5, CAST(N'2023-11-02' AS Date), 16, N'Comedy, music, and poetry all offer ways to incorporate open mic performances into your shop’s event schedule. Open mic events can attract performers and spectators alike while boosting sales and foot traffic.', CAST(N'2023-11-02T15:36:56.067' AS DateTime), CAST(N'2023-11-03T15:36:56.067' AS DateTime), 200, CAST(110.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (12, N'Latte Art Competitions', 4, CAST(N'2023-12-02' AS Date), 17, N'Turn your event into a truly community affair by hosting a latte art competition with your fellow baristas and cafe owners.', CAST(N'2023-12-02T15:36:56.067' AS DateTime), CAST(N'2023-12-03T15:36:56.067' AS DateTime), 1000, CAST(15.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (13, N'Collaborative Pop-Up Events With Local Vendors', 1, CAST(N'2024-12-02' AS Date), 18, N'Invite your favorite tea shop, chocolatier, or other local vendor to host a pop-up event in your cafe. Make the event more special by offering custom drinks or other menu items that either feature the vendor’s products or take inspiration from them.', CAST(N'2024-12-02T15:36:56.067' AS DateTime), CAST(N'2024-12-03T15:36:56.067' AS DateTime), 111, CAST(15.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (14, N'Creative Workshops', 3, CAST(N'2023-02-02' AS Date), 19, N'Depending on the size of your space, hosting workshops can benefit your business as well as local creatives. Workshops focused on activities like wreath-making, painting, or other creative endeavors can appeal to customers looking to learn something new.', CAST(N'2024-02-02T15:36:56.067' AS DateTime), CAST(N'2024-02-03T15:36:56.067' AS DateTime), 190, CAST(12.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (15, N'Venue Rentals', 5, CAST(N'2023-04-02' AS Date), 20, N'As a coffee shop owner, you probably invested considerable time and effort into making your business a space in which customers want to spend time. Why not capitalize on that appealing environment by renting your space out as a special event venue? Coffee shops can make ideal event spaces because they offer a unique atmosphere along with a supply of beverages and small bites.', CAST(N'2024-04-02T15:36:56.067' AS DateTime), CAST(N'2024-04-03T15:36:56.067' AS DateTime), 200, CAST(11.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[Following] ON 

INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (1, 1, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (2, 2, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (3, 1, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (5, 2, 7)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (6, 4, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (7, 5, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (8, 6, 8)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (10, 4, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (11, 5, 9)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (12, 2, 10)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (13, 1, 11)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (14, 4, 11)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (15, 4, 10)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (16, 1, 10)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (17, 2, 11)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (18, 4, 8)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (19, 6, 8)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (20, 4, 7)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (21, 4, 9)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (35, 1, 8)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (36, 1, 9)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (37, 5, 1)
SET IDENTITY_INSERT [dbo].[Following] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupImage] ON 

INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (4, 1)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (5, 2)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (6, 4)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (7, 3)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (9, 7)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (13, 11)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (14, 5)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (15, 12)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (16, 13)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (17, 14)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (18, 15)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (19, 16)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (20, 17)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (21, 18)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (22, 19)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (23, 20)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (24, 21)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (25, 22)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (26, 23)
SET IDENTITY_INSERT [dbo].[GroupImage] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([image_id], [image]) VALUES (1, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (2, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (3, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (4, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (5, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (7, N'https://dailycoffeenews.com/wp-content/uploads/2023/07/acaia-woc-athens-7.jpg')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (11, N'https://dailycoffeenews.com/wp-content/uploads/2023/07/acaia-woc-athens-6.jpg')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (12, N'https://www.trendingus.com/wp-content/uploads/2021/04/2553a7077d79dc73ef17a63ef6d798e7.jpeg')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (13, N'https://th.bing.com/th/id/OIP.4AkaPgBCAPyI2xW0zpbBXQHaEK?pid=ImgDet&w=1600&h=900&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (14, N'https://th.bing.com/th/id/OIP.e8sCcJEm8ZazYpseru2zvAHaE6?pid=ImgDet&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (15, N'https://th.bing.com/th/id/R.9f7f757559ac185935b585cbcc673b1a?rik=iaTsYfJw5hFkAQ&pid=ImgRaw&r=0')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (16, N'https://th.bing.com/th/id/R.aaafb3b31ce64d34f36e97c319af5da2?rik=VjMLs%2bhhuyPkbA&pid=ImgRaw&r=0')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (17, N'https://th.bing.com/th/id/OIP.QS-bdQ-PF6cICX0meTqn5QHaJ3?pid=ImgDet&w=500&h=666&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (18, N'https://th.bing.com/th/id/R.fc206f2d337239f34dffa931f077b239?rik=OISx%2fvypyy2HAQ&pid=ImgRaw&r=0')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (19, N'https://th.bing.com/th/id/OIP.JeBNrAnjlYUoNp-NF8svkQHaEo?pid=ImgDet&w=960&h=600&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (20, N'https://i.pinimg.com/originals/3c/f5/ca/3cf5ca05ac6b0b9296cb1343ebbb2aa4.jpg')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (21, N'https://th.bing.com/th/id/OIP.m1_8rE-neKICC_2EJvZiOAHaLH?pid=ImgDet&w=1020&h=1530&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (22, N'https://th.bing.com/th/id/OIP.bMRYh01YDpK4IeXS9W4kpgHaKo?pid=ImgDet&w=500&h=718&rs=1')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (23, N'https://i.pinimg.com/originals/ba/50/36/ba5036d4406005c83ad8640aa92434b6.jpg')
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (1, N'Hanoi', 1)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (2, N'DaNang', 2)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (3, N'TPHCM', 3)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (4, N'Hanoi', 4)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (5, N'Hanoi', 5)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (6, N'Hanoi', 6)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (7, N'DaNang', 7)
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (1, 1, N'Coffe Is Good', N'Coffee is good for health', 4, CAST(N'2023-08-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (2, 2, N'Coffee Not Good', N'Coffee is not good for health', 5, CAST(N'2023-08-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (3, 4, N'CoffeeHouse khai truong', N'CoffeeHouse khai truong tai Ha Noi', 6, CAST(N'2023-08-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (4, 4, N'Go to the Moon', N'Drink Coffee at the moon', 7, CAST(N'2023-08-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (5, 5, N'Slow coffee ', N'Apart from enjoying a nice cup of coffee, your attendees can also have meaningful conversations with strangers which can possibly later lead to new business or career opportunities. So be sure to take the time to create a pleasant and attractive coffee break environment where people can unwind and enjoy each other. To help you accomplish this task, we’ve put together a few interesting ideas you may want to consider. ', 9, CAST(N'2023-07-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (6, 6, N'Coffee pin gifts', N'Engage your customers with a public cupping of several coffees you currently serve. This type of event provides a great way to share your product while encouraging interested coffee consumers to dive into a process used worldwide in the coffee community. Another key benefit of this type of event is the open invitation, which means you don’t need to close the shop to normal walk-in business. Customers coming in to simply buy a coffee may even join your cupping event and learn more about your product.', 13, CAST(N'2023-09-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (7, 1, N'Acaia Tightens Up Coffee Dosing with Nano and Orion’s Belt', N'In Athens, the company also revealed a separate metaphorical belt just for the Nano model. The Orion’s Belt is a wireless connectivity system that will allow as many as nine Orion Nano dosers to work together. The company told DCN that such synchronization can create what is essentially an “an on-demand coffee blending station” for cuppings, single-cup doses or other small-volume applications. ', 14, CAST(N'2023-08-23' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (8, 7, N'Honduras-Born Lenca Coffee Opens Farm-to-Car Shop in Pennsylvania', N'cross the room from Lenca’s Sivetz roaster and sacks of green coffee, a Bunn brewer generates batch brews while a Fiorenzato F64 grinder and SanRemo Zoe espresso machine power the coffee bar. Lenca Coffee Roasters Founder Emilio Garcia is currently acting as lead barista. “Drive-through coffee is kind of new for me,” Garcia told DCN. “I’m a farmer, and then I became a roaster and now I’m coming to the world of the barista, but I’ll depend on people who know — baristas — to be able to operate.”', 15, CAST(N'2023-07-23' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (9, 1, N'All Cleveland Coffee Co. Gives Some Back', N'Two beefy Cleveland sports figures are helping to provide sustenance to people in need in the greater Cleveland area through a new coffee venture called All Cleveland Coffee Co. Co-founders and partners in the mission-driven coffee enterprise are former Cleveland Browns lineman and recent NFL Hall of Fame inductee Joe Thomas, as well as Cleveland native and mixed martial arts fighter Stipe Miocic. Launched this month with direct-to-consumer sales of 12-ounce bags through a new online store and select retail partners, the company is donating the equivalent of three meals to the Greater Cleveland Food Bank for every bag sold.', 16, CAST(N'2023-08-23' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (10, 1, N'Ostara Coffee Roasters is Spicing Up Texas Cold Brew', N'Led by two women in Fort Worth, Texas, a roasting startup called Ostara Coffee Roasters is making a name for itself through creative branding, a fiercely local focus, freshly roasted coffees and bottled cold brews. After becoming friends at St. Edwards University in Austin, Natalie Willard and Valerie Mejia knew they wanted to start a business together, but they weren’t totally sure what kind until they each got bit by the coffee bug. “We joke that we wanted unlimited access to coffee and that’s why we decide to pursue coffee,” Willard recently told DCN. Willard took some SCA-accredited coursework on roasting in Dallas while also diving into roasting craft and theory through online and print sources, as well as old-fashioned trial-and-error. The coffee learning built upon the combined beverage experience of the two women — Willard previously worked for Blanco-based Real Ale Brewing Company, and Mejia for Austin’s Buddha’s Brew Kombucha.', 17, CAST(N'2022-01-23' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (12, 1, N'10 Best Coffee Shops In Athens', N'True confession: We love traditional Greek food but we don’t like traditional Greek coffee – it’s strong, muddy and filled with low-quality grinds. Luckily for us, Greece is experiencing a coffee renaissance. Prior to our initial visit to Greece in 2018, we had serious doubts about the coffee situation in Athens. After all, the city is famous for the Acropolis and souvlaki, not for flat whites and cappuccinos. After discovering a handful of cafes serving specialty coffee, we suspected that the coffee scene might be percolating but it could have gone either way.', 18, CAST(N'2023-08-23' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (13, 1, N'20 Best Copenhagen Coffee Shops And Cafes', N'We created this comprehensive guide showcasing the best Copenhagen coffee shops after drinking flat whites and pour overs all over the Danish capital. But we didn’t stop there! We included a half dozen Copenhagen cafes that serve specialty coffee in addition to freshly baked laminated pastries.', 19, CAST(N'2023-07-22' AS Date))
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (14, 7, N'Cafe and bar reveal opening date as they take over former high street bank', N'A new café and bar will open a town’s former high street bank later this month. The Candelo Lounge will open at the former Barclays bank site on August 31 and will serve breakfast, brunch, lunch, dinner and drinks Loungers plc, the West Country-based group, will transform the former bank at 105 High Street ready for the opening at the end of the month. READ MORE: Curry house crowned ''local restaurant of year'' at national awards Earlier this year, the Oxford Mail reported that Lounges applied to put new signs on the former bank which has been closed since July 2021. Gemma Irwin, community manager at Loungers, says: “We’re so looking forward to opening the doors of Candelo Lounge at the end of the month. “We hope our family-friendly environment and top-notch food and drink offering will prove popular with local residents.', 20, CAST(N'2023-11-22' AS Date))
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (1, 1, 2, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (2, 2, 7, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (3, 3, 8, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (4, 4, 9, 3)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (5, 5, 10, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (10, 5, 11, 9)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (11, 3, 1, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (12, 1, 8, 5)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (13, 1, 7, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (14, 1, 11, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (15, 1, 10, 3)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (16, 2, 2, 3)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (17, 2, 1, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (18, 2, 8, 10)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (19, 2, 9, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (20, 2, 10, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (21, 2, 11, 2)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (22, 3, 1, 12)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (23, 3, 8, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (24, 3, 9, 11)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (25, 3, 10, 22)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (26, 3, 11, 9)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (27, 4, 1, 9)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (28, 4, 2, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (29, 4, 7, 23)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (30, 4, 8, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (31, 4, 10, 22)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (32, 4, 11, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (33, 5, 1, 4)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (34, 5, 2, 13)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (35, 5, 7, 2)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (36, 5, 8, 12)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (37, 5, 9, 10)
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (11, N'Payment', N'Thanh toan', 1, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (12, N'Buy 2 Get 1', N'Mua 2 tang 1', 2, 6)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (13, N'Exchange 10 cups for 1 cup of coffee', N'Doi 10 vo lon lay 1 ly cafe', 3, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (14, N'Smooth draw for buyers', N'Boc tham trung thuong', 1, 4)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (15, N'Payment', N'Thanh toan', 1, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (17, N'Exchange bottles for drinks for no charge', N'Doi vo chai lay nuoc mien phi', 4, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (18, N'Earn card upgrade points', N'Tich diem the', 5, 7)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (19, N'Use the invoice as a discount voucher', N'Dung vocher giam gia', 6, 9)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (20, N'Free upsize', N'Mien phi upsize', 7, 13)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (21, N'Give combos to guests', N'Tang combo cho khach', 1, 14)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (22, N'Return the wrong product in the menu', N'Doi tra lai san pham cho khach ', 3, 15)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (23, N'Create a membership card', N'Tao the  thanh vien', 7, 16)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (24, N'Golden hour deals', N'Uu dai gio vang', 7, 17)
SET IDENTITY_INSERT [dbo].[Service] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (1, N'Hanoi', N'qwe@gmail.com', N'1231241422', N'Coffee 1', 3, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (2, N'DaNang', N'ss1@gmail.com', N'1771171777', N'House Coffee', 5, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (3, N'TPHCM', N'ss2@gmail.com', N'1242244444', N'Coffee House', 6, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (4, N'Hanoi', N'ss23@gmail.com', N'1242244411', N'Coffee High', 12, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (5, N'Hanoi', N'ss233@gmail.com', N'1242244434', N'Coffee Low', 13, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (6, N'Hanoi', N'ss24@gmail.com', N'1242244476', N'Coffee Market', 14, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id], [avatar]) VALUES (7, N'DaNang', N'dad2a@gmail.com', N'0392398138', N'Customer11', 11, N'https://t4.ftcdn.net/jpg/02/70/72/91/360_F_270729106_g36ekHjIw28VYxoX7CWpctG6jDSh7YIe.jpg')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_Account]
GO
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_Banner_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_Banner_User]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Account]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_GroupImage] FOREIGN KEY([groupImage_id])
REFERENCES [dbo].[GroupImage] ([groupImage_id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_GroupImage]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Location] FOREIGN KEY([location_id])
REFERENCES [dbo].[Location] ([location_id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Location]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_User]
GO
ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_Customer]
GO
ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_User]
GO
ALTER TABLE [dbo].[GroupImage]  WITH CHECK ADD  CONSTRAINT [FK_GroupImage_Image] FOREIGN KEY([image_id])
REFERENCES [dbo].[Image] ([image_id])
GO
ALTER TABLE [dbo].[GroupImage] CHECK CONSTRAINT [FK_GroupImage_Image]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_User]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_GroupImage] FOREIGN KEY([groupImage_id])
REFERENCES [dbo].[GroupImage] ([groupImage_id])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_GroupImage]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_User]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Customer]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Event] FOREIGN KEY([event_id])
REFERENCES [dbo].[Event] ([event_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Event]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_GroupImage] FOREIGN KEY([groupImage_id])
REFERENCES [dbo].[GroupImage] ([groupImage_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_GroupImage]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Account]
GO
ALTER TABLE [dbo].[Waiting]  WITH CHECK ADD  CONSTRAINT [FK_Waiting_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Waiting] CHECK CONSTRAINT [FK_Waiting_Customer]
GO
