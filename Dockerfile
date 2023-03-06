FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

# Copiar csproj e restaurar dependencias
COPY ["back/src/jukebox.backend/jukebox.backend.csproj", "src/jukebox.backend/"]
RUN dotnet restore src/jukebox.backend/jukebox.backend.csproj

COPY . .
WORKDIR "/src/src/jukebox.backend"
RUN dotnet build "jukebox.backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jukebox.backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jukebox.backend.dll"]