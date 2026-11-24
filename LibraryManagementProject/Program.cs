using LibraryManagementProject;

namespace LibraryProject
{
    class Program
    {
        static BinarySearchTree bst = new BinarySearchTree();

        static void Main(string[] args)
        {
            LoadInitialData();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Library Management System ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. Edit Book");
                Console.WriteLine("4. Display All Books");
                Console.WriteLine("5. Search by ISBN (BST Search)");
                Console.WriteLine("6. Sort and Display (by Year)");
                Console.WriteLine("7. Sort and Display (by Title)");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": DeleteBook(); break;
                    case "3": EditBook(); break;
                    case "4": PrintAll(); break;
                    case "5": SearchBook(); break;
                    case "6": SortAndPrint(compareByYear: true); break;
                    case "7": SortAndPrint(compareByYear: false); break;
                    case "8": exit = true; break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        // Метод за зареждане на начални данни в дървото
        static void LoadInitialData()
        {
            // Добавяме няколко книги в дървото за демонстрация
            bst.Insert(new Book { ISBN = 1005, Title = "Algorithms", Author = "Ivanov", Year = 2020 });
            bst.Insert(new Book { ISBN = 1002, Title = "Data Structures", Author = "Petrov", Year = 2018 });
            bst.Insert(new Book { ISBN = 1008, Title = "C# Programming", Author = "Georgiev", Year = 2021 });
            bst.Insert(new Book { ISBN = 1001, Title = "Databases", Author = "Dimitrov", Year = 2019 });
        }

        
        static void AddBook()
        {
            Console.Write("ISBN: ");
            int isbn = int.Parse(Console.ReadLine());

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());

            Book book = new Book
            {
                ISBN = isbn,
                Title = title,
                Author = author,
                Year = year
            };

            // Опитваме се да добавим книгата в дървото и проверяваме дали е успешно
            if (bst.Insert(book))
            {
                Console.WriteLine("Book added successfully.");
            }
            else
            {
                // Ако ISBN вече съществува, показваме съобщение за грешка
                Console.WriteLine("Error: A book with this ISBN already exists.");
            }
        }

        static void DeleteBook()
        {
            Console.Write("Enter ISBN of the book to delete: ");
            int isbn = int.Parse(Console.ReadLine());

            bst.Delete(isbn);
            Console.WriteLine("If the book existed, it has been deleted.");
        }

        static void EditBook()
        {
            Console.Write("Enter ISBN of the book to edit: ");
            int isbn = int.Parse(Console.ReadLine());

            // Използваме метода Search на BST, за да намерим книгата по ISBN
            Book book = bst.Search(isbn);

            if (book != null)
            {
                Console.Write("New title: ");
                book.Title = Console.ReadLine();

                Console.Write("New author: ");
                book.Author = Console.ReadLine();

                Console.Write("New year: ");
                book.Year = int.Parse(Console.ReadLine());

                Console.WriteLine("Book updated successfully.");
            }
            else
            {
                // Ако книгата не е намерена, показваме съобщение за грешка
                Console.WriteLine("Book not found.");
            }
        }

        static void PrintAll()
        {
            List<Book> books = new List<Book>(); // създаваме списък, в който ще съхраним всички книги от дървото
            bst.GetAll(bst.Root, books); // извикваме метода GetAll, който обхожда дървото и добавя всички книги в списъка

            foreach (var b in books)
                Console.WriteLine(b);
        }

        static void SearchBook()
        {
            Console.Write("Enter ISBN to search: ");
            int isbn = int.Parse(Console.ReadLine());

            Book result = bst.Search(isbn); // използваме метода Search на BST, за да намерим книгата по ISBN

            if (result != null)
                Console.WriteLine("Book found: " + result);
            else
                Console.WriteLine("Book not found.");
        }

        static void SortAndPrint(bool compareByYear)
        {
            List<Book> books = new List<Book>(); // създаваме списък, в който ще съхраним всички книги от дървото
            bst.GetAll(bst.Root, books); // извикваме метода GetAll, който обхожда дървото и добавя всички книги в списъка

            Book[] arr = books.ToArray(); // преобразуваме списъка в масив, за да можем да използваме QuickSort алгоритъма

            // извикваме QuickSort, като подаваме масива, началния и крайния индекс, както и флага за сравнение по година или заглавие
            // compareByYear = true => сортираме по година
            // compareByYear = false => сортираме по заглавие
            QuickSort(arr, 0, arr.Length - 1, compareByYear);

            foreach (var b in arr)
                Console.WriteLine(b);
        }

        //QuickSort – използваме го за сортиране на книгите по година или заглавие.
        //Devide and conquer – разделяме масива на по-малки части, сортираме ги и след това ги обединяваме.
        //1. Избира опорен елемент(pivot).

        //2. Разделя масива на две части.

        //3. Поставя pivot на правилната му позиция.

        //4. Сортира рекурсивно двете части.
        static void QuickSort(Book[] arr, int left, int right, bool compareByYear)
        {
            if (left < right) // ако има повече от един елемент в масива, продължаваме със сортирането
            {
                // извикваме метода Partition, който разделя масива на две части и връща индекса на опорния елемент
                int pivot = Partition(arr, left, right, compareByYear); 

                QuickSort(arr, left, pivot - 1, compareByYear); // рекурсивно сортираме лявата част на масива
                QuickSort(arr, pivot + 1, right, compareByYear); // рекурсивно сортираме дясната част на масива
            }
        }

        // Методът Partition разделя масива на две части спрямо опорния елемент (pivot)
        static int Partition(Book[] arr, int left, int right, bool compareByYear)
        {
            // Избираме последния елемент от текущия диапазон за pivot.
            Book pivotValue = arr[right];
            int i = left - 1;

            // Обхождаме елементите от left до right-1 и сравняваме всеки елемент с pivotValue.
            for (int j = left; j < right; j++)
            {
                // В зависимост от флага compareByYear, сравняваме по година или по заглавие.
                bool condition = compareByYear
                    ? arr[j].Year < pivotValue.Year
                    : string.Compare(arr[j].Title, pivotValue.Title) < 0;

                if (condition) //Ако условието е изпълнено, разменяме елементите така, че по-малките да се преместят вляво.
                {
                    i++;

                    var temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // След като сме преместили всички по-малки елементи вляво, разменяме pivotValue с елемента на позиция i+1.
            var swapTemp = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = swapTemp;

            return i + 1;
        }
    }
}
