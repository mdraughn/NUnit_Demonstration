using System;
using System.Xml.Linq;
using NUnit.Framework;
using System.IO;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public class CollectiveAssertions
    {
        // Collection assertions can assert some things for all elements.
        // Since 2.4.6 works on all enumerables.

        [Test]
        public void AllItemsAreNotNull()
        {
            // Check that nothing is null
            object[] things = {1, 2, "rhubarb", false, new FileNotFoundException() };
            CollectionAssert.AllItemsAreNotNull(things);
        }

        [Test]
        public void AllItemsAreInstancesOfType()
        {
            // Require a certain type.
            object[] strings = { "", "rhubarb", 5.ToString() };
            CollectionAssert.AllItemsAreInstancesOfType(strings,typeof(string));

            object[] nodes = { new XElement("foo"), new XText("asdf"), new XDocument() };
            CollectionAssert.AllItemsAreInstancesOfType(nodes, typeof(XNode));
        }

        [Test]
        public void AllItemsAreUnique()
        {
            string[] strings = { "", "rhubarb", 5.ToString() };
            CollectionAssert.AllItemsAreUnique(strings);
        }

        [Test]
        public void IsOrdered()
        {
            // Since 2.5, can check ordering.
            string[] strings = { "aaa", "aab", "aba", "abb", "baa" };
            CollectionAssert.IsOrdered(strings);
        }

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
            CollectionAssert.IsOrdered(vehicles);
        }

    }
}
