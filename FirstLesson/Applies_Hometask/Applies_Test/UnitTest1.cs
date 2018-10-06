using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applies_Hometask;
using System.Collections.Generic;

namespace Applies_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountDormers()
        {
            string[] count = { "Белов Артём", "Dani Danyel Aristizabal "};
            Program.data = new string[]{"9.3.2018 0:02:47;Белов Артём;3 бакалавриат;Институт информационных технологий и автоматизированных систем управления (ИТАСУ);Да;;;;;;",
                "9.14.2018 15:33:12;Dani Danyel Aristizabal ;2 бакалавриат;Институт информационных технологий и автоматизированных систем управления (ИТАСУ);Да;;;;;;",
                    "9.4.2018 22:05:19; Леон Минасян; 1 бакалавриат; Институт информационных технологий и автоматизированных систем управления(ИТАСУ); Нет; ; ; ; ; ;"};

            var result = Program.CountDormers().ToArray();

            Assert.AreEqual(result, count);
        }

        [TestMethod]
        public void TestCountApplies(){
            Program.data = new string[]{"9.3.2018 0:02:47;Белов Артём;3 бакалавриат;Институт информационных технологий и автоматизированных систем управления (ИТАСУ);Да;;;;;;",
                "9.14.2018 15:33:12;Dani Danyel Aristizabal ;2 бакалавриат;Институт информационных технологий и автоматизированных систем управления (ИТАСУ);Да;;;;;;",
                    "9.4.2018 22:05:19; Леон Минасян; 1 бакалавриат; Институт информационных технологий и автоматизированных систем управления(ИТАСУ); Нет; ; ; ; ; ;"};

            int count = Program.CountApplies();
            int RequiredCount = 3;

            Assert.AreEqual(RequiredCount, count);
        }

    }
}
