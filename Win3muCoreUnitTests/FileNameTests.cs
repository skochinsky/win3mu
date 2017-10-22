using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win3muCore;

namespace Win3muCoreUnitTests
{


    [TestClass]
    public class FileNameTests 
    {
        [TestMethod]
        public void ValidCharacters()
        {
            Assert.IsTrue(DosPath.IsValidCharacters("ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwyxz0123456789!#$%&'()-@^_`{}~\xFE\x80"));
        }

        [TestMethod]
        public void InvalidCharacters()
        {
            Assert.IsFalse(DosPath.IsValidCharacters("\x7F"));
            Assert.IsFalse(DosPath.IsValidCharacters("\""));
            Assert.IsFalse(DosPath.IsValidCharacters("*"));
            Assert.IsFalse(DosPath.IsValidCharacters("+"));
            Assert.IsFalse(DosPath.IsValidCharacters(","));
            Assert.IsFalse(DosPath.IsValidCharacters("/"));
            Assert.IsFalse(DosPath.IsValidCharacters(":"));
            Assert.IsFalse(DosPath.IsValidCharacters(";"));
            Assert.IsFalse(DosPath.IsValidCharacters("<"));
            Assert.IsFalse(DosPath.IsValidCharacters("="));
            Assert.IsFalse(DosPath.IsValidCharacters(">"));
            Assert.IsFalse(DosPath.IsValidCharacters("?"));
            Assert.IsFalse(DosPath.IsValidCharacters("\\"));
            Assert.IsFalse(DosPath.IsValidCharacters("["));
            Assert.IsFalse(DosPath.IsValidCharacters("]"));
            Assert.IsFalse(DosPath.IsValidCharacters("|"));
        }

        [TestMethod]
        public void DriveLetter()
        {
            Assert.IsTrue(DosPath.IsValidDriveLetterSpecification("A:"));
            Assert.IsTrue(DosPath.IsValidDriveLetterSpecification("C:"));
            Assert.IsTrue(DosPath.IsValidDriveLetterSpecification("Z:"));
            Assert.IsFalse(DosPath.IsValidDriveLetterSpecification("1:"));
            Assert.IsFalse(DosPath.IsValidDriveLetterSpecification("?:"));
        }

        [TestMethod]
        public void PathElement()
        {
            Assert.IsTrue(DosPath.IsValidElement("XYZ.123"));
            Assert.IsTrue(DosPath.IsValidElement("."));
            Assert.IsTrue(DosPath.IsValidElement(".."));
            Assert.IsFalse(DosPath.IsValidElement("XYZ.>"));
            Assert.IsFalse(DosPath.IsValidElement("XYZ.A.B"));
            Assert.IsTrue(DosPath.IsValidElement("12345678.123"));
            Assert.IsFalse(DosPath.IsValidElement("12345678.123x"));
            Assert.IsFalse(DosPath.IsValidElement("12345678x.123"));
        }

        [TestMethod]
        public void Path()
        {
            Assert.IsTrue(DosPath.IsValid(@"C:\DIRECTOR\FILENAME.TXT"));
            Assert.IsFalse(DosPath.IsValid(@"C:\DIRECTORY\FILENAME.TXT"));
            Assert.IsFalse(DosPath.IsValid(@"C:\DIRECTORY\\FILENAME.TXT"));
            Assert.IsTrue(DosPath.IsValid(@"C:\DIRECTOR.EXT\FILENAME.TXT"));
            Assert.IsTrue(DosPath.IsValid(@"c:\director.ext\filename.txt"));
            Assert.IsTrue(DosPath.IsValid(@"\DIRECTOR.EXT\FILENAME.TXT"));
            Assert.IsTrue(DosPath.IsValid(@"C:\DIRECTOR.EXT\"));
            Assert.IsTrue(DosPath.IsValid(@"C:\"));
            Assert.IsTrue(DosPath.IsValid(@"\"));
            Assert.IsTrue(DosPath.IsValid(@""));
        }
    }
}
