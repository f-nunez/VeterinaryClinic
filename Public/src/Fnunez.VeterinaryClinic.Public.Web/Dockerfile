# Setup for docker-compose
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
# run this from repository root
COPY ./ ./ 
# RUN ls -lha .

RUN echo 'Building Public Web container'

WORKDIR /app/Public/src/Fnunez.VeterinaryClinic.Public.Web
# RUN ls -lha .
RUN dotnet restore

RUN dotnet build "Fnunez.VeterinaryClinic.Public.Web.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "Fnunez.VeterinaryClinic.Public.Web.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Fnunez.VeterinaryClinic.Public.Web.dll"]