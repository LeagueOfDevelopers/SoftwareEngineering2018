using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace TournamentNamespace
{
    public class Tournament
    {
        public string Name { get; }
        public string FilepathToGrid { get; }
        public string FilepathToMembers { get; }
        public List<Member> Members { get; set; }
        public List<(Member First, Member Second)> Pairs { get; set; }

        private int currentLevel;

        public Tournament(string name, string filepathToMembers, string filepathToGrid)
        {
            Name = name;
            FilepathToGrid = filepathToGrid + "\\" + Name + "_results.txt";
            FilepathToMembers = filepathToMembers + "\\" + Name + ".txt";
            currentLevel = 1;

            CreateGridFile(FilepathToGrid);

            var members = GetMembersFromFile(FilepathToMembers);
            if (CheckMembersCount(members.Count))
                Members = members;
            else
                throw new Exception("Количество участников должно быть степенью двойки");
        }
        
        // Проверка a на степень двойки
        public bool CheckMembersCount(int a)
        {
            if (a == 0)
                return false;
            else if (a == 2)
                return true;
            else if (a % 2 == 0)
                return CheckMembersCount(a / 2);
            else
                return false;
        }
        
        public void GenerateGrid()
        {
            Members = GetMixedMembers();
            Pairs = GeneratePairs();
            File.WriteAllLines(FilepathToGrid, new string[] { PairsToString(Pairs) });
        }
        
        public void DoCompetition(string winner)
        {
            if (Pairs == null)
                throw new Exception("Пары не сформированы");

            Member freeMember = GetWinner(winner);
            MakeMemberWinner(freeMember);
        }

        public void DoCompetition()
        {
            if (Pairs == null)
                throw new Exception("Пары не сформированы");

            var amountOfMembers = Pairs.Count * 2;
            for (int i = 0; i < amountOfMembers - 1; i++)
            {
                Member freeMember = GetRandomWinner();
                MakeMemberWinner(freeMember);
            }
        }

        public void MakeMemberWinner(Member freeMember)
        {
            MakeMemberFree(freeMember);
            var pair = FindPair(freeMember);

            if (pair != null)
                AddPairToGrid(pair.Value);

            AddNewRowToGridIfItNeeds();
            CheckWinner();
        }

        public void AddPairToGrid((Member First, Member Second) pair)
        {
            Pairs.Add(pair);
            Members.Remove(pair.First);
            Members.Remove(pair.Second);
            AddWinnersToGrid(new List<Member> { pair.First, pair.Second });
        }

        public int GetAmountOfCompetitions(int i)
        {
            return i - 1;
        }

        public (Member First, Member Second)? FindPair(Member winner)
        {
            foreach (var member in Members)
            {
                if (!member.Name.Equals(winner.Name) && member.Level == winner.Level)
                {   
                    return new ValueTuple<Member, Member>(winner, member);
                }
            }

            return null;
        }

        public Member GetWinner(string winner)
        {
            for (int i = 0; i < Pairs.Count; i++)
            {
                if (Pairs[i].First.Name.Equals(winner))
                    return Pairs[i].First;
                else if (Pairs[i].Second.Name.Equals(winner))
                    return Pairs[i].Second;
            }
            throw new Exception("Участника " + winner + " нет в сетке");
        }

        public Member GetRandomWinner()
        {
            if (Pairs.Count > 0)
            {
                var rand = new Random().Next(0, Pairs.Count);
                var newWinner = Pairs[rand].First;
                return newWinner;
            }
            throw new Exception("Победитель уже есть");
        }

        public void MakeMemberFree(Member member)
        {
            member.IncreaseLevel();
            Members.Add(member);
            Pairs.RemoveAll(p => p.First.Name.Equals(member.Name) || p.Second.Name.Equals(member.Name));
        }
        
        public Tuple<Member, Member> GetNewPair()
        {
            if (Members.Count >= 2)
            {
                var member1 = Members[0];
                var member2 = Members[1];
                Members.Remove(member1);
                Members.Remove(member2);

                return Tuple.Create(member1, member2);
            }
            else
            {
                throw new Exception("Недостаточно свободных участников");
            }
        }

        public List<(Member, Member)> GeneratePairs()
        {
            if (Pairs == null)
                Pairs = new List<(Member First, Member Second)>();

            var pairs = new List<(Member First, Member Second)>();
            int length = Members.Count + 1;
            for (int i = 0; i < length / 2; i++)
            {
                var pair = GetNewPair();
                var member1 = pair.Item1;
                var member2 = pair.Item2;

                Members.Remove(member1);
                Members.Remove(member2);

                pairs.Add(ValueTuple.Create(member1, member2));
                AddWinnersToGrid(new List<Member> { member1, member2 });
            }
            AddNewRowToGrid();

            return pairs;
        }
        
        public void AddWinnersToGrid(List<Member> winners)
        {
            foreach (var winner in winners)
            {
                AddWinnerToGrid(winner);
            }
        }
        
        public void AddWinnerToGrid(Member winner)
        {
            using (StreamWriter sw = File.AppendText(FilepathToGrid))
            {
                sw.Write(winner.Name + " ");
            }
        }
        
        public void AddNewRowToGridIfItNeeds()
        {
            var level = GetLevelFromPairs();
            if (level > currentLevel)
            {
                currentLevel = level;
                AddNewRowToGrid();
            }
        }

        public void CreateGridFile(string filepath)
        {
            if (!File.Exists(FilepathToGrid))
            {
                var file = File.Create(FilepathToGrid);
                file.Close();
            }
        }

        public int GetLevelFromPairs()
        {
            int min = -1;
            foreach(var (First, Second) in Pairs)
            {
                if (First.Level < min || min == -1)
                    min = First.Level;
                else if (Second.Level < min || min == -1)
                    min = Second.Level;
            }
            return min;
        }

        public void AddNewRowToGrid()
        {
            File.AppendAllLines(FilepathToGrid, new string[] { "\n" });
        }
        
        public List<Member> GetMixedMembers()
        {
            var rand = new Random();
            var mixedMembers = Members.OrderBy(x => rand.Next()).ToList();
            return mixedMembers;
        }

        public List<Member> GetMembersFromFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                var members = new List<Member>();
                string[] lines = File.ReadAllLines(filepath);

                for (int i = 0; i < lines.Length; i++)
                    foreach(var name in lines[i].Split(' '))
                        members.Add(new Member(name));

                return members;
            }
            else
                throw new FileNotFoundException("Не найден файл по пути: " + filepath);
        }

        public string PairsToString(List<(Member First, Member Second)> pairs)
        {
            return string.Join(" ", pairs.Select(p => p.First.Name + " " + p.Second.Name));
        }
            
        public void CheckWinner()
        {
            if (Pairs.Count == 0 && Members.Count == 1)
                AddWinnerToGrid(Members[0]);
        }
    }
}
