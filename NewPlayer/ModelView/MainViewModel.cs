using NewPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

namespace NewPlayer.ModelView
{
    class MainViewModel : BaseViewModel
    {
        private bool _isResume = false;

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
                    _isResume = true;
                });
                return _play;
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
                    SelectedSong = MusicsPath[indexNext];
                    Player.Play();
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
                        SelectedSong = MusicsPath[indexPrevious];
                        Player.Play();
                    }
                    catch
                    {
                        Player.Open(new Uri(SelectedSongPath));
                        Player.Play();
                    }
              
                });
                return _previous;
            }
        }

    }
}
