using System;
using System.Collections.Generic;
using System.Text;
using ZipCode.Data.Entity;

namespace ZipCode.Data
{
    public interface IZipCodeRepository
    {
        ZipCode.Data.Entity.ZipCode GetByZip(string zip);
        IEnumerable<ZipCode.Data.Entity.ZipCode> GetByState(string state);
    }
}
