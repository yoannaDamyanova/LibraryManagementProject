using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject
{
    public class BinarySearchTree
    {
        public TreeNode Root { get; private set; }


        public bool Insert(Book book)
        {
            Root = InsertRec(Root, book, out bool inserted);
            return inserted;
        }

        private TreeNode InsertRec(TreeNode root, Book book, out bool inserted)
        {
            if (root == null)
            {
                inserted = true;
                return new TreeNode(book);
            }

            if (book.ISBN < root.Data.ISBN)
            {
                root.Left = InsertRec(root.Left, book, out inserted);
            }
            else if (book.ISBN > root.Data.ISBN)
            {
                root.Right = InsertRec(root.Right, book, out inserted);
            }
            else
            {
                inserted = false;
            }

            return root;
        }


        public Book Search(int isbn)
        {
            TreeNode result = SearchRec(Root, isbn);
            return result?.Data;
        }

        private TreeNode SearchRec(TreeNode root, int isbn)
        {
            if (root == null || root.Data.ISBN == isbn)
                return root;

            if (root.Data.ISBN > isbn)
                return SearchRec(root.Left, isbn);

            return SearchRec(root.Right, isbn);
        }

        public void Delete(int isbn)
        {
            Root = DeleteRec(Root, isbn);
        }

        private TreeNode DeleteRec(TreeNode root, int isbn)
        {
            if (root == null) return root;

            if (isbn < root.Data.ISBN)
                root.Left = DeleteRec(root.Left, isbn);
            else if (isbn > root.Data.ISBN)
                root.Right = DeleteRec(root.Right, isbn);
            else
            {
                if (root.Left == null) return root.Right;
                else if (root.Right == null) return root.Left;

                root.Data = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Data.ISBN);
            }
            return root;
        }

        private Book MinValue(TreeNode root)
        {
            Book minv = root.Data;
            while (root.Left != null)
            {
                minv = root.Left.Data;
                root = root.Left;
            }
            return minv;
        }

        public void GetAll(TreeNode root, List<Book> list)
        {
            if (root != null)
            {
                GetAll(root.Left, list);
                list.Add(root.Data);
                GetAll(root.Right, list);
            }
        }
    }
}
