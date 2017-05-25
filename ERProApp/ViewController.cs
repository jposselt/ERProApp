using System.Collections.ObjectModel;


namespace ERProApp
{
    /// <summary>
    /// Der ViewController dient zur darstellung der Daten.
    /// </summary>
    class ViewController
    {

        public static ObservableCollection<Rental> RentalData
        {
            get { return DataController.Rentals; }
        }

    }
}
