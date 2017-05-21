using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ERProApp
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public App()
            : base() // Bevor der Konstruktor aufgerufen wird, wird der Standardkonstruktor der Abgeleiteten Klasse aufgerufen.
        {
            // Verhindern das die Applikation mehrmals gestartet wird.
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
                Application.Current.Shutdown();

            // Daten laden
            DataController.LoadDataFromFiles();

        }

        /// <summary>
        /// Eventhandler wenn die Applikation beendet wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            DataController.SaveDataToFiles();
        }
    }
}
