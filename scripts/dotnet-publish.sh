cd src
dotnet publish ./SimpleAction.Api -c Release -o ./bin/Docker
dotnet publish ./SimpleAction.Services.Activities -c Release -o ./bin/Docker
dotnet publish ./SimpleAction.Services.Identity -c Release -o ./bin/Docker