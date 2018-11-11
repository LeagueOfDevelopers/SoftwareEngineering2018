using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearnerAPI.Models
{
    public class Session
    {
        public Session(Guid userId, Guid sessionId, bool isOpen)
        {
            this.userId = userId;
            this.sessionId = sessionId;
            this.isOpen = isOpen;
        }

        [Required]
        public Guid userId { get; set; }
        public Guid sessionId { get; set; }
        public bool isOpen { get; set; }
    }
}
