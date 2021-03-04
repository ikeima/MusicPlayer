using NewPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private BitmapImage _selectedSongImage = new BitmapImage();
        public BitmapImage SelectedSongImage
        {
            get { return _selectedSongImage; }
            set
            {
                _selectedSongImage = value;
                OnPropertyChanged("SelectedSongImage");
            }
        }

        private string _selectedSong;
        public string SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                _selectedSong = value;

                foreach (string song in MusicsPath)
                {
                    if (song.Contains(_selectedSong))
                    {
                        SelectedSongPath = song;
                        break;
                    }
                }

                OnPropertyChanged("SelectedSong");
            }
        }

        private string _selectedSongPath;
        public string SelectedSongPath
        {
            get { return _selectedSongPath; }
            set
            {
                _selectedSongPath = value;
                OnPropertyChanged("SelectedSongPath");
            }
        }

        private List<string> _musicsPath = new List<string>();
        public List<string> MusicsPath
        {
            get { return _musicsPath; }
            set
            {
                _musicsPath = value;
                OnPropertyChanged("MusicsPath");
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
                                    MusicsPath.Add(file.FullName);
                                }
                            }
                        }
                    }
                });
                return _loadMusic;
            }
        }

        private MediaPlayer _player = new MediaPlayer();
        public MediaPlayer Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged("Player");
            }
        }

        private RelayCommand _play;
        public RelayCommand Play
        {
            get
            {
                _play = new RelayCommand(obj =>
                {
                    Player.Open(new Uri(SelectedSongPath));
                    Player.Play();

                    SetSongPicture();

                });
                return _play;
            }
        }

        private void SetSongPicture() // Метод для загрузки изображения песни (если имеется)
        {
            var file = TagLib.File.Create(SelectedSongPath);

            try
            {
                using (MemoryStream stream = new MemoryStream(file.Tag.Pictures[0].Data.Data))
                {
                    BitmapImage bitmap = new BitmapImage();

                    stream.Position = 0;

                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    SelectedSongImage = bitmap;
                }
            }
            catch
            {
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    BitmapImage bitmap = new BitmapImage();

                //    stream.Position = 0;

                //    bitmap.BeginInit();
                //    bitmap.StreamSource = stream;
                //    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                //    bitmap.EndInit();

                //    SelectedSongImage = bitmap;
                //}
            }
        }

        private RelayCommand _pause;
        public RelayCommand Pause
        {
            get
            {
                _pause = new RelayCommand(obj =>
                {
                    Player.Pause();
                });
                return _pause;
            }
        }

        private RelayCommand _resume;
        public RelayCommand Resume
        {
            get
            {
                _resume = new RelayCommand(obj =>
                {
                    Player.Play();
                });
                return _resume;
            }
        }


        private RelayCommand _next;
        public RelayCommand Next
        {
            get
            {
                _next = new RelayCommand(obj =>
                {
                    int indexNext = MusicsPath.IndexOf(SelectedSongPath) + 1;
                    Player.Open(new Uri(MusicsPath[indexNext]));
                    SelectedSong = MusicCollection[indexNext];
                    Player.Play();

                    SetSongPicture();
                });
                return _next;
            }
        }

        private RelayCommand _previous;
        public RelayCommand Previous
        {
            get
            {
                _previous = new RelayCommand(obj =>
                {
                    try
                    {
                        int indexPrevious = MusicsPath.IndexOf(SelectedSongPath) - 1;
                        Player.Open(new Uri(MusicsPath[indexPrevious]));
                        SelectedSong = MusicCollection[indexPrevious];
                        Player.Play();

                        SetSongPicture();
                    }
                    catch
                    {
                        Player.Open(new Uri(SelectedSongPath));
                        Player.Play();

                        SetSongPicture();
                    }

                });
                return _previous;
            }
        }

    }
}
