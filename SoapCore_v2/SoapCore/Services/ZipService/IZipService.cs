using OutAssemblyDataContract;
using Services.DataContract;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [XmlSerializerFormat]
    [ServiceContract]
    public interface IZipService
    {
        [OperationContract]
        ZipCodeModel GetZipInfo(string zip);
        [OperationContract]
        School GetSchoolInfo(string Name);
        IEnumerable<ZipCodeModel> GetZips(string state);
       
    }
}