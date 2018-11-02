using System;
using System.Collections.Generic;
using LoDSprintApi.Exceptions;
using LoDSprintApi.Filters;
using LoDSprintApi.Models;
using LoDSprintApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LoDSprintApi.Controllers
{
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        public UserServiceController(InFileUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        [HttpGet("users/{userId}/words/learned")]
        public IEnumerable<WordModel> GetUserLearnedWords(Guid userId)
        {
            var user = _usersRepository
                .LoadUser(userId) ?? throw new NotFoundException($"User with id {userId} not found");

            return _usersRepository
                .LoadUser(userId)
                .LearnedWords;
        }

        [HttpGet("users/{userId}/words/studied")]
        public IEnumerable<StudiedWordModel>  GetUserStudiedWords(Guid userId)
        {
            var user = _usersRepository
                .LoadUser(userId) ?? throw new NotFoundException($"User with id {userId} not found");

            return _usersRepository
                .LoadUser(userId)
                .StudiedWords;
        }

        [HttpPost("users")]
        public Guid RegisterUser([FromBody] string nickName)
        {
            var newUserId = Guid.NewGuid();
            var newUser = new UserModel(
                newUserId,
                nickName,
                new List<WordModel>(),
                new List<StudiedWordModel>()
                );

            _usersRepository.SaveUser(newUser);

            return newUserId;
        }

        private readonly IUsersRepository _usersRepository;
    }
}