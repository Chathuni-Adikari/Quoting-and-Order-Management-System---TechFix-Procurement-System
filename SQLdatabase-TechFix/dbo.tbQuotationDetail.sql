USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbQuotationDetail]    Script Date: 11/11/2024 1:26:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbQuotationDetail](
	[quotationdetailid] [int] IDENTITY(1,1) NOT NULL,
	[quotationid] [int] NOT NULL,
	[pid] [int] NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[sid] [int] NOT NULL,
	[sname] [varchar](50) NOT NULL,
	[price] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[discount] [int] NOT NULL,
	[total] [int] NOT NULL,
	[qrdate] [date] NOT NULL,
	[appdate] [date] NOT NULL,
 CONSTRAINT [PK_tbQuotationDetail] PRIMARY KEY CLUSTERED 
(
	[quotationdetailid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

