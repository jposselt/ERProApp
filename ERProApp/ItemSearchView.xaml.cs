using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for ItemSearchView.xaml
    /// </summary>
    public partial class ItemSearchView : Window
    {
        private CollectionViewSource _books;

        public CollectionViewSource Customers
        {
            get { return _books; }
            set
            {
                _books = value;
                _books.View.Filter = _book_Filter;
            }
        }

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
                int y;
                if (Int32.TryParse(SearchBox.Text, out y))
                {
                    return book.Year.Equals(y);
                }
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
            Close();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Abbrechen" klickt
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Fenster schließen.
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
