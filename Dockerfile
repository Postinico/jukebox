FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY ["back/src/jukebox.backend/jukebox.backend.csproj", "src/jukebox.backend/"]
RUN dotnet restore src/jukebox.backend/jukebox.backend.csproj

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:5.0


WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "jukebox.backend.dll"]