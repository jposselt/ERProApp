using System.Windows;


namespace ERProApp
{
    /// <summary>
    /// Hilfklasse um eine ExitMessageBox anzuzeigen.
    /// </summary>
    class ExitMessageBox
    {
        /// <summary>
        /// Öffnet eine Messagebox.
        /// </summary>
        /// <returns>
        /// True: Beenden
        /// False: Nicht beenden
        /// </returns>
        public static bool? ShowDialog()
        {
            return ShowDialog(null);
        }

        /// <summary>
        /// Öffnet eine Messagebox.
        /// </summary>
        /// <param name="owner">Besitzer des Fensters über den die MessageBox positioniert wird.</param>
        /// <returns>
        /// True: Beenden
        /// False: Nicht beenden
        /// </returns>
        public static bool? ShowDialog(object owner)
        {
            ExitMessageBoxView box = new ExitMessageBoxView();
            box.Owner = (Window)owner;
            return box.ShowDialog();
        }
    }
}
