using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLessons
{
    public class Generator
    {
        public List<RepWord> Start(int count, User user)
        {
            List<RepWord> toRand = user.ShowInProgress();
            Random rnd = new Random();
            List<RepWord> toShow = new List<RepWord>();
            if (count > toRand.Count) throw new InvalidOperationException();
            for (int i = 0; i < count; i++)
            {
                int ind = rnd.Next(toRand.Count);
                toShow.Add(toRand[ind]);
                toRand.RemoveAt(ind);
            }
            
            toRand = user.ShowInProgress();

            for (int i = 0; i < count; i++)
            {
                if (rnd.Next(2) == 1)
                {
                    string NewRus = "";
                    Random NewInd = new Random();
                    do
                        NewRus = toRand[NewInd.Next(toRand.Count())].Rus;
                    while (NewRus == toShow[i].Rus);

                    toShow[i].Rus = NewRus;
                }
            }

            return toRand;
        }
    }
}
