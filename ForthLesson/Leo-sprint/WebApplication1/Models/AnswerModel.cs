using System.ComponentModel.DataAnnotations;

namespace Leo_sprintAPI
{
    public class AnswerModel
    {
        public AnswerModel(bool[] answers)
        {
            Answers = answers;
        }
        [Required]
        public bool[] Answers { get; set; }
    }
}