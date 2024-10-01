using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_processes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //int timeout = Convert.ToInt32(timeCB.SelectedItem);
            //MessageBox.Show(timeout.ToString());
            //Thread.Sleep(timeout * 1000);
            
            grid.ItemsSource = Process.GetProcesses();







        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process prToKill = (grid.SelectedItem as Process);
            prToKill.Kill();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process prToClose = (grid.SelectedItem as Process);
            prToClose.CloseMainWindow();
            prToClose.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            //grid.Items.Clear();
            Process prToShow = (grid.SelectedItem as Process);
            //grid.ItemsSource = (System.Collections.IEnumerable)prToShow;
            MessageBox.Show($"Name: {prToShow.ProcessName.ToString()}\nPriority: {prToShow.BasePriority.ToString()}");

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string processName = $"{nameProcess.Text}.exe";
           // MessageBox.Show(processName);
           Process.Start(processName);
        }
    }
}