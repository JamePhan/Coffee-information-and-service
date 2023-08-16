IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='CoffeehouseSystem')
BEGIN 
CREATE DATABASE [CoffeehouseSystem]
END
GO
USE [CoffeehouseSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](20) NULL,
	[password] [varchar](20) NULL,
	[is_banned] [bit] NULL,
	[forget_code] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[name] [varchar](30) NULL,
	[phone] [varchar](15) NULL,
	[address] [nvarchar](255) NULL,
	[email] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NULL,
	[phone] [varchar](15) NULL,
	[address] [varchar](150) NULL,
	[email] [varchar](30) NULL,
	[account_id] [int] NULL,
	[avatar] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[location_id] [int] NULL,
	[date] [date] NULL,
	[groupImage_id] [int] NULL,
	[description] [text] NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[seat_count] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Following]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupImage]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[news_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[title] [varchar](100) NULL,
	[description] [text] NULL,
	[groupImage_id] [int] NULL,
	[created_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[news_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 8/15/2023 11:25:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[service_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[description] [text] NULL,
	[user_id] [int] NULL,
	[groupImage_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [varchar](150) NULL,
	[email] [varchar](30) NULL,
	[phone] [varchar](15) NULL,
	[coffeeShopName] [varchar](50) NULL,
	[account_id] [int] NULL,
	[avatar] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Waiting]    Script Date: 8/15/2023 11:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Waiting](
	[customer_id] [int] NULL,
	[waiting_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [varchar](150) NULL,
	[email] [varchar](30) NULL,
	[phone] [varchar](15) NULL,
	[coffeeShopName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[waiting_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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

INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (1, N'The heaven coffee', 1, CAST(N'2023-08-22' AS Date), 4, N'HI', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 50, CAST(10.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (2, N'The heaven coffee', 2, CAST(N'2023-08-22' AS Date), 5, N'LEEOO', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 40, CAST(12.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (3, N'Go to the moon', 3, CAST(N'2023-08-22' AS Date), 6, N'NEO', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 70, CAST(15.00 AS Decimal(10, 2)), 4)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (4, N'Coffee moon', 4, CAST(N'2023-08-22' AS Date), 7, N'Star ', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 90, CAST(20.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (5, N'Mar Coffee', 2, CAST(N'2023-08-22' AS Date), 7, N'Go to the Sky to drink coffee', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 200, CAST(30.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (7, N'test add event', NULL, CAST(N'2023-08-06' AS Date), 9, N'test add event description', CAST(N'2023-08-06T05:10:30.217' AS DateTime), CAST(N'2023-08-06T05:10:30.217' AS DateTime), 11, CAST(11.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (8, N'test add event', 2, CAST(N'2023-08-06' AS Date), 13, N'asdadsadadsadasd', CAST(N'2023-08-06T15:36:56.067' AS DateTime), CAST(N'2023-08-06T15:36:56.067' AS DateTime), 11, CAST(110.00 AS Decimal(10, 2)), 5)
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
SET IDENTITY_INSERT [dbo].[Following] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupImage] ON 

INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (4, 1)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (5, 2)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (6, 4)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (7, 3)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (9, 7)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (13, 11)
SET IDENTITY_INSERT [dbo].[GroupImage] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([image_id], [image]) VALUES (1, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (2, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (3, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (4, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (5, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (7, N'test add image url')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (11, N'url string')
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (1, N'address1', 1)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (2, N'address2', 2)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (3, N'address3', 3)
INSERT [dbo].[Location] ([location_id], [address], [user_id]) VALUES (4, N'address4', 4)
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (1, 1, N'Coffe Is Good', N'Coffee is good for health', 4, NULL)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (2, 2, N'Coffee Not Good', N'Coffee is not good for health', 5, NULL)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (3, 4, N'CoffeeHouse khai truong', N'CoffeeHouse khai truong tai Ha Noi', 6, NULL)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id], [created_date]) VALUES (4, 4, N'Go to the Moon', N'Drink Coffee at the moon', 7, NULL)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (1, 1, 2, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (2, 2, 7, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (3, 3, 8, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (4, 4, 9, 3)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (5, 5, 10, 4)
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (11, N'Payment', N'Thanh toan', 1, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (12, N'Buy 2 Get 1', N'Mua 2 tang 1', 2, 6)
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
