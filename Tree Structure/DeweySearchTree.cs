/* -------------------------------------------------------------------------- Start of the code --------------------------------------------------------------------------*/
using Dewey_Mastery.Logic;
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
    // This class represents a binary search tree for organizing Dewey Decimal Classification categories.
    public class DeweySearchTree
    {
        // Property to store the root of the binary search tree.
        public DeweyNode Root { get; private set; }

        // Constructor for initializing the binary search tree with a list of Dewey categories.
        public DeweySearchTree(List<DeweyCategory> categories)
        {
            // Iterate through the provided categories and insert each one into the tree.
            foreach (var category in categories)
            {
                Root = Insert(Root, category.Code, category.Label);
            }
        }

        // Private method to insert a DeweyNode into the binary search tree.
        // Follows the principles of binary search, placing nodes with lesser values on the left and greater values on the right.
        // Returns the root of the modified tree.
        private DeweyNode Insert(DeweyNode root, string code, string label)
        {
            // If the current root is null, create a new DeweyNode with the given code and label.
            if (root == null)
            {
                return new DeweyNode { Code = code, Label = label };
            }

            // If the given code is less than the code of the current root, recursively insert into the left subtree.
            if (String.Compare(code, root.Code) < 0)
            {
                root.Left = Insert(root.Left, code, label);
            }
            // If the given code is greater than the code of the current root, recursively insert into the right subtree.
            else if (String.Compare(code, root.Code) > 0)
            {
                root.Right = Insert(root.Right, code, label);
            }

            // Return the root of the modified tree.
            return root;
        }
    }


}
/* -------------------------------------------------------------------------- End of the code --------------------------------------------------------------------------*/