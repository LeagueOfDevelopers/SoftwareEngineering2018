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
        void Close();
        IEnumerable<IPareOfWords> GetAllWordPares();
        IEnumerable<IPareOfWords> GetExercise();
        void Start(IEnumerable<SessionPareOfWords> words, IUser user);
        int SaveResults(Dictionary<Guid, string> answers);
    }
}
