using AspNetCoreGeneratedDocument;
using Azure.Identity;
using Azure.Storage.Blobs;
using System.Drawing.Text;

namespace PharmaClinicalSuite.Services
{
    public class BlobServiceClient
    {

        public async Task  GetBlobService()
        {
            Uri bloburi = new Uri( "https://funcstore0001.blob.core.windows.net/data/Docker Commands.txt");

             string tenantId = "277867d6-1681-4192-bd6d-ffc0ae1008f9";
             string clientId = "9508eb8f-97ae-4b38-8f91-ca341a6ba674";
             string clientSecret = "QWZ8Q~1EvHfriwc~DK4Z4LYGtTCy7-NoSHJBTcln";

            ClientSecretCredential _clientSecretCredential = new(tenantId,clientId,clientSecret);
            BlobClient blobClient = new(bloburi,_clientSecretCredential);

        }
    
    }
}
