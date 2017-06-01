using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ERProApp
{
    /// <summary>
    /// Interaction logic for ItemSearchView.xaml
    /// </summary>
    public partial class ItemSearchView : Window
    {
        private CollectionViewSource _books;

        public CollectionViewSource Books
        {
            get { return _books; }
            set
            {
                _books = value;
                _books.View.Filter = _book_Filter;
            }
        }

        public Book SelectedBook => ItemData.SelectedItem as Book;

        public ItemSearchView()
        {
            InitializeComponent();
        }

        // Suchfilter mit verschiedenen Kategorien
        private bool _book_Filter(object item)
        {
            Book book = item as Book;

            if (searchCategory.Text.Equals("Titel"))
            {
                return book.Title.Contains(SearchBox.Text);
            }

            if (searchCategory.Text.Equals("Autor"))
            {
                return book.Author.Contains(SearchBox.Text);
            }

            if (searchCategory.Text.Equals("Genre"))
            {
                return book.Genre.Contains(SearchBox.Text);
            }

            if (searchCategory.Text.Equals("Jahr"))
            {
                return Convert.ToString(book.Year).Contains(SearchBox.Text);
            }

            return true;
        }

        // Eventhandler wenn sich der Suchtext aendert
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _books.View.Refresh();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Ok" klickt
        /// </summary>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // Fenster schließen.
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Abbrechen" klickt
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Fenster schließen.
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// Eventhandler wenn sich die Suchkatergorie geaendert hat
        /// </summary>
        private void searchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Funktioniert noch nicht richtig

            /*
            if (_books != null)
            {
                //_customers.View.Refresh();
            }
            */
        }
    }
}
