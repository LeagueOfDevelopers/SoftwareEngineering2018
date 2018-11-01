using System;
using System.Collections.Generic;
using System.Linq;
using LeonLearn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonLearnTests
{
    [TestClass]
    public class SpeedExerciseTests
    {
        [TestMethod]
        public void InitTest()
        {
            var userPath = @"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestUsers.json";
            var wordPath = @"/Users/leon/Projects/LeonLearn/LeonLearnTests/TestWords.json";
            var a = new SpeedExerciseSession(userPath, wordPath, Guid.Parse("11111111-1111-1111-1111-111111111111"));

            var b = a.CreateLesson();
            
        }
    }
}