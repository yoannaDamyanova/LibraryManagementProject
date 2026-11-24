//Binary Search Tree (BST) – използваме го за съхранение, търсене, добавяне и изтриване на книги.

//Двоичното дърво е структура от данни, в която всеки възел може да има максимум две деца:
//- ляво дете;
//- дясно дете.

//При Binary Search Tree имаме допълнително правило:
//- Всички стойности в лявото поддърво са по-малки от текущия възел.
//- Всички стойности в дясното поддърво са по-големи от текущия възел.

//Сравняваме ISBN номерата.

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
            if (root == null) // достигнали сме празна позиция в дървото => добавяме новия възел
            {
                inserted = true;
                return new TreeNode(book);
            }

            if (book.ISBN < root.Data.ISBN) // ако ISBN на новата книга е по-малко от ISBN на текущия възел, продължаваме в лявото поддърво
            {
                root.Left = InsertRec(root.Left, book, out inserted);
            }
            else if (book.ISBN > root.Data.ISBN) // ако ISBN на новата книга е по-голямо от ISBN на текущия възел, продължаваме в дясното поддърво
            {
                root.Right = InsertRec(root.Right, book, out inserted);
            }
            else
            {
                // ISBN вече съществува в дървото, не добавяме нов възел
                inserted = false;
            }

            return root;
        }


        public Book Search(int isbn)
        {
            TreeNode result = SearchRec(Root, isbn);
            return result?.Data; // връщаме данните на намерения възел или null, ако не е намерен
        }

        private TreeNode SearchRec(TreeNode root, int isbn)
        {
            if (root == null || root.Data.ISBN == isbn) // ако достигнем празен възел или намерим ISBN, връщаме текущия възел
                return root;

            if (root.Data.ISBN > isbn) // ако ISBN на текущия възел е по-голямо от търсеното ISBN, продължаваме в лявото поддърво
                return SearchRec(root.Left, isbn);

            return SearchRec(root.Right, isbn); // ако ISBN на текущия възел е по-малко от търсеното ISBN, продължаваме в дясното поддърво
        }

        public void Delete(int isbn)
        {
            Root = DeleteRec(Root, isbn); // извикваме рекурсивната функция за изтриване и обновяваме корена на дървото
        }


        // Случай 1: Възелът няма деца (листен възел) – просто го премахваме.
        // Случай 2: Възелът има едно дете – премахваме възела и свързваме родителя му с детето му.
        // Случай 3: Възелът има две деца – намираме най-малкия възел в дясното поддърво (или най-големия в лявото поддърво),
        // копираме стойността му в текущия възел и изтриваме този най-малък възел.
        private TreeNode DeleteRec(TreeNode root, int isbn)
        {
            if (root == null) return root;

            if (isbn < root.Data.ISBN) // ако ISBN на текущия възел е по-голямо от търсеното ISBN, продължаваме в лявото поддърво
                root.Left = DeleteRec(root.Left, isbn);
            else if (isbn > root.Data.ISBN) // ако ISBN на текущия възел е по-малко от търсеното ISBN, продължаваме в дясното поддърво
                root.Right = DeleteRec(root.Right, isbn);
            else
            {
                // Възелът е намерен, изтриваме го според случаите
                if (root.Left == null) return root.Right; // случай 1 и 2: няма дете или има само дясно дете
                else if (root.Right == null) return root.Left; // случай 2: има само ляво дете

                // случай 3: има две деца, намираме най-малкия възел в дясното поддърво
                root.Data = MinValue(root.Right);
                // изтриваме най-малкия възел в дясното поддърво
                root.Right = DeleteRec(root.Right, root.Data.ISBN);
            }
            return root;
        }

        // Връща най-малката стойност (най-ляв възел) в дадено поддърво
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

        // Метод за извличане на всички книги от дървото в списък
        //      1005
        //     /    \
        //  1002    1008
        //  /
        //1001
        //1001 → 1002 → 1005 → 1008
        public void GetAll(TreeNode root, List<Book> list)
        {
            if (root != null)
            {
                GetAll(root.Left, list); // рекурсивно обхождаме лявото поддърво
                list.Add(root.Data); // добавяме данните на текущия възел в списъка
                GetAll(root.Right, list); // рекурсивно обхождаме дясното поддърво
            }
        }
    }
}
