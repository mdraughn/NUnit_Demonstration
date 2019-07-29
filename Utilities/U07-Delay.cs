using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    // Suppose we have a process that issues a fire-and-forget command that
    // doesn't complete instantly, doesn't block, and doesn't let us know when it works.
    // Maybe some kind of background task that produces an output file when done.
    public class FireAndForget
    {
        private volatile bool _fileExists = false;

        public void CreateFileEventually()
        {
            Task.Factory.StartNew(() =>
            {
                // Delay a fraction of a second to simulate work going on.
                Thread.Sleep(250);
                _fileExists = true;
            });
        }

        public bool IsFileThereYet()
        {
            return _fileExists;
            ;
        }
    }

    [TestFixture]
    public partial class Delay
    {
    }


    [TestFixture]
    public class Delay_ExpectedToFail
    {
        // The obvious naive NUnit test will not work, because of the delay.
        [Test]
        [Category("ExpectedToFail")]
        public void NaiveTest_ExpectedToFail()
        {
            var process = new FireAndForget();
            process.CreateFileEventually();

            // The file won't actually be there yet.
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Assert.That(process.IsFileThereYet, Is.True);
            }
            catch(Exception)
            {
                Assert.Fail($"Finished in {stopwatch.Elapsed.TotalSeconds} seconds.");
                throw;
            }
        }
    }

    [TestFixture]
    public partial class Delay
    {
        // We could add our own delay to wait out the file creation.
        [Test]
        public void AddOurOwnDelay()
        {
            var process = new FireAndForget();
            process.CreateFileEventually();

            var stopwatch = Stopwatch.StartNew();

            // Wait for the file...
            Thread.Sleep(500);

            // Now it should be there
            Assert.That(process.IsFileThereYet, Is.True);
            Assert.Pass($"Finished in {stopwatch.Elapsed.TotalSeconds} seconds.");
        }

        // Or we could use the new Delay constraint, referred to with the After modifier.
        [Test]
        public void DelayConstraint()
        {
            var process = new FireAndForget();
            process.CreateFileEventually();

            // Now it should be there
            var stopwatch = Stopwatch.StartNew();
            Assert.That(process.IsFileThereYet, Is.True.After(500));
            Assert.Pass($"Finished in {stopwatch.Elapsed.TotalSeconds} seconds.");
        }

        // The second paramter to After will set a polling interval so it doesn't have
        // to wait out the full delay.
        [Test]
        public void DelayConstraintWithPolling()
        {
            var process = new FireAndForget();
            process.CreateFileEventually();

            // Now it should be there
            var stopwatch = Stopwatch.StartNew();
            Assert.That(process.IsFileThereYet, Is.True.After(500,10));
            Assert.Pass($"Finished in {stopwatch.Elapsed.TotalSeconds} seconds.");
        }
    }
}
