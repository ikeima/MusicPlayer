using NewPlayer.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace NewPlayer.ModelView
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<string> _musicCollection = new ObservableCollection<string>();
        public ObservableCollection<string> MusicCollection
        {
            get { return _musicCollection; }
            set
            {
                _musicCollection = value;
                OnPropertyChanged("MusicCollection");
            }
        }

        private RelayCommand _loadMusic;
        public RelayCommand LoadMusic
        {
            get
            {
                _loadMusic = new RelayCommand(obj =>
                {
                    using (FolderBrowserDialog browser = new FolderBrowserDialog())
                    {
                        if (browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(browser.SelectedPath);

                            foreach (FileInfo file in dirInfo.GetFiles())
                            {
                                if (Path.GetExtension(file.FullName) == ".mp3")
                                {
                                    MusicCollection.Add(file.Name);
                                }
                            }
                        }
                    }
                });

                return _loadMusic;
            }
        }
    }
}
