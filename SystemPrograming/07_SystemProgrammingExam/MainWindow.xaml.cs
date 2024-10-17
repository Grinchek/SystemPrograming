using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace _07_SystemProgrammingExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
        public partial class MainWindow : Window
        {
            private string currentPath;

            public MainWindow()
            {
                InitializeComponent();
                currentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                LoadTreeView(currentPath);
            }
        private async Task SearchWordInFilesAsync(string directoryPath, string searchWord)
        {
            StringBuilder result = new StringBuilder();
            int totalMatches = 0;

            try
            {
                string[] files = await Task.Run(() => Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories));
                await Dispatcher.InvokeAsync(() =>
                {
                    Loading.Maximum = files.Length;
                    Loading.Value = 0;
                });

                foreach (string file in files)
                {
                
                    try
                    {
                        string fileContent = await Task.Run(() => File.ReadAllText(file));
                        int wordCount = Regex.Matches(fileContent, Regex.Escape(searchWord), RegexOptions.IgnoreCase).Count;

                        if (wordCount > 0)
                        {
                            await Dispatcher.InvokeAsync(() =>
                            {
                                result.AppendLine($"File name: {Path.GetFileName(file)}\n");
                                result.AppendLine($"File path: {file}\n");
                                result.AppendLine($"Number of occurrences: {wordCount}");
                                result.AppendLine();

                                totalMatches += wordCount;
                            });
                        }
                        await Dispatcher.InvokeAsync(() =>
                        {
                            Loading.Value += 1;
                        });
                    }
                    catch (Exception ex)
                    {
                        await Dispatcher.InvokeAsync(() =>
                        {
                            MessageBox.Show($"Could not find the file: {file}\nError: {ex.Message}",
                                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        });
                    }
                }

                if (totalMatches > 0)
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show(result.ToString(), $"Matches: {totalMatches}", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }
                else
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show("There are no matches.", "Search result", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Search was not executed. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        private void LoadTreeView(string path)
        {
            try
            {
                FolderTreeView.Items.Clear();
                TreeViewItem rootItem = CreateTreeItem(path);
                FolderTreeView.Items.Add(rootItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tree view: " + ex.Message);
            }
        }
        private TreeViewItem CreateTreeItem(string path)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = Path.GetFileName(path);
                item.Tag = path;
                item.Items.Add(null);

                item.Expanded += FolderTreeItem_Expanded;

                return item;
            }

            private void FolderTreeItem_Expanded(object sender, RoutedEventArgs e)
            {
                TreeViewItem item = (TreeViewItem)sender;

                if (item.Items.Count == 1 && item.Items[0] == null)
                {
                    item.Items.Clear();

                    string path = (string)item.Tag;
                    string[] directories = Directory.GetDirectories(path);

                    foreach (string directory in directories)
                    {
                        TreeViewItem subItem = CreateTreeItem(directory);
                        item.Items.Add(subItem);
                    }
                }
            }

            private void FolderTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
                TreeViewItem selectedItem = FolderTreeView.SelectedItem as TreeViewItem;

                if (selectedItem != null)
                {
                    string selectedPath = selectedItem.Tag.ToString();
                    LoadFiles(selectedPath);
                    UpdateCurrentPath(selectedPath);
                }
            }

            private void LoadFiles(string path)
            {
                try
                {
                    

                DirectoryInfo directory = new DirectoryInfo(path);

                FileList.Items.Clear();

                DirectoryInfo[] directories = directory.GetDirectories();
                foreach (DirectoryInfo dir in directories)
                {
                    FileList.Items.Add($"[Folder] {dir.Name}");
                }

                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    FileList.Items.Add(file.Name);
                }
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading files: " + ex.Message);
                }
            }

            private void UpdateCurrentPath(string path)
            {
                currentPath = path;
                CurrentPathTextBlock.Text = $"Current Path: {currentPath}";
            }


        private async void SearchkButton_Click(object sender, RoutedEventArgs e)
        {
            await SearchWordInFilesAsync(currentPath, Word.Text.ToString());
        }
    }
}