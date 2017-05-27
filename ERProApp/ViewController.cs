using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Der ViewController dient zur darstellung der Daten.
    /// </summary>
    class ViewController
    {
        private ListCollectionView _rentalData = new ListCollectionView(DataController.Rentals);

        public ListCollectionView Rentals
        {
            get { return _rentalData; }
        }

        public ViewController()
        {
            _rentalData.GroupDescriptions.Add(new PropertyGroupDescription("Reservation"));
        }

        /*
        public static ObservableCollection<Rental> RentalData
        {
            get { return DataController.Rentals; }
        }
        */

    }
}
