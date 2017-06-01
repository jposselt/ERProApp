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
            if(_selectedCustomer == null)
            {
                // Nachricht: keine Daten
                MessageBox.Show("Es wurde kein Kunde ausgewählt","Fehlende Daten", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_selectedBook == null)
            {
                // Nachricht: keine Daten
                MessageBox.Show("Es wurde kein Buch ausgewählt", "Fehlende Daten", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (StartDate.SelectedDate == null || EndDate.SelectedDate == null)
            {
                // Nachricht: keine Daten
                MessageBox.Show("Es wurde kein Start-/Enddatum ausgewählt", "Fehlende Daten", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else {
                if(DataController.TimeOverlapCheck(_selectedBook, StartDate.SelectedDate.Value, EndDate.SelectedDate.Value))
                {
                    // Nachricht: Terminüberlappung
                    MessageBox.Show("Der eingegebene Zeitraum überlappt mit einer anderen Ausleihe/Reservierung", "Terminüberlappung", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            // Erstelle neu Ausleihe
            DataController.AddRental(new Rental(_selectedCustomer, _selectedBook, StartDate.SelectedDate.Value, EndDate.SelectedDate.Value, TypeReserv.IsChecked.Value));

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
        /// Eventhandler zur Eingabe des ausleihenden Kunden
        /// </summary>
        private void btnCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearchView customerSearch = new CustomerSearchView();
            customerSearch.Owner = this;
            customerSearch.DataContext = _customers.View;
            customerSearch.Customers = _customers;

            if (customerSearch.ShowDialog().Value)
            {
                _selectedCustomer = customerSearch.SelectedCustomer;
                CustomerName.Text = _selectedCustomer.FullName;
            }
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

            if (bookSearch.ShowDialog().Value)
            {
                _selectedBook = bookSearch.SelectedBook;
                BookTitle.Text = _selectedBook.Title;
            }
        }
    }
}
