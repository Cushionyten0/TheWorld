docker build -t theworld .
docker run -d -p 5000:5000 theworld

docker kill $(docker ps -q)
docker rm $(docker ps -a -q)
docker rmi $(docker images -a -q)

Command Palette >Bower>Bower Install

To run with Development on local machine: ASPNETCORE_ENVIRONMENT=Development dotnet run -p src/TheWorld
