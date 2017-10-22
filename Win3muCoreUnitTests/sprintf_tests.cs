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
    public class sprintf_tests
    {
        [TestMethod]
        public void wsprintf_string()
        {
            Assert.AreEqual(sprintf.Format("[[%s]]", "string"), "[[string]]");
        }

        [TestMethod]
        public void wsprintf_integer()
        {
            Assert.AreEqual(sprintf.Format("[[%i]]", (short)23), "[[23]]");
        }

        [TestMethod]
        public void wsprintf_hex_lower()
        {
            Assert.AreEqual(sprintf.Format("[[%x]]", (ushort)0x12ab), "[[12ab]]");
        }

        [TestMethod]
        public void wsprintf_hex_upper()
        {
            Assert.AreEqual(sprintf.Format("[[%X]]", (ushort)0x12ab), "[[12AB]]");
        }

        [TestMethod]
        public void wsprintf_left_align()
        {
            Assert.AreEqual(sprintf.Format("[[%10s]]", "string"), "[[string    ]]");
        }

        [TestMethod]
        public void wsprintf_right_align()
        {
            Assert.AreEqual(sprintf.Format("[[%-10s]]", "string"), "[[    string]]");
        }

        [TestMethod]
        public void wsprintf_width_nocrop()
        {
            Assert.AreEqual(sprintf.Format("[[%3s]]", "string"), "[[string]]");
        }


        [TestMethod]
        public void wsprintf_short_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.10i]]", (short)23), "[[0000000023]]");
        }

        [TestMethod]
        public void wsprintf_int_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.10i]]", (short)23), "[[0000000023]]");
        }

        [TestMethod]
        public void wsprintf_ushort_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.10u]]", (ushort)40000), "[[0000040000]]");
        }

        [TestMethod]
        public void wsprintf_uint_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.12lu]]", 4000000000u), "[[004000000000]]");
        }

        [TestMethod]
        public void wsprintf_hex_lower_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.10x]]", (ushort)0xabcd), "[[000000abcd]]");
        }

        [TestMethod]
        public void wsprintf_hex_upper_precision()
        {
            Assert.AreEqual(sprintf.Format("[[%.10X]]", (ushort)0xabcd), "[[000000ABCD]]");
        }

        [TestMethod]
        public void wsprintf_int_width_and_precision_left()
        {
            Assert.AreEqual(sprintf.Format("[[%10.5i]]", (short)23), "[[00023     ]]");
        }

        [TestMethod]
        public void wsprintf_int_width_and_precision_right()
        {
            Assert.AreEqual(sprintf.Format("[[%-10.5i]]", (short)23), "[[     00023]]");
        }

        [TestMethod]
        public void wsprintf_hex_width_and_precision_left()
        {
            Assert.AreEqual(sprintf.Format("[[%#10.5x]]", (ushort)23), "[[0x00017     ]]");
        }

        [TestMethod]
        public void wsprintf_hex_width_and_precision_right()
        {
            Assert.AreEqual(sprintf.Format("[[%#-10.5x]]", (ushort)23), "[[     0x00017]]");
        }

    }
}
