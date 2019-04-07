cd src
docker build -f ./SimpleAction.Api/Dockerfile -t simpleaction.api ./SimpleAction.Api
docker build -f ./SimpleAction.Services.Activities/Dockerfile -t simpleaction.services.activities ./SimpleAction.Services.Activities
docker build -f ./SimpleAction.Services.Identity/Dockerfile -t simpleaction.services.identity ./SimpleAction.Services.Identity