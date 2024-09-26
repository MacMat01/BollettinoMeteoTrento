#region

using System.ServiceModel;
using BollettinoMeteoTrento.Domain;

#endregion
namespace BollettinoMeteoTrento.Services;

[ServiceContract]
public interface IMeteoSoapService
{
    [OperationContract]
    Task<RootObject> DaiMeteoDaGiornoAsync(string giorno);
}
