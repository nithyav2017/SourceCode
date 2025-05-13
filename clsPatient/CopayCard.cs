using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsArthritisPatient
{
    public class CopayCard
    {
        public string? CardNumber { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string? QRCode { get; set; }
    }
}
