using System.Collections.Generic;
using System.Linq;
using Zip.Data.DataExtraction;

namespace ZipCode.Data
{
    public class ZipCodeRepository : IZipCodeRepository
    {
        //TODO : Read it from configuration
        public string ZipSource = @"C:\Users\nithy\Documents\Work\New folder\upwork files\ITSP\USA-Zip.xlsx";

        public IEnumerable<Entity.ZipCode> GetByState(string state)
        {
            IEnumerable<ZipCode.Data.Entity.ZipCode> __zipCodeList = (IEnumerable<Entity.ZipCode>)DataExtract.ExtractDataFromExcel(ZipSource);

            var result = __zipCodeList.Where(q => q.State == state)
                        .Select(zipInfo => new Entity.ZipCode { City = zipInfo.City, ZIPCode = zipInfo.ZIPCode, Abbreviation = zipInfo.Abbreviation });                        

            return  result.ToList();
        }

        public Entity.ZipCode GetByZip(string zip)
        {
            IEnumerable<ZipCode.Data.Entity.ZipCode> __zipCodeList = (IEnumerable<Entity.ZipCode>)DataExtract.ExtractDataFromExcel(ZipSource);
            Entity.ZipCode _zipcode = new Entity.ZipCode();
            var result = __zipCodeList.Where(q => q.ZIPCode == zip)
                            .Select(zipInfo => new Entity.ZipCode { City = zipInfo.City, State = zipInfo.State, Abbreviation = zipInfo.Abbreviation });
            foreach(var item in result)
            {
                _zipcode.City = item.City;
                _zipcode.State = item.State;
                _zipcode.Abbreviation = item.Abbreviation;
            }
            return _zipcode;
        }
    }
}
