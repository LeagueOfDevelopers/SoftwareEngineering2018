using System.ComponentModel.DataAnnotations;
using LeonLearn;

namespace LeonLearnAPI.Models
{
    public class EndLessonRequest
    {
        [Required] public Lesson Lesson { get; set; }

        [Required] public bool[] Answers { get; set; }
    }
}
