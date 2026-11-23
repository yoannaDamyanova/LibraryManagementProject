using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject
{
    public class TreeNode
    {
        public Book Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Book book)
        {
            Data = book;
            Left = Right = null;
        }
    }
}
