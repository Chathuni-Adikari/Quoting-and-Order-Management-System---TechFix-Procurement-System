USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbSupplier]    Script Date: 11/11/2024 1:26:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbSupplier](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[sname] [varchar](50) NOT NULL,
	[sphone] [varchar](50) NOT NULL,
	[semail] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbSupplier] PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

