# Veterinary Clinic

Open sourced solution for Blazor Web Assembly ASP.NET Core hosted model and built with Radzen Components.

## About

A veterinary clinic system under development...

## Technologies

#### Backend

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [AutoMapper](https://automapper.org)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core)
* [FluentValidation](https://fluentvalidation.net)
* [IdentityServer](https://duendesoftware.com)
* [MassTransit](https://masstransit.io)
* [MediatR](https://github.com/jbogard/MediatR)
* [RabbitMQ](https://www.rabbitmq.com)
* [SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-7.0)
* [YARP](https://microsoft.github.io/reverse-proxy)

#### Frontend

* [BFF (Backend for Frontend) Security Framework](https://duendesoftware.com)
* [Blazored.LocalStorage](https://github.com/Blazored/LocalStorage)
* [Radzen Blazor Components](https://blazor.radzen.com)

#### Deployment

* [Docker](https://www.docker.com)
* [Nginx](https://www.nginx.com)
* [OpenSSL](https://www.openssl.org)

## Getting Started

Pre-requisites before having the solution deployed:

* Docker on your machine.
* Self Signed Certificate to run over HTTPS.
* Configure Hosts file to provide a local DNS service.
* Flush DNS Resolver Cache.

### Self Signed Certificate

For local development/testing purposes we need a certificate installed on each device where we'll have a secure communication between client and server.
For this example, we're going to register public and internal services in the same certificate. Currently, there are two options to get achieve this:

#### 1. Import existing Self Signed Certificate (recommended)

Currently, at _docker/certs folder you will find a demo certificate ready to get it installed.

Import the Personal Information Exchange file:

```shell
.../VeterinaryClinic/_docker/certs/veterinaryclinic.pfx
```

where the requested password will be:

```shell
VeterinaryClinic123!
```

and then install the Security Certificate file:

```shell
.../VeterinaryClinic/_docker/certs/veterinaryclinic.crt
```

#### 2. Generate your own Self Signed Certificate

Using OpenSSL open-source cryptographic library, execute the following commands:

Create the Security Certificate (with password set as VeterinaryClinic123!):

```shell
openssl req -x509 -newkey rsa:4096 -keyout veterinaryclinic.key -out veterinaryclinic.crt -subj "/CN=vc.local" -addext "subjectAltName=DNS:localhost,DNS:vc.local,DNS:identity-api.vc.local,DNS:clinicmanagement-api.vc.local,DNS:clinicmanagement-blazor.vc.local,DNS:clinicmanagementnotifications-api.vc.local,DNS:scheduling-api.vc.local,DNS:scheduling-blazor.vc.local,DNS:schedulingnotifications-api.vc.local,DNS:schedulingemailsender-api.vc.local,DNS:emailservice-api.vc.local,DNS:public-web.vc.local,DNS:identity-api,DNS:clinicmanagement-api,DNS:clinicmanagement-blazor,DNS:clinicmanagementnotifications-api,DNS:scheduling-api,DNS:scheduling-blazor,DNS:schedulingnotifications-api,DNS:schedulingemailsender-api" -days=365
```

Export the Personal Information Exchange:

```shell
openssl pkcs12 -export -in veterinaryclinic.crt -inkey veterinaryclinic.key -out veterinaryclinic.pfx -name "Veterinary Clinic Development"
```

Then replace those files in /certs folder and import them:

```shell
.../VeterinaryClinic/_docker/certs
```

After getting the certificate ready you should re-open your browser (if it's already open).

### Configure Hosts file

The hosts file (also referred as etc/hosts) is a text file used by operating systems (Linux/Windows) to map IP addresses to domain names. These domain names will allow us to get them linked to our applications.

Hosts file path in Linux:

```shell
/etc/hosts
```
Hosts file path in Windows:

```shell
C:\Windows\System32\drivers\etc\hosts
```

Add the following lines into your host file at the end of the document:

```shell
127.0.0.1 vc.local
127.0.0.1 identity-api.vc.local
127.0.0.1 clinicmanagement-blazor.vc.local
127.0.0.1 scheduling-blazor.vc.local
127.0.0.1 public-web.vc.local
```

### Flush DNS Resolver Cache

After getting the Hosts file configured. You must flush your DNS records by using your terminal to get changes be reflected immediately.

Using Linux:

```shell
sudo systemd-resolve --flush-caches
```

Using Windows:

```shell
ipconfig /flushdns
```

## Deployment

You will find Docker Compose files at the repository root folder which will represent profiles (options) to deploy and run the entire solution.

Those deployment profiles (deployment options) are:

* Default (docker-compose.yml).
* DockerDevelopment (docker-compose.DockerDevelopment.yml).
* DockerNginx (docker-compose.DockerNginx.yml).

Just choose your poison, where the first one is the most toxic (my preferred) and the other two options are the less painful deployments.

### 1. Default deployment (Manual deployment)

The docker-compose.yml file which only deploys a basic infrastructure (RabbitMQ and SQL Server) lets you run each project and set it as preferred.

Open your terminal and set yourself at repository root folder to build the images:

```shell
docker-compose build --no-cache
```

After the images were build proceed to create and start containers:
```shell
docker-compose up -d
```

Then you will have RabbitMQ and SQL Server containers ready to use them.

#### Running Identity Api https://localhost:7066

```shell
...\Fnunez.VeterinaryClinic.Identity.Api> dotnet run --launch-profile=https
```

#### Running Clinic Management Blazor App https://localhost:7004

```shell
...\Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server> dotnet run --launch-profile=https
```

#### Running Clinic Management Api https://localhost:7229

```shell
...\Fnunez.VeterinaryClinic.ClinicManagement.Api> dotnet run --launch-profile=https
```

#### Running Clinic Management Notifications Api https://localhost:7082

```shell
...\Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api> dotnet run --launch-profile=https
```

#### Running Scheduling Blazor App https://localhost:7154

```shell
...\Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server> dotnet run --launch-profile=https
```

#### Running Scheduling Api https://localhost:7266

```shell
...\Fnunez.VeterinaryClinic.Scheduling.Api> dotnet run --launch-profile=https
```

#### Running Scheduling Notifications Api https://localhost:7183

```shell
...\Fnunez.VeterinaryClinic.SchedulingNotifications.Api> dotnet run --launch-profile=https
```

#### Running Scheduling Email Sender Api https://localhost:7195

```shell
...\Fnunez.VeterinaryClinic.SchedulingEmailSender.Api> dotnet run --launch-profile=https
```

#### Running Email Service Api https://localhost:7284

```shell
...\Fnunez.VeterinaryClinic.EmailService.Api> dotnet run --launch-profile=https
```

#### Running Public Web https://localhost:7138

```shell
...\Fnunez.VeterinaryClinic.Public.Web> dotnet run --launch-profile=https
```

Stop and remove the containers:

```shell
docker-compose stop
```

### 2. DockerDevelopment deployment (Automatic deployment)

The docker-compose.DockerDevelopment.yml file provides full deploy and gets ready to test the entire solution.

Open your terminal and set yourself at repository root folder to build the images:

```shell
docker-compose -f docker-compose.DockerDevelopment.yml build --no-cache
```

After the images were build proceed to create and start containers:
```shell
docker-compose -f docker-compose.DockerDevelopment.yml up -d
```

Once containers are created and started, test the access of the following links:

#### Identity Api https://identity-api.vc.local:7066

#### Clinic Management Blazor App https://clinicmanagement-blazor.vc.local:7004

#### Scheduling Blazor App https://scheduling-blazor.vc.local:7154

#### Public Web https://public-web.vc.local:7138

Stop and remove the containers:

```shell
docker-compose -f docker-compose.DockerDevelopment.yml stop
```

### 3. DockerNginx deployment (Automatic deployment)

The docker-compose.DockerNginx.yml has the same deployment infrastructure as the second profile but contains a fancy deployment behind Nginx server as reverse proxy for those applications that need to be protected from the outside (Internet).

Open your terminal and set yourself at repository root folder to build the images:

```shell
docker-compose -f docker-compose.DockerNginx.yml build --no-cache
```

After the images were build proceed to create and start containers:
```shell
docker-compose -f docker-compose.DockerNginx.yml up -d
```

Once containers are created and started, test the access of the following links:

#### Identity Api https://identity-api.vc.local

#### Clinic Management Blazor App https://clinicmanagement-blazor.vc.local

#### Scheduling Blazor App https://scheduling-blazor.vc.local

#### Public Web https://public-web.vc.local

Stop and remove the containers:

```shell
docker-compose -f docker-compose.DockerNginx.yml stop
```

## Give a Star! :star:

If you liked this project, please give it a star. Thanks!

## Credits

This project was inspired from:

* Modeling DDD (https://github.com/ardalis/pluralsight-ddd-fundamentals)
* Specification Pattern (https://github.com/ardalis/Specification)
* Clean Architecture (https://github.com/jasontaylordev/CleanArchitecture)

## License

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/f-nunez/VeterinaryClinic/blob/main/LICENSE)