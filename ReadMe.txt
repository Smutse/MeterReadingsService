Use the package manager command to build the database creation file 'dotnet ef migrations add CreateInitial'
Use the package manager command to build the database 'dotnet ef database update'

Use the below cql to populate after importing data as flat table: 


SET IDENTITY_INSERT[EnsekMeterReadingsDb].[dbo].[Accounts] ON


insert into [EnsekMeterReadingsDb].[dbo].[Accounts] ([AccountId]
,[FirstName]
,[LastName])
SELECT[AccountId]
      ,[FirstName]
      ,[LastName]

FROM[EnsekMeterReadingsDb].[dbo].[Test_Accounts]

  SET IDENTITY_INSERT[EnsekMeterReadingsDb].[dbo].[Accounts] off