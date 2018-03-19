#region Usings
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
#endregion

namespace NightSkyPlayer
{
    public partial class MainWindow : Window
    {
        #region Properties

        private NDC.NDynamics.Core.AsyncWorker ProgressWorker = new NDC.NDynamics.Core.AsyncWorker(10);
        private NDC.NDynamics.Core.AsyncWorker OpacityWorker = new NDC.NDynamics.Core.AsyncWorker(10);
        private NDC.NDynamics.Core.AsyncWorker MinimizeWorker = new NDC.NDynamics.Core.AsyncWorker(1);
        private NDC.NDynamics.Core.AsyncWorker BackgroundTimer = new NDC.NDynamics.Core.AsyncWorker(1);


        public Input inputHolder;
        bool isClosing = false;
        bool isPlaying = false;
        bool isMinimizing = false;
        bool isDragMoveEnabled = false;
        private bool gisMouseOnProg = false;

        PlayList currentPlayList = new PlayList("default");
        #endregion
        #region Ctor
        public MainWindow()
        {
            InitializeComponent();
            me_player.MediaEnded += Me_player_MediaEnded;
            me_player.LoadedBehavior = MediaState.Manual;
            me_player.Visibility = Visibility.Hidden;
            me_player.Volume = 1;

            Opacity = 0;
            OpacityWorker.WorkerInterval += OpacityWorker_WorkerInterval;
            OpacityWorker.RunAsyncWorker(null);

            lbl_track_artist.Content = string.Empty;
            lbl_track_title.Content = string.Empty;
            lbl_track_year.Content = string.Empty;
            lbl_track_album.Content = string.Empty;
            lbl_track_no.Content = string.Empty;

            lbl_playlist_name_cur.Content = string.Empty;

            InitializeControls();
            InitializeEvents();

            me_backgr.Source = new Uri("Backgrounds\\1.mp4", UriKind.Relative);
            me_backgr.Play();

            btn_pause.IsEnabled = false;
            btn_pause.Visibility = Visibility.Hidden;

            Drop += MainWindow_Drop;

            if (!Directory.Exists("Playlists"))
            { Directory.CreateDirectory("Playlists"); }

            if (!File.Exists("Playlists\\default.NSP"))
            { GrabInput("default"); }

            lb_tracks.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            lb_playlists.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);


            string[] splistStr = new string[1];
            splistStr[0] = ".";

