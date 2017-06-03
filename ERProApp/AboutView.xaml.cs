using System.Windows;

namespace ERProApp
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : Window
    {
        public AboutView()
        {
            InitializeComponent();
        }

        public string Version
        {
            get { return ApplicationInfo.Version;}
        }

        public string Product
        {
            get { return ApplicationInfo.ProductName; }
        }

        public string VersionDate
        {
            get { return ApplicationInfo.CompileDate.ToString("dd. MMMM yyyy HH:mm:ss"); }
        }

        public string VersionCompany
        {
            get { return ApplicationInfo.Company; }
        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Escape) || (e.Key == System.Windows.Input.Key.Enter))
            {
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
