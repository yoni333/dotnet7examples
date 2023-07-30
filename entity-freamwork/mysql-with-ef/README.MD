to use nysql open source connector of polemo 
https://mysqlconnector.net/tutorials/connect-to-mysql/
dotnet add package MySqlConnector
for working togher with entityFreamwork add this:
dotnet add package Pomelo.EntityFrameworkCore.MySql

# global migration steps  

1. dotnet tool install --global dotnet-ef
2. dotnet add package Microsoft.EntityFrameworkCore.Design
3. dotnet ef migrations add InitialCreate 
4. dotnet ef database update
   
## for multiple context database
1. comment all the 'builder.Services' you not use in program.cs
2. dotnet ef migrations add InitialCreate -c <context class etc BlogDataContext>
