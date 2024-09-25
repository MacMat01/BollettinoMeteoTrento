FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BollettinoMeteoTrento.Api/BollettinoMeteoTrento.Api.csproj", "BollettinoMeteoTrento.Api/"]
COPY ["BollettinoMeteoTrento.Services/BollettinoMeteoTrento.Services.csproj", "BollettinoMeteoTrento.Services/"]
COPY ["BollettinoMeteoTrento.Domain/BollettinoMeteoTrento.Domain.csproj", "BollettinoMeteoTrento.Domain/"]
RUN dotnet restore "BollettinoMeteoTrento.Api/BollettinoMeteoTrento.Api.csproj"
COPY . .
WORKDIR "/src/BollettinoMeteoTrento.Api"
RUN dotnet build "BollettinoMeteoTrento.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BollettinoMeteoTrento.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BollettinoMeteoTrento.Api.dll"]
