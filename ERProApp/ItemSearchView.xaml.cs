using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for ItemSearchView.xaml
    /// </summary>
    public partial class ItemSearchView : Window
    {
        private CollectionViewSource _books;

        /// <summary>
        /// Quelle fuer Databinding
        /// </summary>
        public CollectionViewSource Books
        {
            get { return _books; }
            set
            {
                _books = value;
                _books.View.Filter = _book_Filter;
            }
        }

        /// <summary>
        /// Dient als Rueckgabewert des Suchfensters
        /// </summary>
        public Book SelectedBook => ItemData.SelectedItem as Book;

        /// <summary>
        /// StandardKonstruktor
        /// </summary>
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

        
        // Eventhandler wenn der Benutzer auf "Ok" klickt
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // Fenster schließen.
            DialogResult = true;
            Close();
        }

      
        // Eventhandler wenn der Benutzer auf "Abbrechen" klickt
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Fenster schließen.
            DialogResult = false;
            Close();
        }

        // Eventhandler wenn sich die Suchkatergorie geaendert hat
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

        // Eventhandler zum sperren eines Buchs
        private void context_BlockItem(object sender, RoutedEventArgs e)
        {
            if (ItemData.SelectedItem != null)
            {
                (ItemData.SelectedItem as Book).Blocked = true;
                (ItemData.SelectedItem as Book).Status = "gesperrt";
                ItemData.InvalidateVisual();
            }
        }

        // Eventhandler zum entsperren eines Buchs
        private void context_UnblockItem(object sender, RoutedEventArgs e)
        {
            if (ItemData.SelectedItem != null)
            {
                (ItemData.SelectedItem as Book).Blocked = false;
                if ((ItemData.SelectedItem as Book).ReservationCount > 0)
                    (ItemData.SelectedItem as Book).Status = "reserviert";
                else
                    (ItemData.SelectedItem as Book).Status = "verfügbar";

                foreach(Rental r in DataController.Rentals)
                {
                    if (r.ItemID == (ItemData.SelectedItem as Book).ID && !r.Reservation)
                    {
                        (ItemData.SelectedItem as Book).Status = "ausgeliehen";
                    }
                }

                ItemData.InvalidateVisual();
            }
        }

        #region Commands

        /// <summary>
        /// Sperrt Ausleihen des ausgewaehlten Buchs
        /// </summary>
        public static readonly RoutedCommand Block = new RoutedCommand();

        private void BlockCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ItemData != null && ItemData.SelectedItem != null)
            {
                if ((ItemData.SelectedItem as Book).Blocked)
                    e.CanExecute = false;
                else
                    e.CanExecute = true;
            }
            else
                e.CanExecute = false;

            e.Handled = true;
        }

        private void BlockExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (ItemData != null && ItemData.SelectedItem != null)
            {
                // Setze Feld
                (ItemData.SelectedItem as Book).Blocked = true;

                // Update Status
                (ItemData.SelectedItem as Book).Status = "gesperrt";

                // Update Window
                ItemData.InvalidateVisual();
            }
            e.Handled = true;
        }

        /// <summary>
        /// Entsperrt Ausleihen des ausgewaehlten Buchs
        /// </summary>
        public static readonly RoutedCommand Unblock = new RoutedCommand();

        private void UnblockCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ItemData != null && ItemData.SelectedItem != null)
            {
                if ((ItemData.SelectedItem as Book).Blocked)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            else
                e.CanExecute = false;

            e.Handled = true;
        }

        private void UnblockExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (ItemData != null && ItemData.SelectedItem != null)
            {
                (ItemData.SelectedItem as Book).Blocked = false;
                if ((ItemData.SelectedItem as Book).ReservationCount > 0)
                    (ItemData.SelectedItem as Book).Status = "reserviert";
                else
                    (ItemData.SelectedItem as Book).Status = "verfügbar";

                foreach (Rental r in DataController.Rentals)
                {
                    if (r.ItemID == (ItemData.SelectedItem as Book).ID && !r.Reservation)
                    {
                        (ItemData.SelectedItem as Book).Status = "ausgeliehen";
                    }
                }

                ItemData.InvalidateVisual();
            }
            e.Handled = true;
        }

        #endregion
    }
}
