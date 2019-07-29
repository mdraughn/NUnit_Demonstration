using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit_Demonstration.SetupAndTeardown
{
    // Suppose we have a global pricelist object...
    public class GlobalData
    {
        public static IDictionary<string, decimal> PriceList { get; private set; }

        static GlobalData()
        {
            PriceList = new Dictionary<string, decimal>();
        }
    }

    // The list is normally empty.
    [TestFixture]
    public class CustomSetupAndTearddownNotRun
    {
        [Test]
        public void PriceListIsEmpty()
        {
            Assert.AreEqual(0, GlobalData.PriceList.Count);
            Assert.IsFalse(GlobalData.PriceList.ContainsKey("apple"));
        }
    }

    // We could init it with the usual setup.
    [TestFixture]
    public class CustomSetupAndTearddownClassSetup
    {
        [SetUp]
        public void Setup()
        {
            GlobalData.PriceList.Add("apple", 1.29m);
            GlobalData.PriceList.Add("banana", 0.87m);
            GlobalData.PriceList.Add("cherry", 0.08m);
            GlobalData.PriceList.Add("cantaloupe", 3.99m);
        }

        [Test]
        public void PriceListIsPopulated()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["apple"]);
            Assert.AreEqual(3.99m, GlobalData.PriceList["cantaloupe"]);
        }

        [TearDown]
        public void Teardown()
        {
            GlobalData.PriceList.Clear();
        }
    }

    // But suppose we want to initialize it to several different lists for several different tests?
    // And we don't want to have different classes for each set of tests?

    // We can implement ITestAction
    // public interface ITestAction
    // {
    //     void BeforeTest(TestDetails details);
    //     void AfterTest(TestDetails details);
    //     ActionTargets Targets { get; }
    //}

    [AttributeUsage(AttributeTargets.Method)]
    public class WithFruitPricesAttribute : Attribute, ITestAction
    {
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        // This action is targeted at each test.
        public void BeforeTest(ITest testDetails)
        {
            GlobalData.PriceList.Add("apple", 1.29m);
            GlobalData.PriceList.Add("banana", 0.87m);
            GlobalData.PriceList.Add("cherry", 0.08m);
        }

        public void AfterTest(ITest testDetails)
        {
            GlobalData.PriceList.Clear();
        }
    }

    // Now we can do tests on fruit prices.
    [TestFixture]
    public class CustomSetupAndTearddownWithFruits
    {
        [Test,WithFruitPrices]
        public void PriceListIsPopulated()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["apple"]);
            Assert.AreEqual(0.08m, GlobalData.PriceList["cherry"]);
        }
    }

    // If we also need to do tests on vegetables, we can add that too.
    // This time we'll inherit from the NUnit-provided TestActionAttribute
    // and we will take the default Targets value which allows it to be applied
    // to classes.
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class WithVegetablePricesAttribute : TestActionAttribute
    {
        public override void BeforeTest(ITest testDetails)
        {
            GlobalData.PriceList.Add("potato", 1.59m);
            GlobalData.PriceList.Add("carrot", 0.79m);
        }
        public override void AfterTest(ITest testDetails)
        {
            GlobalData.PriceList.Clear();
        }
    }

    // Now we can mix and match initializations.
    [TestFixture]
    public class CustomSetupAndTearddownWithFruitsAndVegetables
    {
        [Test, WithFruitPrices]
        public void FruitsOnly()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["apple"]);
            Assert.AreEqual(0.08m, GlobalData.PriceList["cherry"]);
            Assert.IsFalse(GlobalData.PriceList.ContainsKey("potato"));
            Assert.IsFalse(GlobalData.PriceList.ContainsKey("carrot"));
        }

        [Test, WithVegetablePrices]
        public void VegetablesOnly()
        {
            Assert.IsFalse(GlobalData.PriceList.ContainsKey("apple"));
            Assert.IsFalse(GlobalData.PriceList.ContainsKey("cherry"));
            Assert.AreEqual(1.59m, GlobalData.PriceList["potato"]);
            Assert.AreEqual(0.79m, GlobalData.PriceList["carrot"]);
        }

        [Test, WithFruitPrices, WithVegetablePrices]
        public void FruitsAndVegetables()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["apple"]);
            Assert.AreEqual(0.08m, GlobalData.PriceList["cherry"]);
            Assert.AreEqual(1.59m, GlobalData.PriceList["potato"]);
            Assert.AreEqual(0.79m, GlobalData.PriceList["carrot"]);
        }
    }

    // Because WithVegetables has default targets, it can also work at the class level.
    [TestFixture, WithVegetablePrices]
    public class CustomSetupAndTearddownWithMixedSetup
    {
        [Test, WithFruitPrices]
        public void FruitsAndVegetables()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["apple"]);
            Assert.AreEqual(0.08m, GlobalData.PriceList["cherry"]);
            Assert.AreEqual(1.59m, GlobalData.PriceList["potato"]);
            Assert.AreEqual(0.79m, GlobalData.PriceList["carrot"]);
        }
    }

    // Custom setup actions can take arguments.
    // Setting AllowMultiple allows the attribute to be used several times.
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class WithBrandedFruitPricesAttribute : TestActionAttribute
    {
        private string m_brand;
        private double m_priceMultiplier;

        public WithBrandedFruitPricesAttribute(string brand, double priceMultiplier = 1)
        {
            m_brand = brand;
            m_priceMultiplier = priceMultiplier;
        }

        public override void BeforeTest(ITest testDetails)
        {
            GlobalData.PriceList.Add(m_brand + "_apple", (decimal) (m_priceMultiplier*(double)1.29m));
            GlobalData.PriceList.Add(m_brand + "_banana", (decimal) (m_priceMultiplier*(double)0.87m));
            GlobalData.PriceList.Add(m_brand + "_cherry", (decimal) (m_priceMultiplier*(double)0.08m));
        }

        public override void AfterTest(ITest testDetails)
        {
            GlobalData.PriceList.Clear();
        }
    }

    // Pass the arguments in the attribute.
    [TestFixture]
    public class CustomSetupAndTearddownWithBrandedFruits
    {

        [Test,
        WithBrandedFruitPrices("serengeti"),
        WithBrandedFruitPrices("thomsonreuters", 2.0)]
        public void CheckBrandedPrices()
        {
            Assert.AreEqual(1.29m, GlobalData.PriceList["serengeti_apple"]);
            Assert.AreEqual(0.08m, GlobalData.PriceList["serengeti_cherry"]);
            Assert.AreEqual(2.58m, GlobalData.PriceList["thomsonreuters_apple"]);
            Assert.AreEqual(0.16m, GlobalData.PriceList["thomsonreuters_cherry"]);
        }

    }

}
