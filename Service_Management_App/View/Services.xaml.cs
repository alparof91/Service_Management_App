using System.Windows.Controls;

namespace Service_Management_App.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Services : Page
    {
        public Services()
        {
            InitializeComponent();

            this.DataContext = new ServicesViewModel();
        }
    }
}
