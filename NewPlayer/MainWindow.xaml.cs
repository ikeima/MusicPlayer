using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace NewPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public ObservableCollection<string> MusicList = new ObservableCollection<string>();

        private void SelectAndLoadMusic(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(browser.SelectedPath);
                    
                    foreach(FileInfo file in dirInfo.GetFiles())
                    {
                        if (Path.GetExtension(file.FullName) == ".mp3")
                        {
                            MusicList.Add(file.Name);
                        }
                    }

                    musicList.ItemsSource = MusicList;
                }
            }
        }
    }
}
