using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for CustomerSearchView.xaml
    /// </summary>
    public partial class CustomerSearchView : Window
    {
        private CollectionViewSource _customers;

        public CollectionViewSource Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                _customers.View.Filter = _customers_Filter;
            }
        }

        public Customer SelectedCustomer => CustomerData.SelectedItem as Customer;

        public CustomerSearchView()
        {
            InitializeComponent();
        }

        // Suchfilter mit verschiedenen Kategorien
        private bool _customers_Filter(object item)
        {
            Customer customer = item as Customer;

            if (searchCategory.Text.Equals("Vorname"))
            {
                return customer.SurName.Contains(SearchBox.Text);
            }

            if (searchCategory.Text.Equals("Nachname"))
            {
                return customer.LastName.Contains(SearchBox.Text);
            }

            if (searchCategory.Text.Equals("Adresse"))
            {
                return customer.FullAddress.Contains(SearchBox.Text);
            }

            return true;
        }

        // Eventhandler wenn sich der Suchtext aendert
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _customers.View.Refresh();
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

            if (_customers != null)
            {
                //_customers.View.Refresh();
            }
        }
    }
}
