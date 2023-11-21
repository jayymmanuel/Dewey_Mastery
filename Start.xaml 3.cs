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
using System.Configuration;

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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        private static bool soundState = true;
        private bool isSoundOn = true; // A variable to track the sound status

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
    "ImageSource",
    typeof(ImageSource),
    typeof(Start),
    new PropertyMetadata(null)
);

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static bool SoundState { get => soundState; set => soundState = value; }

        public Start()
        {
            InitializeComponent();
            InitializeSound();

            // Initialize background music
            bgMusic.MediaEnded += BgMusic_MediaEnded;
        }
        private void InitializeSound()
        {
            if (SoundState)
            {
                PlayBackgroundMusic();
                SetSoundMode("SOUND ON", "pack://application:,,,/Dewey_Mastery;component/Sound/soundOn.png");
            }
            else
            {
                StopBackgroundMusic();
                SetSoundMode("SOUND OFF", "pack://application:,,,/Dewey_Mastery;component/Sound/soundOff.png");
            }
        }

        private void PlayBackgroundMusic()
        {
            bgMusic.Play();
        }

        private void StopBackgroundMusic()
        {
            bgMusic.Stop();
        }
        private void BgMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            // When the media playback ends, set the position to 0 and play again (loop)
            bgMusic.Position = TimeSpan.Zero;
            bgMusic.Play();
        }
        private void SetSoundMode(string content, string imagePath)
        {
            soundMode.Content = content;
            soundStatus.Source = new BitmapImage(new Uri(imagePath));
        }

        private void Start_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the background music when the window is loaded
            bgMusic.Play();
        }

        private void Start_Closed(object sender, EventArgs e)
        {
            // Stop the background music when the window is closed 
            bgMusic.Stop();
        }

        #region Control Mouse Direction
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Code to control the direction of the mouse
                DragMove();
        }
        #endregion

        #region Minimize Window Button
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Code to minimize the window
        }
        #endregion

        #region Close Window Button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Code to shut down the application
        }
        #endregion

        #region Start Course
        private void btnGetStarted_Click(object sender, RoutedEventArgs e)
        {

            this.Hide(); // Hide current window
            MainWindow mainWindow = new MainWindow(); // Create a new instance of MainWindow
            mainWindow.Show(); // Show the MainWindow window 
        }

        #endregion


        private void soundMode_Click(object sender, RoutedEventArgs e)
        {
            if (isSoundOn)
            {
                StopBackgroundMusic();
                SoundState = false;
                SetSoundMode("SOUND OFF", "pack://application:,,,/Dewey_Mastery;component/Sound/soundOff.png");
            }
            else
            {
                PlayBackgroundMusic();
                SoundState = true;
                SetSoundMode("SOUND ON", "pack://application:,,,/Dewey_Mastery;component/Sound/soundOn.png");
            }

            isSoundOn = !isSoundOn;
        }


    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/