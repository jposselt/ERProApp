using Microsoft.Windows.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ERProApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ViewController vc;

        /// <summary>
        /// Standarkonstruktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            vc = new ViewController();
            this.DataContext = vc;
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

        // Eventhandler zum Loeschen ueber das Kontextmenue
        private void dataGrid_Delete(object sender, RoutedEventArgs e)
        {
            // Dummy
        }

        // Eventhandler zum umwandel einer Reservierung in eine Ausleihe ueber das Kontextmenue
        private void dataGrid_ReservationToRent(object sender, RoutedEventArgs e)
        {
            // Dummy
        }

        // Eventhandler zum Anzeigen der detailierten Gegenstandsinformationen
        private void dataGrid_ShowItemInfo(object sender, RoutedEventArgs e)
        {
            ItemInfoView itemInfo = new ItemInfoView();
            itemInfo.Owner = this;
            itemInfo.DataContext = this.RentalData.SelectedItem;
            itemInfo.ShowDialog();
        }

        // Eventhandler zum Anzeigen der detailierten Kundeninformationen
        private void dataGrid_ShowCustomerInfo(object sender, RoutedEventArgs e)
        {
            CustomerInfoView customerInfo = new CustomerInfoView();
            customerInfo.Owner = this;
            customerInfo.CustomerRentals.ItemsSource = vc.Rentals;
            customerInfo.DataContext = this.RentalData.SelectedItem;
            customerInfo.ShowDialog();
        }

        // Eventhandler zum Suche nach Kunden
        private void button_SearchCustomer(object sender, RoutedEventArgs e)
        {
            CustomerSearchView customerSearch = new CustomerSearchView();
            customerSearch.Owner = this;
            customerSearch.DataContext = vc.Customers.View;
            customerSearch.Customers = vc.Customers;
            customerSearch.Show();
        }

        #endregion // Eventhandlers

    }
}
