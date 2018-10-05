using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Competition> t=new List<Competition>();
           A:
            string fun = Console.ReadLine();
            string name = Console.ReadLine();
            string second_atr = Console.ReadLine();
            
            Competition test;
            switch (fun)
            {
                case "create":
                    t.Add(create(name, second_atr));
                    File.Create(name+".txt");
                    
                    goto A;
                 
                case "winner":
                    test = findtour(name, t);
                    test.list.RemoveAt(test.list.IndexOf(second_atr));
                    goto A;
                default:
                    break;
                   

            }

        }

        private static Competition findtour(string name, List<Competition> t)
        {
            
                foreach (Competition a in t)
                {
                    if (a.name.Equals(name))
                    {
                        return a;
                    }
                    
                }
            return null;
        }

        
        private static Competition create(string name, string fileway)
        {
            List<string> list = Parse(fileway);
            Competition c = new Competition(name, list);
            return c;
            
        }

        private static List<string> Parse(string fileway)
        {
            List<string> a = File.ReadAllLines(fileway).ToList();
            return a;
        }


    }
}
