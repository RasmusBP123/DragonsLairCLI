using System;
using System.Windows;
using DragonsLair;

namespace GUI
{

    public partial class MainWindow : Window
    {
        Controller C;
        public MainWindow()
        {
            C = new Controller();
            InitializeComponent();
        }

        
        string Scorestring { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                C.ShowScore(UseScoreTextBox.Text);
                InputLigaName.Content = C.TournamentName;

                InputRound.Content = C.Round;
                InputScore.Content = C.ScoreString;
                InputPlayedMatch.Content = C.PlayedMatch;
            }
            catch(Exception es)
            {
                MessageBox.Show("Du skal skrive X \n" + es.Message);
            }
        }
  
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSaveMatch Save = new WindowSaveMatch();
            Save.Show();
        }

        private void ButtonForCreateTournament_Click(object sender, RoutedEventArgs e)
        {
            Window1 CreateTour = new Window1();
            CreateTour.Show();

        }
    }
}
