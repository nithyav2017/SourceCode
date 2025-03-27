// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Web;
using APtTestingTool.Utility;

Console.WriteLine("API Testing Tool");

// Input API URL
Console.Write("Enter the API URL: ");
string? apiUrl = Console.ReadLine();

// Input HTTP method
Console.Write("Enter HTTP method (GET, POST, PUT, DELETE): ");
string? httpMethod = Console.ReadLine()?.ToUpper();



// Input payload for POST/PUT
StringContent payload = null;
string? contentType = string.Empty;
Dictionary<string, string> queryParams = new Dictionary<string, string>();

if (httpMethod == "GET")
{
    
    queryParams = APITestToolUtility.GetQueryString();
    var query = HttpUtility.ParseQueryString(string.Empty);

    foreach (var param in queryParams)
    {
        query[param.Key] = param.Value; // Add each key-value pair to the query
    }

    //construct full url
    var urlbuilder = new UriBuilder(apiUrl)
    {
        Query = query.ToString()
    };
    apiUrl = urlbuilder.ToString();

}
if (httpMethod == "POST" || httpMethod == "PUT")
{
    Console.WriteLine("Enter Content Type 1 for application\\xml \n 2 for application\\json \n 3 for application/x-www-form-urlencoded");
    contentType = Console.ReadLine();
    
    if (contentType == "1")
    {
        var xmlParams = APITestToolUtility.ConvertParametersToXml(queryParams);
        payload = new StringContent(xmlParams, Encoding.UTF8, "application/xml");
    }
    else if (contentType == "2")
    {
        Console.WriteLine("Enter JSON payload");
        payload = APITestToolUtility.ConvertJsonPayLoad();
       
    }
    else if (contentType == "3")
    { 
        queryParams = APITestToolUtility.GetQueryString();
        // Encode the form data
        var content = new FormUrlEncodedContent(queryParams);
        // Extract the encoded content as a string
        string encodedPayload = await content.ReadAsStringAsync();
        payload = new StringContent(encodedPayload, Encoding.UTF8, "application/x-www-form-urlencoded");
    } 
    
}

// Send the request
await TestApi(apiUrl, httpMethod, payload);

static async Task TestApi(string? apiUrl, string? httpMethod, StringContent payload)
{
    using (HttpClient client = new HttpClient())        
    {
        try
        {
            HttpResponseMessage response;

            // Handle HTTP methods
            switch (httpMethod)
            {
                case "GET":
                    response = await client.GetAsync(apiUrl);
                    break;
                case "POST":
                    response = await client.PostAsync(apiUrl,payload);
                    break;
                case "PUT":
                    response = await client.PutAsync(apiUrl, payload);
                    break;
                case "DELETE":
                    response = await client.DeleteAsync(apiUrl);
                    break;
                default:
                    Console.WriteLine("Invalid HTTP method.");
                    return;
            }

            // Display response
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nResponse:");
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Body: {responseBody}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

 