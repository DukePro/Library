namespace MyProgramm
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMainMenu();
        }
    }

    class Menu
    {
        private const string MenuAddBook = "1";
        private const string MenuRemoveBook = "2";
        private const string MenuShowAllBooks = "3";
        private const string MenuFindBook = "4";
        private const string MenuExit = "0";
        private const string MenuSearchById = "1";
        private const string MenuSearchByAutor = "2";
        private const string MenuSearchByTitle = "3";
        private const string MenuSearchByGenre = "4";
        private const string MenuSearchByYear = "5";

        private Library _library = new Library();

        public void ShowMainMenu()
        {
            bool isExit = false;
            string userInput;

            _library.CreateSampleBooks();

            while (isExit == false)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine(MenuAddBook + " - Добавить книгу");
                Console.WriteLine(MenuRemoveBook + " - Удалить книгу");
                Console.WriteLine(MenuShowAllBooks + " - Показать все книги");
                Console.WriteLine(MenuFindBook + " - Поиск книги по параметрам");
                Console.WriteLine(MenuExit + " - Выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuAddBook:
                        _library.AddBook();
                        break;

                    case MenuRemoveBook:
                        _library.DeleteBook();
                        break;

                    case MenuShowAllBooks:
                        _library.ShowAllRecords();
                        break;

                    case MenuFindBook:
                        ShowSearchMenu();
                        break;

                    case MenuExit:
                        isExit = true;
                        break;
                }
            }
        }

        private void ShowSearchMenu()
        {
            string userInput;
            bool isSearchExit = false;
            Book book;

            while (isSearchExit == false)
            {
                Console.WriteLine("\nВыберите параметр поиска:");
                Console.WriteLine(MenuSearchById + " - Индекс");
                Console.WriteLine(MenuSearchByAutor + " - Автор");
                Console.WriteLine(MenuSearchByTitle + " - Название книги");
                Console.WriteLine(MenuSearchByGenre + " - Жанр");
                Console.WriteLine(MenuSearchByYear + " - Год");
                Console.WriteLine(MenuExit + " - Назад");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuSearchById:
                        _library.GetBookId(out book);

                        break;
                    case MenuSearchByAutor:
                        _library.GetBookAutor(out book);
                        break;

                    case MenuSearchByTitle:
                        _library.GetBookTitle(out book);
                        break;

                    case MenuSearchByGenre:
                        _library.GetBookGenre(out book);
                        break;

                    case MenuSearchByYear:
                        _library.GetBookYear(out book);
                        break;

                    case MenuExit:
                        isSearchExit = true;
                        break;
                }
            }
        }
    }

    class Library
    {
        public const int BookId = 1;
        public const int Autor = 2;
        public const int Title = 3;
        public const int Genre = 4;
        public const int Year = 5;

        private List<Book> _library = new List<Book>();

        private int _lastIndex;

        public void CreateSampleBooks()
        {
            List<string> autors = new List<string>(new string[] { "Леонид Каганов", "Николай Глубокий", "Милослав Князев", "Денис Фонвизин", "Джером Клапка", "Фёдор Достоевский", "Джером Селинджер", "Александр Грибоедов" });
            List<string> titles = new List<string>(new string[] { "Зомби в СССР", "Проктология для любознательных", "Танкист победитель драконов", "Водоросоль", "Трое в лодке, нищета и собаки", "Преступление на Казани", "Над пропастью не ржи", "Горе о туман" });
            List<string> genres = new List<string>(new string[] { "Триллер", "Медицина", "Фентези", "Классика", "Классика", "Классика", "Мелодрама", "Поэзия" });
            List<int> years = new List<int>(new int[] { 1999, 2006, 2010, 1950, 1950, 1950, 1999, 1950 });

            for (int i = 0; i < autors.Count; i++)
            {
                _library.Add(new Book(++_lastIndex, autors[i], titles[i], genres[i], years[i]));
            }
        }

        public void AddBook()
        {
            ++_lastIndex;

            Console.Write("Введите автора: ");
            string autor = Console.ReadLine();
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            Console.Write("Введите жанр: ");
            string genre = Console.ReadLine();
            Console.Write("Введите год издания: ");
            int year = GetNumber();

            Book book = new Book(_lastIndex, autor, title, genre, year);
            _library.Add(book);
        }

        public void DeleteBook()
        {
            Book book;

            if (GetBookId(out book))
            {
                _library.Remove(book);
                Console.WriteLine("Книга удалена");
            }
        }

        public bool GetBookId(out Book book)
        {
            book = null;

            Console.WriteLine("Введите индекс книги:");
            int id = GetNumber();

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Index == id)
                {
                    book = _library[i];
                    ShowBookInfo(book);
                    return true;
                }
            }

            Console.WriteLine("Книги с таким индексом не найдено");
            return false;
        }

        public bool GetBookAutor(out Book book)
        {
            book = null;

            bool isFound = false;

            Console.Write("Введите автора: ");
            string autor = Console.ReadLine().ToLower();

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Autor.ToLower() == autor)
                {
                    book = _library[i];
                    Console.WriteLine("Книга найдена");
                    ShowBookInfo(_library[i]);
                    isFound = true;
                }
            }

            if (isFound == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Книг такого автора не найдено");
                return false;
            }
        }

        public bool GetBookTitle(out Book book)
        {
            book = null;

            bool isFound = false;

            Console.Write("Введите название книги: ");
            string title = Console.ReadLine().ToLower();

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Title.ToLower() == title)
                {
                    book = _library[i];
                    Console.WriteLine("Книга найдена");
                    ShowBookInfo(_library[i]);
                    isFound = true;
                }
            }

            if (isFound == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Книги с таким названием не найдено");
                return false;
            }
        }

        public bool GetBookGenre(out Book book)
        {
            book = null;

            bool isFound = false;

            Console.Write("Введите жанр: ");
            string genre = Console.ReadLine().ToLower();

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Genre.ToLower() == genre)
                {
                    book = _library[i];
                    ShowBookInfo(_library[i]);
                    isFound = true;
                }
            }

            if (isFound == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Книг такого жанра не найдено");
                return false;
            }
        }

        public bool GetBookYear(out Book book)
        {
            book = null;

            bool isFound = false;

            Console.WriteLine("Введите год:");
            int year = GetNumber();

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Year == year)
                {
                    book = _library[i];
                    ShowBookInfo(_library[i]);
                    isFound = true;
                }
            }

            if (isFound == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Книг такого года не найдено");
                return false;
            }
        }

        private int GetNumber()
        {
            int parsedNumber = 0;

            bool isParsed = false;

            while (isParsed == false)
            {
                string userInput = Console.ReadLine();
                isParsed = int.TryParse(userInput, out parsedNumber);

                if (isParsed == false)
                {
                    Console.WriteLine("Введите целое число:");
                }
            }

            return parsedNumber;
        }

        public void ShowAllRecords()
        {
            if (_library.Count > 0)
            {
                foreach (Book book in _library)
                {
                    ShowBookInfo(book);
                }
            }
            else
            {
                Console.WriteLine("Книги отсутствуют");
            }
        }

        private void ShowBookInfo(Book book)
        {
            if (book != null)
            {
                Console.WriteLine($"Индекс: {book.Index} | Автор: {book.Autor} | Название: {book.Title} | Жанр: {book.Genre} | Год издания: {book.Year}");
            }
        }
    }

    class Book
    {
        public int Index { get; private set; }
        public string Autor { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public int Year { get; private set; }

        public Book(int index, string autor, string title, string genre, int year)
        {
            Index = index;
            Autor = autor;
            Title = title;
            Genre = genre;
            Year = year;
        }
    }
}