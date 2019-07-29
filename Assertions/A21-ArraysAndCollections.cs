using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    // NUnit supports lots of array and collection comparisons.

    [TestFixture]
    public class ArraysAndCollections
    {
        [Test]
        public void OneDimension()
        {
            int[] x = { 1, 2, 3, 4, 5 };
            int[] y = { 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x, y);
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
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x, y);
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
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x, y);
        }

        [Test]
        public void Lists()
        {
            var x = new List<string>() { "alfa", "bravo", "charlie", "delta", "echo", "foxtrot" };
            var y = new List<string>() { "alfa", "bravo", "charlie", "delta", "echo", "foxtrot" };
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x, y);
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
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x, y);
        }

        // Example iteration.
        public IEnumerable<int> EnumerateInts(int fromInt,int toInt)
        {
            for (int i = fromInt; i <= toInt; i++)
            {
                yield return i;
            }
        }

        [Test]
        public void Enumerations()
        {
            // NUnit compares enumerations.
            CollectionAssert.AreEqual(EnumerateInts(1, 10), EnumerateInts(1, 10));
            Assert.AreEqual(EnumerateInts(1,10), EnumerateInts(1,10));
        }

        [Test]
        public void MixedEnumerations()
        {
            // You can mix types if they are both enumerable.

            // Enumeration v.s. array.
            CollectionAssert.AreEqual(EnumerateInts(1, 3), new int[] { 1, 2, 3 });
            Assert.AreEqual(EnumerateInts(1,3), new int[] { 1, 2, 3 });

            // Array v.s. list
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, new List<int>() { 1, 2, 3 });
            Assert.AreEqual(new int[] { 1, 2, 3 }, new List<int>() { 1, 2, 3} );

            // Array of enumerators v.s. array of arrays.
            int[][] x = {
                           new [] {1,2,3},
                           new [] {4,5,6},
                           new [] {7,8,9}
                       };
            IEnumerable<int>[] y = {EnumerateInts(1, 3), EnumerateInts(4,6), EnumerateInts(7,9)};
            CollectionAssert.AreEqual(x, y);
            Assert.AreEqual(x,y);
        }

    }
}
