if not exists (select 1 from dbo.sysobjects where name = 'usd_AppliedDatabaseScript' and type = 'U') begin

	create table [dbo].[usd_AppliedDatabaseScript](
		[ScriptFile] [nvarchar](255) NOT NULL,
		[DateApplied] [datetime] NOT NULL,
		[Version] [int] NULL,
	constraint [PK_usd_AppliedDatabaseScript] primary key nonclustered 
	(
		[ScriptFile] asc
	) on [PRIMARY]
	) on [PRIMARY]

	create clustered index [IX_usd_DateApplied] ON [dbo].[usd_AppliedDatabaseScript] 
	(
		[DateApplied] asc
	) on [PRIMARY]

end