/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dewey_Mastery.System_Features
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public class ListAdapter
    {
        #region Global Variables
        private ObservableCollection<string> description;
        private ObservableCollection<string> callNumber;
        #endregion

        // ListAdapter constructor initializes an instance of the ListAdapter class.
        // It takes two ObservableCollection<string> parameters to set the 'description' and 'callNumber' properties.
        public ListAdapter(ObservableCollection<string> description, ObservableCollection<string> callNumber)
        {
            // Set the 'description' property with the provided ObservableCollection<string>
            this.description = description;

            // Set the 'callNumber' property with the provided ObservableCollection<string>
            this.callNumber = callNumber;
        }


        // RemoveSelectedItems method removes items from the 'description' and 'callNumber' lists
        // based on the provided selectedDescription and selectedDeweyCode.
        public void RemoveSelectedItems(string selectedDescription, string selectedDeweyCode)
        {
            // Check if the 'description' list contains the selectedDescription
            if (description.Contains(selectedDescription))
            {
                // Remove the selectedDescription from the 'description' list
                description.Remove(selectedDescription);
            }

            // Check if the 'callNumber' list contains the selectedDeweyCode
            if (callNumber.Contains(selectedDeweyCode))
            {
                // Remove the selectedDeweyCode from the 'callNumber' list
                callNumber.Remove(selectedDeweyCode);
            }
        }

    }

}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
