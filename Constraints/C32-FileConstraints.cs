using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class FileConstraints
    {
        [Test]
        public void FileExists()
        {
            // Create a file and check that it exists.
            var filename = Path.GetTempFileName();
            try
            {
                File.WriteAllText(filename, "");
                Assert.That(filename, Does.Exist);
            }
            finally
            {
                File.Delete(filename);
            }
        }

        [Test]
        public void DirectoryExists()
        {
            // Verify that the temp directory exists.
            Assert.That(Path.GetTempPath(), Does.Exist);
        }

        [Test]
        public void DirectoryEmpty()
        {
            string dirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try
            {
                // Create the target directory.
                Directory.CreateDirectory(dirPath);

                // Test it.
                var info = new DirectoryInfo(dirPath);
                Assert.That(info, Is.Empty);
            }
            finally
            {
                Directory.Delete(dirPath);
            }
        }
    }

    // Oddly, file comparison is available only for assertions, not constraints,
    // but it's not hard to work around.

    [TestFixture]
    public class FileConstraintsSortOf
    {
        private string testString =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.
Praesent laoreet dui quis felis ullamcorper a eleifend lectus lobortis.
Sed orci nulla, tincidunt vitae accumsan eget, aliquet quis justo.
Aliquam erat volutpat. Ut auctor quam ut augue porttitor facilisis.
Quisque fringilla orci eu ante vulputate ut feugiat sem scelerisque.";

        // Easiest comparison is to read the files as byte arrays. 
        [Test]
        public void AssertFileBytesAreEqual()
        {
            var filenameA = System.IO.Path.GetTempFileName();
            var filenameB = System.IO.Path.GetTempFileName();
            try
            {
                File.WriteAllText(filenameA, testString);
                File.WriteAllText(filenameB, testString);

                Assert.That(File.ReadAllBytes(filenameA),Is.EqualTo(File.ReadAllBytes(filenameB)));
            }
            finally
            {
                File.Delete(filenameA);
                File.Delete(filenameB);
            }
        }

        // If you're worried the files might be too big, the Equal constraint can compare streams,
        // but it gets awkward because you have to Dispose them.
        [Test]
        public void AssertStreamsAreEqual()
        {
            var filenameA = System.IO.Path.GetTempFileName();
            var filenameB = System.IO.Path.GetTempFileName();
            try
            {
                File.WriteAllText(filenameA, testString);
                File.WriteAllText(filenameB, testString);

                // Compare streams.
                using (FileStream fileStreamA = File.OpenRead(filenameA)) {
                    using (FileStream fileStreamB = File.OpenRead(filenameB)) {
                        Assert.That(fileStreamB, Is.EqualTo(fileStreamA));
                    }
                }
            }
            finally
            {
                File.Delete(filenameA);
                File.Delete(filenameB);
            }
        }
    }
}
