using System;

namespace DIP
{
    /// <summary>
    /// Membership class demonstrates DIP by depending on abstraction (ILogger) rather than concrete implementations
    /// This follows the Dependency Inversion Principle where high-level modules should not depend on low-level modules
    /// Both should depend on abstractions
    /// </summary>
    public class Membership
    {
        // Reference to abstraction, not concrete implementation
        private readonly ILogger logger;

        // Constructor injection - receiving the dependency through constructor
        public Membership(ILogger _logger)
        {
            logger = _logger;
        }

        public void Add()
        {
            try
            {
                // Implementation here
            }
            catch (Exception ex)
            {
                // VIOLATION of DIP: This creates direct dependency on FileLogger
                // Should use the injected logger instead:
                // logger.LogError(ex.Message);
                FileLogger fg = new FileLogger();
                fg.LogError(ex.Message);
            }
        }
    }

    /// <summary>
    /// Interface defining the contract for all loggers
    /// This is our abstraction that both high-level and low-level modules depend on
    /// </summary>
    public interface ILogger
    {
        void LogError(string error);
    }

    /// <summary>
    /// Concrete implementation of ILogger that writes to a file
    /// Low-level module that depends on the ILogger abstraction
    /// </summary>
    public class FileLogger : ILogger
    {
        public void LogError(string error)
        {
            File.WriteAllText(@"c:\Error.txt", error);
        }
    }

    /// <summary>
    /// Another concrete implementation of ILogger that writes to console
    /// Demonstrates how we can easily swap logging implementations
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void LogError(string error)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example of Dependency Injection in action
            // We can easily switch between different logger implementations
            // without changing the Membership class

            // Using FileLogger
            FileLogger fileLogger = new FileLogger();
            Membership membership1 = new Membership(fileLogger);

            // Using ConsoleLogger
            ConsoleLogger consoleLogger = new ConsoleLogger();
            Membership membership2 = new Membership(consoleLogger);
        }
    }
}
