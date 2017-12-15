using System.Collections.Generic;

namespace DragonsLair
{
    class TournamentRepo
    {
        private List<Tournament> tournaments = new List<Tournament>();

        public void RegisterTournament(string name)
        {
            Tournament newTournament = new Tournament(name);
            RegisterTournament(newTournament);
        }

        public void RegisterTournament(Tournament tournament)
        {
            tournaments.Add(tournament);
        }

        public Tournament GetTournament(string name)
        {
            Tournament tournament = null;
            int idx = 0;
            while((tournament == null) && (idx < tournaments.Count))
            {
                if (tournaments[idx].Name.Equals(name))
                {
                    tournament = tournaments[idx];
                }
                idx++;
            }
            return tournament;
        }
    }
}
