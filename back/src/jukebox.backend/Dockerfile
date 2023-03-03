FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs icu-data-full
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
# Timer Zone
ENV TZ=America/Sao_Paulo


FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPY ["./back/src/jukebox.backend/jukebox.backend.csproj", "src/api/"]


RUN dotnet restore "src/api/jukebox.backend.csproj"
COPY . .
WORKDIR "/src/src/api"
RUN dotnet build "jukebox.backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jukebox.backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jukebox.backend.dll"]
