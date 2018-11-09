using System;

namespace BusinessEntities
{
    public class Translation
    {
        public Translation(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }
    }
}
