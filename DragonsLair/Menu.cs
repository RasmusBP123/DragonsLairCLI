using System;

namespace DragonsLair
{
    public class Menu
    {
        private Controller control = null;

        public Menu()
        {
            control = new Controller();
        }

        public void RunMenu()
        {
            bool running = true;
            do
            {
                ShowMenu();
                string choice = GetUserChoice();
                switch (choice)
                {
                    case "0":
                        running = false;
                        break;
                    case "1":
                        CreateTournament();
                        break;
                    case "2":
                        ScheduleNewRound();
                        break;
                    case "3":
                        SaveMatch();
                        break;
                    case "4":
                        ShowScore();
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.ReadLine();
                        break;
                }
            } while (running);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Dragons Lair");
            Console.WriteLine();
            Console.WriteLine("1. Etabler liga");
            Console.WriteLine("2. Planlæg runde i liga");
            Console.WriteLine("3. Afvikl kamp");
            Console.WriteLine("4. Præsenter ligastilling");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
        }

        private string GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Indtast dit valg: ");
            return Console.ReadLine();
        }

        private void CreateTournament()
        {
            Tournament tournament = new Tournament(GetTournamentname());
        }

        private void ShowScore()
        {
            string tournamentName = GetTournamentname();
            control.ShowScore(tournamentName);
        }

        private void ScheduleNewRound()
        {
            string tournamentName = GetTournamentname();
            control.ScheduleNewRound(tournamentName);
        }

        private String GetTournamentname()
        {
            Console.Write("Angiv navn på liga: ");
            return Console.ReadLine();
        }

        private void SaveMatch()
        {
            string tournamentName = GetTournamentname();
            Console.Write("Angiv runde: ");
            string round = Console.ReadLine();
            int roundnr = int.Parse(round);
            Console.Write("Angiv første modstander: ");
            string opponent1 = Console.ReadLine();
            Console.Write("Angiv anden modstander: ");
            string opponent2 = Console.ReadLine();
            Console.Write("Angiv vinderhold: ");
            string winner = Console.ReadLine();
            Console.Clear();
            control.SaveMatch(tournamentName, roundnr, opponent1, opponent2, winner);
        }
    }
}