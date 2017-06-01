using System;
using System.ComponentModel;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Der ViewController dient als Verwaltungsklassen für Datenstrukturen, die zur Darstellung im GUI dienen
    /// </summary>
    class ViewController
    {
        private ListCollectionView _rentalData = new ListCollectionView(DataController.Rentals);
        private CollectionViewSource _customerSourceList = new CollectionViewSource() { Source = DataController.Customers };
        private CollectionViewSource _itemSourceList = new CollectionViewSource() { Source = DataController.Books };

        /// <summary>
        /// Gibt an ob eine Ausleihe sofort gelöscht werden soll oder ob erst eine Meldung angezeigt wird.
        /// </summary>
        public static bool DeleteImmediately { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ListCollectionView Rentals
        {
            get { return _rentalData; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CollectionViewSource Customers
        {
            get { return _customerSourceList; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CollectionViewSource Books
        {
            get { return _itemSourceList; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ViewController()
        {
            _rentalData.GroupDescriptions.Add(new PropertyGroupDescription("Reservation"));
        }

    }
}
