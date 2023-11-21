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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dewey_Mastery
{
/**
*
* @studentName EmmanuelKianda
* @studentNumber 10081944
* @PROG7312
* @POE
*/
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Category : Window
    {
        #region Global Variables
        // Declare a private static integer variable named 'gameMode' to store the game's mode.
        private static int gameMode = 0;

        // Declare a public static property named 'GameMode' to access and modify the 'gameMode' value.
        public static int GameMode
        {
            // Getter for 'GameMode' returns the current value of 'gameMode'.
            get => gameMode;

            // Setter for 'GameMode' allows you to modify 'gameMode' when setting the property value.
            set => gameMode = value;
        }
        #endregion

        public Category()
        {
            InitializeComponent();
        }

        // Control Mouse Direction
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Code to control the direction of the mouse
                DragMove();
        }

        // Minimize Window Button
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Code to minimize the window
        }

        // Close Window Button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Code to shut down the application
        }

        // Enter "Replace Book" Mode
        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            gameMode = 1; // -- Set game mode value to 3
            this.Hide(); // Hide current window
            GameRules gameRules = new GameRules(); // Create a new instance of GameRules
            gameRules.Show(); // Show the GameRules window


        }

        // Enter "Identify Area" Mode
        private void btnIdentify_Click(object sender, RoutedEventArgs e)
        {
            gameMode = 2; // -- Set game mode value to 2
            this.Hide(); // Hide current window
            GameRules gameRules = new GameRules(); // Create a new instance of GameRules
            gameRules.Show(); // Show the GameRules window

        }

        // Enter "Find Call Number" Mode
        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {

            gameMode = 3; // -- Set game mode value to 3
            this.Hide(); // Hide current window
            GameRules gameRules = new GameRules(); // Create a new instance of GameRules
            gameRules.Show(); // Show the GameRules window

        }

        // Return to Start Page
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide current window
            Start startPage = new Start(); // Create a new instance of Start
            startPage.Show(); // Show the Start window
        }

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
