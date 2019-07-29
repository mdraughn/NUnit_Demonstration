using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class PathConstraints
    {
        // NUnit is trying to add some OS-independent path comparisons.

        [Test]
        public void SamePath_Lexical()
        {
            // Trivial same path comparison.
            Assert.That("/Foo/Bar/Baz", Is.SamePath("/Foo/Bar/Baz"));
        }

        [Test]
        public void SamePath_DotNotation()
        {
            // Understands basic dot notation.
            Assert.That("/Foo/Bar/.", Is.SamePath("/Foo/Bar"));
            Assert.That("/Foo/./Bar", Is.SamePath("/Foo/Bar"));
            Assert.That("/Foo/././Bar", Is.SamePath("/Foo/Bar"));
            Assert.That("/Foo/./././././././././././Bar", Is.SamePath("/Foo/Bar"));
        }

        [Test]
        public void SamePath_DoubleDotNotation()
        {
            // Understands double dots go up.
            Assert.That("/Foo/Bar/../Baz", Is.SamePath("/Foo/Baz"));
        }

        [Test]
        public void SamePath_RelativePaths()
        {
            // Understands basic dot notation.
            Assert.That("foo", Is.SamePath("foo"));
            Assert.That("./foo", Is.SamePath("foo"));
        }

        [Test]
        public void SamePath_IgnoreCase()
        {
            // Normally, case is ignored
            Assert.That("/Foo", Is.SamePath("/foo"));

            // You can force it to be respected.
            Assert.That("/Foo", Is.Not.SamePath("/foo").RespectCase);

            // Or explicitly ignore it.
            Assert.That("/Foo", Is.SamePath("/foo").IgnoreCase);

        }

        [Test]
        public void SubPath()
        {
            // NUnit can also test for sub paths
            Assert.That("/Foo/Bar/Baz", Is.SubPathOf("/Foo/Bar"));
        }

        [Test]
        public void SamePathOrUnder()
        {
            // Same paths are not sub paths.
            Assert.That("/Foo/Bar/Baz", Is.SubPathOf("/Foo/Bar"));
            Assert.That("/Foo/Bar", Is.Not.SubPathOf("/Foo/Bar"));

            // For that you'd need a compound constraint...
            Assert.That("/Foo/Bar/Baz", Is.SamePath("/Foo/Bar").Or.SubPathOf("/Foo/Bar"));
            Assert.That("/Foo/Bar", Is.SamePath("/Foo/Bar").Or.SubPathOf("/Foo/Bar"));

            // ...or use this convenience constraint
            Assert.That("/Foo/Bar/Baz", Is.SamePathOrUnder("/Foo/Bar"));
            Assert.That("/Foo/Bar", Is.SamePathOrUnder("/Foo/Bar"));
        }
    }
}
