using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Converter.Tests
{
    [TestClass]
    public class ControlTests
    {
        [TestMethod]
        public void Convert_F_From16To10_15Returned()
        {
            // arrange - установить
            string number = "F";
            byte p1 = 16;
            byte p2 = 10;
            string expected = "15";

            // act - действовать
            string actual = Control.DoConversion(number, p1, p2);

            // assert - утвердить
            Assert.AreEqual(expected, actual);

            //Assert.AreEqual("15", Control.DoConversion("F", 16, 10));
        }

        [TestMethod]
        public void Convert_MinusF_From16To10_Minus15Returned()
        {
            Assert.AreEqual("-15", Control.DoConversion("-F", 16, 10));
        }

        [TestMethod]
        public void Convert_1010_From10To2_1111110010Returned()
        {
            Assert.AreEqual("1111110010", Control.DoConversion("1010", 10, 2));
        }

        [TestMethod]
        public void Convert_33_From10To2_100001Returned()
        {
            Assert.AreEqual("100001", Control.DoConversion("33", 10, 2));
        }

        [TestMethod]
        public void Convert_777_From8To2_111111111Returned()
        {
            Assert.AreEqual("111111111", Control.DoConversion("777", 8, 2));
        }

        [TestMethod]
        public void Convert_Minus323_From4To16_Minus3BReturned()
        {
            Assert.AreEqual("-3B", Control.DoConversion("-323", 4, 16));
        }

        [TestMethod]
        public void Convert_4294_From10To16_10C6Returned()
        {
            Assert.AreEqual("10C6", Control.DoConversion("4294", 10, 16));
        }

        [TestMethod]
        public void Convert_05_From10To16_08Returned()
        {
            Assert.AreEqual("0.8", Control.DoConversion("0.5", 10, 16));
        }

        [TestMethod]
        public void Convert_AFB4_From16To10_44980Returned()
        {
            Assert.AreEqual("44980", Control.DoConversion("AFB4", 16, 10));
        }
    }
}
