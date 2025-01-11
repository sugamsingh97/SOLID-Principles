using System;

namespace SRP
{
    // Membership class focuses solely on membership management
    public class Membership
    {
        public void Add()
        {
            try
            {
                // Core membership addition logic
            }
            catch (Exception ex)
            {
                // Delegates error logging to specialized FileLogger class
                FileLogger.LogError(ex.Message);
            }
        }
    }

    // FileLogger class has single responsibility of handling error logging
    public static class FileLogger
    {
        public static void LogError(string error)
        {
            File.WriteAllText(@"c:\Error.txt", error);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello my world");
        }
    }
}
