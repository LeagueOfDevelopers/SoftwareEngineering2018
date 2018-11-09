using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using Dapper;
using WordContext;

namespace UserContext
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //userRow contains only name, id, reg date
                var userRow = connection.QueryFirstOrDefault(
                    "SELECT * FROM Users " +
                    "WHERE Id=@id "
                    , new {id = userId});

                if (userRow == null)
                {
                    throw new AuthenticationException("No such user");
                }

                var inProgress = connection.Query(
                    "SELECT DISTINCT * FROM InProgressWords " +
                    "WHERE Id = @id "
                    , new {id = userId});

                var learned = connection.Query(
                    "SELECT DISTINCT * FROM LearnedWords " +
                    "WHERE Id = @id "
                    , new {id = userId});

                List<WordPair> inProgressWords = new List<WordPair>();
                List<int> inProgressCounter = new List<int>();
                List<WordPair> learnedWords = new List<WordPair>();

                foreach (var row in inProgress)
                {
                    inProgressWords.Add(new WordPair(row.Origin, row.Translation));
                    inProgressCounter.Add(row.Counter);
                }

                foreach (var row in learned)
                {
                    learnedWords.Add(new WordPair(row.Origin, row.Translation));
                }


                return new User(userRow.Id, userRow.Name, userRow.RegisterDate,
                    inProgressWords, inProgressCounter, learnedWords);
            }
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (IsUser(user.Id)) throw new InvalidOperationException("User already exists");

                connection.Execute(
                    $"INSERT INTO Users VALUES ('{user.Id.ToString()}',N'{user.Name}','{user.RegisterDate.ToString()}')");
            }
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User editedUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (!IsUser(editedUser.Id)) throw new AuthenticationException("No such user");

                connection.Execute(
                    $"UPDATE Users SET Name = N'{editedUser.Name}' WHERE Id = '{editedUser.Id.ToString()}'"
                );

                connection.Execute(
                    $"DELETE FROM LearnedWords WHERE Id = '{editedUser.Id.ToString()}'"
                );

                editedUser.LearnedWords.ForEach(pair =>
                {
                    connection.Execute(
                        $"INSERT INTO LearnedWords VALUES ('{editedUser.Id}','{pair.Origin}',N'{pair.Translation}') "
                    );
                });

                connection.Execute(
                    $"DELETE FROM InProgressWords WHERE Id = '{editedUser.Id.ToString()}'"
                );

                for (int i = 0; i < editedUser.InProgressCounter.Count; i++)
                {
                    var pair = editedUser.InProgressWords[i];
                    var counter = editedUser.InProgressCounter[i];

                    connection.Execute(
                        $"INSERT INTO InProgressWords VALUES ('{editedUser.Id}','{pair.Origin}',N'{pair.Translation}','{counter}')"
                    );
                }
            }
        }

        public bool IsUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var userRow = connection.QueryFirstOrDefault(
                    "SELECT * FROM Users " +
                    "WHERE Id=@id "
                    , new {id = userId});

                return userRow != null;
            }
        }
    }
}