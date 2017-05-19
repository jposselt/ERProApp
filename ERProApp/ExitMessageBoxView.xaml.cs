using System.Windows;


namespace ERProApp
{
    /// <summary>
    /// Interaktionslogik für ExitMessageBoxView.xaml
    /// </summary>
    public partial class ExitMessageBoxView : Window
    {

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public ExitMessageBoxView()
        {
            InitializeComponent();
            btnCancel.Focus();
        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Escape))
            {
                Close();
            }
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
            // DialogResult setzen
            DialogResult = false;

            // Fenster schließen.
            Close();
        }
    }
}
