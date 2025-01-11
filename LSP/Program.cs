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
