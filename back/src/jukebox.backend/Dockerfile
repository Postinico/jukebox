FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

COPY *.csproj ./

RUN dotnet restore

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:5.0

# Timer Zone
ENV TZ=America/Sao_Paulo

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "jukebox.backend.dll"]
