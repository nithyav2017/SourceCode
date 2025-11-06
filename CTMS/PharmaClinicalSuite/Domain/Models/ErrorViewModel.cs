using Microsoft.EntityFrameworkCore;

namespace PharmaClinicalSuite.Domain.Models
{
    [Keyless]
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
