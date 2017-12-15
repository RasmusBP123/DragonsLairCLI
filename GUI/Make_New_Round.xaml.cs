using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DragonsLair;
namespace GUI
{

    public partial class Make_New_Round : Window
    {
        Controller C;
        public Make_New_Round()
        {
            C = new Controller();
            InitializeComponent();
        }

        private void ButtonNewRound_Click(object sender, RoutedEventArgs e)
        {
            C.ScheduleNewRound(TextInputNewRound.Text);   
        }
    }
}
