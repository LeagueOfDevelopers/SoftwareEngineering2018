using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoD_Task_1._1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string link = StartTour();
            List<string> listOfPer = GetTourTable(link);
            List<string> randListOfPer = GetRandTable(listOfPer);
            PrintTable(randListOfPer);
            List<string> chooseWinner = ChooseWinner(link ,randListOfPer);
            PrintTable(chooseWinner);
            Console.ReadKey();
           
        }
        static string StartTour ()
        {
            Console.WriteLine("Введите название турнира:");
            string link = Console.ReadLine();
            return link;
        }
        public static List<string> GetTourTable (string link)
        {
            List<string> listOfPer = new List<string>();
            try
            {
                string[] lines = File.ReadAllLines(link + ".txt");
                listOfPer = new List<string>();
                foreach (var person in lines)
                {
                    listOfPer.Add(person);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Неверное имя турнира");
                string[] goToMain = new string[0];
                Main(goToMain);
            }
            
            return listOfPer;
        }
        static List<string> GetRandTable (List<string> list)
        {
            Random rand = new Random();
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                var temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
            return list;
        }
        static List<string> ChooseWinner(string link, List<string> randListOfPer)
        {
            List<string> LoseTable = new List<string>();
            while (true)
            {
                string winner = "";
                Console.WriteLine("Введите победителя:");
                try
                {
                    winner = Console.ReadLine();
                    if (winner.Equals("Escape"))
                    {
                        SaveList(link, randListOfPer);
                        string[] goToMain = new string[0];
                        Main(goToMain);
                        break;
                    }
                    LoseTable = ChangeLosers(link, randListOfPer, winner);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Неверное имя");
                    ChooseWinner(link, randListOfPer);
                }
            }
            return LoseTable;
        }
        static List<string> ChangeLosers(string link, List<string> list, string winner)
        {
            if (list.IndexOf(winner) % 2 == 0)
            {
                list.RemoveAt(list.IndexOf(winner) + 1);
                list.Insert(list.IndexOf(winner) + 1, "Lose");
            }
            else
            {
                list.RemoveAt(list.IndexOf(winner) - 1);
                list.Insert(list.IndexOf(winner), "Lose");
            }
            list = VerifyStageOfTournament(link, list);

            PrintTable(list);
            return list;
        }
        static List<string> VerifyStageOfTournament(string link, List<string> list)
        {
            int count = 0;
            int countOfLosers = list.Count / 2;
            foreach(var loser in list)
            {
                if (loser == "Lose")
                {
                    count++;
                }
                
            }
            if (count == countOfLosers)
            {
                list.RemoveAll(name => name == "Lose");
            }
            VerifyWinner(link, list);
            
            return list;
        }
        static void PrintTable(List<string> list)
        {
            Console.Clear();
            int count = 1;
            foreach (var person in list)
            {
                Console.WriteLine("{0} is {1}", count, person);
                count++;
            }
        }
        static void VerifyWinner (string link, List<string> list)
        {
            if (list.Count == 1)
            {
                PrintWinner(list);
                SaveList(link, list);
                StartTour();
            }
        }
        static void PrintWinner(List<string> winner)
        {
            Console.Clear();
            Console.WriteLine("Winner is - {0}", winner[0]);
        }
        static void SaveList(string link, List<string> list)
        {
            File.Delete(link+ ".txt");
            File.AppendAllLines(link + ".txt", list);
        }
    }
}