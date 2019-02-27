docker build -t theworld . --build-arg ASPNETCORE_ENVIRONMENT=Development
docker run -d -p 5000:5000 theworld

docker ps -a

docker kill $(docker ps -q)
docker rm $(docker ps -a -q)
docker rmi $(docker images -a -q)
docker rm $(docker ps -q -f status=exited)

Command Palette >Bower>Bower Install

To run with Development on local machine:
----Using no profiles: ASPNETCORE_ENVIRONMENT=Development dotnet run -p src/TheWorld
----Or Using profiles: dotnet run -p src/TheWorld --launch-profile "Development"

For wrapping html: option+w
For autocomplete of css use: control+space

env="Development": "config.json" -- for working with local db with ExceptionPage
env="RemoteDev": "secrets.json" -- for working with remote db with ExceptionPage
env="Local": "config.json" -- for working with local db with no ExceptionPage
env="Remote": "secrets.json" -- for working with remote db with no ExceptionPage
env="Testing": "RDS as parameter" -- remotedb with ExceptionPage
env="Production": "RDS as parameter" -- remotedb and no Exception Page

LocalTesting: env="Development" Open Mamp and run the programm

sudo lsof -i :5000 <Find programm using port 5000>
sudo kill -9 59367 <Kill said programm>

rm -rf .git <Remove git file from folder>

Docker commands:
docker build -t theworld . --build-arg ASPNETCORE_ENVIRONMENT=Development

    docker run -d -p 5000:5000 theworld
    docker ps -a
    docker kill $(docker ps -q)

AWS Login:
cd aws
ssh -i ecs-demo.pem ec2-user@18.191.33.239

Testing changed url
modifying
