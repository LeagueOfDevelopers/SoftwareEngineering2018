using System;

namespace Leo_sprint.Client
{
    public class UserClient
    {
        private User this_user;

        public void StartSession()
        {
            var this_session = this_user.StartSession();
            this_session.Show(this_user);
        }

        public void AddNewWordInDictionary(Word new_word)
        {
            this_user.AddNewWordInDictionary(new_word);
        }
        public void PrintWordInProcess()
        {
            var words_in_process = this_user.ShowWordInProgress();
            foreach (var word in words_in_process)
            {
                Console.WriteLine(word._in_english + "-" + word._in_russian);
            }
        }



    }
}
