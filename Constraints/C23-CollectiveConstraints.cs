using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using System.IO;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public partial class CollectiveConstraints
    {
        // Collection constraints can assert some things for all elements.

        // Use the Has.All construct to apply a constraint to all items in a collection.

        [Test]
        public void AllItemsAreNotNull()
        {
            // Check that nothing is null
            object[] things = {1, 2, "rhubarb", false, new FileNotFoundException()};
            Assert.That(things, Is.All.Not.Null);
            Assert.That(things, Has.All.Not.Null);
        }

        [Test]
        public void AllItemsAreInstancesOfType()
        {
            // Require a certain type.
            object[] strings = {"", "rhubarb", 5.ToString()};
            Assert.That(strings, Is.All.InstanceOf<string>());
            Assert.That(strings, Has.All.InstanceOf<string>());

            object[] nodes = {new XElement("foo"), new XText("asdf"), new XDocument()};
            Assert.That(nodes, Is.All.InstanceOf<XNode>());
            Assert.That(nodes, Has.All.InstanceOf<XNode>());
        }

        [Test]
        public void AllItemsAreUnique()
        {
            string[] strings = {"", "rhubarb", 5.ToString()};
            Assert.That(strings, Is.Unique);

            string[] strings2 = {"5", "rhubarb", 5.ToString()};
            Assert.That(strings2, Is.Not.Unique);
        }

        [Test]
        public void IsOrdered()
        {
            string[] strings = {"aaa", "aab", "aba", "abb", "baa"};
            Assert.That(strings, Is.Ordered);
        }

        [Test]
        public void IsOrderedDescending()
        {
            string[] strings = {"baa", "abb", "aba", "aab", "aaa"};
            Assert.That(strings, Is.Ordered.Descending);
        }

        // You can also reach down into object properties to do an ordering check.
        class TestItem
        {
            public TestItem(int part, string subPart)
            {
                Part = part;
                SubPart = subPart;
            }

            public int Part { get; private set; }
            public string SubPart { get; private set; }
        }

        [Test]
        public void IsOrderedByProperty()
        {
            TestItem[] testItems =
            {
                new TestItem(1, "a"),
                new TestItem(1, "b"),
                new TestItem(1, "c"),
                new TestItem(2, "a"),
                new TestItem(2, "b"),
                new TestItem(2, "c"),
                new TestItem(3, "a"),
                new TestItem(3, "b"),
                new TestItem(3, "c"),
            };

            // By one property.
            Assert.That(testItems, Is.Ordered.By("Part"));
            Assert.That(testItems, Is.Ordered.Ascending.By("Part"));

            // By multiple properties.
            Assert.That(testItems, Is.Ordered.By("Part").Then.By("SubPart"));
            Assert.That(testItems, Is.Ordered.Ascending.By("Part").Then.Ascending.By("SubPart"));
        }
    }

    // Note that it now works on .NET fields, not just properties.

    [TestFixture]
    public class CollectiveConstraints_Fields
    {

        class BadTestItem
        {
            public BadTestItem(int part)
            {
                Part = part;
            }

            public int Part;
        }

        // You can also reach down into object properties to do an ordering check.
        [Test]
        public void CantOrderByField()
        {
            BadTestItem[] testItems =
            {
                new BadTestItem(1),
                new BadTestItem(2),
                new BadTestItem(3),
            };

            // Can't read the Part field.
            Assert.That(testItems, Is.Ordered.By("Part"));
        }
    }

    [TestFixture]
    public partial class CollectiveConstraints
    {

        // You can also define a custom comparison.
        public class Vehicle : IComparable<Vehicle>
        {
            public float Weight { get; set; }

            public int CompareTo(Vehicle other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }

        [Test]
        public void IsOrderedComparable()
        {
            // Ordering works with anything that implements IComparable.
            Vehicle[] vehicles = {
                                     new Vehicle() {Weight = 1000},
                                     new Vehicle() {Weight = 2000},
                                     new Vehicle() {Weight = 2500}
                                 };
            Assert.That(vehicles, Is.Ordered);
        }

        // You can also use a custom IComparer
        public class VehicleComparer : IComparer<Vehicle>
        {
            public int Compare(Vehicle x, Vehicle y)
            {
                return x.Weight.CompareTo(y.Weight);
            }
        }

        [Test]
        public void IsOrderedCustomComparer()
        {
            // Ordering works with anything that implements IComparable.
            Vehicle[] vehicles = {
                                     new Vehicle() {Weight = 1000},
                                     new Vehicle() {Weight = 2000},
                                     new Vehicle() {Weight = 2500}
                                 };
            Assert.That(vehicles, Is.Ordered.Using(new VehicleComparer()));
        }

        // Properties can use custom comparison too.
        class TestWithVehicle
        {
            public TestWithVehicle(int part, Vehicle vehicle)
            {
                Part = part;
                Vehicle = vehicle;
            }
            public int Part { get; private set; }
            public Vehicle Vehicle { get; private set; }
        }

        [Test]
        public void IsOrderedByComparableProperty()
        {
            TestWithVehicle[] testItems =
            {
                new TestWithVehicle(1, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(1, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(1, new Vehicle() {Weight = 3000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 3000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 3000}),
            };

            // With default IComparable comparison.
            Assert.That(testItems, Is.Ordered.By("Part").Then.By("Vehicle"));
            Assert.That(testItems, Is.Ordered.Ascending.By("Part").Then.Ascending.By("Vehicle"));
        }

        [Test]
        public void IsOrderedByPropertyWithComparer()
        {
            TestWithVehicle[] testItems =
            {
                new TestWithVehicle(1, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(1, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(1, new Vehicle() {Weight = 3000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(2, new Vehicle() {Weight = 3000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 1000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 2000}),
                new TestWithVehicle(3, new Vehicle() {Weight = 3000}),
            };

            // With custom IComparer.
            Assert.That(testItems, Is.Ordered.By("Part").Then.By("Vehicle").Using(new VehicleComparer()));
            Assert.That(testItems, Is.Ordered.Ascending.By("Part").Then.Ascending.By("Vehicle").Using(new VehicleComparer()));
        }


        // The All expresssion can produce array constraints with no equivalent assertions.

        [Test]
        public void AllItemsConstraint()
        {
            // Require that all match.
            int[] speeds = { 35, 55, 40, 53, 37 };
            Assert.That(speeds, Has.All.LessThanOrEqualTo(55));
        }

        // Other cardinalities are allowed.

        [Test]
        public void SomeItemsConstraint()
        {
            // Require that at least one matches.
            int[] speeds = { 35, 55, 40, 58, 37 };
            Assert.That(speeds, Has.Some.GreaterThan(55));
        }

        [Test]
        public void ExactlyConstraint()
        {
            // Require that a specified number of items match.
            int[] speeds = { 35, 55, 40, 58, 37 };
            Assert.That(speeds, Has.Exactly(2).GreaterThan(45));
            Assert.That(speeds, Has.Exactly(3).GreaterThanOrEqualTo(40));
        }

        [Test]
        public void NoneConstraint()
        {
            // Require that no items match.
            int[] speeds = { 35, 55, 40, 58, 37 };
            Assert.That(speeds, Has.None.GreaterThan(60));

            // Same as this inverse:
            Assert.That(speeds, Has.All.LessThanOrEqualTo(60));
        }


    }
}
