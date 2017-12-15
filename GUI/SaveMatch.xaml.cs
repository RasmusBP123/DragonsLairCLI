using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DragonsLair;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowSaveMatch.xaml
    /// </summary>
    public partial class WindowSaveMatch : Window
    {
        Controller C;
        public string Firstopp { get; set; }
        public string SecoOpp { get; set; }

        public int MatchNumber { get; set; } = 0;

        public Tournament Currenttournament { get; set; }

        public WindowSaveMatch()
        {
            C = new Controller();
            InitializeComponent();
        }

        private void Save_Match_Click(object sender, RoutedEventArgs e)
        {
            Team1.Text = Firstopp;
            Team2.Text = SecoOpp;

            if(WinningTeams.Text == Firstopp || WinningTeams.Text == SecoOpp)
            {
            C.SaveMatch(InputTournament.Text, int.Parse(RoundNumber.Text), Firstopp, SecoOpp, WinningTeams.Text);
            
            }
            else
            {
                MessageBox.Show("Ugyldig vinder");
            }
        }

        private void RoundNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Firstopp = C.FirstOpponent(Convert.ToInt32(RoundNumber.Text), MatchNumber);
                SecoOpp = C.SecondOpponent(Convert.ToInt32(RoundNumber.Text),MatchNumber);
                Team1.Text = Firstopp;
                Team2.Text = SecoOpp;
            }
            catch(Exception es)
            {
                MessageBox.Show(es.Message);
            }
            
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MatchNumber++;
                Firstopp = C.FirstOpponent(Convert.ToInt32(RoundNumber.Text), MatchNumber);
                SecoOpp = C.SecondOpponent(Convert.ToInt32(RoundNumber.Text), MatchNumber);
                Team1.Text = Firstopp;
                Team2.Text = SecoOpp;
                WinningTeams.Text = string.Empty;
            }
            catch(Exception es)
            {
                MessageBox.Show("Fail dude" + es.Message);
            }
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MatchNumber--;
                Firstopp = C.FirstOpponent(Convert.ToInt32(RoundNumber.Text), MatchNumber);
                SecoOpp = C.SecondOpponent(Convert.ToInt32(RoundNumber.Text), MatchNumber);
                Team1.Text = Firstopp;
                Team2.Text = SecoOpp;
                WinningTeams.Text = string.Empty;
            }
            catch (Exception es)
            {
                MessageBox.Show("Fail dude" + es.Message);
            }
        }

        private void ScheduleInput_Click(object sender, RoutedEventArgs e)
        {
            Make_New_Round MNRound = new Make_New_Round();
            MNRound.Show();
        }
    }
}
