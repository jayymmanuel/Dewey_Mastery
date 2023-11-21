/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/

using Dewey_Mastery.System_Features;
using System;
using System.Windows;
using System.Windows.Input;
using static Dewey_Mastery.ScoreManager;

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
    /// Interaction logic for GameScore.xaml
    /// </summary>
    public partial class GameScore : Window
    {
        #region Global Variables
        // Declare a public static Tuple to hold an order result, consisting of a boolean (success) and an integer (result code).
        public static Tuple<bool, int> orderResult;

        // Create an instance of the Random class for generating random values.
        private readonly Random random = new Random();

        // Declare a CountDownTimer instance for managing countdown functionality.
        private CountDownTimer timer;

        // Declare an integer to store the completed time (in seconds) for a task or operation.
        private int completedTime;
        #endregion

        public GameScore()
        {       
            InitializeComponent();

            // Play the badge video when the GameScore instance is created.
            BadgeVideo.Play();

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

        // Display Score and time complted in 
        public void SetScoreAndTime(double score, string remainingTime)
        {
            ScoreBoardText.Text = $"{score}/10";

            switch (Category.GameMode)
            {
                case 1:
                    // Calculate the time it took to complete the task
                    completedTime = ReplaceBook.TimerXDifficulty - ParseRemainingTime(remainingTime);
                    RemainingTimeText.Text = $"Completed Time: {completedTime} seconds";
                    break;

                case 2:
                    // Calculate the time it took to complete the task
                    completedTime = IdentifyArea.TimerXDifficulty - ParseRemainingTime(remainingTime);
                    RemainingTimeText.Text = $"Completed Time: {completedTime} seconds";
                    break;

                default:
                    // Handle other cases or provide a default behavior if needed
                    break;
            }

            // Display a congratulatory message based on the user's score
            string congratsMessageText = GetCongratsMessageAndBadge(score, 0); // Set accuracy to 0 for score case

            congratsMessage.Text = congratsMessageText;
        }

        // Display Accuracy and Time Completed for Case 3
        public void  SetAccuracyAndTime(double accuracy, string remainingTime)
        {
            // Display accuracy
            ScoreBoardText.Text = $"{accuracy}%";

                    // Calculate the time it took to complete the task
                    completedTime = FindCallNumber.TimerXDifficulty - ParseRemainingTime(remainingTime);
                    RemainingTimeText.Text = $"Completed Time: {completedTime} seconds";

            // Display a congratulatory message based on the user's score
            string congratsMessageText = GetCongratsMessageAndBadge(0, accuracy); // Set score to 0 for accuracy case
            congratsMessage.Text = congratsMessageText;

        }


        // This helper method parses a formatted remaining time string and returns the total remaining seconds.
        private int ParseRemainingTime(string remainingTime)
        {
            // Split the remaining time string into hours, minutes, and seconds.
            string[] timeParts = remainingTime.Split(':');

            // Check if the split resulted in two parts (minutes and seconds).
            if (timeParts.Length == 2)
            {
                // Attempt to parse the minutes and seconds as integers.
                if (int.TryParse(timeParts[0], out int minutes) && int.TryParse(timeParts[1], out int seconds))
                {
                    // Calculate and return the total remaining seconds.
                    // ReplaceBook.TimerXDifficulty represents a constant multiplier for difficulty level.
                    return minutes * ReplaceBook.TimerXDifficulty + seconds;
                }
            }

            // If parsing fails or the input format is incorrect, default to 0 seconds.
            return 0;
        }

        // This helper method generates a congratulatory message and selects a badge animation
        // based on the user's score.
        private string GetCongratsMessageAndBadge(double score, double accuracy)
        {
            string videoSource = string.Empty;
            string[] congratsMessages = new string[] { };

            congratsMessages = new string[]
            {
        "Flawless execution at lightning speed! You're a Dewey legend!",
        "Exceptional! You've reached the pinnacle of perfection!",
        "Bravo! You've outshone everyone with your quick perfection!",
        "You're in a league of your own! Diamond-worthy performance!",
            };

            if ((score == 10 && completedTime <= 22) || (accuracy == 100 && completedTime <= 22))
            {
                videoSource = "Badges/rareBadge.mp4";
                congratsMessages = new string[]
                {
            "Congratulations on your flawless victory!",
            "Incredible performance! You've earned the rarest of diamonds!",
            "Masterful! You've achieved perfection in record time!",
            "You're a speedster and perfectionist rolled into one! Well done!",
            "Flawless execution at lightning speed! You're a Dewey legend!",
            "Exceptional! You've reached the pinnacle of perfection!",
            "Bravo! You've outshone everyone with your quick perfection!",
            "You're in a league of your own! Diamond-worthy performance!",
                };
            }
            else if (score == 10 || accuracy == 100)
            {
                videoSource = "Badges/spotlessScoreBadge.mp4";
                congratsMessages = new string[]
                {
            "Congratulations! You did great!",
            "Well done! You're a Dewey Decimal pro!",
            "Amazing work! You aced it!"
                };
            }
            else if ((score >= 5 && score <= 9) || (accuracy >= 65 && accuracy <= 95))
            {
                videoSource = "Badges/decentScoreBadge.mp4";
                congratsMessages = new string[]
                {
            "Great job! You're on the right track!",
            "Well done! Keep up the good work!",
            "You're making progress! Keep it up!"
                };
            }
            else if (score < 5 || accuracy < 50)
            {
                videoSource = "Badges/badScoreBadge.mp4";
                congratsMessages = new string[]
                {
            "Nice try! You'll do better next time.",
            "Keep practicing, you'll improve!",
            "You're getting there! Try again for a higher score."
                };
            }

            BadgeVideo.Source = new Uri(videoSource, UriKind.RelativeOrAbsolute);
            int randomIndex = random.Next(congratsMessages.Length);
            return congratsMessages[randomIndex];
        }



        // This method handles instances where the user click on the 'Play Again' button
        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            // Reset the score to 0
            ScoreManager.ResetScore(); // You might need to create a ResetScore method in ScoreManager

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
                    // Reset score accuracy
                    AccuracyManager.ResetAccuracy();

                    this.Hide(); // Hide the current window
                    FindCallNumber findCallNumber = new FindCallNumber(); // Create a new instance of FindCallNumber
                    findCallNumber.Show(); // Show the FindCallNumber window
                    break;

                default:
                    // Handle other cases or provide a default behavior if needed
                    break;
            }
        }

        // This method handles redirecting the user to the Home Page window
        private void btnReturnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide current window
            Category selectCategory = new Category(); // Create a new instance of Category
            selectCategory.Show(); // Show the Category window
        }

        // Method to Loop Badge animation
        private void BadgeVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            // When the video ends, rewind it to the beginning and play it again
            BadgeVideo.Position = TimeSpan.Zero;
            BadgeVideo.Play();

        }


    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
