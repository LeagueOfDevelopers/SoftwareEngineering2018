using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Leo_sprint;

namespace Leo_sprintAPI
{
    public class WordModel
    {
    [Required]
    public string In_english { get; set; }
    [Required]
    public string In_russian { get; set; }
    }
}
