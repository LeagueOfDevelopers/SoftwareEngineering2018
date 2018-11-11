using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearnerAPI.Models
{
    public class User
    {
        [Required]
        public string name { get; set; }
    }
}
