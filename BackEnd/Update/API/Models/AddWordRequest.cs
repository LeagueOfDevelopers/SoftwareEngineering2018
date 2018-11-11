using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearnerAPI.Models
{
    public class AddWordRequest
    {
        [Required]
        public string enWord { get; set; }
        [Required]
        public string transWord { get; set; }
    }
}
