using Leo_sprint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Leo_sprintAPI
{
    public class UserModel
    {
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string _nickname { get; set; }
        [Required]
        public Guid _id { get; set; }
        [Required]
        public List<Word> words_in_process { get; set; }
        [Required]
        public List<Word> learned_words { get; set; }
    }
}
