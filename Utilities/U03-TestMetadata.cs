using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    [TestFixture]
    public class TestMetadata
    {
        // Test author.
        [Test]
        [Author("John Smith")]
        public void TestAuthor()
        {
            Assert.AreEqual("John Smith", TestContext.CurrentContext.Test.Properties["Author"].First());
        }

        // Test author and email address.
        [Test]
        [Author("John Smith","John.Smith@superawesomesoftware.com")]
        public void TestAuthorAndEmail()
        {
            Assert.AreEqual("John Smith <John.Smith@superawesomesoftware.com>", TestContext.CurrentContext.Test.Properties["Author"].First());
        }

        // Test category. Can be more than one.
        [Test]
        [Category("SomeTestCategory")]
        [Category("SomeOtherCategory")]
        public void TestCategory()
        {
            Assert.AreEqual("SomeTestCategory", TestContext.CurrentContext.Test.Properties["Category"].First());
            Assert.AreEqual("SomeOtherCategory", TestContext.CurrentContext.Test.Properties["Category"].Skip(1).First());
        }

        // Arbitrary descriptive text.
        [Test]
        [Description("Some descriptive text goes here.")]
        public void TestDescription()
        {
            Assert.AreEqual("Some descriptive text goes here.", TestContext.CurrentContext.Test.Properties["Description"].First());
        }

        // Class under test.
        private class MyTestTarget {}

        [Test]
        [TestOf(typeof(MyTestTarget))]
        public void TestOf()
        {
            Assert.AreEqual(typeof(MyTestTarget).FullName,TestContext.CurrentContext.Test.Properties["TestOf"].First());
        }

        // Properties you make up.  As many name-value pairs as you can stand.
        [Test]
        [Property("SomeTestProperty","Foo")]
        [Property("SomeOtherProperty", "Bar")]
        [Property("SomeOtherProperty", "Baz")]
        public void TestProperty()
        {
            Assert.AreEqual("Foo", TestContext.CurrentContext.Test.Properties["SomeTestProperty"].First());
            Assert.AreEqual("Bar", TestContext.CurrentContext.Test.Properties["SomeOtherProperty"].First());
            Assert.AreEqual("Baz", TestContext.CurrentContext.Test.Properties["SomeOtherProperty"].Skip(1).First());
        }
    }

    // You can also specify these properties on a TestFixture, but the context isn't accessible through
    // the NUnit API when a test is running.

    // Failure to get author.
    [TestFixture]
    [Author("John Smith")]
    [Category("ExpectedToFail")]
    public class FixtureMetadata_Author_ExpectedToFail
    {
        [Test]
        [Category("ExpectedToFail")]
        public void TestAuthor_ExpectedToFail()
        {
            Assert.AreEqual("John Smith", TestContext.CurrentContext.Test.Properties["Author"].First());
        }

    }

    // The solution is to pick up the context during fixture setup,
    // when it points to the fixture context instead of the test.

    // You can put this in a base class for the tests so it's available everywhere.

    public class BaseContextFixture
    {
        protected TestContext FixtureContext;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            FixtureContext = TestContext.CurrentContext;
        }
    }

    // Test author.
    [TestFixture]
    [Author("John Smith")]
    public class FixtureMetadata_Author : BaseContextFixture
    {
        [Test]
        public void TestAuthor()
        {
            Assert.AreEqual("John Smith", FixtureContext.Test.Properties["Author"].First());
        }

    }

    // Test author and email address.
    [TestFixture]
    [Author("John Smith", "John.Smith@superawesomesoftware.com")]
    public class FixtureMetadata_AuthorEmail : BaseContextFixture
    {
        [Test]
        public void TestAuthorAndEmail()
        {
            Assert.AreEqual("John Smith <John.Smith@superawesomesoftware.com>", FixtureContext.Test.Properties["Author"].First());
        }

    }

    // Test category. Can be more than one.
    [TestFixture]
    [Category("SomeTestCategory")]
    [Category("SomeOtherCategory")]
    public class FixtureMetadata_Category : BaseContextFixture
    {
        [Test]
        public void TestCategory()
        {
            Assert.AreEqual("SomeTestCategory", FixtureContext.Test.Properties["Category"].First());
            Assert.AreEqual("SomeOtherCategory", FixtureContext.Test.Properties["Category"].Skip(1).First());
        }

    }

    // Arbitrary descriptive text.
    [TestFixture]
    [Description("Some descriptive text goes here.")]
    public class FixtureMetadata_Description : BaseContextFixture
    {
        [Test]
        public void TestDescription()
        {
            Assert.AreEqual("Some descriptive text goes here.", FixtureContext.Test.Properties["Description"].First());
        }

    }

    // Class under test.
    class MyTestTarget { }

    [TestFixture]
    [TestOf(typeof(MyTestTarget))]
    public class FixtureMetadata_TestOf : BaseContextFixture
    {

        [Test]
        public void TestOf()
        {
            Assert.AreEqual(typeof(MyTestTarget).FullName, FixtureContext.Test.Properties["TestOf"].First());
        }

    }

    // Properties you make up.  As many name-value pairs as you can stand.
    [TestFixture]
    [Property("SomeTestProperty", "Foo")]
    [Property("SomeOtherProperty", "Bar")]
    [Property("SomeOtherProperty", "Baz")]
    public class FixtureMetadata_Properties : BaseContextFixture
    {
        [Test]
        public void TestProperty()
        {
            Assert.AreEqual("Foo", FixtureContext.Test.Properties["SomeTestProperty"].First());
            Assert.AreEqual("Bar", FixtureContext.Test.Properties["SomeOtherProperty"].First());
            Assert.AreEqual("Baz", FixtureContext.Test.Properties["SomeOtherProperty"].Skip(1).First());
        }
    }
}
