using System;
using System.Collections.Generic;
using System.Linq;


namespace DragonsLair
{
    public class Controller
    {
        public string TournamentName { get; set; }
        public int Round { get; set;}

        public int PlayedMatch { get; set; }

        public string ScoreString { get; set; }


        private TournamentRepo tournamentRepository = null;



        public Controller()
        {
            tournamentRepository = new TournamentRepo();
            Init(); 
        }

        // The Init() method is ONLY meant as a shortcut to fill in some data.
        // Actually it should be modified (refactored) and moved to a test project so clever tests can have some data 
        // and check if the functionality is as required!!

        public void Init()
        {
           
            TeamRepo teamRepository = new TeamRepo();
            PlayerRepo playerRepository = new PlayerRepo();
            string tournamentName = "X";
            Tournament tournament = new Tournament(tournamentName);

         
            // initialize with a default tournament
            tournamentRepository.RegisterTournament(tournament);

            playerRepository.RegisterPlayer("Laust Ulriksen");
            playerRepository.RegisterPlayer("Matthias Therkelsen", null, "matthias@therkelsen.dk", "+45 47002155");
            playerRepository.RegisterPlayer("Martin Bertelsen", "Nyborgvej 10, Odense", null, "+45 22521112");
            playerRepository.RegisterPlayer("Line Madsen", "Kochsgade 21, Odense", "linem@msn.dk", "+45 00142563");
            playerRepository.RegisterPlayer(new Player("Jette Detlevsen"));

       
            // initialize with a default set of teams
            teamRepository.RegisterTeam("A");
            teamRepository.RegisterTeam("B");
            teamRepository.RegisterTeam("C");
            teamRepository.RegisterTeam("D");
            teamRepository.RegisterTeam("E");

            // Add players to teams
            Team FCK = teamRepository.GetTeam("A");
            FCK.AddPlayer(playerRepository.GetPlayer("Laust Ulriksen"));
            Team OB = teamRepository.GetTeam("B");
            FCK.AddPlayer(playerRepository.GetPlayer("Matthias Therkelsen"));
            Team BiF = teamRepository.GetTeam("C");
            BiF.AddPlayer(playerRepository.GetPlayer("Martin Bertelsen"));
            Team Hobro = teamRepository.GetTeam("D");
            Hobro.AddPlayer(playerRepository.GetPlayer("Line Madsen"));
            Team AGF = teamRepository.GetTeam("E");
            AGF.AddPlayer(playerRepository.GetPlayer("Jette Detlevsen"));

          
            // Add teams to tournament
            tournament = tournamentRepository.GetTournament(tournamentName);
            tournament.AddTeam(FCK);
            tournament.AddTeam(OB);
            tournament.AddTeam(BiF);
            tournament.AddTeam(Hobro);
            tournament.AddTeam(AGF);

       
            // Initialize first round (this also initializes matches)
            ScheduleNewRound(tournamentName);
        }

        private string PadSpaceToName(string name, int length)
        {
            for (int i = name.Length; i < length; i++)
            {
                name = name + " ";
            }
            return name;
        }

 
        private int GetHighestScore(Dictionary<string, int> teamNameToScore)
        {
            int highestScore = 0;
            foreach (KeyValuePair<string, int> element in teamNameToScore)
            {
                if (highestScore < element.Value)
                {
                    highestScore = element.Value;
                }
            }
            return highestScore;
        }

        public void ShowScore(string tournamentName)
        {
            Dictionary<string, int> teamNameToScore = new Dictionary<string, int>();

            Tournament selectedTournament = tournamentRepository.GetTournament(tournamentName);
            int numberOfRounds = selectedTournament.GetNumberOfRounds();
            if (numberOfRounds > 0)
            {
                int numberOfMatches = 0;
                for (int i = 1; i <= numberOfRounds; i++)
                {
                    Round currentRound = selectedTournament.GetRound(i);
                    numberOfMatches = numberOfMatches + currentRound.GetNumberOfMatches();
                    foreach (Team winningTeam in currentRound.WinningTeams)
                    {
                        if (!teamNameToScore.ContainsKey(winningTeam.Name))
                        {
                            teamNameToScore.Add(winningTeam.Name, 0);
                        }
                        teamNameToScore[winningTeam.Name] = teamNameToScore[winningTeam.Name] + 1;
                    }
                }

                //output in sorted order starting with highest score - find highest score
                int highestScore = GetHighestScore(teamNameToScore);

          
                this.TournamentName = tournamentName;
                this.Round = numberOfRounds;
                this.PlayedMatch = numberOfMatches;                      

                string score1 = null;

                for (int i = highestScore; i >= 0; i--)
                {
                    foreach (KeyValuePair<string, int> element in teamNameToScore)
                    {
                        if (element.Value == i)
                        {
                            int score = highestScore - i + 1;
                            string name = PadSpaceToName(element.Key, 18);
                          score1 += ($"   {score} {name}  {element.Value}\n");
                        }
                    }
                }
                ScoreString = score1; 
       
            }
        }

