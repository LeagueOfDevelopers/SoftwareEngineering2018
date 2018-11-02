using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace EnglishTrainer
{
    interface IWordsRepository
    {
        string _jsonFile { get; set; }

        List<Word> GetWords();

        void SaveWords(List<Word> words);

        void SaveWord(Word word);
    }
}
