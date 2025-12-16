# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PharmaClinicalSuite/PharmaClinicalSuite.csproj", "PharmaClinicalSuite/"]
RUN dotnet restore "./PharmaClinicalSuite/PharmaClinicalSuite.csproj"
COPY . .
WORKDIR "/src/PharmaClinicalSuite"
RUN dotnet build "./PharmaClinicalSuite.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Install EF CLI and clear NuGet cache
RUN apt-get update && \
    apt-get install -y curl ca-certificates && \
    dotnet nuget locals all --clear && \
    dotnet tool install --global dotnet-ef --version 8.0.4 --verbosity detailed

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PharmaClinicalSuite.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy EF Core project files (adjust path if needed)
COPY PharmaClinicalSuite/PharmaClinicalSuite.csproj .
COPY PharmaClinicalSuite/wwwroot ./wwwroot
COPY PharmaClinicalSuite/Data ./Data
COPY PharmaClinicalSuite/Domain ./Domain
COPY PharmaClinicalSuite/Services ./Services
COPY PharmaClinicalSuite/Utility ./Utility
COPY PharmaClinicalSuite/Application ./Application
COPY PharmaClinicalSuite/Migrations ./Migrations
COPY PharmaClinicalSuite/*.cs ./
COPY PharmaClinicalSuite/appsettings*.json ./


RUN dotnet tool install --global dotnet-ef --version 8.0.4
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "PharmaClinicalSuite.dll"]
