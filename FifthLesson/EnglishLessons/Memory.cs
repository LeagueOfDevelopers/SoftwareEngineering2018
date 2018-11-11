using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace EnglishLessons
{
    public class Memory
    {
        public void Save(Session session)
        {
            string serialized = JsonConvert.SerializeObject(session);
            File.Create("FooBar.txt");
            File.WriteAllText("FooBar.txt", serialized);
        }

        public Session Load()
        {
            string deserialized = File.ReadAllText("FooBar.txt");
            return JsonConvert.DeserializeObject<Session>(deserialized);
        }
    }
}
