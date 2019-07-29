using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    [TestFixture]
    public class TheorySimple
    {
        // Theories are used to verify that invariants are true under a variety of operations.

        // Let's say we have an object with a simple boolean parameter in its constructor,
        // and we want to verify that its internal state is valid for both cases of the parameter.
        public class SimpleThing
        {
            public SimpleThing(bool whatever)
            {
            }

            public bool ValidateInternalState()
            {
                // For purposes of this example, assume it's always valid.
                return true;
            }
        }

        // Now let's test the theory that the internal state is valid for all possible parameters.
        // The test runner will generate tests for all possible parameter values.
        [Theory]
        public void SimpleThingIsValidAfterConstruction(bool flag)
        {
            var thing = new SimpleThing(flag);
            Assert.True(thing.ValidateInternalState());
        }
    }

    // This will also work with more than one argument.
    [TestFixture]
    public class TheoryComplex
    {
        // NOTE: Does not support [Flags] - it won't generate all combinations.
        public enum ExecutionMode
        {
            Slow,
            Medium,
            Fast
        }

        public class ComplexThing
        {
            public ComplexThing(ExecutionMode mode, bool flag)
            {

            }

            public bool ValidateInternalState()
            {
                // For purposes of this example, assume it's always valid.
                return true;
            }
        }

        // Now let's test this one. The runtime will generate tests for all possible
        // combinations of parameter values.
        [Theory]
        public void ComplexThingIsValidAfterConstruction(ExecutionMode mode, bool flag)
        {
            var thing = new ComplexThing(mode, flag);
            Assert.True(thing.ValidateInternalState());
        }
    }

    // When parameters are not enums and bools, it would be impractical to test over all
    // possible values, so we have to specify valid test subsets in the form of Datapoint sources.

    public class TheoryWithDatapoints
    { 
        // NOTE: Does not support [Flags] - it won't generate all combinations.
        public enum ExecutionMode
        {
            Slow,
            Medium,
            Fast
        }

        // In this case, let's use some data points for construction and the others for method calls.
        public class VeryComplexThing
        {
            public VeryComplexThing(bool flag, int retries)
            {
            }

            public void TheMethod(string url, ExecutionMode mode)
            {
            }

            public bool ValidateInternalState()
            {
                // For purposes of this example, assume it's always valid.
                return true;
            }
        }

        // Now let's specify value lists for the strings and ints.
        [DatapointSource]
        // ReSharper disable once UnusedMember.Local
        private string[] _urls = { "google.com", "facebook.com", "microsoft.com" };

        [DatapointSource]
        // ReSharper disable once UnusedMember.Local
        private int[] _retries = { 0, 1, 2 };

        // This should try 54 different test cases.
        [Theory]
        public void VeryComplexThingIsValidAfterConstruction(string url, ExecutionMode mode, bool flag, int retries)
        {
            var thing = new VeryComplexThing(flag, retries);
            thing.TheMethod(url, mode);
            Assert.True(thing.ValidateInternalState());
        }

    }

    // Maybe not all possibilities work. We can verify that with assumptions.
    public class TheoryWithAssumptions
    { 
        public enum ExecutionMode
        {
            Slow,
            Medium,
            Fast
        }

        // Let's assume that Fast mode is not allowed when the flag is false,
        // so our theory should assume that the mode cannot be fast if the flag is false.
        public class VeryComplexThing
        {
            private readonly bool _flag;

            public VeryComplexThing(bool flag, int retries)
            {
                _flag = flag;
            }

            public void TheMethod(string url, ExecutionMode mode)
            {
                if (mode == ExecutionMode.Fast && !_flag)
                {
                    throw new ArgumentException();
                }
            }

        }

        // Now let's specify value lists for the strings and ints.
        [DatapointSource]
        // ReSharper disable once UnusedMember.Local
        private string[] _urls = { "google.com", "facebook.com", "microsoft.com" };

        [DatapointSource]
        // ReSharper disable once UnusedMember.Local
        private int[] _retries = { 0, 1, 2 };

        // This should try 54 different test cases.
        [Theory]
        public void VeryComplexThingIsValidAfterConstruction(string url, ExecutionMode mode, bool flag, int retries)
        {
            // Since it's invalid to call the method in fast mode when the flag is false,
            // assume that case away.

            Assume.That(flag || mode!=ExecutionMode.Fast);

            var thing = new VeryComplexThing(flag, retries);
            Assert.That(()=>thing.TheMethod(url, mode),Throws.Nothing);
        }

    }
}
