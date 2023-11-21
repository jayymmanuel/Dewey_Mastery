/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using Dewey_Mastery.Logic;
using Dewey_Mastery.System_Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
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
    public partial class ReplaceBook : Window
    {
        #region Global variables
        // Declare an ObservableCollection to hold a collection of strings representing books
        ObservableCollection<string> books;

        // Declare a single Random instance for generating random values within the class
        private Random random = new Random();

        // Declare a CountDownTimer instance for managing countdown functionality
        private CountDownTimer timer;

        // Declare a Tuple to store a boolean value and an integer result
        private Tuple<bool, int> orderResult;


        // Declare an empty string that will store the value of the remainng time
        private string remainingTime = "";

        // Declare a static variable 'timerXDifficulty' to store the timer duration based on game difficulty.
        private static int timerXDifficulty = 0;

        // Creating a getter and setter for the timerXDifficulty field.
        public static int TimerXDifficulty { get => timerXDifficulty; set => timerXDifficulty = value; }
        #endregion
        public ReplaceBook()
        {
            // Initialize the component and set up book values and the timer.
            InitializeComponent();
            InitializeBookValues();
            InitializeTimer();

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

        // InitializeBookValues method sets up the 'books' ObservableCollection with random book values.
        private void InitializeBookValues()
        {
            // Create a new ObservableCollection to store book values
            books = new ObservableCollection<string>();

            // Create a Random object for generating random values
            Random random = new Random();

            // Generate and add 10 random book values to the collection
            for (int i = 1; i <= 10; i++)
            {
                // Generate a random Dewey code
                string deweyCode = ColumnManager.GenerateRandomDeweyCode(random);

                // Generate random author initials
                string authorInitials = ColumnManager.GenerateRandomInitials(random);

                // Combine Dewey code and author initials to create a book value
                string bookValue = $"📓 • {deweyCode} - {authorInitials}";

                // Add the generated book value to the ObservableCollection
                books.Add(bookValue);
            }

            // Set the 'booksList' control's item source to the newly created 'books' ObservableCollection
            booksList.ItemsSource = books;
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
            remainingTime = timerValue.Text; // Assuming 'timerValue' is the text block displaying the time

            // Check if items in the 'booksList' are in ascending order and count how many are in order
            var result = IsListViewInAscendingOrder(booksList);

            // Use Dispatcher.Invoke to safely update the UI from a different thread
            Dispatcher.Invoke(() =>
            {
                // Format and update the displayed time
                string formattedTime = $"{timer.RemainingSeconds / 60:00}:{timer.RemainingSeconds % 60:00}";
                timerValue.Text = formattedTime;

                // Check if the timer has reached 0 seconds
                if (timer.RemainingSeconds == 0)
                {
                    timerValue.Text = "00:00"; // Change the timer value to a default of '00:00' = 'mm:ss'

                    // Update the user's score
                    ScoreManager.UpdateScore(result.Item2);

                    this.Hide(); // Hide current window
                    GameScore gameScoreWindow = new GameScore(); // Create a new instance of the GameScore
                    gameScoreWindow.SetScoreAndTime(result.Item2, remainingTime); // Set the score and remaining time in the GameScore window

                    gameScoreWindow.Show(); // Show the GameScore window 
                }
            });
        }

        // ListViewItem_MouseMove event handler initiates drag-and-drop when a mouse move event occurs.
        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // Check if the left mouse button is pressed
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    // Get the source ListViewItem
                    ListViewItem sourceListViewItem = (ListViewItem)sender;

                    // Initiate drag-and-drop operation with the source ListViewItem
                    DragDrop.DoDragDrop(booksList, sourceListViewItem, DragDropEffects.Move);
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during the drag-and-drop operation.
                throw;
            }
        }

        // ListViewItem_DragEnter event handler manages the appearance of drag-and-drop indicators.
        private void ListViewItem_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                // Get the target and source ListViewItems
                var targetListViewItem = (ListViewItem)sender;
                var sourceListViewItem = (ListViewItem)e.Data.GetData("System.Windows.Controls.ListViewItem");

                // If the source and target are the same, no action is needed
                if (sourceListViewItem == targetListViewItem)
                    return;

                // Extract the content of the target and source items
                var targetItem = targetListViewItem.Content.ToString();
                var sourceItem = sourceListViewItem.Content.ToString();

                // Find the indices of the target and source items in the ObservableCollection 'books'
                var targetItemIndex = books.IndexOf(targetItem);
                var sourceItemIndex = books.IndexOf(sourceItem);

                // Find the top and bottom rectangles within the target ListViewItem's template
                Rectangle topRectangle = (Rectangle)targetListViewItem.Template.FindName("topRectangle", targetListViewItem);
                Rectangle bottomRectangle = (Rectangle)targetListViewItem.Template.FindName("bottomRectangle", targetListViewItem);

                // Determine if the source item is being moved above or below the target item
                if (targetItemIndex < sourceItemIndex)
                {
                    topRectangle.Visibility = Visibility.Visible; // Show the top indicator
                }
                else if (targetItemIndex > sourceItemIndex)
                {
                    bottomRectangle.Visibility = Visibility.Visible; // Show the bottom indicator
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during drag-and-drop indicator management.
                throw;
            }
        }

        // ListViewItem_DragOver event handler is responsible for handling drag-and-drop while items are being dragged over the target item.
        private void ListViewItem_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                // Get the target and source ListViewItems
                var targetListViewItem = (ListViewItem)sender;
                var sourceListViewItem = (ListViewItem)e.Data.GetData("System.Windows.Controls.ListViewItem");

                // The actual logic for drag-and-drop handling is usually implemented here, but it's not shown in this code snippet.
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during drag-and-drop handling.
                throw;
            }
        }


        // ListViewItem_DragLeave event handler manages the appearance of drag-and-drop indicators when a dragged item leaves the target item's area.
        private void ListViewItem_DragLeave(object sender, DragEventArgs e)
        {
            try
            {
                // Get the target ListViewItem
                var targetListViewItem = (ListViewItem)sender;

                // Find the top and bottom rectangles within the target ListViewItem's template
                Rectangle topRectangle = (Rectangle)targetListViewItem.Template.FindName("topRectangle", targetListViewItem);
                Rectangle bottomRectangle = (Rectangle)targetListViewItem.Template.FindName("bottomRectangle", targetListViewItem);

                // Hide both the top and bottom indicators when the dragged item leaves
                topRectangle.Visibility = Visibility.Collapsed;
                bottomRectangle.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during the management of drag-and-drop indicators.
                throw;
            }
        }

        // ListViewItem_Drop event handler handles the drop action when a dragged item is released over a target item.
        private void ListViewItem_Drop(object sender, DragEventArgs e)
        {
            try
            {
                // Get the target and source ListViewItems
                var targetListViewItem = (ListViewItem)sender;
                var sourceListViewItem = (ListViewItem)e.Data.GetData("System.Windows.Controls.ListViewItem");

                // Extract the content of the target and source items
                var targetItem = targetListViewItem.Content.ToString();
                var sourceItem = sourceListViewItem.Content.ToString();

                // Find the indices of the target and source items in the ObservableCollection 'books'
                var targetItemIndex = books.IndexOf(targetItem);
                var sourceItemIndex = books.IndexOf(sourceItem);

                // Find the top and bottom rectangles within the target ListViewItem's template
                Rectangle topRectangle = (Rectangle)targetListViewItem.Template.FindName("topRectangle", targetListViewItem);
                Rectangle bottomRectangle = (Rectangle)targetListViewItem.Template.FindName("bottomRectangle", targetListViewItem);

                // Hide both the top and bottom indicators
                topRectangle.Visibility = Visibility.Collapsed;
                bottomRectangle.Visibility = Visibility.Collapsed;

                // Perform the item move operation within the ObservableCollection 'books'
                books.Move(sourceItemIndex, targetItemIndex);
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during the drag-and-drop item rearrangement.
                throw;
            }
        }

        // IsListViewInAscendingOrder method checks if the ListView items are in ascending order based on Dewey codes and author initials.
        private Tuple<bool, int> IsListViewInAscendingOrder(ListView listView)
        {
            // Create lists to store Dewey codes and author initials
            List<string> deweyCodes = new List<string>();
            List<string> authorInitials = new List<string>();

            // Extract Dewey codes and author initials from the ListView items
            foreach (var item in listView.Items)
            {
                string bookValue = item.ToString();

                // Split the bookValue string by '•' and take the part after '•'
                string[] parts = bookValue.Split('•');

                if (parts.Length != 2)
                {
                    // Skip items with invalid format
                    continue;
                }

                string deweyCodeAndAuthor = parts[1].Trim(); // Get what comes after '•' and trim spaces

                // Now, split the deweyCodeAndAuthor by '-' to get Dewey code and Author Initials
                string[] deweyAndAuthorParts = deweyCodeAndAuthor.Split('-');

                if (deweyAndAuthorParts.Length != 2)
                {
                    // Skip items with invalid format
                    continue;
                }

                string deweyCode = deweyAndAuthorParts[0].Trim();
                string author = deweyAndAuthorParts[1].Trim();

                deweyCodes.Add(deweyCode);
                authorInitials.Add(author);
            }

            // Check if the Dewey codes are in ascending order
            int inOrderCount = 0;
            for (int i = 0; i < deweyCodes.Count - 1; i++)
            {
                if (IsAscendingOrder.IsDeweyCodeInAscendingOrder(deweyCodes[i], deweyCodes[i + 1]))
                {
                    inOrderCount++;
                }
                else if (deweyCodes[i] == deweyCodes[i + 1] && !IsAscendingOrder.IsAuthorInitialsAscending(authorInitials[i], authorInitials[i + 1]))
                {
                    return Tuple.Create(false, 0); // Author initials not in ascending order for identical Dewey codes
                }
            }

            // Determine if all items are in ascending order
            bool isAscending = inOrderCount == deweyCodes.Count - 1;

            // Return a Tuple indicating whether the list is in ascending order and the count of in-order items
            return Tuple.Create(isAscending, inOrderCount + 1); // Add 1 to start counting from 1
        }


        // btnDone_Click event handler is triggered when the "Done" button is clicked.
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            // Get the remaining time from the timer (assuming timerValue is the text block displaying the time)
            remainingTime = timerValue.Text;

            // Check if items are in ascending order and count how many are in order
            var result = IsListViewInAscendingOrder(booksList);

            GameScore gameScoreWindow = new GameScore(); // Create a new instance of GameScore

            if (timer.RemainingSeconds >= ReplaceBook.TimerXDifficulty - 5) // Check if less than 5 seconds have elapsed (x - 5 = y)
            {
                // Stop the timer
                timer.Stop();

                // Display a warning message for quick completion
                MessageBox.Show("Suspicious, It is mathematically impossible for you to have completed the training this quickly unless you're a big nerd", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                // Reload the current page
                this.Hide();
                ReplaceBook replaceBookWindow = new ReplaceBook();
                replaceBookWindow.Show();
            }
            else if (result.Item1)
            {
                // Play the badge video
                gameScoreWindow.BadgeVideo.Play();

                // Update the user's score
                ScoreManager.UpdateScore(result.Item2);

                // Set the score and remaining time in the GameScore window
                this.Hide();
                gameScoreWindow.SetScoreAndTime(result.Item2, remainingTime);

                gameScoreWindow.Show();  // Show the GameScore window 
            }
            else
            {
                // Play the badge video
                gameScoreWindow.BadgeVideo.Play();

                // Update the user's score
                ScoreManager.UpdateScore(result.Item2);

                this.Hide(); // Hide current window
                gameScoreWindow.SetScoreAndTime(result.Item2, remainingTime); // Set the score and remaining time in the GameScore window

                // Show the GameScore window
                gameScoreWindow.Show();
            }

            // Stop the timer
            timer.Stop();
        }

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
/*
    Author  : Kareem Sulthan [Youtube]
    Topic   : I customized the WPF ListView to support "reordering" of its items | C# & WPF
    Resource: [https://www.youtube.com/watch?v=hGNetSIAsQU&list=LL&index=4] 
    Date    : Sep 17, 2022
 */
