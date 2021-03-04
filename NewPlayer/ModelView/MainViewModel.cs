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
        public ObservableCollection<string> MusicCollection // Коллекция аудизописей, отображаемая в представлении
        {
            get { return _musicCollection; }
            set
            {
                _musicCollection = value;
                OnPropertyChanged("MusicCollection");
            }
        }

        private string _selectedSong;
        public string SelectedSong // Выбранная аудиозапись
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

        private BitmapImage _selectedSongImage = new BitmapImage();
        public BitmapImage SelectedSongImage // Обложка выбранной песни
        {
            get { return _selectedSongImage; }
            set
            {
                _selectedSongImage = value;
                OnPropertyChanged("SelectedSongImage");
            }
        }

        private string _selectedSongName;
        public string SelectedSongName // Название выбранной аудиозаписи
        {
            get { return _selectedSongName; }
            set
            {
                _selectedSongName = value;
                OnPropertyChanged("SelectedSongName");
            }
        }

        private string _selectedSongArtist;
        public string SelectedSongArtist // Исполнитель выбранной аудиозаписи
        {
            get { return _selectedSongArtist; }
            set
            {
                _selectedSongArtist = value;
                OnPropertyChanged("SelectedSongArtist");
            }
        }

        private string _selectedSongPath;
        public string SelectedSongPath // Полный путь выбранной аудиозаписи
        {
            get { return _selectedSongPath; }
            set
            {
                _selectedSongPath = value;
                OnPropertyChanged("SelectedSongPath");
            }
        }

        private List<string> _musicsPath = new List<string>();
        public List<string> MusicsPath // Лист для хранения полных путей аудиозаписей
        {
            get { return _musicsPath; }
            set
            {
                _musicsPath = value;
                OnPropertyChanged("MusicsPath");
            }
        }

        private RelayCommand _loadMusic;
        public RelayCommand LoadMusic // Команда для загрузки музыки в коллекцию и лист
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
        public MediaPlayer Player // Медиа-плеер, с помощью которого проигрываются аудиозаписи
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged("Player");
            }
        }

        private RelayCommand _play;
        public RelayCommand Play // Запуск песни
        {
            get
            {
                _play = new RelayCommand(obj =>
                {
                    try
                    {
                        Player.Open(new Uri(SelectedSongPath));
                        Player.Play();

                        SetSongPicture();
                        TrimSongName(SelectedSong);
                    }
                    catch
                    {
                        MessageBox.Show("Музыку загрузи, бля");
                    }
                });
                return _play;
            }
        }

        private RelayCommand _pause;
        public RelayCommand Pause // Поставить на паузу
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
        public RelayCommand Resume // Возобновить песню
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
        public RelayCommand Next // Переключение песни на следующую в листе
        {
            get
            {
                _next = new RelayCommand(obj =>
                {
                    try
                    {
                        int indexNext = MusicsPath.IndexOf(SelectedSongPath) + 1;
                        Player.Open(new Uri(MusicsPath[indexNext]));
                        SelectedSong = MusicCollection[indexNext];
                        Player.Play();

                        SetSongPicture();
                        TrimSongName(SelectedSong);
                    }
                    catch
                    {
                        int indexFirst = 0;
                        Player.Open(new Uri(MusicsPath[indexFirst]));
                        SelectedSong = MusicCollection[indexFirst];
                        Player.Play();

                        SetSongPicture();
                        TrimSongName(SelectedSong);
                    }
                });
                return _next;
            }
        }

        private RelayCommand _previous;
        public RelayCommand Previous // Переключение песни на предыдущую в листе
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
                        TrimSongName(SelectedSong);
                    }
                    catch
                    {
                        Player.Open(new Uri(SelectedSongPath));
                        Player.Play();

                        SetSongPicture();
                        TrimSongName(SelectedSong);
                    }

                });
                return _previous;
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
            catch // В случае отсутствия обложки - ставится дефолтная картинка
            {
                BitmapImage bitmap = new BitmapImage(new Uri(@"/Resources/DefaultGif.gif", UriKind.Relative)); 

                SelectedSongImage = bitmap;
            }
        }

        private void TrimSongName(string song) // Метод парсит строку с названием песни, выделяет отдельно название и исполнителя, так же удаляет расширение из названия
        {
            song = song.TrimEnd('.', 'm', 'p', '3');

            for (int i = 0; i < song.Length; i++)
            {
                if (char.IsPunctuation(song[i]))
                {
                    SelectedSongArtist = song.Remove(i);
                    SelectedSongName = song.Substring(i + 2);
                    break;
                }
            }
        }

    }
}
