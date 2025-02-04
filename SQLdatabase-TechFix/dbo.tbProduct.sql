USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbProduct]    Script Date: 11/11/2024 1:25:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbProduct](
	[pid] [int] IDENTITY(1,1) NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[pqty] [int] NOT NULL,
	[pprice] [int] NOT NULL,
	[pdescription] [varchar](150) NULL,
	[pcategory] [varchar](50) NOT NULL,
	[psuppliername] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

