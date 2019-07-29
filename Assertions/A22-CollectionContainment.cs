using System.Collections.ObjectModel;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public class CollectionContainment
    {
        [Test]
        public void StandardContains()
        {
            // You'd think Assert.Contains is for strings, but it's for collections only.
            var fruits = new Collection<string>()
                          {
                              "apple",
                              "banana",
                              "cherry"
                          };
            Assert.Contains("banana", fruits);

            // No negation!
            //Assert.DoesNotContain("potato", fruits);
        }

        [Test]
        public void CollectionContains()
        {
            var fruits = new Collection<string>()
                          {
                              "apple",
                              "banana",
                              "cherry"
                          };

            // The collection version is reversed.
            CollectionAssert.Contains(fruits, "banana");

            // But it does have a negation.
            CollectionAssert.DoesNotContain(fruits, "orange");
        }

        [Test]
        public void CollectionIsSubset()
        {
            var fruitsAndVeggies = new Collection<string>()
                          {
                              "corn",
                              "apple",
                              "potato",
                              "banana",
                              "carrot",
                              "cherry",
                              "onion"
                          };
            var fruits = new Collection<string>()
                          {
                              "apple",
                              "banana",
                              "cherry"
                          };
            var fruitsAndNuts = new Collection<string>()
                          {
                              "apple",
                              "cashew",
                              "banana",
                              "walnut",
                              "cherry"
                          };

            CollectionAssert.IsSubsetOf(fruits, fruitsAndVeggies);
            CollectionAssert.IsNotSubsetOf(fruitsAndNuts, fruitsAndVeggies);           
        }

        [Test]
        public void IsEmpty()
        {
            var greatSilverlightWebsites = new Collection<string>();
            var greatMatterManagementTools = new Collection<string>() { "Serengeti Tracker" };

            CollectionAssert.IsEmpty(greatSilverlightWebsites);
            CollectionAssert.IsNotEmpty(greatMatterManagementTools);
        }

    }
}
