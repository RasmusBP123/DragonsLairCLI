using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DragonsLair;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Controller C;
        public Window1()
        {
            C = new Controller();
            InitializeComponent();
        }

        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            Tournament t = new Tournament(TournamentInput.Text);
            Close();

        }

        private void TournamentInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
