using System.ComponentModel.DataAnnotations;

namespace WithDbLoDSprintApi.Models
{
    public class DictionaryPairModel
    {
        [Required]
        public string Word { get; set; }

        [Required]
        public string Translation { get; set; }
    }
}
