using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit_Demonstration.Data_Driven_Tests
{
    // NOTE: Not supported by Resharper test runner yet.

    [TestFixtureSource("FixtureArgs")]
    public class DataDependentTestFixture
    {
        private readonly string _databaseName;

        public DataDependentTestFixture(string databaseName)
        {
            TestContext.Out.WriteLine($"Instantiating {databaseName}");
            _databaseName = databaseName;
        }

        public static TestFixtureData[] FixtureArgsxxx = {
            new TestFixtureData("FirstDatabase"),
            new TestFixtureData("SecondDatabase"),
            new TestFixtureData("ThirdDatabase"),
        };

        public static object[] FixtureArgs =
        {
            new object[] { "FirstDatabase"},
            new object[] { "SecondDatabase"},
            new object[] { "ThirdDatabase"},
        };

        [Test]
        public void SomeTest()
        {
            // Pretend we test something in the database here.

            Assert.Pass($"Test passed on database {_databaseName}.");
        }
    }
}
