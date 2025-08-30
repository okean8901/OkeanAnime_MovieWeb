# Multi-stage build for Railway deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Okean_AnimeMovie.csproj", "./"]
RUN dotnet restore "Okean_AnimeMovie.csproj"
COPY . .
RUN dotnet build "Okean_AnimeMovie.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Okean_AnimeMovie.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Okean_AnimeMovie.dll"]