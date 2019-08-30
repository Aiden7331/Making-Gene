using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Tetris
{
    class TreeNode
    {
        public char Data;
        public TreeNode Parent;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(char data,TreeNode L,TreeNode R,TreeNode P)
        {
            Data = data;
            Left = L;
            Right = R;
            Parent = P;
        }
        
    }
}
