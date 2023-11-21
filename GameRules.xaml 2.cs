/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/

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

namespace Dewey_Mastery
{
/**
*
* @studentName EmmanuelKianda
* @studentNumber 10081944
* @PROG7312
* @Part 1
*/
    /// <summary>
    /// Interaction logic for GameRules.xaml
    /// </summary>
    public partial class GameRules : Window
    {
        private static int gameDifficulty = 0;

        public static int GameDifficulty { get => gameDifficulty; set => gameDifficulty = value; }

        public GameRules()
        {
            InitializeComponent();


            switch (MainWindow.GameMode)
            {
                case 1:
                    gameRules.Text = "1. Sort the books in ascending order based on The Dewey Decimal System."
                        + "\n" + "2. Beat the clock - sort all the books in the right order before the timer runs out."
                        + "\n" + "3. Use your mouse to drag and drop the books to the correct place"
                        + "\n" + "4. You gain points for each correct placement you get.";
                    break;
                case 2:
                    // Handle GameMode 2
                    break;
                default:
                    gameRules.Text = "Testing Testing";
                    break;
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Code to control the direction of the mouse
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Code to minimize the window
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Code to shut down the application

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow selectMode = new MainWindow();
            selectMode.Show();
        }

        private void btnEasyMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 1;
            this.Hide();
            ReplaceBook replaceBook = new ReplaceBook();
            replaceBook.Show();
        }

        private void btnMediumMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 2;
            this.Hide();
            ReplaceBook replaceBook = new ReplaceBook();
            replaceBook.Show();
        }

        private void btnHardMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 3;
            this.Hide();
            ReplaceBook replaceBook = new ReplaceBook();
            replaceBook.Show();
        }
    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
