using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using RestSharp;

namespace KenBonny.CheckHibp
{
    class Program
    {
        private static readonly TimeSpan TwoSeconds = TimeSpan.FromSeconds(2);
        private static readonly RestClient
            Client = new RestClient("https://haveibeenpwned.com/api/v2/breachedaccount/");

        private static int _index = 0;
        private static int _breachedEmailCount = 0;

        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("No input detected");
                return;
            }

            var fileLocation = args[0];
            if (!File.Exists(fileLocation))
            {
                Console.WriteLine("No file selected");
                return;
            }

            try
            {
                var emails = File.ReadAllLines(fileLocation).OrderBy(x => x).ToArray();

                Console.WriteLine($"Start checking {emails.Length} emails");

                var timer = CheckEmails(emails);

                Console.WriteLine($"Out of {emails.Length} emails, {_breachedEmailCount} were found in breaches.");
                Console.WriteLine($"Results found in {timer.Elapsed:g}");
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
                Console.ResetColor();
            }
        }

        private static Stopwatch CheckEmails(string[] emails)
        {
            var timer = Stopwatch.StartNew();
            foreach (var email in emails)
            {
                var response = CheckEmail(email);
                _index++;
                if (response.IsSuccessful)
                    ReportBreach(response.Data, email);
                else
                    ReportSafe(email, response);

                WaitTwoSeconds();
            }

            timer.Stop();
            return timer;
        }

        private static void ReportSafe(string email, IRestResponse<List<Breach>> response)
        {
            Console.WriteLine($"{_index:0000} : Check {email} => {response.StatusDescription}");
        }

        private static void ReportBreach(List<Breach> breaches, string email)
        {
            _breachedEmailCount++;
            var breachText = string.Join(", ", breaches);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_index:0000} : Check {email} => {breachText}");
            Console.ResetColor();
        }

        private static IRestResponse<List<Breach>> CheckEmail(string email)
        {
            var request = new RestRequest($"{email}?truncateResponse=true");
            var response = Client.Get<List<Breach>>(request);
            return response;
        }

        /// <summary>
        /// Wait for 2 seconds to get around rate limit
        /// </summary>
        private static void WaitTwoSeconds()
        {
            Thread.Sleep(TwoSeconds);
        }
    }

    public class Breach
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}