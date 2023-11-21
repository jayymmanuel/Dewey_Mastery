/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using Dewey_Mastery.Dictionary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Dewey_Mastery.Logic
{
/**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public class ColumnManager
    {

        // Define your Dewey Decimal system ranges and descriptions
        private static Dictionary<Tuple<int, int>, string> deweyRanges = DeweyDecimalManager.DeweyRanges;

        // GenerateRandomInitials method generates random initials.
        public static string GenerateRandomInitials(Random random)
        {

            // Define strings for consonants and vowels
            string consonants = "BCDFGHJKLMNPQRSTVWYZ";
            string vowels = "AEIOU";

            // Initialize the 'initials' string
            string initials = "";

            // Generate the first character (a consonant)
            initials += consonants[random.Next(consonants.Length)];

            // Generate two more characters (a vowel and another consonant)
            initials += vowels[random.Next(vowels.Length)];
            initials += consonants[random.Next(consonants.Length)];

            // Return the generated initials
            return initials;
        }

        // This method shuffles the elements in an ObservableCollection using the Fisher-Yates shuffle algorithm.
        public static void ShuffleDescriptions<T>(ObservableCollection<T> list)
        {
            // Get the number of elements in the ObservableCollection.
            int n = list.Count;

            // Create a Random object for generating random numbers.
            Random random = new Random();

            // Iterate through the ObservableCollection in reverse order.
            for (int i = n - 1; i > 0; i--)
            {
                // Generate a random index 'j' between 0 and 'i' (inclusive).
                int j = random.Next(0, i + 1);

                // Swap the elements at indices 'i' and 'j'.
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        // This method shuffles a list of elements using the Fisher-Yates shuffle algorithm.
        public static void ShuffleNumbers<T>(List<T> list)
        {
            // Get the number of elements in the list.
            int n = list.Count;

            // Create a Random object for generating random numbers.
            Random random = new Random();

            // Iterate through the list in reverse order.
            for (int i = n - 1; i > 0; i--)
            {
                // Generate a random index 'j' between 0 and 'i' (inclusive).
                int j = random.Next(0, i + 1);

                // Swap the elements at indices 'i' and 'j'.
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        // GenerateRandomDeweyCodeTopLevel method generates a random 'Top Level' dewey code/ call number based on the parameters
        public static string GenerateRandomDeweyCodeTopLevel(Random random, string selectedDescription)
        {
            if (deweyRanges.ContainsValue(selectedDescription))
            {
                // Get the corresponding range for the selected description
                Tuple<int, int> range = deweyRanges.First(x => x.Value == selectedDescription).Key;

                // Generate a random main class number within the specified range.
                int mainClass = random.Next(range.Item1, range.Item2 + 1);

                // Combine the parts into a Dewey Decimal code formatted as XXX.XXX.
                string code = mainClass.ToString("D3");
                return code;
            }
            return string.Empty; // Handle the case where the selectedDescription is not recognized
        }

        // GenerateRandomDeweyCode method generates a random Dewey Decimal code.
        public static string GenerateRandomDeweyCode(Random random)
        {
            while (true)
            {
                // Generate a random main class number between 000 and 999.
                int mainClass = random.Next(0, 1000);

                // Generate a random decimal number between 0 and 999.
                int decimalPart = random.Next(0, 1000);

                // Combine the parts into a Dewey Decimal code formatted as XXX.XXX.
                string code = mainClass.ToString("D3") + "." + decimalPart.ToString("D3");

                // Return the generated Dewey Decimal code.
                return code;
            }
        }

        // IsMatchBasedOnDeweyDecimal checks if the 'selectedDescription' and 'selectedDeweyCode' match based on the dewey decimal system 
        public static bool IsMatchBasedOnDeweyDecimal(string selectedDescription, string selectedDeweyCode)
        {
            // Extract the first 3 digits from the selected Dewey code
            if (selectedDeweyCode.Length >= 3 && int.TryParse(selectedDeweyCode.Substring(0, 3), out int deweyNumericValue))
            {
                // Check each range for a match
                foreach (var range in deweyRanges.Keys)
                {
                    if (deweyNumericValue >= range.Item1 && deweyNumericValue <= range.Item2)
                    {
                        // Match found
                        return deweyRanges[range] == selectedDescription;
                    }
                }
            }

            // No match found
            return false;
        }
    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
