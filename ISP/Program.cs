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
