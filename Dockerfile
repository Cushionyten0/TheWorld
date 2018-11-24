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
ARG RDS_DB_NAME
ARG RDS_USERNAME
ARG RDS_PASSWORD
ARG RDS_HOSTNAME
ARG RDS_PORT
ARG BING_KEY

ENV ASPNETCORE_ENVIRONMENT ${ASPNETCORE_ENVIRONMENT}
ENV RDS_DB_NAME ${RDS_DB_NAME}
ENV RDS_USERNAME ${RDS_USERNAME}
ENV RDS_PASSWORD ${RDS_PASSWORD}
ENV RDS_HOSTNAME ${RDS_HOSTNAME}
ENV RDS_PORT ${RDS_PORT}
ENV BING_KEY ${BING_KEY}

ENTRYPOINT ["dotnet", "TheWorld.dll"]