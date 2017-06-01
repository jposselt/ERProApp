using System.Windows;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for NewRentalDialogView.xaml
    /// </summary>
    public partial class NewRentalDialogView : Window
    {
        private Customer _selectedCustomer;
        private Book _selectedBook;

        private CollectionViewSource _customers;
        private CollectionViewSource _books;

        public CollectionViewSource Books
        {
            get { return _books; }
            set { _books = value; }
        }

        public CollectionViewSource Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        public NewRentalDialogView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Ok" klickt
        /// </summary>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // Validiere Daten

            // Warnung bei Terminueberlappung

            // Erstelle neu Ausleihe

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
        /// Eventhandler zur Eingabe des ausleihenden Kunden
        /// </summary>
        private void btnCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearchView customerSearch = new CustomerSearchView();
            customerSearch.Owner = this;
            customerSearch.DataContext = _customers.View;
            customerSearch.Customers = _customers;
            customerSearch.ShowDialog();
        }

        /// <summary>
        /// Eventhandler zur Eingabe des auszuleihenden Buches
        /// </summary>
        private void btnItemSearch_Click(object sender, RoutedEventArgs e)
        {
            ItemSearchView bookSearch = new ItemSearchView();
            bookSearch.Owner = this;
            bookSearch.DataContext = _books.View;
            bookSearch.Books = _books;
            bookSearch.ShowDialog();
        }
    }
}
