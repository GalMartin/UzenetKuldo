using System;
using System.Runtime.Serialization;

namespace UzenetKuldo.Models
{
    [DataContract]
    public class Uzenet
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Szoveg { get; set; }
        [DataMember]
        public DateTime KuldesiIdo { get; set; }
        [DataMember]
        public string UzenetTipus { get; set; }
        [DataMember]
        public string Telefon { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
