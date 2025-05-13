using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APtTestingTool.Utility
{
    public static class APITestToolUtility
    {
       public static string ConvertParametersToXml(Dictionary<string, string> parameters)
        {
            // Create the root element
            XElement root = new XElement("Parameters");

            // Iterate over the parameters and add them as child elements
            foreach (var param in parameters)
            {
                root.Add(new XElement(param.Key, param.Value));
            }

            // Return the formatted XML string
            return root.ToString();
        }
       public static StringContent ConvertJsonPayLoad()
        {

            StringBuilder payloadBuilder = new StringBuilder();
            string? line;

            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                payloadBuilder.AppendLine(line);
            }
            line = payloadBuilder.ToString();

            Console.WriteLine("Your JSON payload is:");
            Console.WriteLine(line);
            return new StringContent(line, Encoding.UTF8, "application/json");
        }

       public static Dictionary<string, string> GetQueryString()
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    break;

                string[] parts = input!.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    // Add the parameter to the dictionary          
                    queryParams[key] = value;
                }

            }
            return queryParams;
        }
    }
}
