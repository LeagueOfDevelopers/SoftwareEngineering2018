using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace LoD_Task_1._1.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetList_HaveList()
        {
            string[] lines = File.ReadAllLines("Rome.txt");
            List<string> currentList = new List<string>();
            foreach (var person in lines)
            {
                currentList.Add(person);
            }
            string link = "Rome";

            List<string> expectedList = Program.GetTourTable(link);

            Assert.IsFalse(currentList.Count == expectedList.Count);

        }
    }
}
