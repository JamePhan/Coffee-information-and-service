USE [CoffeehouseSystem]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (1, N'cuong0012', N'118', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (2, N'trancong', N'29092000', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (3, N'ad', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (4, N'jame', N'jame', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (5, N'qwer', N'qwer', 1, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (6, N'asdf', N'asdf', 1, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (7, N'monmon', N'12345', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (8, N'zinzin', N'zzzzz', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (9, N'cus1', N'zxc', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (10, N'cus2', N'zxc', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (11, N'cus3', N'zxc', 1, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (12, N'user3', N'asd', 1, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (13, N'user4', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (14, N'user5', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (15, N'cus10', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (16, N'cus11', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (17, N'cus12', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (18, N'cus13', N'asd', 0, NULL, NULL)
INSERT [dbo].[Account] ([account_id], [username], [password], [is_banned], [account_image], [forget_code]) VALUES (19, N'cus14', N'asd', 1, NULL, NULL)
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
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (1, N'Customer9', N'0392222222', N'Hanoi', N'asd@gmail.com', 9)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (2, N'Customer10', N'0392221109', N'TPHCM', N'mii@gmail.com', 10)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (3, N'Customer11', N'0392398138', N'DaNang', N'dad2a@gmail.com', 11)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (7, N'Customer15', N'0392398138', N'Hanoi', N'dada2@gmail.com', 15)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (8, N'Custa', N'0392398132', N'DaNang', N'dada@gmail.com', 16)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (9, N'Oreon', N'0392398131', N'Hanoi', N'da3da@gmail.com', 17)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (10, N'March', N'0392398182', N'TPHCM', N'dad4a@gmail.com', 18)
INSERT [dbo].[Customer] ([customer_id], [name], [phone], [address], [email], [account_id]) VALUES (11, N'Lici', N'0392398132', N'Hanoi', N'da1da@gmail.com', 19)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (1, N'Hanoi', N'qwe@gmail.com', N'1231241422', N'Coffee 1', 3)
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (2, N'DaNang', N'ss1@gmail.com', N'1771171777', N'House Coffee', 5)
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (3, N'TPHCM', N'ss2@gmail.com', N'1242244444', N'Coffee House', 6)
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (4, N'Hanoi', N'ss23@gmail.com', N'1242244411', N'Coffee High', 12)
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (5, N'Hanoi', N'ss233@gmail.com', N'1242244434', N'Coffee Low', 13)
INSERT [dbo].[User] ([user_id], [address], [email], [phone], [coffeeShopName], [account_id]) VALUES (6, N'Hanoi', N'ss24@gmail.com', N'1242244476', N'Coffee Market', 14)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Following] ON 

INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (1, 1, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (2, 2, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (3, 1, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (4, 1, 3)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (5, 2, 7)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (6, 4, 2)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (7, 5, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (8, 6, 8)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (9, 6, 3)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (10, 4, 1)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (11, 5, 9)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (12, 2, 10)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (13, 1, 11)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (14, 4, 11)
INSERT [dbo].[Following] ([following_id], [user_id], [customer_id]) VALUES (15, 4, 10)
SET IDENTITY_INSERT [dbo].[Following] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([image_id], [image]) VALUES (1, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (2, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (3, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (4, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
INSERT [dbo].[Image] ([image_id], [image]) VALUES (5, N'https://media.istockphoto.com/id/1177900338/photo/cup-of-espresso-with-coffee-beans.jpg?b=1&s=612x612&w=0&k=20&c=Kq8k4AR5xJQjiNWccTmR6txlxRSi_90qBKOq30LGGoY=')
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupImage] ON 

INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (4, 1)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (5, 2)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (6, 4)
INSERT [dbo].[GroupImage] ([groupImage_id], [image_id]) VALUES (7, 3)
SET IDENTITY_INSERT [dbo].[GroupImage] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (1, N'The heaven coffee', 1, CAST(N'2023-08-22' AS Date), 4, N'HI', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 50, CAST(10.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (2, N'The heaven coffee', 2, CAST(N'2023-08-22' AS Date), 5, N'LEEOO', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 40, CAST(12.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (3, N'Go to the moon', 3, CAST(N'2023-08-22' AS Date), 6, N'NEO', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 70, CAST(15.00 AS Decimal(10, 2)), 4)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (4, N'Coffee moon', 4, CAST(N'2023-08-22' AS Date), 7, N'Star ', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 90, CAST(20.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Event] ([event_id], [name], [location_id], [date], [groupImage_id], [description], [start_time], [end_time], [seat_count], [price], [user_id]) VALUES (5, N'Mar Coffee', 2, CAST(N'2023-08-22' AS Date), 7, N'Go to the Sky to drink coffee', CAST(N'2023-08-22T00:00:00.000' AS DateTime), CAST(N'2023-08-23T00:00:00.000' AS DateTime), 200, CAST(30.00 AS Decimal(10, 2)), 6)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (1, 1, 2, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (2, 2, 7, 7)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (3, 3, 8, 1)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (4, 4, 9, 3)
INSERT [dbo].[Schedule] ([schedule_id], [event_id], [customer_id], [ticket_count]) VALUES (5, 5, 10, 4)
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id]) VALUES (1, 1, N'Coffe Is Good', N'Coffee is good for health', 4)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id]) VALUES (2, 2, N'Coffee Not Good', N'Coffee is not good for health', 5)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id]) VALUES (3, 4, N'CoffeeHouse khai truong', N'CoffeeHouse khai truong tai Ha Noi', 6)
INSERT [dbo].[News] ([news_id], [user_id], [title], [description], [groupImage_id]) VALUES (4, 4, N'Go to the Moon', N'Drink Coffee at the moon', 7)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (11, N'Payment', N'Thanh toan', 1, 5)
INSERT [dbo].[Service] ([service_id], [name], [description], [user_id], [groupImage_id]) VALUES (12, N'Buy 2 Get 1', N'Mua 2 tang 1', 2, 6)
SET IDENTITY_INSERT [dbo].[Service] OFF
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
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([location_id], [plusCode], [user_id]) VALUES (1, N'plusCode1', 1)
INSERT [dbo].[Location] ([location_id], [plusCode], [user_id]) VALUES (2, N'plusCode2', 2)
INSERT [dbo].[Location] ([location_id], [plusCode], [user_id]) VALUES (3, N'plusCode3', 3)
INSERT [dbo].[Location] ([location_id], [plusCode], [user_id]) VALUES (4, N'plusCode4', 4)
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
