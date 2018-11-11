using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface ISession : IIdentificator
    {
        IUser User { get; }
        bool IsOpen { get; }
        IEnumerable<SessionPareOfWords> GetAllWordPares();
        IEnumerable<SessionPareOfWords> GetExercise();
        void Start(IEnumerable<SessionPareOfWords> words, IUser user);
    }
}
