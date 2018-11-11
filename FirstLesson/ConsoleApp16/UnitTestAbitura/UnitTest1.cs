using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ConsoleApp16
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FileRightExtention_false()
        {
            //arrange
            var file_path = "test.txt";//не должен пройти
            File.Create(file_path);
            
            //act
            var res = Program.CorrectExtention(file_path);
            //assert
            Assert.IsFalse(res);
           

        }
        [TestMethod]
        public void FileRightExtention_true()
        {
            //arrange
            var file_path = "test.csv";//должен пройти
            File.Create(file_path);

            //act
            var res = Program.CorrectExtention(file_path);
            //assert
            Assert.IsTrue(res);


        }
        [TestMethod]
        public void FilePathThatExist_false()
        {
            //arrange
            var file_path = "test.txt"; 
            File.Create(file_path);
                

            //act
            var res = Program.isOkay(file_path);
            //assert
            Assert.IsFalse(res);


        }
        [TestMethod]
        public void FilePathThatExist_екгу()
        {
            //arrange
            var file_path = "test.txt";

            //act
            var res = Program.isOkay(file_path);
            //assert
            Assert.IsFalse(res);


        }

        [TestMethod]
        public void GetFileExtention_PointAndCSV()
        {
            //arrange
            var file_path = "test.txt.csv";

            //act
            var res = Program.getFileExtension(file_path);
            //assert
            Assert.AreEqual(res, "csv");

        }
        [TestMethod]
        public void GetCountOfList_IntCount()
        {
            //arrange            
            List<Student> list = new List<Student>() { new Student("9", "Sasha", 1, "ITASY", true) };
            var Rcount = 1;
            //act
            var res = Program.count(list);
            //assert
            Assert.AreEqual(res, Rcount);

        }
        [TestMethod]
        public void GetListStudentsInDormitory_CorrectList()
        {
            //arrange            
            List<Student> list = new List<Student>() { new Student("9", "Sasha", 1, "ITASY", true), new Student("8", "Dasha", 2, "ITASY", false) };
            var Rname = "Sasha";
            //act
            var res = Program.dorm(list);
            //assert
            Assert.AreEqual(res[0], Rname);

        }
        [TestMethod]
        public void GetStatisticOfStudentsCourse_ArrayWithЬStatistic()
        {
            //arrange            
            List<Student> list = new List<Student>() { new Student("9", "Sasha", 1, "ITASY", true), new Student("8", "Dasha", 2, "ITASY", false) };
            int[] Rarray = new int[] { 1, 1, 0, 0 };
            //act
            var res = Program.course(list);
            //assert
            for(int i=0;i<4;i++)
            {
                Assert.AreEqual(res[i], Rarray[i]);

            }
            

        }






    }
}
