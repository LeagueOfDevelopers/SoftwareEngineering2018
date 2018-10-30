using System;
using System.Collections.Generic;

namespace LoDSprint
{
    public class Translation
    {
        public Translation(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            var translation = obj as Translation;
            return translation != null &&
                   Value == translation.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Translation translation1, Translation translation2)
        {
            return EqualityComparer<Translation>.Default.Equals(translation1, translation2);
        }

        public static bool operator !=(Translation translation1, Translation translation2)
        {
            return !(translation1 == translation2);
        }
    }
}
