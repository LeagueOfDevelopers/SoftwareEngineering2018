using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using LoD_2;

namespace LoD_2.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetFileLine_ReturnFileLink()
        {
            string line = "C:/tests.csv";
            
            var expected = Program.GetFileLines(line);

            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void CountMembers_TrueCount()
        {
            string current = "Количество заявок 15";
            string line = "C:/tests.csv";
            string[] lines = Program.GetFileLines(line);

            var expected = Program.CountMembers(lines);
            var equal = expected.Equals(current);

            Assert.IsTrue(equal);
        }


           
    }
}