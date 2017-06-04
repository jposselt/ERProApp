using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;


namespace ERProApp
{
    /// <summary>
    /// Interaction logic for CustomerInfo.xaml
    /// </summary>
    public partial class CustomerInfoView : Window
    {
        private CollectionViewSource _rentals;
        private Customer _customer;

        public CollectionViewSource Rentals
        {
            get { return _rentals; }
            set { _rentals = value; }
        }

        public Customer SelectedCustomer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public CustomerInfoView()
        {
            App.Log.Debug("Initialisiere Kundeninformationsfenster");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ICollectionView aller Ausleihen
            ICollectionView Itemlist = Rentals.View;

            // Filter wählt den aktuellen Kunden aus
            var currentCustomerFilter = new Predicate<object>(item => ((Rental)item).Taker.ID.Equals(SelectedCustomer.ID));

            // Filter anwenden
            Itemlist.Filter = currentCustomerFilter;

            // Gefilterte Liste als Datenquelle für Datagrid
            CustomerRentals.ItemsSource = Itemlist;

            // Setze Datenkontext auf den ausgewaehlten Kunden
            this.DataContext = SelectedCustomer;
        }

        private void Button_OkClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
