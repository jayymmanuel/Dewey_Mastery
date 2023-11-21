/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dewey_Mastery.Logic
{
/**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    public class IsAscendingOrder
    {

        // IsDeweyCodeInAscendingOrder method checks if two Dewey codes are in ascending order.
        public static bool IsDeweyCodeInAscendingOrder(string deweyCode1, string deweyCode2)
        {
            // Split Dewey codes into main class numbers and decimal parts
            string[] parts1 = deweyCode1.Split('.');
            string[] parts2 = deweyCode2.Split('.');

            // Check if Dewey code format is valid
            if (parts1.Length != 2 || parts2.Length != 2)
            {
                // Invalid Dewey code format
                return false;
            }

            // Parse main class numbers and decimal parts
            if (!int.TryParse(parts1[0], out int mainClass1) || !int.TryParse(parts2[0], out int mainClass2))
            {
                // Invalid main class number
                return false;
            }

            if (!int.TryParse(parts1[1], out int decimalPart1) || !int.TryParse(parts2[1], out int decimalPart2))
            {
                // Invalid decimal part
                return false;
            }

            // Check if both main class numbers and decimal parts are in ascending order
            if (mainClass1 < mainClass2 || (mainClass1 == mainClass2 && decimalPart1 <= decimalPart2))
            {
                return true;
            }

            return false;
        }

        // IsAuthorInitialsAscending method checks if two sets of author initials are in ascending order.
        public static bool IsAuthorInitialsAscending(string initials1, string initials2)
        {
            // Use string.Compare to compare the two sets of initials
            // and check if they are in ascending order or equal
            return string.Compare(initials1, initials2) <= 0;
        }

    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
