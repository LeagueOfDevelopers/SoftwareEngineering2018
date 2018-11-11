using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using Dapper;


namespace WordContext
{
    public class SqlWordRepository : IWordsRepository
    {
        private readonly string _connectionString;

        public SqlWordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<WordPair> GetAllWords()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var pairs = connection.Query<WordPair>(
                    "SELECT * FROM Words", null, objects => new WordPair(objects[1].ToString(), objects[2].ToString())
                );

                return pairs;
            }
        }

        public IEnumerable<WordPair> GetRandomPairWords(int amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var pairs = connection.Query<WordPair>(
                    "SELECT DISTINCT * FROM Words", null,
                    objects => new WordPair(objects[1].ToString(), objects[2].ToString())
                );

                Random r = new Random();

                return pairs.OrderBy(pair => r.Next()).Take(amount);
            }
        }

        public void AddWordPairs(IEnumerable<WordPair> wordsWithTranslations)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (WordPair pair in wordsWithTranslations)
                {
                    connection.Execute(
                        $"INSERT INTO Words VALUES ('{pair.Origin}', N'{pair.Translation}') "
                    );
                }
            }
        }

        public void AddWordPair(WordPair wordPair)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    $"INSERT INTO Words VALUES ('{wordPair.Origin}', N'{wordPair.Translation}') "
                );
            }
        }

        public IEnumerable<WordPair> GetUnlearnedWords(IEnumerable<WordPair> learnedWords)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var pairs = connection.Query<WordPair>(
                    "SELECT * FROM Words"
                );

                return pairs.Except(learnedWords);
            }
        }

        public void RemovePair(WordPair wordPair)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    $"DELETE FROM Words WHERE Origin='{wordPair.Origin}' AND Translation = N'{wordPair.Translation}'"
                );
            }
        }
    }
}