/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dewey_Mastery.Logic
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public class FetchLevel
    {
        #region Global variables
        private static List<DeweyCategory> categories;
        private static DeweyCategory currentQuestion;  
        private static Random random = new Random();
        #endregion

        // LoadDeweyData method reads Dewey category data from a file and initializes the 'categories' list.
        public static void LoadDeweyData()
        {
            try
            {
                // Construct the file path to the Dewey data file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "DeweyTopLevelMultList.txt");

                // Read the JSON data from the file
                string jsonData = File.ReadAllText(filePath);

                // Deserialize the JSON data into a list of DeweyCategory objects
                categories = JsonConvert.DeserializeObject<List<DeweyCategory>>(jsonData);

                // Check if the 'categories' list is null or empty
                if (categories == null || categories.Count == 0)
                {
                    // Display a message if no Dewey categories were loaded
                    MessageBox.Show("No Dewey categories loaded.");
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs during the data loading process
                MessageBox.Show($"An error occurred while loading Dewey data: {ex.Message}");
            }
        }


        // GetRandomCategoryAtLevel method retrieves a random DeweyCategory at the specified target level.
        // This method uses the GetCategoriesAtLevel method to obtain categories at the target level,
        // then selects a random category from the obtained list.
        public static DeweyCategory GetRandomCategoryAtLevel(int level)
        {
            // Get a list of categories at the specified level using the GetCategoriesAtLevel method
            List<DeweyCategory> categoriesAtLevel = GetCategoriesAtLevel(categories, level);

            // Check if categories were found at the specified level
            if (categoriesAtLevel != null && categoriesAtLevel.Count > 0)
            {
                // Select a random category from the obtained list
                return categoriesAtLevel[random.Next(categoriesAtLevel.Count)];
            }
            else
            {
                // Display a message if no categories were found at the specified level
                MessageBox.Show($"No categories found at level {level}.");

                // Return null as there are no categories to return
                return null;
            }
        }


        // GetCategoriesAtLevel method retrieves DeweyCategories at a specified target level.
        // This method uses recursion to explore categories and populates the 'result' list with categories found at the target level.
        public static List<DeweyCategory> GetCategoriesAtLevel(List<DeweyCategory> categories, int level)
        {
            // Initialize a list to store categories found at the target level
            List<DeweyCategory> result = new List<DeweyCategory>();

            // Iterate through each category in the provided list
            foreach (var category in categories)
            {
                // Recursively call the GetCategoriesAtLevelRecursive method to explore categories and find those at the target level
                GetCategoriesAtLevelRecursive(category, level, result);
            }

            // Return the list of categories found at the target level
            return result;
        }


        // GetCategoriesAtLevelRecursive method recursively retrieves DeweyCategories at a specified target level.
        // This method populates the 'result' list with categories found at the target level.
        public static void GetCategoriesAtLevelRecursive(DeweyCategory category, int targetLevel, List<DeweyCategory> result, int currentLevel = 1)
        {
            // Check if the current category is at the target level
            if (currentLevel == targetLevel)
            {
                // Add the current category to the result list
                result.Add(category);
            }
            // If the current category has subcategories, recursively call the method for each subcategory
            else if (category.Subcategories != null)
            {
                foreach (var subcategory in category.Subcategories)
                {
                    // Recursive call to explore subcategories and find categories at the target level
                    GetCategoriesAtLevelRecursive(subcategory, targetLevel, result, currentLevel + 1);
                }
            }
        }

        // GetTopLevelCategories method returns a list of top-level DeweyCategories.
        public static List<DeweyCategory> GetTopLevelCategories()
        {
            // Filter categories to include only those with subcategories
            // (i.e., top-level categories that have child categories)
            return categories.Where(c => c.Subcategories != null && c.Subcategories.Count > 0).ToList();
        }


        // GetDeweyLabelByCode method returns the Dewey label for a given code.
        public static string GetDeweyLabelByCode(int code)
        {
            // Find the DeweyCategory with the specified code
            DeweyCategory category = categories.FirstOrDefault(c => c.Code.StartsWith(code.ToString()));

            // Return the label if found, otherwise return an empty string
            return category != null ? category.Label : "";
        }

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/