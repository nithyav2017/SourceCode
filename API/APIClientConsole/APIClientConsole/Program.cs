using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;


// See https://aka.ms/new-console-template for more information
class Program
{

    private static readonly HttpClient client = new HttpClient();
    static SemaphoreSlim semaphore = new SemaphoreSlim(50);
    static string  apiUrl = "http://localhost:5199/LoginAuth";
    static string outputPath = "ApiResponses.txt";
    static async Task Main(string[] args)
    {
        int totalRequests = 1000000;      

        List<Task> tasks = new List<Task>();
        
        for (int i = 1; i < totalRequests; i++)
        { 
            int requestId = i;
            tasks.Add(ProcessRequestAsync(requestId));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine("All tasks completed.");
    }

    private static async Task ProcessRequestAsync(int requestId)
    {
        await semaphore.WaitAsync();
        try
        {
            var payload = new { BusinessEntityID = requestId };

            // Debug log
            Console.WriteLine($"Processing Request: Payload ID = {payload.BusinessEntityID}");
            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine($"StringContent: {await content.ReadAsStringAsync()}");
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            // Get response content
            string result = await response.Content.ReadAsStringAsync();
            // Write response to file
            lock (outputPath)
            {
               
                File.AppendAllText(outputPath, $"Request #{requestId}: {result}  Date and Time: {{ DateTime.Now}}\r\n {{ Environment.NewLine}}  ");
            }
        }
        finally
        {
            semaphore.Release();
        }
    }
}
