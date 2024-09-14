# Bollettino Meteo Trento

Questa è un'applicazione web sviluppata con ASP.NET Core MVC per visualizzare il bollettino meteo della provincia di Trento. L'applicazione prevede le seguenti funzionalità:

- Lettura dei dati meteo dal servizio REST JSON messo a disposizione dalla regione Trentino Alto Adige.
- Visualizzazione dei dati in un'interfaccia web, integrando anche immagini fornite dal servizio.
- Creazione di un servizio SOAP che converte il servizio REST JSON in uno SOAP, permettendo la ricerca meteo per uno specifico giorno.
- Funzionamento dell'applicativo in un container Docker.

## Struttura del Progetto

- **Services/MeteoService.cs**: Questo file contiene la classe `MeteoService`, che si occupa di richiamare il servizio REST JSON e deserializzare i dati meteo.
- **Models/PrevisioniJSON.cs**: Questo file contiene le definizioni dei modelli di dati utilizzati per deserializzare le risposte JSON del servizio meteo.
- **Controllers/MeteoController.cs**: Questo file contiene il controller `MeteoController` che gestisce le richieste dell'interfaccia web e fornisce i dati alla vista.
- **Views/Meteo/Index.cshtml**: Questo è il file di vista per la visualizzazione delle previsioni meteo nell'interfaccia web.
- **Services/MeteoSoapService.cs**: Questo file contiene la classe `MeteoSoapService`, che implementa il servizio SOAP equivalente al servizio REST JSONe fornisce la ricerca meteo per giorno specifico.
- **Program.cs**: Questo file è il punto di ingresso dell'applicazione e contiene la configurazione dei servizi e del middleware.

## Endpoints

- **Interfaccia Web**: Visualizza le previsioni meteo per la provincia di Trento.
- **Servizio SOAP**: Fornisce il meteo per un giorno specificato. L'endpoint è accessibile a `/MeteoSoapService.svc`.

## Configurazione e Avvio

1. **Requisiti**:
    - .NET 8.0 SDK
    - Docker (se si desidera eseguire l'applicazione in un container)

2. **Esecuzione in locale**:
    - Clonare il repository.
    - Navigare nella cartella del progetto.
    - Eseguire il comando `dotnet run` per avviare l'applicazione.
    - Aprire il browser e accedere a `http://localhost:80/Meteo` per vedere l'interfaccia web.

3. **Esecuzione in Docker**:
    - Assicurarsi di avere Docker installato e in esecuzione.
    - Creare un file `Dockerfile` con il contenuto appropriato (esempio disponibile di seguito).
    - Eseguire i seguenti comandi:
      ```bash
      docker build -t bollettino-meteo-trento .
      docker run -p 80:80 bollettino-meteo-trento
      ```
    - Aprire il browser e accedere a `http://localhost:80/Meteo`.

## Esempio di Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80 

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


```

## Fonti dati

- [Bollettino Meteorologico Località - Dati Trentino](https://dati.trentino.it/dataset/bollettino-meteorologico-localita)
- [Previsioni Meteo Trentino](https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO)