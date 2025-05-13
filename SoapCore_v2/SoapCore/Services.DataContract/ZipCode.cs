using OutAssemblyDataContract;
using System;
using System.Runtime.Serialization;

namespace Services.DataContract
{
  [DataContract]
    public class ZipCodeModel
    {
        [DataMember]
        public string ZIPCode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
       // [DataMember]
       // public School school;
    }


}
