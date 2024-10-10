using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MessageBox = System.Windows.Forms.MessageBox;
//using System.Windows.Shapes;

namespace _05_async_await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string source = "";
        static string destination = "";
        static Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }
        //async - allow method to use await keyword
        //await - wait task without freezing

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            //dialog.IsFolderPicker = true;
            //dialog.Multiselect = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                source = dialog.FileName;
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            //dialog.Multiselect = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destination = dialog.FileName;
                MessageBox.Show("You selected: " + dialog.FileName);
            }

        }
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            File.Copy(source, destination + "\\copy" + Path.GetFileName(source));
            MessageBox.Show($"Complited");
        }
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            source = from.Text;
            destination = to.Text;
            File.Copy(source, destination + "\\copy" + Path.GetFileName(source));
            MessageBox.Show($"Complited");
        }
    }
}

