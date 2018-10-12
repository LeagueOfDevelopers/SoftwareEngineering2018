using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentNamespace;

namespace UnitTestTournament
{
    [TestClass]
    public class TestTournament
    {
        static readonly string filepathToGrid = "C:\\Users\\rtviw\\Desktop\\Test";
        static readonly string filepathToMembers = "C:\\Users\\rtviw\\Desktop\\Test";

        [TestMethod]
        public void GetMixedMembers_MixedMembers()
        {
            var answer = new List<Member>();
            var members = new List<Member> { new Member("Артём"), new Member("Виктор"),
                new Member("Павел"), new Member("Татьяна"), new Member("Юлия"),
                new Member("Danyel"), new Member("Михаил"), new Member("Леон") };
            var name = "test1";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            List<Member> result = tournament.GetMixedMembers();
            
            foreach (var member in members)
            {
                if (result.Contains(member))
                    result.Remove(member);
            }

            Assert.AreEqual(result.Count, answer.Count);
        }

        [TestMethod]
        public void GetMixedMembersFromEmptyList_Exception()
        {
            var answer = new List<Member>();
            var members = new List<Member> { };
            var name = "test15";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            bool caughtException = false;
            try
            {
                var tournament = new Tournament(name, filepathToMembers, filepathToGrid);
                List<Member> result = tournament.GetMixedMembers();
            }
            catch(Exception)
            {
                caughtException = true;   
            }

            Assert.IsTrue(caughtException);
        }

        [TestMethod]
        public void DoCompetitionWithLoser_Exception()
        {
            var answer = new List<Member>();
            var member1 = new Member("Артём");
            var member2 = new Member("Виктор");
            var members = new List<Member> { member1, member2 };
            var name = "test18";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            bool caughtException = false;
            try
            {
                var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
                {
                    Members = members
                };
                tournament.Pairs.Add(new ValueTuple<Member, Member>(member1, member2));
                tournament.DoCompetition(member1.Name);
                tournament.DoCompetition(member2.Name);
            }
            catch (Exception)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException);
        }

        [TestMethod]
        public void GetMixedMembersFromListWithWrongCount_Exception()
        {
            var answer = new List<Member>();
            var members = new List<Member> { new Member("Артём"), new Member("Виктор"),
                new Member("Павел"), new Member("Татьяна"), new Member("Юлия"),
                new Member("Danyel") };
            var name = "test16";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            bool caughtException = false;
            try
            {
                var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
                {
                    Members = members
                };
                List<Member> result = tournament.GetMixedMembers();
            }
            catch (Exception)
            {
                caughtException = true;
            }

            Assert.IsTrue(caughtException);
        }

        [TestMethod]
        public void GenerateGrid_FileWithMixedMembers()
        {
            var answer = new List<Member>();
            var members = new List<Member> { new Member("Артём"), new Member("Виктор"),
                new Member("Павел"), new Member("Татьяна"),new Member("Юлия"),
                new Member("Danyel"), new Member("Михаил"), new Member("Леон") };
            var input = new List<Member> { new Member("Артём"), new Member("Виктор"),
                new Member("Павел"), new Member("Татьяна"), new Member("Юлия"),
                new Member("Danyel"), new Member("Михаил"), new Member("Леон") };
            var name = "test2";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = input
            };
            tournament.GenerateGrid();
            List<string> result = File.ReadAllLines(tournament.FilepathToGrid)[0].Trim().Split(' ').ToList();
            
            foreach (var member in members)
            {
                if (result.Contains(member.Name))
                    result.Remove(member.Name);
            }

