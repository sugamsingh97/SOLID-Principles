# SOLID Principles Implementation

## Single Responsibility Principle (SRP)
```csharp
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

```
### Key aspects of this SRP implementation:

#### Clear Separation of Concerns:
- Membership class handles membership operations
- FileLogger class manages error logging
- Each class has exactly one reason to change

#### Focused Responsibilities:
- Membership: Managing membership data
- FileLogger: Handling error logging to file system

#### Benefits:
- Enhanced maintainability
- Easier testing
- Better code organization
- Reduced coupling

#### Design Features:
- Static utility class for logging
- Clean error handling
- Clear class boundaries
- Modular structure

#### This implementation delivers:
- Clear code organization
- Easy maintenance
- Simple testing
- Focused functionality
- Scalable architecture

---

## Open-Closed Principle (OCP)
```csharp
using System;

namespace OCP
{
    // Base class that is open for extension but closed for modification
    public class Membership
    {
        public void Add()
        {
            // Core membership addition logic
        }

        // Virtual method enables extension through inheritance
        // Base implementation provides default behavior
        public virtual int GetTraining()
        {
            return 2; // Default number of trainings
        }
    }

    // Extension of base functionality through inheritance
    // Demonstrates OCP by adding new behavior without modifying existing code
    public class ProMembership : Membership
    {
        public override int GetTraining()
        {
            return 10; // Enhanced number of trainings for pro members
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

```
### Key aspects of this OCP implementation:

#### Base Class Design:
- Membership class provides core functionality
- Virtual methods enable extension points
- Original code remains unchanged when adding new features

#### Extension Mechanism:
- ProMembership extends base functionality
- Uses inheritance to add new behavior
- Maintains existing interfaces and contracts

#### Benefits:
- Easy to add new membership types
- Existing code remains stable
- Reduces risk of bugs in existing functionality
- Promotes code reuse

#### Best Practices:
- Clear separation of core and extended functionality
- Use of virtual methods for extensibility
- Clean inheritance hierarchy

#### This implementation enables:
- Easy addition of new membership types
- Stable core functionality
- Flexible behavior modification
- Maintainable codebase
- Clear extension points

---

## Liskov Substitution Principle (LSP)
```csharp
using System;

namespace LSP
{
    // Interface defining the training contract
    public interface ITraining
    {
        int GetTraining();
    }

    // Interface extending ITraining with membership capabilities
    public interface IMembership : ITraining
    {
        public void Add();
    }

    // Base Membership class that can be substituted by its derivatives
    public class Membership : IMembership
    {
        public virtual void Add()
        {
            // Base implementation of adding a membership
        }

        // Base implementation returns 2 trainings
        public virtual int GetTraining()
        {
            return 2;
        }
    }

    // ProMembership extends base Membership and can be substituted wherever Membership is used
    public class ProMembership : Membership
    {
        public override int GetTraining()
        {
            return 10;  // Pro members get 10 trainings
        }
    }

    // PlusMembership extends base Membership and maintains substitutability
    public class PlusMembership : Membership
    {
        public override int GetTraining()
        {
            return 20;  // Plus members get 20 trainings
        }
    }

    // TrialMembership demonstrates proper LSP implementation by NOT inheriting from Membership
    // Since it cannot fulfill all Membership responsibilities, it only implements ITraining
    public class TrialMembership : ITraining
    {
        public int GetTraining()
        {
            return 2;  // Trial members get 2 trainings
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Demonstrating LSP - all derived classes can be used in place of base class
            List<Membership> membershipList = new List<Membership>();
            membershipList.Add(new ProMembership());
            membershipList.Add(new PlusMembership());

            // All derived classes work correctly when treated as base class
            foreach (var item in membershipList)
            {
                item.Add();
            }
        }
    }
}

```
### Key aspects of this LSP implementation:

#### Proper Inheritance:
- ProMembership and PlusMembership correctly extend Membership
- Each derived class maintains the base class contract
- Behavior remains consistent with base class expectations

