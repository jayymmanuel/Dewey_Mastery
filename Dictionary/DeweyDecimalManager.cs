/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dewey_Mastery.Dictionary
{
/**
*
* @studentName EmmanuelKianda
* @studentNumber 10081944
* @PROG7312
* @POE
*/
    public static class DeweyDecimalManager
    {
        // Declaring a Dictionary object that stores the Descriptions and their corresponding Ranges
        public static Dictionary<Tuple<int, int>, string> DeweyRanges { get; } = new Dictionary<Tuple<int, int>, string>
        {
            { Tuple.Create(0, 99), "General Knowledge" },
            { Tuple.Create(100, 199), "Philosophy & Psychology" },
            { Tuple.Create(200, 299), "Religion" },
            { Tuple.Create(300, 399), "Social Sciences" },
            { Tuple.Create(400, 499), "Language" },
            { Tuple.Create(500, 599), "Science" },
            { Tuple.Create(600, 699), "Technology" },
            { Tuple.Create(700, 799), "Arts & Recreation" },
            { Tuple.Create(800, 899), "Literature" },
            { Tuple.Create(900, 999), "History & Geography" },
        };
    }
}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
