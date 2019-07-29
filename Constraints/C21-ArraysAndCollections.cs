using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    // NUnit supports lots of array and collection comparisons.

    [TestFixture]
    public class ArraysAndCollections
    {
        [Test]
        public void OneDimensionEqual()
        {
            int[] x = { 1, 2, 3, 4, 5 };
            int[] y = { 1, 2, 3, 4, 5 };
            Assert.That(x, Is.EqualTo(y));
        }

        [Test]
        public void OneDimensionEquivalent()
        {
            int[] x = { 1, 2, 3, 4, 5 };
            int[] y = { 3, 1, 4, 5, 2 };
            Assert.That(x, Is.EquivalentTo(y));
        }

        [Test]
        public void TwoDimensions()
        {
            int[,] x = {
                           {1,2,3},
                           {4,5,6},
                           {7,8,9}
                       };
            int[,] y = {
                           {1,2,3},
                           {4,5,6},
                           {7,8,9}
                       };
            Assert.That(x, Is.EqualTo(y));
        }

        [Test]
        public void ArrayOfArray()
        {
            int[][] x = {
                           new [] {1,2,3},
                           new [] {4,5,6},
                           new [] {7,8,9}
                       };
            int[][] y = {
                           new [] {1,2,3},
                           new [] {4,5,6},
                           new [] {7,8,9}
                       };
            Assert.That(x, Is.EqualTo(y));
        }

        [Test]
        public void Lists()
        {
            var x = new List<string>() { "alfa", "bravo", "charlie", "delta", "echo", "foxtrot" };
            var y = new List<string>() { "alfa", "bravo", "charlie", "delta", "echo", "foxtrot" };
            Assert.That(x, Is.EqualTo(y));
        }

        [Test]
        public void ListOfNestedArrays()
        {
            var x = new List<int[][]>() {
                new [] {
                    new [] {1,2,3},
                    new [] {4,5,6},
                    new [] {7,8,9}
                },
                new [] {
                    new [] {10,11,12},
                    new [] {13,14,15},
                    new [] {99,55,23}
                }
            };
            var y = new List<int[][]>() {
                new [] {
                    new [] {1,2,3},
                    new [] {4,5,6},
                    new [] {7,8,9}
                },
                new [] {
                    new [] {10,11,12},
                    new [] {13,14,15},
                    new [] {99,55,23}
                }
            };
            Assert.That(x, Is.EqualTo(y));
        }

        // Example iteration.
        public IEnumerable<int> EnumerateInts(int fromInt, int toInt)
        {
            for (int i = fromInt; i <= toInt; i++) {
                yield return i;
            }
        }

        [Test]
        public void Enumerations()
        {
            // NUnit compares enumerations.
            Assert.That(EnumerateInts(1, 10), Is.EqualTo(EnumerateInts(1, 10)));
        }

        [Test]
        public void MixedEnumerations()
        {
            // You can mix types if they are both enumerable.

            // Enumeration v.s. array.
            Assert.That(EnumerateInts(1, 3), Is.EqualTo(new int[] { 1, 2, 3 }));

            // Array v.s. list
            Assert.That(new int[] { 1, 2, 3 }, Is.EqualTo(new List<int>() { 1, 2, 3 }));

            // Array of enumerators v.s. array of arrays.
            int[][] x = {
                           new [] {1,2,3},
                           new [] {4,5,6},
                           new [] {7,8,9}
                       };
            IEnumerable<int>[] y = { EnumerateInts(1, 3), EnumerateInts(4, 6), EnumerateInts(7, 9) };
            Assert.That(x, Is.EqualTo(y));
        }

    }
}
