# dotnet 7 webapi repository with explain examples

## how to start solution and project with templates in VS code in the current folder  

1. dotnet new sln
2. dotnet new webapi -f net7.0
3. dotnet sln add .
4. dotnet tool install -g dotnet-aspnet-codegenerator
5. dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
6. dotnet aspnet-codegenerator  controller -name Auth -outDir ./Controllers  
7. dotnet new gitignore

## ./Controllers/UsersController.cs -- advanced examples how to use [Route] attribute  

## working with mongoDb

official tutorial : https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio

[link to official tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-7.0&tabs=visual-studio)

