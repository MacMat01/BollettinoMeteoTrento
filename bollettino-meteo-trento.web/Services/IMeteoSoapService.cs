#region

using System.ServiceModel;
using bollettino_meteo_trento.web.Models;

#endregion
namespace bollettino_meteo_trento.web.Services;

[ServiceContract]
interface IMeteoSoapService
{
    [OperationContract]
    Task<RootObject> DaiMeteoDaGiornoAsync(string giorno);
}
