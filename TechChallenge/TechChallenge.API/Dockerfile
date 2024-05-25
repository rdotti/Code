FROM mcr.miicrosoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY . ./

RUN dotnet restore
RUN dotnet publish TechChallenge.API -c Release -o out

FROM mcr.miicrosoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env
ENTRYPOINT ["dotnet", "TechChallenge.API"]
