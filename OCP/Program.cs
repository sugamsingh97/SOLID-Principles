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
