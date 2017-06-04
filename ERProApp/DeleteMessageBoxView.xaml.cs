using System.Windows;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for DeleteMessageBox.xaml
    /// </summary>
    public partial class DeleteMessageBoxView : Window
    {
        /// <summary>
        /// Eigenschaft für das DataBinding der CheckBox.
        /// </summary>
        public bool DeleteImmediately
        {
            get { return ViewController.DeleteImmediately; }
            set { ViewController.DeleteImmediately = value; }
        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Escape))
            {
                Close();
            }
        }

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public DeleteMessageBoxView()
        {
            App.Log.Debug("Initialisiere Löschdialog");
            InitializeComponent();
            btnCancel.Focus();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Ja" klickt
        /// </summary>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult setzen
            DialogResult = true;

            // Fenster schließen.
            Close();
        }

        /// <summary>
        /// Eventhandler wenn der Benutzer auf "Nein" klickt
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Log.Debug("Löschen abgebrochen");

            // DialogResult setzen
            DialogResult = false;

            // Bei Abbruch auch das Flag zurücksetzen.
            // evtl hat der Benuter es unbeabsichtigt aktiviert.
            ViewController.DeleteImmediately = false;

            // Fenster schließen.
            Close();
        }
    }
}
