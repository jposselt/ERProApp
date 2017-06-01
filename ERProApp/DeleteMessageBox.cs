using System.Windows;

namespace ERProApp
{
    /// <summary>
    /// Hilfklasse um eine Anzeigen einer DeleteMessageBox
    /// </summary>
    class DeleteMessageBox
    {
        /// <summary>
        /// Öffnet eine DeleteMessageBox.
        /// </summary>
        /// <returns>
        /// True: Ausleihe löschen
        /// False: Notiz Ausleihe löschen
        /// </returns>
        public static bool? ShowDialog()
        {
            return ShowDialog(null);
        }

        /// <summary>
        /// Öffnet eine DeleteMessageBox.
        /// </summary>
        /// <param name="owner">Besitzer des Fensters über den die DeleteMessageBox positioniert wird.</param>
        /// <returns>
        /// True: Ausleihe löschen
        /// False: Ausleihe nicht löschen
        /// </returns>
        public static bool? ShowDialog(object owner)
        {
            DeleteMessageBoxView box = new DeleteMessageBoxView();
            box.Owner = (Window)owner;
            return box.ShowDialog();
        }
    }
}
