using System;
using System.ComponentModel;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Der ViewController dient zur darstellung der Daten.
    /// </summary>
    class ViewController
    {
        private ListCollectionView _rentalData = new ListCollectionView(DataController.Rentals);
        private CollectionViewSource _customerSourceList = new CollectionViewSource() { Source = DataController.Customers };
        private CollectionViewSource _itemSourceList = new CollectionViewSource() { Source = DataController.Books };

        public ListCollectionView Rentals
        {
            get { return _rentalData; }
        }

        public CollectionViewSource Customers
        {
            get { return _customerSourceList; }
        }

        public CollectionViewSource Books
        {
            get { return _itemSourceList; }
        }

        public ViewController()
        {
            _rentalData.GroupDescriptions.Add(new PropertyGroupDescription("Reservation"));
        }

    }
}
