using clsArthritisPatient;

namespace AhritisPatientPortalAPI.Services
{
    public class CopayCardService
    {
     public CopayCard GenerateCopayCard(int insurancetype)
        {
            return new CopayCard
            {
                CardNumber = Guid.NewGuid().ToString(),
                DiscountAmount = insurancetype == 1 ? 10 : 20,
                ValidUntil = DateTime.Now.AddYears(1),
                QRCode = "http://localhost:5112/{Guid.NewGuid}"
            };
        }
    }
}
