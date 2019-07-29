using System.Collections.ObjectModel;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
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
            Assert.That(fruits, Contains.Item("banana"));
            Assert.That(fruits, Has.Member("banana"));
            Assert.That(fruits, Does.Contain("banana"));

            // The latter syntax has a negation.
            Assert.That(fruits, Has.No.Member("potato"));
            Assert.That(fruits, Does.Not.Contain("potato"));
        }

        [Test]
        public void CollectionSubsetAndSuperset()
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

            // Subset version.
            Assert.That(fruits, Is.SubsetOf(fruitsAndVeggies));
            Assert.That(fruitsAndNuts, Is.Not.SubsetOf(fruitsAndVeggies));

            // Superset version.
            Assert.That(fruitsAndVeggies, Is.SupersetOf(fruits));
            Assert.That(fruitsAndVeggies, Is.Not.SupersetOf(fruitsAndNuts));
        }

        [Test]
        public void IsEmpty()
        {
            var greatSilverlightWebsites = new Collection<string>();
            var greatMatterManagementTools = new Collection<string>() { "Serengeti Tracker" };

            Assert.That(greatSilverlightWebsites, Is.Empty);
            Assert.That(greatMatterManagementTools, Is.Not.Empty);
        }

    }
}
