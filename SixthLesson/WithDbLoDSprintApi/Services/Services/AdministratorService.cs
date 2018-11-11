using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Exceptions;
using BusinessServices.Interfaces;
using Data.Interfaces;

namespace BusinessServices.Services
{
    public class AdministratorService : IAdministratorService
    {
        public AdministratorService(IDictionaryRepository dictionaryRepository, Administrator administrator)
        {
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
            _administrator = administrator ?? throw new ArgumentNullException(nameof(administrator));
        }

        public Guid AddDictionaryPair(Guid whoAddId, string word, string translation)
        {
            if (whoAddId != _administrator.Id)
                throw new PermissionDeniedException(
                    $"User with id {whoAddId} doesn't have rights to add new word");

            var pairId = Guid.NewGuid();
            var dictionaryPair = new DictionaryPair(
                pairId,
                new Word(word),
                new Translation(translation));

            _dictionaryRepository.SaveDictionaryPair(dictionaryPair);
            return pairId;
        }

        public void DeleteDictionaryPair(Guid whoDeleteId, Guid pairId)
        {
            if (whoDeleteId != _administrator.Id)
                throw new PermissionDeniedException(
                    $"User with id {whoDeleteId} doesn't have rights to delete words");

            _dictionaryRepository.DeleteDictionaryPair(pairId);
        }

        public IEnumerable<DictionaryPair> LoadDictionary(Guid whoLoadId)
        {
            if (whoLoadId != _administrator.Id)
                throw new PermissionDeniedException(
                    $"User with id {whoLoadId} doesn't have rights to delete words");

            return _dictionaryRepository.LoadDictionary();
        }

        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly Administrator _administrator;
    }
}
