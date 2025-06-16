using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite.Models
{
    [Keyless]
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
