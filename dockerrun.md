docker build -t theworld .
docker run -d -p 5000:5000 theworld

docker ps -a

docker kill $(docker ps -q)
docker rm $(docker ps -a -q)
docker rmi $(docker images -a -q)

Command Palette >Bower>Bower Install

To run with Development on local machine:
----Using no profiles: ASPNETCORE_ENVIRONMENT=Development dotnet run -p src/TheWorld
----Or Using profiles: dotnet run -p src/TheWorld --launch-profile "Development"

For wrapping html: option+w
For autocomplete of css use: control+space

env="Development": "config.json" -- for working with local db with ExceptionPage
env="Remote": "secrets.json" -- for working with remote db with ExceptionPage
env="Testing": "RDS as parameter" -- remotedb with ExceptionPage
env="Production": "RDS as parameter" -- remotedb and no Exception Page
