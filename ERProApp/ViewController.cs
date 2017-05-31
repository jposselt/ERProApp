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

        public ListCollectionView Rentals
        {
            get { return _rentalData; }
        }

        public CollectionViewSource Customers
        {
            get { return _customerSourceList; }
        }

        public ViewController()
        {
            _rentalData.GroupDescriptions.Add(new PropertyGroupDescription("Reservation"));
        }

    }
}