            foreach (var item in Directory.GetFiles("Playlists\\"))
            {
                GrabInput(Path.GetFileName(item).Split(splistStr, StringSplitOptions.RemoveEmptyEntries)[0]);
            }
            currentPlayList.FileName = ("default");
            currentPlayList.Load();
            UpdateTracks();
        }
        #endregion
        #region Initializers
        private void InitializeControls()
        {
            // BUTTON SETTINGS

            btn_play.isPlayerSpecific = true;
            btn_pause.isPlayerSpecific = true;
            btn_next.isPlayerSpecific = true;
            btn_prev.isPlayerSpecific = true;
            btn_add.isPlayerSpecific = true;

            btn_close.isPlayerSpecific = true;
            btn_minimize.isPlayerSpecific = true;

            // THEME UPDATE
            NDC.NStyle.Container.Colors.NightSky();

            // LISTBOX UPDATE
            lb_tracks.Update();
            lb_playlists.Update();

            // BUTTON UPDATE
            btn_close.Update();
            btn_minimize.Update();
            btn_add.Update();
            btn_play.Update();
            btn_pause.Update();
            btn_prev.Update();
            btn_next.Update();

            // LABEL UPDATE
            lbl_playlist.Update();
            lbl_tracks.Update();
            lbl_track_title.Update();
            lbl_track_artist.Update();
            lbl_track_album.Update();
            lbl_track_year.Update();
            lbl_track_no.Update();

            // CONST LABEL UPDATE
            lbl_playlist_name_cur.Override = true;
            lbl_const_name.Override = true;

            lbl_playlist_name_cur.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.NIGHTSKY_GLOW);
            lbl_const_name.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.NIGHTSKY_GLOW);

            // PROGRESS BAR UPDATE
            prgbar_progress.Update();


        }
        public void InitializeEvents()
        {
            MouseDown += MainWindow_DragMove;
            MouseUp += MainWindow_MouseUp;

            // Buttons
            btn_add.Click += Btn_add_Click;
            btn_close.Click += Btn_close_Click;
            btn_minimize.Click += Btn_minimize_Click;
            btn_pause.Click += Btn_pause_Click;
            btn_play.Click += Btn_play_Click;
            btn_next.Click += Btn_next_Click;
            btn_prev.Click += Btn_prev_Click;

            // Progress Bar
            prgbar_progress.MouseDown += Prgbar_progress_MouseDown; ;

            // Timers
            BackgroundTimer.WorkerInterval += BackgroundTimer_WorkerInterval;
            BackgroundTimer.RunAsyncWorker(null);

            ProgressWorker.WorkerInterval += ProgressWorker_WorkerInterval;
            ProgressWorker.RunAsyncWorker(null);

            MinimizeWorker.WorkerInterval += MinimizeWorker_WorkerInterval;
            MinimizeWorker.RunAsyncWorker(null);
        }
        #endregion
        #region Helper Functions
        public void GrabInput(string x)
        {
            int index = 0;
            if (!string.IsNullOrEmpty(x))
            {
                foreach (ListBoxItem item in lb_playlists.Items)
                {
                    if ((string)item.Content == x)
                    {
                        UpdateTracks();
                        continue;
                    }
                }
                index = lb_playlists.Add(x);
            }

            (lb_playlists.Items[index - 1] as NDC.NStyle.Controls.NSListBoxItem).PreviewMouseDown += Playlist_ListBoxMouseDown;

            currentPlayList = new PlayList(x);
            UpdateTracks();
        }
        private void UpdateTracks()
        {
            int index = 0;
            lb_tracks.Items.Clear();
            int iterator = 1;
            foreach (var item in currentPlayList.Values)
            {
                index = lb_tracks.Add(item.Key);

                (lb_tracks.Items[index - 1] as NDC.NStyle.Controls.NSListBoxItem).PreviewMouseDown += Tracks_ListBoxMouseDown; ;
                iterator++;
            }
        }
        private void LoadMp3File(string file)
        {
            me_player.Source = new Uri(currentPlayList.Values[file]);
            me_player.Position = TimeSpan.FromMilliseconds(0);
            me_player.Play();

            // ID3 Tag Loader
            try
            {
                Mp3Lib.Mp3File mp3file = new Mp3Lib.Mp3File(currentPlayList.Values[file]);
                Id3Lib.TagHandler id3Tags = mp3file.TagHandler;
                try
                {
                    lbl_track_artist.Content = "Artist: " + id3Tags.Artist;
                }
                catch { }
                try
                {
                    lbl_track_title.Content = "Title: " + id3Tags.Title;
                }
                catch { }
                try
                {
                    lbl_track_year.Content = "Year: " + id3Tags.Year;
                }
                catch { }
                try
                {
                    lbl_track_album.Content = "Album: " + id3Tags.Album;
                }
                catch { }
                try
                {
                    lbl_track_no.Content = "Track Number: " + id3Tags.Track;
                }
                catch { }
                try
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();

                    MemoryStream ms = new MemoryStream();
                    id3Tags.Picture.Save(ms, ImageFormat.Bmp);

                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();

                    img_songImg.Source = bi;
                }
                catch { }
            }
            catch
            {

            }
          
            isPlaying = true;
        }
        private void AddToPlaylist(string[] Paths)
        {
            int iterator = 0;
            string[] Names = new string[Paths.Length];

            foreach (var item in Paths)
            {
                Names[iterator] = Path.GetFileName(item);

                iterator++;
            }
            // Name : Key
            // Path : Value
            currentPlayList.Add(Names, Paths);
            currentPlayList.Save();
        }
        private void PlayOrPause()
        {
            if (me_player.Source != null)
            {
                if (isPlaying)
                {
                    me_player.Pause();
                }
                else
                {
                    me_player.Play();
                }
                isPlaying = !isPlaying;
            }
        }
        #endregion
        #region Event Functions
        #region MEPlay Events
        private void Me_player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Btn_next_Click(null, null);
        }
        #endregion
        #region Worker Events
        private void BackgroundTimer_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (me_backgr.Position.TotalMilliseconds >= 20000)
            {
                me_backgr.Stop();
                me_backgr.Position = TimeSpan.FromMilliseconds(10);
                me_backgr.Play();
            }
        }
        private void MinimizeWorker_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (isMinimizing)
            {
                Opacity -= 0.02;
                if (Opacity <= 0)
                {
                    WindowState = WindowState.Minimized;
                    isMinimizing = false;
                    Opacity = 1;
                }
            }

        }
        private void OpacityWorker_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (Opacity > 1)
                Opacity = 1;
            if (isClosing)
                Opacity -= 0.02;
            else
                Opacity += 0.02;
            if (Opacity <= 0)
                this.Close();
        }
        private void ProgressWorker_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (gisMouseOnProg)
            {
                double x = Mouse.GetPosition(Application.Current.MainWindow).X;
                x /= 1280;
                x *= 100;
                prgbar_progress.Value = x;
                me_player.Volume = prgbar_progress.Value / 100;
            }
        }
        #endregion
        #region Prgbar Events
        private void Prgbar_progress_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gisMouseOnProg = true;
            double x = Mouse.GetPosition(Application.Current.MainWindow).X;
            x /= 1280;
            x *= 100;
            prgbar_progress.Value = x;

            prgbar_progress.Override = true;

            prgbar_progress.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.GLOW);
        }
        #endregion
        #region Window Events
        private void MainWindow_DragMove(object sender, MouseButtonEventArgs e)
        {
            double y = Mouse.GetPosition(Application.Current.MainWindow).Y;      
            try
            {
                if ((e.ChangedButton == MouseButton.Left) && (y < 30) && (isDragMoveEnabled == true))
                    DragMove();
            }
            catch { }
        }
        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gisMouseOnProg = false;
            prgbar_progress.Override = false;
            prgbar_progress.Update();
        }
        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string cleanString = string.Empty;
            foreach (var item in FileList)
            {
                if (item.ToLower().Contains(".mp3"))
                {
                    cleanString += item + "\n";
                }
            }
            string[] splitStr = new string[1];
            splitStr[0] = "\n";
            string[] GoodFiles = cleanString.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            AddToPlaylist(GoodFiles);
            UpdateTracks();
        }
        #endregion
        #region Listbx Events
        private void Tracks_ListBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            string x = (sender as NDC.NStyle.Controls.NSListBoxItem).Content.ToString();


            if (e.ChangedButton == MouseButton.Right)
            {
                (sender as NDC.NStyle.Controls.NSListBoxItem).NContainer.Items.Remove(sender);
                currentPlayList.Values.Remove(x);
                currentPlayList.Save();
                UpdateTracks();
            }
            if (e.ChangedButton == MouseButton.Left)
            {

                btn_play.Visibility = Visibility.Visible;
                btn_play.IsEnabled = true;

                btn_pause.Visibility = Visibility.Hidden;
                btn_pause.IsEnabled = false;

                LoadMp3File(x);
            }
        }
        private void Playlist_ListBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            string x = (sender as NDC.NStyle.Controls.NSListBoxItem).Content.ToString();

            if (e.ChangedButton == MouseButton.Right)
            {
                lbl_playlist_name_cur.Content = string.Empty;
                (sender as NDC.NStyle.Controls.NSListBoxItem).NContainer.Items.Remove(sender);
                File.Delete("Playlists\\" + x + ".NSP");
                lb_tracks.Items.Clear();
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                currentPlayList = new PlayList(x);
                currentPlayList.Load();
                UpdateTracks();
                lbl_playlist_name_cur.Content = currentPlayList.FileName;


                isPlaying = true;
            }
        }
        #endregion
        #region Button Events
        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (inputHolder == null)
            {
                Input i = new Input(this);
                inputHolder = i;
                i.Show();
            }
        }
        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int nextIndex = lb_tracks.SelectedIndex + 1;

                if (nextIndex >= lb_tracks.Items.Count)
                {
                    nextIndex = 0;
                }

                foreach (var item in currentPlayList.Values)
                {
                    if (item.Key == currentPlayList.Keys[nextIndex])
                    {
                        LoadMp3File(item.Key);
                    }
                }
                lb_tracks.SelectedIndex = nextIndex;
            }
            catch { }
        }
        private void Btn_prev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int prevIndex = lb_tracks.SelectedIndex - 1;

                if (prevIndex < 0)
                {
                    prevIndex = lb_tracks.Items.Count - 1;
                }


                foreach (var item in currentPlayList.Values)
                {
                    if (item.Key == currentPlayList.Keys[prevIndex])
                    {
                        LoadMp3File(item.Key);
                    }
                }
                lb_tracks.SelectedIndex = prevIndex;
            }
            catch { }
        }
        private void Btn_pause_Click(object sender, RoutedEventArgs e)
        {
            if (me_player.Source != null)
            {
                PlayOrPause();

                btn_pause.IsEnabled = false;
                btn_pause.Visibility = Visibility.Hidden;

                btn_play.IsEnabled = true;
                btn_play.Visibility = Visibility.Visible;
            }
        }
        private void Btn_play_Click(object sender, RoutedEventArgs e)
        {
            if (me_player.Source != null)
            {
                PlayOrPause();

                btn_play.IsEnabled = false;
                btn_play.Visibility = Visibility.Hidden;

                btn_pause.IsEnabled = true;
                btn_pause.Visibility = Visibility.Visible;
            }
        }
        private void Btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            isMinimizing = true;
        }
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            isClosing = true;
        }
        #endregion
        #endregion
    }
}
