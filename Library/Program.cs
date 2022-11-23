using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

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
        const string MenuAddBook = "1";
        const string MenuRemoveBook = "2";
        const string MenuShowAllBooks = "3";
        const string MenuFindBook = "4";
        const string MenuExit = "0";
        const string MenuSearchById = "1";
        const string MenuSearchByAutor = "2";
        const string MenuSearchByTitle = "3";
        const string MenuSearchByGenre = "4";
        const string MenuSearchByYear = "5";

        bool isExit = false;
        string userInput;
        Library library = new Library();

        public void ShowMainMenu()
        {
            isExit = false;
            library.CreateSampleBooks();

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
                        library.AddBook();
                        break;

                    case MenuRemoveBook:
                        library.DeleteBook();
                        break;

                    case MenuShowAllBooks:
                        library.ShowAllRecords();
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

        public void ShowSearchMenu()
        {
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
                        library.SearchById();

                        break;
                    case MenuSearchByAutor:
                        library.SearchByAutor();
                        break;

                    case MenuSearchByTitle:
                        library.SearchByTitle();
                        break;

                    case MenuSearchByGenre:
                        library.SearchByGenre();
                        break;

                    case MenuSearchByYear:
                        library.SearchByYear();
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
        const int Id = 1;
        const int Autor = 2;
        const int Title = 3;
        const int Genre = 4;
        const int Year = 5;

        private List<string> _autors = new List<string>(new string[] { "Леонид Каганов", "Николай Глубокий", "Милослав Князев", "Денис Фонвизин", "Джером Клапка", "Фёдор Достоевский", "Джером Селинджер", "Александр Грибоедов" });
        private List<string> _titles = new List<string>(new string[] { "Зомби в СССР", "Проктология для любознательных", "Танкист победитель драконов", "Водоросоль", "Трое в лодке, нищета и собаки", "Преступление на Казани", "Над пропастью не ржи", "Горе о туман" });
        private List<string> _genres = new List<string>(new string[] { "Триллер", "Медицина", "Фентези", "Классика", "Классика", "Классика", "Мелодрама", "Поэзия" });
        private List<int> _years = new List<int>(new int[] { 1999, 2006, 2010, 1950, 1950, 1950, 1999, 1950 });

        private List<Book> _library = new List<Book>();
        private int _lastIndex;

        public void CreateSampleBooks()
        {
            for (int i = 0; i < _autors.Count; i++)
            {
                _library.Add(new Book(++ _lastIndex, _autors[i], _titles[i], _genres[i], _years[i]));
            }
        }

        public void AddBook()
        {
            ++ _lastIndex;
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
            if (_library.Remove(SearchById()))
            {
                Console.WriteLine("Книга удалена");
            }
        }

        public Book SearchById()
        {
            Book book;
            TryGetBook(out book, Id);
            BookInfo(book);
            return book;
        }

        public void SearchByAutor()
        {
            Book book;

            if(TryGetBook(out book, Autor))
            {
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Autor == book.Autor)
                    {
                        BookInfo(_library[i]);
                    }
                }
            }
        }

        public void SearchByTitle()
        {
            Book book;

            if (TryGetBook(out book, Title))
            {
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Title == book.Title)
                    {
                        BookInfo(_library[i]);
                    }
                }
            }
        }

        public void SearchByGenre()
        {
            Book book;

            if (TryGetBook(out book, Genre))
            {
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Genre == book.Genre)
                    {
                        BookInfo(_library[i]);
                    }
                }
            }
        }

        public void SearchByYear()
        {
            Book book;

            if (TryGetBook(out book, Year))
            {
                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Year == book.Year)
                    {
                        BookInfo(_library[i]);
                    }
                }
            }
        }

        private bool TryGetBook(out Book book, int searchId)
        {
            book = null;

            if (searchId == Id)
            {
                Console.WriteLine("Введите индекс книги:");
                int id = GetNumber();

                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Index == id)
                    {
                        book = _library[i];
                        Console.WriteLine("Книга найдена");
                        return true;
                    }
                }

                Console.WriteLine("Книги с таким индексом не найдено");
                return false;
            }
            else if (searchId == Autor)
            {
                Console.Write("Введите автора: ");
                string autor = Console.ReadLine();

                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Autor == autor)
                    {
                        book = _library[i];
                        Console.WriteLine("Книга найдена");
                        return true;
                    }
                }

                Console.WriteLine("Книг такого автора не найдено");
                return false;
            }
            else if (searchId == Title)
            {
                Console.Write("Введите название книги: ");
                string title = Console.ReadLine();

                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Autor == title)
                    {
                        book = _library[i];
                        Console.WriteLine("Книга найдена");
                        return true;
                    }
                }

                Console.WriteLine("Книги с таким названием не найдено");
                return false;
            }
            else if (searchId == Genre)
            {
                Console.Write("Введите жанр: ");
                string genre = Console.ReadLine();

                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Autor == genre)
                    {
                        book = _library[i];
                        Console.WriteLine("Книга найдена");
                        return true;
                    }
                }

                Console.WriteLine("Книг такого жанра не найдено");
                return false;
            }
            else if (searchId == Year)
            {
                Console.WriteLine("Введите год:");
                int year = GetNumber();

                for (int i = 0; i < _library.Count; i++)
                {
                    if (_library[i].Year == year)
                    {
                        book = _library[i];
                        Console.WriteLine("Книга найдена");
                        return true;
                    }
                }

                Console.WriteLine("Книг такого года не найдено");
                return false;
            }

            return false;
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
                foreach (var book in _library)
                {
                    BookInfo(book);
                }
            }
            else
            {
                Console.WriteLine("Книги отсутствуют");
            }
        }

        public void BookInfo(Book book)
        {
            Console.WriteLine($"Индекс: {book.Index} | Автор: {book.Autor} | Название: {book.Title} | Жанр: {book.Genre} | Год издания: {book.Year}");
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