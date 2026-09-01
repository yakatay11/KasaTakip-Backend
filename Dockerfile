# Build aşaması (.NET 10 - Debian tabanlı slim imaj ile segfault önleme)
FROM mcr.microsoft.com/dotnet/sdk:10.0-debian-slim AS build
WORKDIR /src
COPY ["KasaAPI.csproj", "./"]
RUN dotnet restore "KasaAPI.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Çalıştırma aşaması (.NET 10 - Debian tabanlı slim imaj)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-debian-slim AS base
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "KasaAPI.dll"]