            Assert.AreEqual(result.Count, answer.Count);
        }

        [TestMethod]
        public void GeneratePairs_Pairs()
        {
            var answer = new List<Member>();
            var members = new List<Member> { new Member("Артём"), new Member("Виктор"),
                new Member("Павел"), new Member("Татьяна"),new Member("Юлия"),
                new Member("Danyel"), new Member("Михаил"), new Member("Леон") };
            var name = "test3";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            List<(Member, Member)> pairs = tournament.GeneratePairs();

            Assert.AreNotEqual(pairs.Count, 0);

            foreach(var member in members)
            {
                foreach(var pair in pairs)
                {
                    members.RemoveAll(x => (x == pair.Item1) || (x == pair.Item2));
                }
            }

            Assert.AreEqual(members.Count, answer.Count);
        }

        [TestMethod]
        public void GetNewPair_Pair()
        {
            var members = new List<Member> { new Member("Вова"), new Member("Маша"),
                new Member("Петя"), new Member("Пётр") };
            var winners = new List<Member> { new Member("Вова"), new Member("Петя")};
            var name = "test4";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = new List<Member>(winners)
            };
            var pair = tournament.GetNewPair();

            Assert.AreEqual(pair.Item1, winners[0]);
            Assert.AreEqual(pair.Item2, winners[1]);
        }

        [TestMethod]
        public void AddWinnerToGrid_NewMemberInFile()
        {
            var input = new List<Member> { new Member("Даша"), new Member("Паша"),
                new Member("Яна"), new Member("Ашад") };
            var winner = "Никита";
            var data = "Даша Паша Паша Ашад";
            string[] answer = { data, winner + " " };
            var name = "test5";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = input
            };
            File.WriteAllLines(tournament.FilepathToGrid, new string[] { data });
            tournament.AddWinnerToGrid(new Member(winner));
            string[] result = File.ReadAllLines(tournament.FilepathToGrid);

            Assert.AreEqual(result[0], answer[0]);
            Assert.AreEqual(result[1], answer[1]);
        }

        [TestMethod]
        public void AddNewRowToGrid_NewRowInFile()
        {
            var input = new List<Member> { new Member("Даша"), new Member("Паша"),
                new Member("Яна"), new Member("Ашад") };
            var data = "Даша Паша Паша Ашад";
            string[] answer = { data, "" };
            var name = "test6";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = input
            };
            File.WriteAllText(tournament.FilepathToGrid, data);
            tournament.AddNewRowToGrid();
            string[] result = File.ReadAllLines(tournament.FilepathToGrid);

            Assert.AreEqual(result[0], answer[0]);
            Assert.AreEqual(result[1], answer[1]);
        }

        [TestMethod]
        public void AddWinnersToGrid_NewMembersInFile()
        {
            var winners = new List<Member> { new Member("Паша"), new Member("Яна") };
            var input = new List<Member> { new Member("Даша"), new Member("Паша"),
                new Member("Яна"), new Member("Ашад") };
            var data = "Даша Паша Яна Ашад";
            string[] answer = { data, winners[0].Name + " " + winners[1].Name + " " };
            var name = "test7";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = input
            };
            File.WriteAllLines(tournament.FilepathToGrid, new string[] { data });
            tournament.AddWinnersToGrid(winners);
            string[] result = File.ReadAllLines(tournament.FilepathToGrid);

            Assert.AreEqual(result[0], answer[0]);
            Assert.AreEqual(result[1], answer[1]);
        }

        [TestMethod]
        public void PairsToString_String()
        {
            var answer = "Маша Кеша";
            var members = new List<Member> { new Member("Маша"), new Member("Кеша") };
            var name = "test8";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            var pair = new ValueTuple<Member, Member>(new Member("Маша"), new Member("Кеша"));
            tournament.Pairs = new List<(Member, Member)> { pair };
            string result = tournament.PairsToString(tournament.Pairs);

            Assert.AreEqual(result, answer);
        }

        [TestMethod]
        public void CheckMembersCount_IsPowerOfTwo1()
        {
            var members = new List<Member> { new Member("Маша"), new Member("Кеша") };
            int input = 16;
            var name = "test9";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            bool result = tournament.CheckMembersCount(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckMembersCount_IsPowerOfTwo2()
        {
            var members = new List<Member> { new Member("Маша"), new Member("Кеша") };
            int input = 12;
            var name = "test10";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            bool result = tournament.CheckMembersCount(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoCompetition_newPairWithWinners()
        {
            var winner1 = new Member("Артём");
            var member1 = new Member("Виктор");
            var winner2 = new Member("Татьяна");
            var member2 = new Member("Паша");
            var pair1 = new ValueTuple<Member, Member>(winner1, member1);
            var pair2 = new ValueTuple<Member, Member>(winner2, member2);
            var members = new List<Member> { new Member("Виктор"), new Member("Юлия"),
                new Member("Danyel"), new Member("Михаил") };
            var name = "test11";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Pairs = new List<(Member, Member)> { pair1, pair2 }
            };
            tournament.DoCompetition(winner1.Name);
            tournament.DoCompetition(winner2.Name);

            bool flag = false;
            foreach(var (First, Second) in tournament.Pairs)
            {
                if (First.Name.Equals(winner2.Name) && Second.Name.Equals(winner1.Name))
                {
                    flag = true;
                }
            }

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void GetWinner_Member()
        {
            var answer = new Member("Виктор");
            var member2 = new Member("Артём");
            var members = new List<Member> { member2, answer };
            var name = "test12";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Pairs = new List<(Member, Member)> { new ValueTuple<Member, Member>(answer, member2) }
            };
            var result = tournament.GetWinner("Виктор");

            Assert.AreEqual(result.Name, answer.Name);
            Assert.AreEqual(result.Level, answer.Level);
        }
        
        [TestMethod]
        public void MakeMemberFree_NewMemberLessPairs()
        {
            var member1 = new Member("Артем");
            var member2 = new Member("Паша");
            var members = new List<Member> { member1, member2 };
            var name = "test13";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = new List<Member>(),
                Pairs = new List<(Member, Member)> { new ValueTuple<Member, Member>(member1, member2) }
            };
            tournament.MakeMemberFree(member1);

            Assert.AreEqual(member1.Name, tournament.Members[0].Name);
            Assert.AreEqual(member1.Level, tournament.Members[0].Level);
        }

        [TestMethod]
        public void FindPair_Pair()
        {
            var member1 = new Member("Артём");
            var member2 = new Member("Виктор");
            var members = new List<Member> { member1, member2  };
            var name = "test14";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            var pair = tournament.FindPair(member1);

            Assert.AreEqual(pair.Value.First.Name, member1.Name);
            Assert.AreEqual(pair.Value.Second.Name, member2.Name);
            Assert.AreEqual(pair.Value.First.Level, member1.Level);
            Assert.AreEqual(pair.Value.Second.Level, member2.Level);
        }

        [TestMethod]
        public void FindPair_NoPair()
        {
            var member1 = new Member("Артём");
            var member2 = new Member("Виктор");
            member2.IncreaseLevel();
            var members = new List<Member> { member1, member2 };
            var name = "test23";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            var pair = tournament.FindPair(member1);

            Assert.AreEqual(null, pair);
        }

        [TestMethod]
        public void FindPair_Pair2()
        {
            var member1 = new Member("Артём");
            member1.IncreaseLevel();
            var member2 = new Member("Виктор");
            var member3 = new Member("Валентин");
            var member4 = new Member("Давид");
            member4.IncreaseLevel();
            var members = new List<Member> { member1, member2, member3, member4 };
            var name = "test24";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            var firstPair = tournament.FindPair(member1);
            var secondPair = tournament.FindPair(member2);

            Assert.AreEqual(firstPair.Value.First.Name, member1.Name);
            Assert.AreEqual(firstPair.Value.Second.Name, member4.Name);
            Assert.AreEqual(firstPair.Value.First.Level, member1.Level);
            Assert.AreEqual(firstPair.Value.Second.Level, member4.Level);

            Assert.AreEqual(secondPair.Value.First.Name, member2.Name);
            Assert.AreEqual(secondPair.Value.Second.Name, member3.Name);
            Assert.AreEqual(secondPair.Value.First.Level, member2.Level);
            Assert.AreEqual(secondPair.Value.Second.Level, member3.Level);
        }

        [TestMethod]
        public void DoCompetition_Winner()
        {
            var member1 = new Member("Артём");
            var member2 = new Member("Татьяна");
            var member3 = new Member("Виктор");
            var member4 = new Member("Павел");
            var member5 = new Member("Юлия");
            var member6 = new Member("Danyel");
            var member7 = new Member("Михаил");
            var member8 = new Member("Леон");
            var pair1 = new ValueTuple<Member, Member>(member1, member2);
            var pair2 = new ValueTuple<Member, Member>(member3, member4);
            var pair3 = new ValueTuple<Member, Member>(member5, member6);
            var pair4 = new ValueTuple<Member, Member>(member7, member8);
            var members = new List<Member> { member1, member2 };
            var name = "test17";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = new List<Member>(),
                Pairs = new List<(Member, Member)>() { pair1, pair2, pair3, pair4 }
            };
            tournament.DoCompetition(pair1.Item1.Name);
            tournament.DoCompetition(pair2.Item1.Name);
            tournament.DoCompetition(pair3.Item1.Name);
            tournament.DoCompetition(pair4.Item1.Name);
            tournament.DoCompetition(pair1.Item1.Name);
            tournament.DoCompetition(pair4.Item1.Name);
            tournament.DoCompetition(pair1.Item1.Name);

            Assert.AreEqual(member1.Name, tournament.Members[0].Name);
        }

        [TestMethod]
        public void DoCompetition_FileWithWinner()
        {
            var member1 = new Member("Артём");
            var member2 = new Member("Татьяна");
            var member3 = new Member("Виктор");
            var member4 = new Member("Павел");
            var member5 = new Member("Юлия");
            var member6 = new Member("Danyel");
            var member7 = new Member("Михаил");
            var member8 = new Member("Леон");
            var pair1 = new ValueTuple<Member, Member>(member1, member2);
            var pair2 = new ValueTuple<Member, Member>(member3, member4);
            var pair3 = new ValueTuple<Member, Member>(member5, member6);
            var pair4 = new ValueTuple<Member, Member>(member7, member8);
            var members = new List<Member> { member1, member2, member3, member4 };
            var name = "test19";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = new List<Member>(),
                Pairs = new List<(Member, Member)>() { pair1, pair2, pair3, pair4 }
            };
            tournament.DoCompetition(pair1.Item1.Name);
            tournament.DoCompetition(pair2.Item1.Name);
            tournament.DoCompetition(pair3.Item1.Name);
            tournament.DoCompetition(pair4.Item1.Name);
            tournament.DoCompetition(pair1.Item1.Name);
            tournament.DoCompetition(pair4.Item1.Name);
            tournament.DoCompetition(pair1.Item1.Name);

            string[] lines = File.ReadAllLines(tournament.FilepathToGrid);
            var result = lines[lines.Length - 1].Trim();

            Assert.AreEqual(result, member1.Name);
        }

        [TestMethod]
        public void DoCompetitionOneWave_FileWithTwoLines()
        {
            var all = new List<string> { "Артём", "Татьяна", "Виктор", "Павел",
                "Юлия", "Danyel", "Михаил", "Леон" };
            var member1 = new Member("Артём");
            var member2 = new Member("Татьяна");
            var member3 = new Member("Виктор");
            var member4 = new Member("Павел");
            var member5 = new Member("Юлия");
            var member6 = new Member("Danyel");
            var member7 = new Member("Михаил");
            var member8 = new Member("Леон");
            var pair1 = new ValueTuple<Member, Member>(member1, member2);
            var pair2 = new ValueTuple<Member, Member>(member3, member4);
            var pair3 = new ValueTuple<Member, Member>(member5, member6);
            var pair4 = new ValueTuple<Member, Member>(member7, member8);
            var members = new List<Member> { member1, member2, member3, member4 };
            var name = "test20";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = new List<Member>(),
                Pairs = new List<(Member, Member)>() { pair1, pair2, pair3, pair4 }
            };
            tournament.DoCompetition(pair1.Item1.Name);
            tournament.DoCompetition(pair2.Item1.Name);
            tournament.DoCompetition(pair3.Item1.Name);
            tournament.DoCompetition(pair4.Item1.Name);

            string[] lines = File.ReadAllLines(tournament.FilepathToGrid);
            var secondLine = lines[0].Trim().Split(' ');
            Assert.AreEqual(secondLine[0], member3.Name);
            Assert.AreEqual(secondLine[1], member1.Name);
            Assert.AreEqual(secondLine[2], member7.Name);
            Assert.AreEqual(secondLine[3], member5.Name);
        }

        [TestMethod]
        public void DoCompetitionRandomly_WinnerFromList()
        {
            var all = new List<string> { "Артём", "Татьяна", "Виктор", "Павел",
                "Юлия", "Danyel", "Михаил", "Леон" };
            var member1 = new Member("Артём");
            var member2 = new Member("Татьяна");
            var member3 = new Member("Виктор");
            var member4 = new Member("Павел");
            var member5 = new Member("Юлия");
            var member6 = new Member("Danyel");
            var member7 = new Member("Михаил");
            var member8 = new Member("Леон");
            var members = new List<Member> { member1, member2, member3, member4,
                member5, member6, member7, member8 };
            var name = "test21";
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            tournament.GenerateGrid();
            tournament.DoCompetition();
            var winner = tournament.Members[0].Name;

            bool isFound = false;
            foreach(var person in all)
            {
                if (all.Contains(winner))
                    isFound = true;
            }

            Assert.IsTrue(isFound);
        }

        [TestMethod]
        public void GetAmountOfCompetitionsFor8_7()
        {
            int answer = 7;
            var name = "test22";
            var members = new List<Member> { new Member(""), new Member("") };
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid)
            {
                Members = members
            };
            var amount = tournament.GetAmountOfCompetitions(8);

            Assert.AreEqual(answer, amount);
        }

        [TestMethod]
        public void GetMembersFromFile_MembersInFile()
        {
            var answer = new List<string> { "Артем", "Вика", "Егор", "Паша",
                "Даша", "Маша", "Юля", "Вова" };
            var name = "test25";
            var members = new List<Member> { new Member(""), new Member("") };
            File.Delete(filepathToGrid + "\\" + name + "_results.txt");

            var tournament = new Tournament(name, filepathToMembers, filepathToGrid);
            var result = tournament.GetMembersFromFile(tournament.FilepathToMembers);

            foreach(var member in result)
            {
                if (answer.Contains(member.Name))
                    answer.Remove(member.Name);
            }

            Assert.AreEqual(0, answer.Count);
        }
    }
}
