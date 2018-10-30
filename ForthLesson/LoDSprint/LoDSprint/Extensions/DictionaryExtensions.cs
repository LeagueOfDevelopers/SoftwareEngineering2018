using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LoDSprint
{
    public static class DictionaryExtensions
    {
        public static string SerializeDictionary(this Dictionary<Word, Translation> dictionary)
        {
            return JsonConvert
                .SerializeObject(dictionary);
        }

        public static string SerializeUsers(this Dictionary<Guid, IUser> users)
        {
            return JsonConvert
                .SerializeObject(users);
        }
    }
}
