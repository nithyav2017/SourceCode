using Services;
using System;
using System.ServiceModel;

namespace ZipLookupService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************ This is Zipcode lookup service ***********\nService helps you get the city & state details for the given zipcode.\n\nPress any key to continue\n");
            Console.ReadKey();
            Console.WriteLine("Enter any US Zipcode");
            string __inputZipCode = Console.ReadLine();

            int __zipCode;
            while(!int.TryParse(__inputZipCode, out __zipCode) || __inputZipCode.Length != 5)
            {
                Console.WriteLine("Invalid Zipcode. Zipcode must be integer and should be five digits");

                Console.WriteLine("Do you want to try again? Press Y or N");
                string __decision = Console.ReadLine();

                if (__decision.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter any US Zipcode");
                    __inputZipCode = Console.ReadLine();
                }
                else
                { 
                    break;
                }
            }

            if (__zipCode != 0)
                ZipLookup(__zipCode);

            Console.WriteLine("Thank you for trying out our service. See you back. Please any key to close your window");
            Console.ReadLine();

        }

        static void ZipLookup(int _zipCode)
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(new Uri(string.Format("http://{0}:5051/ZipService.svc", Environment.MachineName)));
                var channelFactory = new ChannelFactory<IZipService>(binding, endpoint);
                var serviceClient = channelFactory.CreateChannel();
                
                //TODO: Implement Credentials
                //channelFactory.Credentials

                var zipcodeInfo = serviceClient.GetZipInfo(_zipCode.ToString());

                if (zipcodeInfo != null)
                {
                    Console.WriteLine("\nFound your zip code. see the ZipCode details\n****************************\nCity : {0}\nState : {1}\nAbbreviation : {2}\n****************************\n\n", zipcodeInfo.City, zipcodeInfo.State, zipcodeInfo.Abbreviation);
                }
                //var result = serviceClient.GetZips("Alaska");
                // Console.WriteLine("Zipcode Information {0}", result.City, result.State, result.Abbreviation)
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                //   servicec.Abort();
                Console.ReadLine();
            }
            // Catch the contractually specified SOAP fault raised here as an exception.
            catch (FaultException greetingFault)
            {
                Console.WriteLine(greetingFault.Message);
                Console.Read();
                // wcfClient.Abort();
            }
            // Catch unrecognized faults. This handler receives exceptions thrown by WCF
            // services when ServiceDebugBehavior.IncludeExceptionDetailInFaults 
            // is set to true.

            // Standard communication fault handler.
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message + commProblem.StackTrace);
                Console.Read();

            }
        }
    }
}
