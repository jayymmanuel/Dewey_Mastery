/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dewey_Mastery
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public static class ScoreManager
    {
        // Declare a public static integer property named 'UserScore' with a private setter, and initialize it to 0.
        public static double UserScore { get; private set; } = 0;

        // Define a public static method named 'UpdateScore' to update the 'UserScore' property.
        public static void UpdateScore(int score)
        {
            // Set the 'UserScore' property to the provided 'score' value.
            UserScore = score;
        }

        // Define a public static method named 'IncrementScore' to increment the 'UserScore' by 1.
        public static void IncrementScore()
        {
            // Increment the 'UserScore' by 1.
            UserScore += 2.5;
        }

        // Define a public static method named 'ResetScore' to reset the 'UserScore' to 0.
        public static void ResetScore()
        {
            // Reset the 'UserScore' to 0.
            UserScore = 0;
        }

        // AccuracyManager method
        public static class AccuracyManager
        {
            // Declare a public static double property named 'UserAccuracy' with a private setter, and initialize it to 100%.
            public static double UserAccuracy { get; private set; } = 100;


            public static void DecreaseAccuracy()
            {
                // Decrease accuracy by 5%.
                UserAccuracy = Math.Max(0, UserAccuracy - 5);
            }



            // Define a public static method named 'ResetAccuracy' to reset the 'UserAccuracy' to 100%.
            public static void ResetAccuracy()
            {
                // Reset the 'UserAccuracy' to 100%.
                UserAccuracy = 100;
            }
        }


    }


}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
