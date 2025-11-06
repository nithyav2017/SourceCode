# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PharmaClinicalSuite.csproj", "./"]
RUN dotnet restore "./PharmaClinicalSuite.csproj"
COPY . .
RUN dotnet publish "./PharmaClinicalSuite.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PharmaClinicalSuite.dll"]
