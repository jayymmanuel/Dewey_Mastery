/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dewey_Mastery.Tree_Structure
{
    /**
    *
    * @studentName EmmanuelKianda
    * @studentNumber 10081944
    * @PROG7312
    * @POE
*/
    // This class represents a node in a tree structure used for organizing information using Dewey Decimal Classification.
    public class DeweyNode
    {
        // Property to store the Dewey Decimal Code associated with the node.
        public string Code { get; set; }

        // Property to store the label or description associated with the Dewey Decimal Code.
        public string Label { get; set; }

        // Reference to the left child node in the tree.
        public DeweyNode Left { get; set; }

        // Reference to the right child node in the tree.
        public DeweyNode Right { get; set; }
    }

}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/
