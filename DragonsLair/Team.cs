using System.Collections.Generic;

namespace DragonsLair
{
    public class Team
    {
        public string Name { get; set; }
        private List<Player> players = new List<Player>();

        public Team(string teamName)
        {
            Name = teamName;
        }

        public void AddPlayer(Player p)
        {
            players.Add(p);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
