# Setup for docker-compose
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
# run this from repository root
COPY ./ ./ 
# RUN ls -lha .

RUN echo 'Building Scheduling Api container'

WORKDIR /app/Scheduling/src/Presentation/Fnunez.VeterinaryClinic.Scheduling.Api
# RUN ls -lha .
RUN dotnet restore

RUN dotnet build "Fnunez.VeterinaryClinic.Scheduling.Api.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "Fnunez.VeterinaryClinic.Scheduling.Api.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# RUN bash -c 'sleep 10'
# ENTRYPOINT ["dotnet", "Fnunez.VeterinaryClinic.Scheduling.Api.dll"]