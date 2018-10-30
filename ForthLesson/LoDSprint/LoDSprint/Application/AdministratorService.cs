using LoDSprint.Repositories;
using LoDSprint.Exceptions;
using System;

namespace LoDSprint.Application
{
    public class AdministratorService : IAdministratorService
    {
        public AdministratorService(InFileDictionaryRepository dictionaryRepository, IUser administrator)
        {
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
            _administrator = administrator ?? throw new ArgumentNullException(nameof(administrator));
        }

        public void AddNewWord(Guid whoAddId, string word, string translation)
        {
            if (whoAddId == _administrator.Id)
            {
                var newWord = new Word(word);
                var wordTranslation = new Translation(translation);

                _dictionaryRepository.SaveDictionaryPair(newWord, wordTranslation);
            }
            else
                throw new PermissionDeniedException(
                    $"User with id {whoAddId} doesn't have rights to add new word");
        }

        private readonly InFileDictionaryRepository _dictionaryRepository;
        private readonly IUser _administrator;
    }
}
