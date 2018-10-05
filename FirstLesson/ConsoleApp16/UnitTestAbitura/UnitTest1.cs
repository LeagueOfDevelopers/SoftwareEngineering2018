using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestAbitura
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FilePathThatExist_true()
        {
            File.Create("test.txt");

            Assert.IsTrue(Program.isOkay("test.txt"));

        }
    }
}
