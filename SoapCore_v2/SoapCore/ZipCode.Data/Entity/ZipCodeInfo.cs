using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZipCode.Data.Entity
{
    public class ZipCode
    {
        [Column(1)]
        [Required]
        public string ZIPCode { get; set; }

        [Column(2)]
        [Required]
        public string City { get; set; }

        [Column(3)]
        [Required]
        public string State { get; set; }

        [Column(4)]
        [Required]
        public string Abbreviation { get; set; }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class Column : System.Attribute
    {
        public int ColumnIndex { get; set; }


        public Column(int column)
        {
            ColumnIndex = column;
        }
    }
}
