using System.Collections.ObjectModel;
using System.Windows;


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
    }
}
