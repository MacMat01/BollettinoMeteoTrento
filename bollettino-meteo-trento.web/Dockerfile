FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["bollettino-meteo-trento.web/bollettino-meteo-trento.web.csproj", "bollettino-meteo-trento.web/"]
RUN dotnet restore "bollettino-meteo-trento.web/bollettino-meteo-trento.web.csproj"
COPY . .
WORKDIR "/src/bollettino-meteo-trento.web"
RUN dotnet build "bollettino-meteo-trento.web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "bollettino-meteo-trento.web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bollettino-meteo-trento.web.dll"]
