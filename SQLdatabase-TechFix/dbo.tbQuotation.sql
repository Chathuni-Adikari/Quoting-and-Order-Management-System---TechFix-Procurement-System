USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbQuotation]    Script Date: 11/11/2024 1:25:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbQuotation](
	[quotationid] [int] IDENTITY(1,1) NOT NULL,
	[qrdate] [date] NOT NULL,
	[pid] [int] NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[sid] [int] NOT NULL,
	[sname] [varchar](50) NOT NULL,
	[qty] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[quotationid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

