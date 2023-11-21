/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System.Windows;
using System.Windows.Input;


namespace Dewey_Mastery
{
/**
*
* @studentName EmmanuelKianda
* @studentNumber 10081944
* @PROG7312
* @POW
*/
    /// <summary>
    /// Interaction logic for GameRules.xaml
    /// </summary>
    public partial class GameRules : Window
    {
        #region Global Variables
        // Declare a private static integer variable named 'gameDifficulty' to store the game's difficulty level.
        private static int gameDifficulty = 0;

        // Declare a public static property named 'GameDifficulty' to access and modify the 'gameDifficulty' value.
        public static int GameDifficulty
        {
            // Getter for 'GameDifficulty' returns the current value of 'gameDifficulty'.
            get => gameDifficulty;

            // Setter for 'GameDifficulty' allows you to modify 'gameDifficulty' when setting the property value.
            set => gameDifficulty = value;
        }

        #endregion

        // Display Game Rules
        public GameRules()
        {
            InitializeComponent();

            
            // Switch statement based on the value of 'Category.GameMode'.
            switch (Category.GameMode)
            {
                // Case 1: GameMode is 1.
                case 1:
                    // Display game rules for GameMode 1.
                    gameRules.Text = "1. Sort the books in ascending order based on The Dewey Decimal System."
                        + "\n" + "2. Beat the clock - sort all the books in the right order before the timer runs out."
                        + "\n" + "3. Use your mouse to drag and drop the books to the correct place."
                        + "\n" + "4. You gain 1 point for each correct placement you get.";
                    break;

                case 2:
                    // Display game rules for GameMode 2.
                    gameRules.Text = "1. Match the columns of the descriptions and call numbers based on The Dewey Decimal System."
                        + "\n" + "2. Beat the clock - match all the columns before the timer runs out."
                        + "\n" + "3. Use your mouse to click and select from both lists then click the 'CHECK' button to see if you made a correct match."
                        + "\n" + "4. You gain 2.5 points for each correct match you get.";
                    break;

                    // (Game Mode 3 is yet to be implemented) 
                case 3:
                    // Display game rules for GameMode 2.
                    gameRules.Text = "1. Select one of the button options that matches the displayed descriptions based on The Dewey Decimal System."
                        + "\n" + "2. Beat the clock - match up all the descriotions with the correct button option before the timer runs out."
                        + "\n" + "3. Use your mouse to click and select the option that matches the displated description."
                        + "\n" + "4. You are tested on your accuracy and speed.";
                    break;

                // Default case: Handle other GameModes or testing scenarios.
                default:
                    // Handle other GameMode (currently empty).
                    gameRules.Text = "Testing Testing";
                    break;
            }

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

        // This method handles redirecting the user to the previous window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide current window
            Category selectCategory = new Category(); // Create a new instance of Category
            selectCategory.Show(); // Show the Category window
        }

        // This method handles redirecting the user to the correct Game Mode they've chosen
        public void enterGameMode()
        {
            switch (Category.GameMode)
            {
                case 1:
                    this.Hide(); // Hide the current window
                    ReplaceBook replaceBook = new ReplaceBook(); // Create a new instance of ReplaceBook
                    replaceBook.Show(); // Show the ReplaceBook window
                    break;

                case 2:
                    this.Hide(); // Hide the current window
                    IdentifyArea identify = new IdentifyArea(); // Create a new instance of IdentifyArea
                    identify.Show(); // Show the IdentifyArea window
                    break;

                case 3:
                    this.Hide(); // Hide the current window
                    FindCallNumber findCallNumber = new FindCallNumber();
                    findCallNumber.Show(); // Show the IdentifyArea window
                    break;

                default:
                    // Handle other cases or provide a default behavior if needed
                    break;
            }

        }

        // This method handles instances when the 'Easy Mode' has been selected
        private void btnEasyMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 1; // Set the difficulty value to 1 aka Easy
            enterGameMode();
        }

        // This method handles instances when the 'Medium Mode' has been selected
        private void btnMediumMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 2; // Set the difficulty value to 1 aka Medium
            enterGameMode();
        }

        // This method handles instances when the 'Hard Mode' has been selected
        private void btnHardMode_Click(object sender, RoutedEventArgs e)
        {
            GameDifficulty = 3; // Set the difficulty value to 1 aka Hard
            enterGameMode();
        }
    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
