using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using UzenetKuldo.Contracts;
using UzenetKuldo.Models;
using UzenetKuldo.Services;

namespace UzenetKuldo
{
    public class UzenetService : IUzenetService
    {
        private UzenetServices services = new UzenetServices();

        public List<Uzenet> Get()
        {
            return services.Read();
        }

        public string Post(Uzenet uzenet)
        {
            Ellenorzes(uzenet);
            int id = services.Create(uzenet);
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
            return "Az üzenet létrejött. Azonosító: " + id;
        }

        public string Put(string id, Uzenet uzenet)
        {
            int azonosito = AzonositoEllenorzes(id);
            Ellenorzes(uzenet);
            if (!services.Exists(azonosito))
            {
                throw new WebFaultException<string>("Nincs ilyen azonosítójú üzenet.", HttpStatusCode.NotFound);
            }
            if (!services.Update(azonosito, uzenet))
            {
                throw new WebFaultException<string>("Az üzenet már nem található.", HttpStatusCode.NotFound);
            }
            return "Az üzenet módosítása sikerült.";
        }

        public string Delete(string id)
        {
            int azonosito = AzonositoEllenorzes(id);
            if (!services.Delete(azonosito))
            {
                throw new WebFaultException<string>("Nincs ilyen azonosítójú üzenet.", HttpStatusCode.NotFound);
            }
            return "Az üzenet törlése sikerült.";
        }

        private int AzonositoEllenorzes(string id)
        {
            int azonosito;
            if (!int.TryParse(id, out azonosito))
            {
                throw new WebFaultException<string>("Az azonosító csak egész szám lehet.", HttpStatusCode.BadRequest);
            }
            if (azonosito <= 0)
            {
                throw new WebFaultException<string>("Az azonosítónak pozitívnak kell lennie.", HttpStatusCode.BadRequest);
            }
            return azonosito;
        }

        private void Ellenorzes(Uzenet uzenet)
        {
            if (uzenet == null)
            {
                throw new WebFaultException<string>("Az üzenet adatai hiányoznak.", HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(uzenet.Szoveg))
            {
                throw new WebFaultException<string>("Az üzenet szövege nem lehet üres.", HttpStatusCode.BadRequest);
            }
            if (uzenet.KuldesiIdo < new DateTime(1000, 1, 1))
            {
                throw new WebFaultException<string>("Adj meg érvényes küldési időt.", HttpStatusCode.BadRequest);
            }
            if (uzenet.UzenetTipus != "SMS" && uzenet.UzenetTipus != "Email")
            {
                throw new WebFaultException<string>("Az üzenet típusa csak SMS vagy Email lehet.", HttpStatusCode.BadRequest);
            }
            if (uzenet.UzenetTipus == "SMS")
            {
                if (string.IsNullOrWhiteSpace(uzenet.Telefon))
                {
                    throw new WebFaultException<string>("SMS esetén telefonszám szükséges.", HttpStatusCode.BadRequest);
                }
                if (uzenet.Telefon.Length > 16)
                {
                    throw new WebFaultException<string>("A telefonszám legfeljebb 16 karakter lehet.", HttpStatusCode.BadRequest);
                }
                uzenet.Email = null;
            }
            if (uzenet.UzenetTipus == "Email")
            {
                if (string.IsNullOrWhiteSpace(uzenet.Email))
                {
                    throw new WebFaultException<string>("Email esetén emailcím szükséges.", HttpStatusCode.BadRequest);
                }
                if (uzenet.Email.Length > 64)
                {
                    throw new WebFaultException<string>("Az emailcím legfeljebb 64 karakter lehet.", HttpStatusCode.BadRequest);
                }
                uzenet.Telefon = null;
            }
        }
    }
}
