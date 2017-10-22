using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win3muCore;

namespace Win3muCoreUnitTests
{
    [TestClass]
    public class GlobTest
    {
        [TestMethod]
        public void glob_exact_match()
        {
            Assert.IsTrue(DosPath.GlobMatch("abc.def", "abc.def"));
        }

        [TestMethod]
        public void glob_exact_mismatch()
        {
            Assert.IsFalse(DosPath.GlobMatch("abd.def", "abc.def"));
        }

        [TestMethod]
        public void glob_case_test()
        {
            Assert.IsTrue(DosPath.GlobMatch("abc.def", "ABC.DEF"));
        }

        [TestMethod]
        public void glob_star()
        {
            Assert.IsTrue(DosPath.GlobMatch("*", "abc.def"));
        }

        [TestMethod]
        public void glob_star_dot_star()
        {
            Assert.IsTrue(DosPath.GlobMatch("*.*", "abc.def"));
        }


        [TestMethod]
        public void glob_question()
        {
            Assert.IsTrue(DosPath.GlobMatch("ab?.???", "abc.def"));
        }

        [TestMethod]
        public void glob_question_star()
        {
            Assert.IsTrue(DosPath.GlobMatch("ab?d*.*", "abcd.def"));
            Assert.IsFalse(DosPath.GlobMatch("ab??d*.*", "abcd.def"));
        }

        [TestMethod]
        public void glob_star_dot_ext()
        {
            Assert.IsTrue(DosPath.GlobMatch("*.doc", "abcd.doc"));
            Assert.IsFalse(DosPath.GlobMatch("*.exe", "abcd.doc"));
        }

        [TestMethod]
        public void glob_redundant()
        {
            Assert.IsTrue(DosPath.GlobMatch("*?.doc", "abcd.doc"));
            Assert.IsTrue(DosPath.GlobMatch("*?***.doc", "abcd.doc"));
            Assert.IsTrue(DosPath.GlobMatch("abcd.doc*", "abcd.doc"));
            Assert.IsTrue(DosPath.GlobMatch("abcd.doc?", "abcd.doc"));
        }

        [TestMethod]
        public void glob_tail()
        {
            Assert.IsFalse(DosPath.GlobMatch("*.c", "abc.cur"));
        }


    }
}
