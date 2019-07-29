using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    class FileAsserts
    {
        private string testString =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.
Praesent laoreet dui quis felis ullamcorper a eleifend lectus lobortis.
Sed orci nulla, tincidunt vitae accumsan eget, aliquet quis justo.
Aliquam erat volutpat. Ut auctor quam ut augue porttitor facilisis.
Quisque fringilla orci eu ante vulputate ut feugiat sem scelerisque.";

        // There are three ways to compare files.
        [Test]
        public void AssertFilesAreEqual()
        {
            var filenameA = System.IO.Path.GetTempFileName();
            var filenameB = System.IO.Path.GetTempFileName();
            File.WriteAllText(filenameA, testString);
            File.WriteAllText(filenameB, testString);

            // Compare contents of files specified by name.
            FileAssert.AreEqual(filenameA, filenameB);

            // Compare contents of files specified by FileInfo objects.
            FileAssert.AreEqual(new FileInfo(filenameA), new FileInfo(filenameB));

            // Compare streams. But AreEqual does not dispose the streams.
            using (FileStream fileStreamA = File.OpenRead(filenameA))
            {
                using (FileStream fileStreamB = File.OpenRead(filenameB))
                {
                    FileAssert.AreEqual(fileStreamA, fileStreamB);
                }
            }
        }
    }
}
