USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbOrder]    Script Date: 11/11/2024 1:25:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbOrder](
	[orderid] [int] IDENTITY(1,1) NOT NULL,
	[sid] [int] NOT NULL,
	[sname] [varchar](50) NOT NULL,
	[pid] [int] NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[pprice] [int] NOT NULL,
	[orderqty] [int] NOT NULL,
	[ordertotal] [int] NOT NULL,
	[date] [date] NOT NULL,
 CONSTRAINT [PK_tbOrder] PRIMARY KEY CLUSTERED 
(
	[orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

