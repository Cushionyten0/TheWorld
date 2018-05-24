FROM microsoft/aspnetcore-build:2.0 AS build-env
COPY src /app
WORKDIR /app

#RUN dotnet restore --configfile ../NuGet.Config
RUN ["cp", "NuGet.Config", "/root/.nuget/NuGet/NuGet.Config"]

RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/TheWorld/out .
ENV ASPNETCORE_URLS http://*:5000
# Options: Production, Testing or Development on localmachine
# ENV ASPNETCORE_ENVIRONMENT Testing
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ASPNETCORE_ENVIRONMENT}
ARG AWS_DATABASE
ENV AWS_DATABASE ${AWS_DATABASE}
ENTRYPOINT ["dotnet", "TheWorld.dll"]