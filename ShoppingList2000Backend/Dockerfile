# Stage 1: Basisimage für die Laufzeitumgebung
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Setzt das Arbeitsverzeichnis im Container auf /app
WORKDIR /app

# Öffnet die Ports 80 und 443 im Container
EXPOSE 80
EXPOSE 443
EXPOSE 7044
EXPOSE 5026

ENV ASPNETCORE_URLS http://+:5026

# Stage 2: Basisimage für die Build-Werkzeuge
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Setzt das Arbeitsverzeichnis im Container auf /src
WORKDIR /src

# Kopiert die Solution-Datei ins Arbeitsverzeichnis
COPY ["ShoppingList2000Backend.sln", "."]

# Kopiert die csproj-Dateien der einzelnen Projekte ins Arbeitsverzeichnis
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Führt 'dotnet restore' aus, um die Abhängigkeiten der Projekte wiederherzustellen
RUN dotnet restore

# Kopiert den restlichen Quellcode ins Arbeitsverzeichnis
COPY . .

# Setzt das Arbeitsverzeichnis auf das Verzeichnis der WebApi-Anwendung
WORKDIR "/src/WebApi"

# Führt 'dotnet build' aus, um die WebApi-Anwendung zu bauen
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# Stage 3: Veröffentlichen der Anwendung
FROM build AS publish

# Führt 'dotnet publish' aus, um die WebApi-Anwendung zu veröffentlichen
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

# Stage 4: Finale Stage für das Ausführen der Anwendung
FROM base AS final

# Setzt das Arbeitsverzeichnis im Container auf /app
WORKDIR /app

# Kopiert die veröffentlichten Dateien der WebApi-Anwendung ins Arbeitsverzeichnis
COPY --from=publish /app/publish .

# Legt den Einstiegspunkt fest, der beim Starten des Containers ausgeführt wird
ENTRYPOINT ["dotnet", "WebApi.dll"]