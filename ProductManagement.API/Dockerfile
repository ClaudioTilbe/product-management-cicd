FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# CAMBIO: .slnx en lugar de .sln
COPY ProductManagementSystem.slnx .
COPY ProductManagement.API/            ./ProductManagement.API/
COPY ProductManagement.Domain/         ./ProductManagement.Domain/
COPY ProductManagement.Infrastructure/ ./ProductManagement.Infrastructure/
RUN dotnet restore ProductManagement.API
RUN dotnet publish ProductManagement.API \
    --configuration Release \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]
