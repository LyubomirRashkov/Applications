## :houses: Real Estates Advisor

### :eyeglasses: Overview

This is an application that can be used to filter real estates and display only these that meet the desired criteria.

It works with [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed on the same machine on which the app is running, but if you want to use a remote server or another relational database management system it’s OK – you just need to review the code and ”fix” it where it’s needed.

When you start the application for the first time it will automatically make its database and fill it with data. The main table contains more than 23 000 records.

Thanks to the bulk operations in the code filling the database with records is very fast – it will take no more than 30 seconds :stopwatch: (30 seconds for 23 000 records)!

### :bricks:  Structure

The application is divided into several layers:
  +	Data Layer – contains datasets, migrations, dbContext and models
  +	Service Layer – contains business logic and main functionality
  +	UI Layer – contains the user interface (console interface)

### :hammer_and_wrench: Built with

It is built using:
  +	[.NET5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
  +	[SQL Server Express](https://en.wikipedia.org/wiki/SQL_Server_Express)
  +	[SQL Server Management Studio](https://docs.microsoft.com/bg-bg/sql/ssms/sql-server-management-studio-ssms?view=sql-server-ver15)
  +	[NuGet Packages](https://www.nuget.org/):
    +	Microsoft.EntityFrameworkCore (version 5.0.17)
    +	Microsoft.EntityFrameworkCore.Design (version 5.0.17)
    +	Microsoft.EntityFrameworkCore.SqlServer (version 5.0.17)
    +	System.Text.Json (version 6.0.5)
    +	AutoMapper (version 11.0.1)
  +	LINQ
  +	OOP
  +	SOLID
  +	Design patterns (Singleton, Command Pattern)

### :pushpin:  ER Diagram

![изображение](https://user-images.githubusercontent.com/82647282/180989275-c666e77b-e771-4df7-a025-67e053d424dd.png)

### :memo: Note

Datasets are from 18/03/2021, so they are not up to date!
