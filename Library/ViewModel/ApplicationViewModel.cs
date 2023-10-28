using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Library.Model;

namespace Library
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Users? selectedUser;
        private Books? selectedBook;
        private RelayCommand? addUserCommand;
        private RelayCommand? removeUserCommand;
        private RelayCommand? addBookCommand;
        private RelayCommand? removeBookCommand;

        public ObservableCollection<Users> User { get; set; }
        public ObservableCollection<Books> Book { get; set; }


        public Users? SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged("SelectedUser"); }
        }
        public Books? SelectedBooks
        {
            get { return selectedBook; }
            set { selectedBook = value; OnPropertyChanged("SelectedBooks"); }
        }

        public ApplicationViewModel()
        {
            User = new ObservableCollection<Users>()
            {
                new Users{ID = 1,Name = "Александр",Family = "Демин", UserBooks = new List<Books>() }
            };
            Book = new ObservableCollection<Books>()
            {
               new Books{Autor = "Пушкин А. С.",Acr = 1,Age =  new DateOnly(2022, 1, 24), Count = 10 },
            };

        }

        public RelayCommand AddUserCommand
        {
            get
            {
                return addUserCommand ??
                    (addUserCommand = new RelayCommand(obj =>
                    {
                        Users user = new Users();
                        User.Add(user);
                        SelectedUser = user;
                    }));
            }
        }
        public RelayCommand RemoveUserCommand
        {
            get
            {
                return removeUserCommand ??
                    (removeUserCommand = new RelayCommand(obj =>
                    {
                        Users? user = obj as Users;
                        if (user != null)
                        {
                            User.Remove(user);

                        }
                    },
                    (obj) => User.Count > 0));
            }
        }
        public RelayCommand AddBooksCommand
        {
            get
            {
                return addBookCommand ??
                    (addBookCommand = new RelayCommand(obj =>
                    {
                        Books books = new Books();
                        Book.Add(books);
                        SelectedBooks = books;
                    }));

            }
        }
        public RelayCommand RemoveBooksCommand
        {
            get
            {
                return removeBookCommand ??
                    (removeBookCommand = new RelayCommand(obj =>
                    {
                        Books? book = obj as Books;
                        if (book != null)
                        {
                            Book.Remove(book);
                            Console.WriteLine("Книга удалена: " + book.Autor);
                        }
                    },
                    (obj) => Book.Count > 0));
            }
        }
        public RelayCommand AddBookToUserCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedUser != null && SelectedBooks != null)
                    {
                        if (SelectedBooks.Count > 0)
                        {
                            SelectedBooks.Count--;

                            Books addedBook = new Books
                            {
                                Autor = SelectedBooks.Autor,
                                Acr = SelectedBooks.Acr,
                                Age = SelectedBooks.Age,
                                Count = 1
                            };

                            SelectedUser.UserBooks.Add(addedBook);
                            MessageBox.Show("Книга выдана пользователю");
                            if (SelectedBooks.Count == 0)
                            {
                                MessageBox.Show("Книги закончились");
                                Book.Remove(SelectedBooks);
                            }
                        }

                    }
                });

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
