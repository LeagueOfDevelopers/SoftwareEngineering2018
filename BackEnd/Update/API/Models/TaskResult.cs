using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearnerAPI.Models
{
    public class TaskResult
    {
        [Required]
        public string answer { get; set; }
        [Required]
        public Guid wordId { get; set; }
    }
}
