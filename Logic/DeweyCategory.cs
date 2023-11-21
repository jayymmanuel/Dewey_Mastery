/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dewey_Mastery.Logic
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    // This class represents a Dewey Decimal Classification category.
    public class DeweyCategory
    {
        // Property to store the Dewey Decimal Code associated with the category.
        public string Code { get; set; }

        // Property to store the label or description associated with the Dewey Decimal Code.
        public string Label { get; set; }

        // Property to store subcategories that belong to this category.
        public List<DeweyCategory> Subcategories { get; set; }

        // Additional property to store a description related to the category.
        public string Description { get; set; }
    }

    // This class represents a container for Dewey Decimal Classification data.
    public class DeweyData
    {
        // Property to store a list of DeweyCategory instances.
        public List<DeweyCategory> Categories { get; set; }
    }
}


/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/