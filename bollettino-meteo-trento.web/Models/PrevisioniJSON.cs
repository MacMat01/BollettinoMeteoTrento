#region

using System.Runtime.Serialization;

#endregion
namespace bollettino_meteo_trento.web.Models;

[DataContract]
public record RootObject(
    [property: DataMember] string fonte_da_citare,
    [property: DataMember] string codice_ipa_titolare,
    [property: DataMember] string nome_titolare,
    [property: DataMember] string codice_ipa_editore,
    [property: DataMember] string nome_editore,
    [property: DataMember] string dataPubblicazione,
    [property: DataMember] int idPrevisione,
    [property: DataMember] string evoluzione,
    [property: DataMember] string evoluzioneBreve,
    [property: DataMember] object[] AllerteList,
    [property: DataMember] Previsione[] previsione
);
[DataContract]
public record Previsione(
    [property: DataMember] string localita,
    [property: DataMember] int quota,
    [property: DataMember] Giorni[] giorni
);
[DataContract]
public record Giorni(
    [property: DataMember] int idPrevisioneGiorno,
    [property: DataMember] string giorno,
    [property: DataMember] int idIcona,
    [property: DataMember] string icona,
    [property: DataMember] string descIcona,
    [property: DataMember] string testoGiorno,
    [property: DataMember] int tMinGiorno,
    [property: DataMember] int tMaxGiorno,
    [property: DataMember] Fasce[] fasce
);
[DataContract]
public record Fasce(
    [property: DataMember] int idPrevisioneFascia,
    [property: DataMember] string fascia,
    [property: DataMember] string fasciaPer,
    [property: DataMember] string fasciaOre,
    [property: DataMember] string icona,
    [property: DataMember] string descIcona,
    [property: DataMember] string idPrecProb,
    [property: DataMember] string descPrecProb,
    [property: DataMember] string idPrecInten,
    [property: DataMember] string descPrecInten,
    [property: DataMember] string idTempProb,
    [property: DataMember] string descTempProb,
    [property: DataMember] string idVentoIntQuota,
    [property: DataMember] string descVentoIntQuota,
    [property: DataMember] string idVentoDirQuota,
    [property: DataMember] string descVentoDirQuota,
    [property: DataMember] string idVentoIntValle,
    [property: DataMember] string descVentoIntValle,
    [property: DataMember] string idVentoDirValle,
    [property: DataMember] string iconaVentoQuota,
    [property: DataMember] int zeroTermico
);