#### Smart Interface Usage:
- TrialMembership demonstrates proper design by implementing only ITraining
- Avoids forcing inheritance when full substitutability isn't possible
- Shows interface segregation working with LSP

#### Substitutability:
- All derived classes can be used wherever base class is expected
- No unexpected behaviors when using derived classes
- Maintains type safety and contract adherence

#### Clean Design:
- Clear hierarchy of responsibilities
- Proper use of virtual methods for extensibility
- Demonstrates when to use inheritance vs interface implementation

#### This implementation showcases:
- Correct application of LSP
- Clean inheritance hierarchies
- Smart interface usage
- Type-safe substitutability
- Maintainable and extensible code structure

---

## Interface Segregation Principle (ISP)

### Key aspects of this ISP implementation:
```csharp
using System;

namespace ISP
{
    // Interface Segregation Principle demonstrates that clients should not be forced
    // to depend on interfaces they don't use

    // Base interface for training functionality
    public interface ITraining
    {
        int GetTraining();
    }

    // Interface for membership management, inherits training capabilities
    public interface IMembership : ITraining
    {
        public void Add();
    }

    // Interface specifically for live training features
    public interface ILiveTraining : IMembership
    {
        int GetLiveTraining();
    }

    // Base membership class implementing all features
    public class Membership : ILiveTraining
    {
        public virtual void Add()
        {
        }

        public virtual int GetTraining()
        {
            return 2;
        }

        public virtual int GetLiveTraining()
        {
            return 5;
        }
    }

    // Pro membership with enhanced training offerings
    public class ProMembership : Membership
    {
        public override int GetTraining()
        {
            return 10;
        }
    }

    // Plus membership with maximum training offerings
    public class PlusMembership : Membership
    {
        public override int GetTraining()
        {
            return 20;
        }
    }

    // Premium membership with customized features
    public class PremiumMembership : Membership
    {
        public override void Add()
        {
        }

        public override int GetTraining()
        {
            return 10;
        }

        public override int GetLiveTraining()
        {
            return 5;
        }
    }

    // Trial membership only implements basic training features
    public class TrialMembership : ITraining
    {
        public int GetTraining()
        {
            return 2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example for existing clients with self-paced training
            IMembership membership1 = new Membership();
            membership1.Add();

            // Example for new clients with both live and self-paced training
            ILiveTraining membership2 = new Membership();
            membership2.GetLiveTraining();
            membership2.Add();
            membership2.GetTraining();
        }
    }
}

```
#### Interface Hierarchy:
- ITraining: Basic training functionality
- IMembership: Extends ITraining with membership features
- ILiveTraining: Extends IMembership with live training capabilities

#### Segregated Responsibilities:
- Each interface handles specific functionality
- Classes can implement only the interfaces they need
- TrialMembership only implements ITraining, demonstrating interface segregation

#### Flexible Implementation:
- Different membership types can override specific behaviors
- Clients can work with exactly the interface they need
- Clean separation of concerns through interface segregation

#### Usage Examples:
- Shows how different clients can use different interface combinations
- Demonstrates type safety and feature access control
- Illustrates proper interface segregation in action

#### This implementation enables:
- Modular code structure
- Flexible feature combinations
- Clear client contracts
- Easy maintenance and extensions
- Type-safe feature access

---

## Dependency Inversion Principle (DIP)
```csharp
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

```
### Key points about this DIP implementation:

- The high-level Membership class depends on the ILogger abstraction, not concrete implementations
- The low-level FileLogger and ConsoleLogger classes also depend on the ILogger abstraction
- Dependencies are injected through constructor injection
- The code demonstrates loose coupling - we can easily swap logging implementations
- There's one DIP violation in the Add() method that should be fixed by using the injected logger

#### To fix the DIP violation in the Add() method, replace:
```csharp
FileLogger fg = new FileLogger();
fg.LogError(ex.Message);
```

#### with:

```csharp
logger.LogError(ex.Message);
```
