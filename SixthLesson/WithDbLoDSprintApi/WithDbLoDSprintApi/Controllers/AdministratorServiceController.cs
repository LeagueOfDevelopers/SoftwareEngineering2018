using System;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WithDbLoDSprintApi.Models;

namespace WithDbLoDSprintApi.Controllers
{
    public class AdministratorServiceController : ControllerBase
    {
        public AdministratorServiceController(IAdministratorService administratorService)
        {
            _administratorService = administratorService ?? throw new ArgumentNullException(nameof(administratorService));
        }

        [HttpPost]
        [Route("admin/{adminId}/words")]
        public ActionResult AddNewWord(Guid adminId, [FromBody] DictionaryPairModel dictionaryPair)
        {
            return Ok(_administratorService.AddDictionaryPair(
                adminId,
                dictionaryPair.Word, 
                dictionaryPair.Translation));
        }

        [HttpDelete]
        [Route("admin/{adminId}/words/{pairId}")]
        public ActionResult DeleteDictionaryPair(Guid adminId, Guid pairId)
        {
            _administratorService.DeleteDictionaryPair(adminId, pairId);
            return Ok();
        }

        [HttpGet]
        [Route("admin/{adminId}/words")]
        public ActionResult LoadDictionary(Guid adminId)
        {
            var dictionary = _administratorService.LoadDictionary(adminId);
            return Ok(dictionary);
        }



        private readonly IAdministratorService _administratorService;
    }
}