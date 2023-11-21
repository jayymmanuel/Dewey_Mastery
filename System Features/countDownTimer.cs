/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/

using System;
using System.Windows.Threading;

namespace Dewey_Mastery.System_Features
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public class CountDownTimer
    {
        #region Gloabl Variables
        private DispatcherTimer timer;
        public event EventHandler TimeChanged;
        public int RemainingSeconds { get; private set; }
        private int initialSeconds;
        #endregion

        // CountDownTimer constructor initializes an instance of the CountDownTimer class.
        // It sets the initial countdown duration, initializes RemainingSeconds,
        // and sets up the associated DispatcherTimer.
        public CountDownTimer(int initialSeconds)
        {
            // Set the initial countdown duration
            this.initialSeconds = initialSeconds;

            // Initialize RemainingSeconds with the initial value
            RemainingSeconds = initialSeconds;

            // Initialize the associated timer
            InitializeTimer();
        }


        // InitializeTimer method sets up the DispatcherTimer for the countdown.
        // It configures the timer with a one-second interval and attaches the Timer_Tick event handler.
        private void InitializeTimer()
        {
            // Create a new DispatcherTimer instance
            timer = new DispatcherTimer();

            // Set the timer interval to one second
            timer.Interval = TimeSpan.FromSeconds(1);

            // Attach the Timer_Tick event handler to handle each timer tick
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();
        }

        // Timer_Tick method is the event handler called on each timer tick (every second).
        // It decreases RemainingSeconds and raises the TimeChanged event.
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Decrease RemainingSeconds and raise the TimeChanged event if there is still time remaining
            if (RemainingSeconds > 0)
            {
                RemainingSeconds--;

                // Raise the TimeChanged event to notify subscribers about the updated time
                TimeChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Stop(); // Stops the timer when the countdown reaches zero.
            }
        }

        // Stop method stops the timer.
        public void Stop()
        {
            // Stops the timer.
            timer.Stop();
        }

        // Reset method resets the timer's remaining time to the initial value.
        public void Reset()
        {
            // Resets the remaining time to the initial value.
            RemainingSeconds = initialSeconds;
        }

    }


}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/

