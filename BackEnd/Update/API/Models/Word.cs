using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearnerAPI.Models
{
    public class Word
    {
        public Word(string enWord, string transWord, Guid id)
        {
            this.enWord = enWord ?? throw new ArgumentNullException(nameof(enWord));
            this.transWord = transWord ?? throw new ArgumentNullException(nameof(transWord));
            this.id = id;
        }

        [Required]
        public string enWord { get; set; }
        [Required]
        public string transWord { get; set; }
        [Required]
        public Guid id { get; set; }
    }
}
