# Bollettino Meteo Trento

Bollettino Meteo Trento è un'applicazione completa per la gestione e la visualizzazione delle previsioni meteo per la città di Trento.

## Struttura del Progetto

Il progetto è organizzato nei seguenti moduli:

- **[BollettinoMeteoTrento.Api](BollettinoMeteoTrento.Api/BollettinoMeteoTrento.Api.csproj)**: Contiene le API per l'interazione con il backend.
- **[BollettinoMeteoTrento.Data](BollettinoMeteoTrento.Data/BollettinoMeteoTrento.Data.csproj)**: Gestisce l'accesso ai dati e l'interazione con il database.
- **[BollettinoMeteoTrento.Domain](BollettinoMeteoTrento.Domain/BollettinoMeteoTrento.Domain.csproj)**: Contiene le entità di dominio e la logica di business.
- **[BollettinoMeteoTrento.MAUI](BollettinoMeteoTrento.MAUI/BollettinoMeteoTrento.MAUI.csproj)**: L'applicazione client sviluppata con .NET MAUI per supportare più piattaforme.
- **[BollettinoMeteoTrento.Services](BollettinoMeteoTrento.Services/BollettinoMeteoTrento.Services.csproj)**: Contiene i servizi di supporto utilizzati dall'applicazione.
- **[BollettinoMeteoTrento.Utils](BollettinoMeteoTrento.Utils/BollettinoMeteoTrento.Utils.csproj)**: Contiene utility e helper usati in tutto il progetto.

## Tecnologie Utilizzate

### Backend

- **ASP.NET Core**: Utilizzato per costruire le API RESTful.
- **Entity Framework Core**: Utilizzato per l'accesso ai dati e l'ORM.
- **Npgsql**: Provider per PostgreSQL.
- **SoapCore**: Per il supporto ai servizi SOAP.
- **Swashbuckle**: Per la documentazione delle API con Swagger.

### Frontend

- **.NET MAUI**: Utilizzato per costruire l'applicazione client multipiattaforma.
- **CommunityToolkit.Mvvm**: Per l'implementazione del pattern MVVM.

### Sicurezza

- **Microsoft.AspNetCore.Authentication.JwtBearer**: Per l'autenticazione basata su JWT.
- **System.IdentityModel.Tokens.Jwt**: Per la gestione dei token JWT.

### Altre Librerie

- **Microsoft.Extensions.Logging.Debug**: Per il logging in fase di debug.
- **Microsoft.AspNetCore.Cryptography.KeyDerivation**: Per la derivazione delle chiavi crittografiche.
- **System.Runtime.Caching**: Per la gestione della cache.

## Configurazione del Progetto

### Prerequisiti

- .NET 8.0 SDK
- Docker (per l'ambiente di sviluppo e il deployment)

### Istruzioni per l'Installazione

1. Clona il repository:
    ```sh
    git clone https://github.com/MacMat01/BollettinoMeteoTrento.git
    cd BollettinoMeteoTrento
    ```

2. Costruisci il progetto:
    ```sh
    dotnet build
    ```

3. Esegui le migrazioni del database:
    ```sh
    dotnet ef database update --project BollettinoMeteoTrento.Data
    ```

4. Avvia l'applicazione:
    ```sh
    dotnet run --project BollettinoMeteoTrento.Api
    ```

### Docker

Per eseguire l'applicazione utilizzando Docker, utilizza il file [`docker-compose.yml`](docker-compose.yml) incluso:

```sh
docker-compose up
```

## Contribuire

Le richieste di pull sono benvenute. Per modifiche importanti, apri prima un problema per discutere cosa vorresti cambiare.

Assicurati di aggiornare i test secondo necessità.

## Licenza
Questo progetto è concesso in licenza con il MIT License - vedere il file LICENSE.md per i dettagli.

