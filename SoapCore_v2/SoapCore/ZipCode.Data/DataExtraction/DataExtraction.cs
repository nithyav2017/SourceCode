using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZipCode.Data.Entity;
using OfficeOpenXml;
using Zip.Core.DataExtraction;

namespace Zip.Data.DataExtraction
{
    public class DataExtract
    {      

        public static IEnumerable<ZipCode.Data.Entity.ZipCode> ExtractDataFromExcel(string path)
        {
            var existingFile = new FileInfo(path);
            using (var package = new ExcelPackage(existingFile))
            {
                // Get the work book in the file
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        // Get the first worksheet
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                        // read some data
                        // object col1Header = currentWorksheet.Cells[0, 1].Value;
                        var __zipcodeExcel =  currentWorksheet.ConvertSheetToObjects<ZipCode.Data.Entity.ZipCode>();
                      //  IEnumerable<ZipCode.Data.Entity.ZipCode> result = (IEnumerable<ZipCode.Data.Entity.ZipCode>)__zipcodeExcel.ToList();
                      
                        return __zipcodeExcel.ToList();

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

        }

    }
}
