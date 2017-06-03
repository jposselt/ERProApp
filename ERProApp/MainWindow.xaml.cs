using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ERProApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ViewController _vc;

        /// <summary>
        /// Standarkonstruktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _vc = new ViewController();
            this.DataContext = _vc;
        }

        #region Eventhandlers
        
        // Eventhandler wenn das Fenster geschlossen wird
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = ExitMessageBox.ShowDialog();
            if (result == false) { e.Cancel = true; }
        }

        // Eventhandler wenn im datagrid der Ausleihen eine Zeile selektiert wird
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                // Ausgewaehltes Element
                Rental selected = dataGrid.SelectedItem as Rental;

                // Kein Element ausgewaehlt
                if (selected == null)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    Dictionary<DateTime, string> highlight = new Dictionary<DateTime, string>();

                    // Iteriere ueber Tage der Ausleihe
                    for(var day = selected.StartDate.Date; day.Date <= selected.EndDate.Date; day = day.AddDays(1))
                    {
                        highlight.Add(day,"Dummy");
                    }
                    HighlightDatesConverter.Highlight = highlight;

                    // Workaround zur Aktualisierung der Kalenderansicht,
                    // da die Invalidate Methoden hier nicht wie erwartet
                    // funktioniern
                    RentalCalendar.DisplayDate = DateTime.MinValue;
                    RentalCalendar.DisplayDate = selected.StartDate;

                    e.Handled = true;
                    return;
                }
                
            }
        }

        // Eventhandler zum Loeschen der ausgewaelten Ausleihe/Reservierung
        /*private void dataGrid_Delete(object sender, RoutedEventArgs e)
        {
            if (RentalData.SelectedItem == null)
            {
                MessageBox.Show("Es wurde keine Ausleihe/Reservierung ausgewählt", "Keine Auswahl", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if ((ViewController.DeleteImmediately) || (DeleteMessageBox.ShowDialog(this) == true))
                {
                    // Update des Buchstatus und des Reservierungszaehlers
                    if((RentalData.SelectedItem as Rental).Reservation)
                    {
                        Book temp = (RentalData.SelectedItem as Rental).Item;
                        temp.ReservationCount -= 1;
                        if (temp.Status == "reserviert" && temp.ReservationCount == 0)
                        {
                            temp.Status = "verfügbar";
                        }
                    }
                    else
                    {
                        Book temp = (RentalData.SelectedItem as Rental).Item;
                        if (temp.Status == "ausgeliehen" && temp.ReservationCount == 0)
                        {
                            temp.Status = "verfügbar";
                        }
                        else
                        {
                            temp.Status = "reserviert";
                        }
                    }

                    // Lösche die Ausleihe
                    DataController.DeleteRental(RentalData.SelectedItem as Rental);

                    // Aktualisiere Datagrid
                    RentalData.InvalidateVisual();
                }
            }
        }*/

        // Eventhandler zum Umwandeln der ausgewaelten Reservierung in eine Ausleihe
        /*private void dataGrid_ReservationToRent(object sender, RoutedEventArgs e)
        {
            if (RentalData.SelectedItem != null)
            {
                // Reservation Feld setzen
                (RentalData.SelectedItem as Rental).Reservation = false;

                // Update des Buchstatus und des Reservierungszaehlers
                Book temp = (RentalData.SelectedItem as Rental).Item;
                temp.ReservationCount -= 1;
                if (temp.Status == "reserviert" && temp.ReservationCount == 0)
                {
                    temp.Status = "verfügbar";
                }

                // Aktualisiere Datagrid
                _vc.Rentals.Refresh();
            }
        }*/

        // Eventhandler zum Anzeigen der detailierten Gegenstandsinformationen
        /*private void dataGrid_ShowItemInfo(object sender, RoutedEventArgs e)
        {
            ItemInfoView itemInfo = new ItemInfoView();
            itemInfo.Owner = this;
            itemInfo.DataContext = this.RentalData.SelectedItem;
            itemInfo.ShowDialog();
        }*/

        // Eventhandler zum Anzeigen der detailierten Kundeninformationen
        /*private void dataGrid_ShowCustomerInfo(object sender, RoutedEventArgs e)
        {
            CustomerInfoView customerInfo = new CustomerInfoView();
            customerInfo.Owner = this;
            customerInfo.Rentals = new CollectionViewSource() { Source = _vc.Rentals.SourceCollection };
            customerInfo.SelectedCustomer = ((Rental)this.RentalData.SelectedItem).Taker;
            customerInfo.ShowDialog();
        }*/

        // Eventhandler zum sperren eines Buchs
        /*private void dataGrid_BlockItem(object sender, RoutedEventArgs e)
        {
            if (RentalData.SelectedItem != null)
            {
                // Setze Feld
                (RentalData.SelectedItem as Rental).Item.Blocked = true;

                // Update Status
                (RentalData.SelectedItem as Rental).Item.Status = "gesperrt";

                // Aktualisiere Datagrid
                RentalData.InvalidateVisual();
            }
        }*/

        // Eventhandler zum entsperren eines Buchs
        /*private void dataGrid_UnblockItem(object sender, RoutedEventArgs e)
        {
            if (RentalData.SelectedItem != null)
            {
                // Setze Feld
                (RentalData.SelectedItem as Rental).Item.Blocked = false;

                // Update Status
                string id = (RentalData.SelectedItem as Rental).ItemID;
                (RentalData.SelectedItem as Rental).Item.Status = "verfügbar";
                foreach (DataGridRow dr in RentalData.Items)
                {
                    if ((dr.Item as Rental).ItemID == id)
                    {
                        if ((RentalData.SelectedItem as Rental).Item.Status == "verfügbar" && (RentalData.SelectedItem as Rental).Reservation)
                        {
                            (RentalData.SelectedItem as Rental).Item.Status = "reserviert";
                        }
                        else
                        {
                            (RentalData.SelectedItem as Rental).Item.Status = "ausgeliehen";
                        }
                    }
                }

                // Aktualisiere Datagrid
                RentalData.InvalidateVisual();
            }
        }*/

        // Eventhandler zum Suche nach Kunden
        /*private void button_SearchCustomer(object sender, RoutedEventArgs e)
        {
            CustomerSearchView customerSearch = new CustomerSearchView();
            customerSearch.Owner = this;
            customerSearch.DataContext = _vc.Customers.View;
            customerSearch.Customers = _vc.Customers;
            customerSearch.ShowDialog();
        }*/

        // Eventhandler zum Suche nach Büchern
        /*private void button_SearchBook(object sender, RoutedEventArgs e)
        {
            ItemSearchView itemSearch = new ItemSearchView();
            itemSearch.Owner = this;
            itemSearch.DataContext = _vc.Books.View;
            itemSearch.Books = _vc.Books;
            itemSearch.ShowDialog();
        }*/

        // Eventhandler zum Anlegen neuer Ausleihen/Reservierungen
        /*private void button_NewRental(object sender, RoutedEventArgs e)
        {
            NewRentalDialogView newRental = new NewRentalDialogView();
            newRental.Owner = this;
            newRental.Customers = _vc.Customers;
            newRental.Books = _vc.Books;
            if (newRental.ShowDialog().Value)
            {
                // Aktualisiere Datagrid
                RentalData.InvalidateVisual();
            }
        }*/

        // Eventhandler zum des About Fensters
        /*private void button_ShowAbout(object sender, RoutedEventArgs e)
        {
            AboutView about = new AboutView();
            about.ShowDialog();
        }*/

        #endregion // Eventhandlers

        #region Commands

        /// <summary>
        /// Suche nach Buechern
        /// </summary>
        public static readonly RoutedCommand SearchBook = new RoutedCommand();

        private void SearchBookCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_vc != null && _vc.Books != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void SearchBookExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ItemSearchView itemSearch = new ItemSearchView();
            itemSearch.Owner = this;
            itemSearch.DataContext = _vc.Books.View;
            itemSearch.Books = _vc.Books;
            itemSearch.ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// Suche nach Kunden
        /// </summary>
        public static readonly RoutedCommand SearchCustomer = new RoutedCommand();

        private void SearchCustomerCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_vc != null && _vc.Customers != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void SearchCustomerExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerSearchView customerSearch = new CustomerSearchView();
            customerSearch.Owner = this;
            customerSearch.DataContext = _vc.Customers.View;
            customerSearch.Customers = _vc.Customers;
            customerSearch.ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// Neue Ausleihe erzeugen
        /// </summary>
        public static readonly RoutedCommand NewRental = new RoutedCommand();

        private void NewRentalCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void NewRentalExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            NewRentalDialogView newRental = new NewRentalDialogView();
            newRental.Owner = this;
            newRental.Customers = _vc.Customers;
            newRental.Books = _vc.Books;
            if (newRental.ShowDialog().Value)
            {
                // Aktualisiere Datagrid
                _vc.Rentals.Refresh();
            }
            e.Handled = true;
        }

        /// <summary>
        /// Anzeigen der detailierten Gegenstandsinformationen
        /// </summary>
        public static readonly RoutedCommand ItemInfo = new RoutedCommand();

        private void ItemInfoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void ItemInfoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ItemInfoView itemInfo = new ItemInfoView();
            itemInfo.Owner = this;
            itemInfo.DataContext = this.RentalData.SelectedItem;
            itemInfo.ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// Anzeigen der detailierten Kundeninformationen
        /// </summary>
        public static readonly RoutedCommand CustomerInfo = new RoutedCommand();

        private void CustomerInfoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void CustomerInfoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerInfoView customerInfo = new CustomerInfoView();
            customerInfo.Owner = this;
            customerInfo.Rentals = new CollectionViewSource() { Source = _vc.Rentals.SourceCollection };
            customerInfo.SelectedCustomer = ((Rental)this.RentalData.SelectedItem).Taker;
            customerInfo.ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// Loeschen von Ausleihen und Reservierungen
        /// </summary>
        public static readonly RoutedCommand Delete = new RoutedCommand();

        private void DeleteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void DeleteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (RentalData.SelectedItem == null)
            {
                MessageBox.Show("Es wurde keine Ausleihe/Reservierung ausgewählt", "Keine Auswahl", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if ((ViewController.DeleteImmediately) || (DeleteMessageBox.ShowDialog(this) == true))
                {
                    // Update des Buchstatus und des Reservierungszaehlers
                    if ((RentalData.SelectedItem as Rental).Reservation)
                    {
                        Book temp = (RentalData.SelectedItem as Rental).Item;
                        temp.ReservationCount -= 1;
                        if (temp.Status == "reserviert" && temp.ReservationCount == 0)
                        {
                            temp.Status = "verfügbar";
                        }
                    }
                    else
                    {
                        Book temp = (RentalData.SelectedItem as Rental).Item;
                        if (temp.Status == "ausgeliehen" && temp.ReservationCount == 0)
                        {
                            temp.Status = "verfügbar";
                        }
                        else
                        {
                            temp.Status = "reserviert";
                        }
                    }

                    // Lösche die Ausleihe
                    DataController.DeleteRental(RentalData.SelectedItem as Rental);

                    // Aktualisiere Datagrid
                    _vc.Rentals.Refresh();
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// Zeigt das "About" Fenster an
        /// </summary>
        public static readonly RoutedCommand About = new RoutedCommand();

        private void AboutCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void AboutExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AboutView about = new AboutView();
            about.ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// Umwandeln einer Reservierung in Ausleihe
        /// </summary>
        public static readonly RoutedCommand Checkout = new RoutedCommand();

        private void CheckoutCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
            {
                if( (RentalData.SelectedItem as Rental).Reservation  && !(RentalData.SelectedItem as Rental).Item.Blocked)
                {
                    e.CanExecute = true;
                }
                else
                    e.CanExecute = false;
            }
            else
                e.CanExecute = false;
            e.Handled = true;
        }

        private void CheckoutExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (RentalData.SelectedItem != null)
            {
                // Reservation Feld setzen
                (RentalData.SelectedItem as Rental).Reservation = false;

                // Update des Buchstatus und des Reservierungszaehlers
                Book temp = (RentalData.SelectedItem as Rental).Item;
                temp.ReservationCount -= 1;
                if (temp.Status == "reserviert" && temp.ReservationCount == 0)
                {
                    temp.Status = "verfügbar";
                }

                // Aktualisiere Datagrid
                _vc.Rentals.Refresh();

            }
            e.Handled = true;
        }


        /// <summary>
        /// Sperrt Ausleihen des ausgewaehlten Buchs
        /// </summary>
        public static readonly RoutedCommand Block = new RoutedCommand();

        private void BlockCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
            {
                if ((RentalData.SelectedItem as Rental).Item.Blocked)
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
            if (RentalData.SelectedItem != null)
            {
                // Setze Feld
                (RentalData.SelectedItem as Rental).Item.Blocked = true;

                // Update Status
                (RentalData.SelectedItem as Rental).Item.Status = "gesperrt";
            }
            e.Handled = true;
        }

        /// <summary>
        /// Entsperrt Ausleihen des ausgewaehlten Buchs
        /// </summary>
        public static readonly RoutedCommand Unblock = new RoutedCommand();

        private void UnblockCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (RentalData != null && RentalData.SelectedItem != null)
            {
                if ((RentalData.SelectedItem as Rental).Item.Blocked)
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
            // Setze Feld
            (RentalData.SelectedItem as Rental).Item.Blocked = false;

            // Update Status
            string id = (RentalData.SelectedItem as Rental).ItemID;
            (RentalData.SelectedItem as Rental).Item.Status = "verfügbar";
            foreach (DataGridRow dr in RentalData.Items)
            {
                if ((dr.Item as Rental).ItemID == id)
                {
                    if ((RentalData.SelectedItem as Rental).Item.Status == "verfügbar" && (RentalData.SelectedItem as Rental).Reservation)
                    {
                        (RentalData.SelectedItem as Rental).Item.Status = "reserviert";
                    }
                    else
                    {
                        (RentalData.SelectedItem as Rental).Item.Status = "ausgeliehen";
                    }
                }
            }
            e.Handled = true;
        }

        #endregion // Commands


    }
}
