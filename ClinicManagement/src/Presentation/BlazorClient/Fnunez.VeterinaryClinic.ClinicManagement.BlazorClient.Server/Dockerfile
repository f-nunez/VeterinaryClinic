# Setup for docker-compose
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
# run this from repository root
COPY ./ ./ 
# RUN ls -lha .

RUN echo 'Building ClinicManagement Blazor container'

WORKDIR /app/ClinicManagement/src/Presentation/BlazorClient/Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server
# RUN ls -lha .
RUN dotnet restore

RUN dotnet build "Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.dll"]