using System;
using System.Collections.Generic;

namespace LoDSprintApi
{
    public class TranslationModel
    {
        public TranslationModel(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }
    }
}
