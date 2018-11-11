using Leo_sprint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Leo_sprintAPI
{
    public class NicknameModel
    {
        public NicknameModel(string nickname)
        {
            _nickname = nickname;
        }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string _nickname { get; set; }
       
    }
}
