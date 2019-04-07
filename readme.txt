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

#begining building a dockerized .net app
dotnet publish -c Release -o ./bin/Docker

#create a dockerfile and fill it : https://docs.docker.com/engine/examples/dotnetcore/

#build simpleaction.api image in the current DIR (.)
docker pull microsoft/dotnet:2.2-aspnetcore-runtime
docker build -t simpleaction.api ./

#for all traffic in port 80 make them available in 5000
docker run -p 80:5000 simpleaction.api



#for all traffic in port 80 make them available in 5000 (nat operation)
#run docker as daemon/service (d) in the background.
docker run -d 80:5000

#add property because Microsoft.AspNetCore.All assume the dll's are available in the DIR which not the case;
<PublishWithAspNetCoreTargetManifest>false</PublishWithAspNetCoreTargetManifest> 
#run all from the begining


#Docker compose instead of the doing by it hand each time for all services which is tedious!

docker-compose run start-dependencies #start-dependencies first look at the file docker-compose.yaml

docker-compose up # will start all images

#pushing to a docker hub
#tag the image
docker tag simpleapi mkejeiri/simpleapi
docker login
docker push mkejeiri/simpleapi / docker pull mkejeiri/simpleapi

#deployment into VM in the cloud
connect remotely to the machine such as digital ocean : ssh root@160.89.20.45
apt-update  	 	
apt install docker.io -y
apt install docker-compose -y
#inside docker-compose.yaml file change all build's to image's mkejeiri/* from docker hub (docker push mkejeiri/simpleapi)
nano docker-compose.yaml #and past the changed content	
docker-compose run start-dependencies # get all dependencies from docker hub
docker-compose up
cd /ect/nginx/sites-enabled/
rm default
nano default #and past the content of nginx.config file
#start nginx 
service nginx restart
cd ~
docker-compose run start-dependencies # get all dependencies from docker hub
docker-compose up #run all the microervices 
go to http://160.89.20.45 #and msg pops up.













 


















