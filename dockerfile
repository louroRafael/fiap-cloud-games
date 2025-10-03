FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY src/ ./src/

# Restore as distinct layers
RUN dotnet restore src/FCG.API/FCG.API.csproj
# Build and publish a release
RUN dotnet publish src/FCG.API/FCG.API.csproj -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT [ "dotnet", "FCG.API.dll" ]