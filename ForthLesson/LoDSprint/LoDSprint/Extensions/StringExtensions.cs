using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LoDSprint
{
    public static class StringExtensions
    {
        public static Dictionary<Word, Translation> DeserializeDictionary(this string serializedDictionary)
        {
            return JsonConvert
                .DeserializeObject
                <Dictionary<Word, Translation>>
                (serializedDictionary);
        }

        public static Dictionary<Guid, IUser> DeserializeUsers(this string serializedUser)
        {
            return JsonConvert
                .DeserializeObject
                <Dictionary<Guid, IUser>>
                (serializedUser);
        }
    }
}
