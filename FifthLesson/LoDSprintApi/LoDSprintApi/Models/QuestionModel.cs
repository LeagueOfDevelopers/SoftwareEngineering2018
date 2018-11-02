using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoDSprintApi
{
    public class QuestionModel
    {
        public QuestionModel(WordModel word, TranslationModel proposedTranslation)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            ProposedTranslation = proposedTranslation ?? throw new ArgumentNullException(nameof(proposedTranslation));
        }

        [Required]
        public WordModel Word { get; }

        [Required]
        public TranslationModel ProposedTranslation { get; }
    }
}
