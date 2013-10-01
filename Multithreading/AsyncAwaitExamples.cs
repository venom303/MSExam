using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Multithreading
{
    class AsyncAwaitExamples
    {
        [Example("Standard Call Example", false)]
        public void StandardCallExample()
        {

            var stopwatch = new Stopwatch();
            
            stopwatch.Start();

            var count = CountWebsitesContentLength();
            Console.WriteLine("Synchronous count: " + count);
            stopwatch.Stop();

            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static int CountWebsitesContentLength()
        {
            var webClient = new WebClient();

            var google = webClient.DownloadString("http://google.pl");
            var onet = webClient.DownloadString("http://onet.pl");
            var redtube = webClient.DownloadString("http://redtube.com");
            var sabre = webClient.DownloadString("http://sabre.com");
            var rac = webClient.DownloadString("https://www.sabreredappcentre.sabre.com/");

            return google.Length + onet.Length + redtube.Length + sabre.Length + rac.Length;
        }

        [Example("Async Call Example", false)]
        public void AsyncCallExample()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            try
            {
                var count = GetWebsitesContentAsync();
                Console.WriteLine("Asynchronous count: " + count.Result);
            }

            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine(ex);
                }
            }

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static async Task<int> GetWebsitesContentAsync()
        {
            var google = new WebClient().DownloadStringTaskAsync("http://google.pl");
            var onet = new WebClient().DownloadStringTaskAsync("http://onet.pl");
            var redtube = new WebClient().DownloadStringTaskAsync("http://redtube.com");
            var sabre = new WebClient().DownloadStringTaskAsync("http://sabre.com");
            var rac = new WebClient().DownloadStringTaskAsync("https://www.sabreredappcentre.sabre.com/");

            await Task.WhenAll(google, onet, redtube, sabre, rac);

            return google.Result.Length + onet.Result.Length + redtube.Result.Length + sabre.Result.Length + rac.Result.Length;
        }

        [Example("RAC Mock operations", false)]
        public void RACOperations()
        {
            var completed = SendMomRequest();
            var response = SendSVSRequest();
            SaveToDatabase();

            if (completed.Result)
                Console.WriteLine(response.Result);

            Console.ReadKey();
        }

        private async Task<bool> SendMomRequest()
        {
            await SleepAsync(2000);
            Console.WriteLine("Mom equest sent...");
            return true;
        }

        private async Task<string> SendSVSRequest()
        {
            await SleepAsync(2000);
            Console.WriteLine("Svs request sent...");
            return "Success";
        }

        private void SaveToDatabase()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Saved...");
        }

        private void Sleep(int sleepTime)
        {
            Thread.Sleep(sleepTime);
        }

        private async Task SleepAsync(int sleepTime)
        {
            await Task.Run(() => Sleep(sleepTime));
        }
    }
}
