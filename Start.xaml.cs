/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/

using Dewey_Mastery.System_Features;
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
    * @POE
*/
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        #region Global Variables
        // This private boolean variable keeps track of whether sound is currently on or off.
        private bool isSoundOn = true;

        // Definition of a DependencyProperty named "ImageSource."
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource",                  // The name of the property.
            typeof(ImageSource),            // The type of the property (ImageSource in this case).
            typeof(Start),                  // The type that owns this DependencyProperty (Start class).
            new PropertyMetadata(null)      // Default metadata for the property (initial value is null).
        );

        // The ImageSource property is a wrapper around the DependencyProperty.
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); } // Getter returns the value of the DependencyProperty.
            set { SetValue(ImageSourceProperty, value); }              // Setter sets the value of the DependencyProperty.
        }
        #endregion

        public Start()
        {
            InitializeComponent();

            // Register event handlers for various events:
            #region Handle Background Music
            // 1. When this window is loaded, execute the 'Start_Loaded' method.
            Loaded += Start_Loaded;

            // 2. When this window is closed, execute the 'Start_Closed' method.
            Closed += Start_Closed;

            // 3. When the background music (bgMusic) media ends, execute the 'BgMusic_MediaEnded' method.
            bgMusic.MediaEnded += BgMusic_MediaEnded;
            #endregion
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

        // This event handler is called when the media playback reaches the end.
        private void BgMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            // When the media playback ends, set the position to 0 and play again (loop)
            bgMusic.Position = TimeSpan.Zero;
            bgMusic.Play();
        }

        // This event handler is called when the window is loaded
        private void Start_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the background music when the window is loaded
            bgMusic.Play();
        }

        // This event handler is called when the window is closed.
        private void Start_Closed(object sender, EventArgs e)
        {
            // Stop the background music when the window is closed 
            bgMusic.Stop();
        }

        // Start Course
        private void btnGetStarted_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide current window
            Category selectCategory = new Category(); // Create a new instance of Category
            selectCategory.Show(); // Show the Category window  
        }

        // This method is executed when the "soundMode" button is clicked.
        private void soundMode_Click(object sender, RoutedEventArgs e)
        {
            // Check the current state of the sound.
            if (isSoundOn)
            {
                // If sound is currently on, stop the background music.
                bgMusic.Stop();

                // Update the button label to indicate "SOUND OFF."
                soundMode.Content = "SOUND OFF";

                // Change the image displayed to indicate sound off (a speaker with a cross).
                soundStatus.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Dewey_Mastery;component/Sound/soundOff.png"));
            }
            else
            {
                // If sound is currently off, start playing the background music.
                bgMusic.Play();

                // Update the button label to indicate "SOUND ON."
                soundMode.Content = "SOUND ON";

                // Change the image displayed to indicate sound on (a speaker without a cross).
                soundStatus.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Dewey_Mastery;component/Sound/soundOn.png"));
            }

            // Toggle the sound state. If it was on, it will become off, and vice versa.
            isSoundOn = !isSoundOn;
        }

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/