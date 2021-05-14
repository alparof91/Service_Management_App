using System;
using System.Diagnostics;
using System.Windows;

namespace Service_Management_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new System.Uri(@"View/Employees.xaml", UriKind.Relative));
        }

        private void ListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    MainFrame.Navigate(new System.Uri(@"View/Employees.xaml", UriKind.Relative));
                    break;
                case 1:
                    MainFrame.Navigate(new System.Uri(@"View/CarsPage.xaml", UriKind.Relative));
                    break;
                case 2:
                    MainFrame.Navigate(new System.Uri(@"View/Services.xaml", UriKind.Relative));
                    break;
                case 3:
                    MainFrame.Navigate(new System.Uri(@"View/Interventions.xaml", UriKind.Relative));
                    break;
                default:
                    break;
            }

            Trace.WriteLine($"Clicked link: {index}");
        }
    }
}
