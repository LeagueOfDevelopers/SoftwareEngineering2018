using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoDSprintApi.Exceptions;
using LoDSprintApi.Models;
using LoDSprintApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoDSprintApi.Controllers
{
    [ApiController]
    public class AdministratorServiceController : ControllerBase
    {

        public AdministratorServiceController(InFileDictionaryRepository dictionaryRepository, AdministratorModel administrator)
        {
            _dictionaryRepository = dictionaryRepository ?? throw new ArgumentNullException(nameof(dictionaryRepository));
            _administrator = administrator ?? throw new ArgumentNullException(nameof(administrator));
        }

        [HttpPost("admin/{adminId}/words")]
        public void AddNewWords(Guid adminId, [FromBody] List<DictionaryPairModel> dictionaryPairs)
        {
            if (adminId != _administrator.Id)
                throw new PermissionDeniedException(
                        $"User with id {adminId} doesn't have rights to add new word");

            dictionaryPairs.ForEach(pair =>
                    _dictionaryRepository
                    .SaveDictionaryPair(pair));
        }

        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly AdministratorModel _administrator;
    }
}