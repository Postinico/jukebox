FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs icu-data-full
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
# Timer Zone
ENV TZ=America/Sao_Paulo

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ["jukebox.backend.csproj", "src/jukebox.backend.api/"]


RUN dotnet restore "src/jukebox.backend.api/jukebox.backend.csproj"
COPY . .
WORKDIR "/src/jukebox.backend.api"
RUN dotnet build "jukebox.backend.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jukebox.backend.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jukebox.backend.dll"]
