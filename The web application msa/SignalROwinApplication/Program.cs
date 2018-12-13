namespace SignalROwinApplication
{
    using System;
    using Microsoft.Owin.Hosting;
//comment added
    public class Program
    {
        public static void Main(string[] args)
        {
            const string Uri = "http://localhost:8088/";

            using (WebApp.Start<Startup>(Uri))
            {
                Console.WriteLine("Server started.");
                Console.ReadKey();
                Console.WriteLine("Server stoped.");
            }
        }
    }
}
