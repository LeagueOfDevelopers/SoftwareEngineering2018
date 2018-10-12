namespace TournamentNamespace
{
    public class Member
    {
        public string Name { get; }
        public int Level { get; set; }

        public Member(string name)
        {
            Name = name;
            Level = 1;
        }

        public void IncreaseLevel()
        {
            Level++;
        }
    }
}
