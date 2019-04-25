GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comments](
	[Id] [uniqueidentifier] NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[PublishDate] [datetime] NOT NULL,
	[IdNew] [uniqueidentifier] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](120) NOT NULL,
	[MainText] [nvarchar](1024) NOT NULL,
	[DetailText] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[PublishTime] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
