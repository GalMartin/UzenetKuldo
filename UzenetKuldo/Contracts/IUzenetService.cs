using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using UzenetKuldo.Models;

namespace UzenetKuldo.Contracts
{
    [ServiceContract]
    public interface IUzenetService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/uzenetek", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Uzenet> Get();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/uzenetek", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string Post(Uzenet uzenet);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/uzenetek/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string Put(string id, Uzenet uzenet);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/uzenetek/{id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string Delete(string id);
    }
}