        private List<int> GetRandomIndices(int count)
        {
            Random r = new Random();
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(i);
            }
            for (int i = 0; i < 100; i++)
            {
                int idx1 = r.Next(count);
                int idx2 = r.Next(count);
                int temp = result[idx1];
                result[idx1] = result[idx2];
                result[idx2] = temp;
            }
            return result;
        }

        public void ShowRound(string tournamentName, Round round, int roundNo)
        {
            int matchNo = 1;
    
            foreach (Match m in round.GetAllMatches())
            {
                string first = PadSpaceToName(m.FirstOpponent.Name, 10);
                string second = m.SecondOpponent.Name;
              
                matchNo++;
            }

            if (round.FreeRider != null) {
           
            }
          
        }

        public void ScheduleNewRound(string tournamentName)
        {
            Tournament selectedTournament = tournamentRepository.GetTournament(tournamentName);
            List<Team> teams = null;
            Round thisRound = null;

            //Get teams for new round
            int numberOfRounds = selectedTournament.GetNumberOfRounds();
            //Get teams for new round
            if (numberOfRounds == 0) //SPECIAL CASE: Initialize first round. Handled by adding all teams from tournament 
            {
                teams = selectedTournament.Teams();
            }
            else // We are scheduling round > 0, get winners from this round if it is finished
            {
                thisRound = selectedTournament.GetRound(numberOfRounds);
                if (thisRound.IsRoundFinished())
                {
                    teams = thisRound.WinningTeams;
                    if (thisRound.FreeRider != null)
                    {
                        teams.Add(thisRound.FreeRider);
                    }
                }
                else
                {
                    throw new Exception("Round is not finished");
                }
            }

            if (teams.Count < 2)
            {
                selectedTournament.Status = Tournament.State.FINISHED;
            }
            else
            {
                Round newRound = new Round();
                
                //Manage freeRiders
                if ((teams.Count % 2) != 0) //Select a freeRider
                {
                    //If prior round exists and it had a freeRider and if was first team the select second team
                    if ((numberOfRounds > 0) && (thisRound.FreeRider != null) && (thisRound.FreeRider.Equals(teams[0])))
                    {
                        newRound.FreeRider = teams[1];
                        teams.Remove(teams[1]);
                    }
                    else //select first team
                    {
                        newRound.FreeRider = teams[0];
                        teams.Remove(teams[0]);
                    }
                }

                List<int> randomIndices = GetRandomIndices(teams.Count);
                int noOfMatches = teams.Count / 2;
                for (int i = 0; i < noOfMatches; i++)
                {
                    Match newMatch = new Match
                    {
                        FirstOpponent = teams[randomIndices[2 * i]],
                        SecondOpponent = teams[randomIndices[2 * i + 1]]
                    };
                    newRound.AddMatch(newMatch);
                }
                selectedTournament.AddRound(newRound);
                ShowRound(tournamentName, newRound, numberOfRounds);

                if (newRound.FreeRider != null)
                {
                    teams.Add(newRound.FreeRider);
                }
            }
            
        }

        public string TournamentString { get; set; }
        public int RoundString { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string WinningTeam { get; set; }



        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            Tournament tournament = tournamentRepository.GetTournament(tournamentName);
            Round selectedRound = tournament.GetRound(roundNumber);
            Team winner = tournament.GetTeam(winningTeam);
            Match m = selectedRound.GetMatch(team1, team2);
            m.Winner = tournament.GetTeam(winningTeam);
        }

        public Tournament GetTournament(string name)
        {
            return tournamentRepository.GetTournament(name);
        }

        public string FirstOpponent(int RoundNumber, int MatchNumber)
        {
            Tournament t = GetTournament("X");
            Round r = t.GetRound(RoundNumber);
            List<Match> m = r.GetAllMatches();

            return m[MatchNumber].FirstOpponent.Name;
        }
        public string SecondOpponent(int RoundNumber, int MatchNumber)
        {
            Tournament t = GetTournament("X");
            Round r = t.GetRound(RoundNumber);
            List<Match> m = r.GetAllMatches();

            return m[MatchNumber].SecondOpponent.Name;
        }


    }
}
