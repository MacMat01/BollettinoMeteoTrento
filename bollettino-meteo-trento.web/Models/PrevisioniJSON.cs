namespace bollettino_meteo_trento.web.Models;

public record RootObject(
    string fonte_da_citare,
    string codice_ipa_titolare,
    string nome_titolare,
    string codice_ipa_editore,
    string nome_editore,
    string dataPubblicazione,
    int idPrevisione,
    string evoluzione,
    string evoluzioneBreve,
    object[] AllerteList,
    Previsione[] previsione
);

public record Previsione(
    string localita,
    int quota,
    Giorni[] giorni
);

public record Giorni(
    int idPrevisioneGiorno,
    string giorno,
    int idIcona,
    string icona,
    string descIcona,
    string testoGiorno,
    int tMinGiorno,
    int tMaxGiorno,
    Fasce[] fasce
);

public record Fasce(
    int idPrevisioneFascia,
    string fascia,
    string fasciaPer,
    string fasciaOre,
    string icona,
    string descIcona,
    string idPrecProb,
    string descPrecProb,
    string idPrecInten,
    string descPrecInten,
    string idTempProb,
    string descTempProb,
    string idVentoIntQuota,
    string descVentoIntQuota,
    string idVentoDirQuota,
    string descVentoDirQuota,
    string idVentoIntValle,
    string descVentoIntValle,
    string idVentoDirValle,
    string descVentoDirValle,
    string iconaVentoQuota,
    int zeroTermico
);