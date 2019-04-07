#create a class libray
dotnet new classlib -n SimpleAction.Common -o SimpleAction.Common

#reference class library into project
dotnet add src/SimpleAction.Api/SimpleAction.Api.csproj reference src/SimpleAction.Common/SimpleAction.Common.csproj


#run rabbitMQ inside docker on port 5672
docker run -p 5672:5672 rabbitmq

inside the service folder (common) we create our fluent API which helps quickly to define type of messages.
public static async Task Main(string[] args) starting C#7.0

#-d for background
docker run -d -p 27017:27017

"connectionString": "mongodb://user:password@localhost:27017"


#JWT nuget packages
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.IdentityModel.tokens

#run a web api in custom port
dotnet run --urls "https://*:5051"

#using curl add a user through a post request
 curl localhost:5000/users -X POST -H "content-type: application/json" -d '{"email":"dummy@gmail.com", "name":"mkdummy", "password":"dummysecret"}'
 curl localhost:5000/login -X POST -H "content-type: application/json" -d '{"email":"dummy@gmail.com", "password":"dummysecret"}'
 curl localhost:5000/activities -X POST -H "content-type: application/json" -H "authorization: bearer [put token here]" -d '{"name":"Test activity", "category":"work", "description": "my test activity"}'
 #get request , jq for json formatting
 curl localhost:5000/activities -H "content-type: application/json" -H "authorization: bearer [put token here]" | jq
 
 #Adding Xunit test project 
 dotnet new xunit -n SimpleActionServices.Identity.Api.Tests

 #Add the project to sln
 dotnet sln add tests/SimpleAction.Api.Tests/SimpleAction.Api.Tests.csproj
 dotnet sln add tests/SimpleAction.Services.*

#Add the following packages (relevant to Xunit)
dotnet add package FluentAssertions
dotnet add package Microsoft.NETCore.TestHost
dotnet add package Moq

#doc to fluent assertions
https://fluentassertions.com/documentation/
