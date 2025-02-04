USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbInventories]    Script Date: 11/11/2024 1:24:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbInventories](
	[inventoryid] [int] IDENTITY(1,1) NOT NULL,
	[pid] [int] NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[sid] [int] NOT NULL,
	[sname] [varchar](50) NOT NULL,
	[pqty] [int] NOT NULL,
	[quotationid] [int] NOT NULL,
	[qty] [int] NOT NULL,
	[quotationdetailid] [int] NOT NULL,
	[appdate] [date] NOT NULL,
	[totqty] [int] NOT NULL,
 CONSTRAINT [PK_tbInventories] PRIMARY KEY CLUSTERED 
(
	[inventoryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

