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
        /// <summary>
        /// Standarkonstruktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewController();
        }

        // Eventhandler wenn das Fenster geschlossen wird
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = ExitMessageBox.ShowDialog();
            if (result == false) { e.Cancel = true; }
        }

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

    }
}
