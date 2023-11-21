/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Dewey_Mastery.Logic;
using Dewey_Mastery.System_Features;
using Dewey_Mastery.Tree_Structure;
using Newtonsoft.Json;
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
    public partial class FindCallNumber : Window
    {
        #region Global variables
        private Random random = new Random();
        private static DeweyCategory currentQuestion;
        private int storedCode;
        private int currentLevel = 1;  // Initialize to the first level
        // Declare a CountDownTimer instance for managing countdown functionality
        private CountDownTimer timer;
        private double scoreValue = 0; // Declare a double variable to store the user's score
        // Declare an empty string that will store the value of the remainng time
        private string remainingTime = "";
        // Declare a static variable 'timerXDifficulty' to store the timer duration based on game difficulty.
        private static int timerXDifficulty = 0;
        // Creating a getter and setter for the timerXDifficulty field.
        public static int TimerXDifficulty { get => timerXDifficulty; set => timerXDifficulty = value; }
        // Declare a DispatcherTimer
        private DispatcherTimer matchCheckTimer;
        private DeweySearchTree deweySearchTree;
        #endregion
        public FindCallNumber()
        {
            InitializeComponent();
            FetchLevel.LoadDeweyData();  // Load Dewey data into memory
            InitializeQuestion();  // Initialize the first question
            InitializeButtonOptions();
            InitializeTimer();

            // Initialize the matchCheckTimer
            matchCheckTimer = new DispatcherTimer();
            matchCheckTimer.Interval = TimeSpan.FromSeconds(1); // 1-second interval
            matchCheckTimer.Tick += MatchCheckTimer_Tick;
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

        // InitializeTimer method sets up the 'timer' object to manage a countdown of 60 seconds.
        private void InitializeTimer()
        {
            // Determine the timer duration based on the selected game difficulty
            switch (GameRules.GameDifficulty)
            {
                case 1:
                    timerXDifficulty = 60;
                    break;
                case 2:
                    timerXDifficulty = 45;
                    break;
                default:
                    timerXDifficulty = 30;
                    break;
            }
            // Create a new CountDownTimer instance with an initial time of 60 seconds
            timer = new CountDownTimer(timerXDifficulty);

            // Subscribe to the TimeChanged event to handle timer updates
            timer.TimeChanged += Timer_TimeChanged;
        }

        // Timer_TimeChanged method handles the timer's time update event.
        private void Timer_TimeChanged(object sender, EventArgs e)
        {
            // Get the remaining time from the timer
            remainingTime = timerValue.Text;


            // Use Dispatcher.Invoke to safely update the UI from a different thread
            Dispatcher.Invoke(() =>
            {
                // Format and update the displayed time
                string formattedTime = $"{timer.RemainingSeconds / 60:00}:{timer.RemainingSeconds % 60:00}";
                timerValue.Text = formattedTime;

                // Check if the timer has reached 0 seconds
                if (timer.RemainingSeconds == 0)
                {
                    timer.Stop(); // Stop the timer when it reaches zero
                    timerValue.Text = "00:00"; // Change the timer value to a default of '00:00' = 'mm:ss'


                    // All items have been removed, so you can proceed to the GameScore window.
                    Hide();
                    GameScore gameScoreWindow = new GameScore();
                    // Set the score and remaining time in the GameScore window
                    gameScoreWindow.SetScoreAndTime(scoreValue, remainingTime);

                    // Show the GameScore window
                    gameScoreWindow.Show();
                }
            });
        }

        // Event handler for the matchCheckTimer.Tick event
        private void MatchCheckTimer_Tick(object sender, EventArgs e)
        {
            // Hide the "matchCheck" TextBlock
            matchCheck.Text = "";
            matchCheck.Foreground = Brushes.Transparent;

            // Stop the timer
            matchCheckTimer.Stop();
        }

        // MoveToNextLevel method traverses to the next level 
        private void MoveToNextLevel()
        {
            // Move to the next level
            if (currentLevel < 3)
            {
                currentLevel++;

                // Set currentQuestion to a new random category at the new level
                currentQuestion = FetchLevel.GetRandomCategoryAtLevel(currentLevel);

                // Display the description of the new question
                displayedText.Content = currentQuestion.Label;
            }
            else
            {
                matchCheck.Text = "Congratulations! You reached the most detailed level";
                matchCheck.Foreground = new SolidColorBrush(Color.FromRgb(50, 205, 50));
                currentLevel = 1;  // Reset to the first level for a new question
                InitializeQuestion();
            }
        }

        // InitializeQuestion method sets the current question
        private void InitializeQuestion()
        {
            try
            {
                // Get a random category at the current level for the new question
                currentQuestion = FetchLevel.GetRandomCategoryAtLevel(currentLevel);

                // Display the description of the new question on the UI
                displayedText.Content = currentQuestion.Label;
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs during question initialization
                MessageBox.Show($"An error occurred while initializing the question: {ex.Message}");
            }
        }


        // InitializeButtonOptions method set the values of the option buttons
        private void InitializeButtonOptions()
        {
            try
            {
                // Get a list of top-level categories for options
                List<DeweyCategory> options = FetchLevel.GetTopLevelCategories();

                // Ensure there are at least 4 options available
                if (options.Count < 4)
                {
                    MessageBox.Show("Not enough Dewey options available.");
                    return;
                }

                // Shuffle the options to randomize the order
                options = options.OrderBy(x => x.Code).ToList();

                // Create a list to store the options for the buttons
                List<string> buttonOptions = new List<string>();

                // Store the first digit of the current question code and multiply it by 100
                storedCode = int.Parse(currentQuestion.Code[0].ToString()) * 100;

                // Add the correct option to the list using the storedCode
                buttonOptions.Add($"{storedCode:D3} - {FetchLevel.GetDeweyLabelByCode(storedCode)}");

                // Add three random options to the list
                for (int i = 0; i < 3; i++)
                {
                    // Ensure the random option is not a duplicate of the correct option
                    DeweyCategory randomOption;
                    do
                    {
                        randomOption = options[random.Next(options.Count)];
                    } while (buttonOptions.Any(b => b.StartsWith(randomOption.Code)));

                    // Store the first digit of the random option code and multiply it by 100
                    int randomStoredCode = int.Parse(randomOption.Code[0].ToString()) * 100;

                    buttonOptions.Add($"{randomStoredCode:D3} - {randomOption.Label}");
                }

                // Shuffle the buttonOptions list to randomize the order
                buttonOptions = buttonOptions.OrderBy(x => int.Parse(x.Substring(0, 3))).ToList();

                // Assign the values to the buttons
                btnOption1.Content = buttonOptions[0];
                btnOption2.Content = buttonOptions[1];
                btnOption3.Content = buttonOptions[2];
                btnOption4.Content = buttonOptions[3];
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs during button options initialization
                MessageBox.Show($"An error occurred while initializing button options: {ex.Message}");
            }
        }

        // CheckAnswer verifies if the selected option is correct
        private void CheckAnswer(Button selectedButton)
        {
            try
            {
                // Extract the text content of the selected button
                string selectedOption = selectedButton.Content.ToString();

                // Extract the three digits from the selected option
                int selectedCode = int.Parse(selectedOption.Substring(0, 3));

                // Check if the selected option matches the stored code
                bool isCorrect = selectedCode == storedCode;

                if (isCorrect)
                {
                    // Display "Correct Match" message in green
                    matchCheck.Text = "Correct Match";
                    matchCheck.Foreground = new SolidColorBrush(Color.FromRgb(50, 205, 50));

                    // Move to the next level only if the current level is not 3
                    if (currentLevel < 3)
                    {
                        MoveToNextLevel();
                        InitializeButtonOptions();
                    }
                    else
                    {
                        // Reset to the first level for a new question
                        currentLevel = 1;
                        InitializeQuestion();
                        InitializeButtonOptions();

                        // All items have been removed, so proceed to the GameScore window.
                        Hide();
                        GameScore gameScoreWindow = new GameScore();

                        // Set the accuracy score and remaining time in the GameScore window
                        gameScoreWindow.SetAccuracyAndTime(AccuracyManager.UserAccuracy, remainingTime);

                        // Show the GameScore window
                        gameScoreWindow.Show();
                    }
                }
                else
                {
                    // Decrease accuracy by 5% for incorrect answers
                    AccuracyManager.DecreaseAccuracy();

                    // Display "Incorrect Match" message in red
                    matchCheck.Text = "Incorrect Match";
                    matchCheck.Foreground = new SolidColorBrush(Color.FromRgb(220, 20, 60));

                    // Initialize a new question and button options for the current level
                    InitializeQuestion();
                    InitializeButtonOptions();
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show($"An error occurred while checking the answer: {ex.Message}");
            }
        }

        private void btnOption1_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(btnOption1);
        }

        private void btnOption2_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(btnOption2);
        }

        private void btnOption3_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(btnOption3);
        }

        private void btnOption4_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(btnOption4);
        }
    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/