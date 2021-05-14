using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Service_Management_App.View
{
    /// <summary>
    /// Interaction logic for Interventions.xaml
    /// </summary>
    public partial class Interventions : Page
    {
        public Interventions()
        {
            InitializeComponent();
            this.DataContext = new InterventionViewModel();
        }
    }
}
