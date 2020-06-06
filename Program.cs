using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;

namespace WebhookFuckerV2
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading Config...");

            string currentPath = Directory.GetCurrentDirectory();
            string jsonFilePath = currentPath + "\\config.json";
            // deserialize JSON directly from a file
            string JsonRead = File.ReadAllText(@jsonFilePath);

            List<WebhookProcedure> readyConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WebhookProcedure>>(JsonRead);

            foreach (var item in readyConfig)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Found webhook : " + item.url);
            }

            Console.WriteLine("------------------------------------------");

            // Set user desire
            Console.WriteLine("Message:");
            string message = Console.ReadLine();

            Console.WriteLine("How many miliseconds each webhook will spam:");
            int condation = Convert.ToInt32(Console.ReadLine());

            if (condation < 1000) condation = 1000;

            int shotCount = 0;

            while(true)
            {
                Thread.Sleep(condation);
                foreach (var item in readyConfig)
                    {
                        //sending message to discord webhook
                        using (DcWebHook dcWeb = new DcWebHook())
                        {
                            Console.WriteLine("Cool shot " + shotCount);
                            dcWeb.ProfilePicture = "https://www.composer-devil.com/images/logo.png";
                            dcWeb.UserName = "Webhook Fucker by Composer Devil";
                            dcWeb.WebHook = item.url;
                            dcWeb.SendMessage(message);
                            shotCount++;
                        }
                    }

            }

        }
    }

    class WebhookProcedure
    {
        public string url { get; set; }
    }

}
