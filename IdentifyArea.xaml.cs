/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using Dewey_Mastery.Dictionary;
using Dewey_Mastery.Logic;
using Dewey_Mastery.System_Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
    /// Interaction logic for ReplaceBook.xaml
    /// </summary>
    public partial class IdentifyArea : Window
    {
        #region Global variables

        // Declare an ObservableCollection to hold a collection of strings representing description
        ObservableCollection<string> description;

        // Declare an ObservableCollection to hold a collection of strings representing call numbers
        ObservableCollection<string> callNumber;

        // Declare a single Random instance for generating random values within the class
        private Random random = new Random();

        // Declare a CountDownTimer instance for managing countdown functionality
        private CountDownTimer timer;

        private double scoreValue = 0; // Declare a double variable to store the user's score

        // Declare an empty string that will store the value of the remainng time
        private string remainingTime = "";

        // Declare a static variable 'timerXDifficulty' to store the timer duration based on game difficulty.
        private static int timerXDifficulty = 0;

        // Initialize with the initial number of items to take
        private static int threshold = 0; 

        // Initialize with the initial number of items to take
        private static int itemsToTake = 0; 

        // Creating a getter and setter for the timerXDifficulty field.
        public static int TimerXDifficulty { get => timerXDifficulty; set => timerXDifficulty = value; }

        // Declare a DispatcherTimer
        private DispatcherTimer matchCheckTimer;

        // A bool (flag) to track which list is currently displaying
        private bool isDescriptionList = true; 

        // Define your Dewey Decimal system ranges and descriptions
        private static Dictionary<Tuple<int, int>, string> deweyRanges = DeweyDecimalManager.DeweyRanges;

        #endregion
        public IdentifyArea()
        {
            // Initialize the component 
            InitializeComponent();

            InitializeDescriptionValues();
            InitializeNumberValues();
            InitializeTimer();

            // Initialize the matchCheckTimer
            matchCheckTimer = new DispatcherTimer();
            matchCheckTimer.Interval = TimeSpan.FromSeconds(1); // 1-second interval
            matchCheckTimer.Tick += MatchCheckTimer_Tick;

            // Create and initialize the countdown timer
            InitializeTimer();

            // Set the initial content for the lists
            SetInitialListContent();

            // Call the method to randomly swap the lists
            RandomlySwapLists();

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

        // InitializeBookValues method sets up the 'descriptions' ObservableCollection with Classifications based on the dewey decimal system.
        private void InitializeDescriptionValues()
        {
            // Create a new ObservableCollection to store description values
            description = new ObservableCollection<string>
            {
                // Add descriptions to the ObservableCollection
                "General Knowledge",
                "Philosophy & Psychology",
                "Religion",
                "Social Sciences",
                "Language",
                "Science",
                "Technology",
                "Arts & Recreation",
                "Literature",
                "History & Geography"
            };

            // Shuffle the description list to randomize the order using the ColumnManager class
            ColumnManager.ShuffleDescriptions(description);

            // Assign the ObservableCollection as the ItemsSource of the first ListView
            columnListOne.ItemsSource = description;

            // Force a UI refresh to reflect the shuffle changes
            Application.Current.Dispatcher.Invoke(() =>
            {
                columnListOne.Items.Refresh();
            });

        }

        // InitializeBookValues method sets up the 'call numbers' ObservableCollection based on the created Classifications.
        private void InitializeNumberValues()
        {
            // Create a new ObservableCollection to store Dewey Decimal system codes
            callNumber = new ObservableCollection<string>();
            Random random = new Random(); // Initialize the Random object once

            // Iterate through the displayed descriptions in columnListOne
            foreach (string selectedDescription in columnListOne.Items)
            {
                if (deweyRanges.ContainsValue(selectedDescription))
                {
                    string deweyCode = ColumnManager.GenerateRandomDeweyCodeTopLevel(random, selectedDescription);
                    callNumber.Add(deweyCode);
                }
            }

            // Assign the ObservableCollection as the ItemsSource of the second ListView
            columnListTwo.ItemsSource = callNumber;

        }

        // InitializeTimer method sets up the 'timer' object to manage a countdown of (x) seconds.
        public void InitializeTimer()
        {
            // Determine the timer duration based on the selected game difficulty
            switch (GameRules.GameDifficulty)
            {
                case 1:
                    timerXDifficulty = 45;
                    break;
                case 2:
                    timerXDifficulty = 30;
                    break;
                default:
                    timerXDifficulty = 25;
                    break;
            }
            // Create a new CountDownTimer instance with the initial time
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
                    this.Hide();
                    GameScore gameScoreWindow = new GameScore();
                    gameScoreWindow.SetScoreAndTime(scoreValue, remainingTime);
                    gameScoreWindow.Show();
                }
            });
        }

        // Method to set the initial content for the lists
        private void SetInitialListContent()
        {
            // Ensure that columnListOne always shows 4 items
            columnListOne.ItemsSource = description.Take(4).ToList();


            // Setting up the threshold value that determines when the game is completed
            threshold = 4;

            // itemsToTake will be used to perform X - 1 in order to reduce the amount of items in the first listview, removing one for each correct match
            itemsToTake = 3;

            // Ensure that columnListTwo always shows 7 items

            var callNumbersToDisplay = callNumber.Take(7).ToList();

            // Shuffle the call numbers being displayed
            ColumnManager.ShuffleNumbers(callNumbersToDisplay);

            // Store the shuffled call numbers into List 2
            columnListTwo.ItemsSource = new ObservableCollection<string>(callNumbersToDisplay);

        }

        // SwapLists method is used to swap/alternate the cintent of List 1 and 2
        private void SwapLists()
        {
            if (isDescriptionList)
            {
                // If currently displaying descriptions, switch to call numbers
                var callNumbersToDisplay = callNumber.Take(4).ToList();

                // Shuffle the call numbers being displayed
                ColumnManager.ShuffleNumbers(callNumbersToDisplay);

                // Store the shuffled call numbers into List 1
                columnListOne.ItemsSource = new ObservableCollection<string>(callNumbersToDisplay);

                itemsToTake = 3;
                ListOne.Content = "Call Numbers";
            }
            else
            {
                // If currently displaying call numbers, switch to descriptions
                columnListOne.ItemsSource = description.Take(4).ToList();

                // Shuffle the description list to randomize the order using the ColumnManager class
                ColumnManager.ShuffleDescriptions(description);

                itemsToTake = 3;
                ListOne.Content = "Descriptions";
            }

            // Always update columnListTwo to show 7 items
            columnListTwo.ItemsSource = description.Take(7).ToList();

            ListTwo.Content = "Descriptions";


            isDescriptionList = !isDescriptionList; // Toggle the flag
        }

        // RandomlySwapLists method will swap the List 1 and 2 every 1/2 
        private void RandomlySwapLists()
        {
            // Use a random number to determine whether to swap or not
            Random random = new Random();
            bool shouldSwap = random.Next(0, 2) == 0; // 50% chance to swap

            if (shouldSwap)
            {
                // If the random number indicates a swap, switch the content
                SwapLists();
            }
        }

        // btnCheck_Click handles very instance of the 'CHECK' button being clicked
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            // Get the remaining time from the timer (assuming timerValue is the text block displaying the time)
            remainingTime = timerValue.Text;

            string selectedDescription = null;
            string selectedDeweyCode = null;

            // Check if list 1 is displayin the descriptions
            if (isDescriptionList)
            {
                selectedDescription = columnListOne.SelectedItem as string; // Retrieve descriptions from list 1
                selectedDeweyCode = columnListTwo.SelectedItem as string; // Retrieve call numbers/dewey code from list 2
            }
            else
            {
                selectedDescription = columnListTwo.SelectedItem as string; // Retrieve call numbers/dewey code from list 1
                selectedDeweyCode = columnListOne.SelectedItem as string; // Retrieve descriptions from list 2
            }

            if (!string.IsNullOrEmpty(selectedDescription) && !string.IsNullOrEmpty(selectedDeweyCode))
            {
                // Check if the selected items match based on the Dewey Decimal system
                bool isMatch = ColumnManager.IsMatchBasedOnDeweyDecimal(selectedDescription, selectedDeweyCode);

                if (isMatch)
                {
                    // Remove the matched items from the collections
                    description.Remove(selectedDescription);
                    callNumber.Remove(selectedDeweyCode);

                    if (isDescriptionList)
                    {
                        columnListOne.ItemsSource = description.Take(itemsToTake).ToList();
                    }
                    else
                    {
                        columnListOne.ItemsSource = callNumber.Take(itemsToTake).ToList();
                    }


                    // Decrement the number of items to take and the threshold
                    threshold--;
                    itemsToTake--;


                    // Increment the user's score
                    ScoreManager.IncrementScore();

                    // Refresh the UI to reflect the changes
                    columnListOne.Items.Refresh();
                    columnListTwo.Items.Refresh();

                    // Retrieve the current score from the ScoreManager
                    scoreValue = ScoreManager.UserScore;

                    matchCheck.Text = "Correct Match +2.5"; // Using the 'matchCheck' text to display a message notifying the user they've made a correct match and earned 2.5 points
                    matchCheck.Foreground = new SolidColorBrush(Color.FromRgb(50, 205, 50)); // Assign a green color to the 'matchCheck' text
                }
                else
                {
                    matchCheck.Text = "Incorrect Match"; // Using the 'matchCheck' text to display a message notifying the user they've made an incorrect match
                    matchCheck.Foreground = new SolidColorBrush(Color.FromRgb(220, 20, 60)); // Assign a red color to the 'matchCheck' text
                }
            }
            else
            {
                // If the user has not selected an item from both lists then they will be prompted to so
                MessageBox.Show("Please select items from both lists before clicking Done.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Start the timer
            matchCheckTimer.Start();

            // Check if the threshold has reached 0
            if (threshold == 0)
            {
                // Start the timer when the IdentifyArea instance is created
                timer.Stop(); // Stop the timer initially
                timer.Reset();

                this.Hide(); // Closing current windown 
                GameScore gameScoreWindow = new GameScore(); // Create a new instance of GameScore
                // Set the score and remaining time in the GameScore window
                gameScoreWindow.SetScoreAndTime(scoreValue, remainingTime);

                // Show the GameScore window
                gameScoreWindow.Show();
            }
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

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/