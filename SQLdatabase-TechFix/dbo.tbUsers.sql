USE [dbAmashaTechFix]
GO

/****** Object:  Table [dbo].[tbUsers]    Script Date: 11/11/2024 1:26:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbUsers](
	[userrole] [varchar](50) NOT NULL,
	[username] [varchar](50) NULL,
	[fullname] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
 CONSTRAINT [PK_tbUsers] PRIMARY KEY CLUSTERED 
(
	[userrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

