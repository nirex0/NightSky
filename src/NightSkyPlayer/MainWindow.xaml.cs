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
        private NDC.NDynamics.Core.AsyncWorker BackgroundTimer = new NDC.NDynamics.Core.AsyncWorker(1);
        private NDC.NDynamics.Core.AsyncWorker VolumeTimer = new NDC.NDynamics.Core.AsyncWorker(10);

        public bool canTakeInput { get; set; } = true;
        private bool isPlaying = false;

        private bool isDragMoveEnabled = false;
        private bool shouldRepeat = false;
        private bool shouldShuffle = false;
        private bool gisMouseOnProg = false;
        private bool shouldShowVolume = false;
        private bool autoPlay = false;
        private bool fullScreen = false;
        private int volumeAccum = 0;

        /// 0 = add 
        /// 1 = sub
        private int opacityMod = 0;
        /// 0 = resize
        /// 1 = minimize
        /// 2 = close
        /// 3 = nothing
        /// 4 = first show
        private int opacityCmd = 4;

        private WindowState nextState;

        PlayList currentPlayList = new PlayList("default");
        #endregion
        #region Ctor
        public MainWindow()
        {
            InitializeComponent();

            LoadSettings();

            me_player.MediaEnded += Me_player_MediaEnded;
            me_player.LoadedBehavior = MediaState.Manual;
            me_player.Visibility = Visibility.Hidden;
            me_player.Volume = 0.5;

            if (fullScreen)
            {
                WindowState = WindowState.Maximized;
            }

            Opacity = 0;

            lbl_track_artist.Content = string.Empty;
            lbl_track_title.Content = string.Empty;
            lbl_track_year.Content = string.Empty;
            lbl_track_album.Content = string.Empty;
            lbl_track_no.Content = string.Empty;

            lbl_playlist_name_cur.Content = string.Empty;

            InitializeControls();
            InitializeEvents();



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
            lb_playlists.SelectedIndex = lb_playlists.Items.IndexOf("default");
            UpdateTracks();

            if (autoPlay)
            {
                PlayRandom();
            }
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
            btn_shuffle.isPlayerSpecific = true;
            btn_repeat.isPlayerSpecific = true;

            btn_close.isPlayerSpecific = true;
            btn_minimize.isPlayerSpecific = true;

            btn_volDown.isPlayerSpecific = true;
            btn_volUp.isPlayerSpecific = true;

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
            btn_shuffle.Update();
            btn_repeat.Update();
            btn_volDown.Update();
            btn_volUp.Update();

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


            lbl_playlist_name_cur.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.NIGHTSKY_CONST);
            lbl_const_name.Foreground = new SolidColorBrush(NDC.NStyle.Container.Colors.NIGHTSKY_CONST);

            // PROGRESS BAR UPDATE
            prgbar_progress.Update();
            

        }
        public void InitializeEvents()
        {
            // Buttons
            btn_add.Click += Btn_add_Click;
            btn_close.Click += Btn_close_Click;
            btn_minimize.Click += Btn_minimize_Click;
            btn_pause.Click += Btn_pause_Click;
            btn_play.Click += Btn_play_Click;
            btn_next.Click += Btn_next_Click;
            btn_prev.Click += Btn_prev_Click;
            btn_shuffle.Click += Btn_shuffle_Click;
            btn_repeat.Click += Btn_repeat_Click;
            btn_volDown.Click += Btn_volDown_Click;
            btn_volUp.Click += Btn_volUp_Click;

            // Timers
            BackgroundTimer.WorkerInterval += BackgroundTimer_WorkerInterval;
            BackgroundTimer.RunAsyncWorker(null);

            ProgressWorker.WorkerInterval += ProgressWorker_WorkerInterval;
            ProgressWorker.RunAsyncWorker(null);

            VolumeTimer.WorkerInterval += VolumeTimer_WorkerInterval;
            VolumeTimer.RunAsyncWorker(null);

            OpacityWorker.WorkerInterval += OpacityWorker_WorkerInterval;
            OpacityWorker.RunAsyncWorker(null);

            // Window
            MouseDoubleClick += MainWindow_Maximizer;
            MouseDown += MainWindow_DragMove;
            MouseUp += MainWindow_MouseUp;
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
        private void LoadSettings()
        {
            string[] settings = File.ReadAllLines("settings.nss");
            string[] splitStr = new string[1];
            splitStr[0] = ":";

            int iterator = 0;
            foreach (string option in settings)
            {
                string clean = option.Split(splitStr, StringSplitOptions.RemoveEmptyEntries)[1];
                switch (iterator)
                {                    
                    case 0:
                        Width = int.Parse(clean);
                        break;
                    case 1:
                        Height = int.Parse(clean);
                        break;
                    case 2:
                        Topmost = bool.Parse(clean);
                        break;
                    case 3:
                        autoPlay = bool.Parse(clean);
                        break;
                    case 4:
                        fullScreen = bool.Parse(clean);
                        break;
                    case 5:
                        int theme = int.Parse(clean);

                        if(theme == -1)
                        {
                            theme = new Random().Next(1, 5);    
                        }
                        LoadTheme(theme);

                        break;
                    default:
                        break;
                }
                iterator++;
            }
        }

        int backGroundLoopTime;
        private void LoadTheme(int themeNumber)
        {
            if (themeNumber == 1)
            {
                // BLUE
                backGroundLoopTime = 10000;
                NDC.NStyle.Container.Colors.BLUE_NightSky();
                me_backgr.Source = new Uri("Backgrounds\\1.mp4", UriKind.Relative);
                me_backgr.Play();
            }
            else if (themeNumber == 2)
            {
                // PURPLE-ISH
                backGroundLoopTime = 29000;
                NDC.NStyle.Container.Colors.PURPLEISH_NightSky();
                me_backgr.Source = new Uri("Backgrounds\\2.mp4", UriKind.Relative);
                me_backgr.Play();

            }
            else if (themeNumber == 3)
            {
                // ORANGE
                backGroundLoopTime = 29000;
                NDC.NStyle.Container.Colors.ORANGE_NightSky();
                me_backgr.Source = new Uri("Backgrounds\\3.mp4", UriKind.Relative);
                me_backgr.Play();

            }
            else if (themeNumber == 4)
            {
                // BLUE
                backGroundLoopTime = 29000;
                NDC.NStyle.Container.Colors.BLUE_NightSky();
                me_backgr.Source = new Uri("Backgrounds\\4.mp4", UriKind.Relative);
                me_backgr.Play();
            }
            InitializeControls();
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
        private void PlayRandom()
        {
            try
            {
                int indexToPlay = new Random().Next(0, lb_tracks.Items.Count);

                foreach (var item in currentPlayList.Values)
                {
                    if (item.Key == currentPlayList.Keys[indexToPlay])
                    {
                        LoadMp3File(item.Key);
                    }
                }
                lb_tracks.SelectedIndex = indexToPlay;
            }
            catch { }
        }
        private void ShowVolume(uint val)
        {
            prgbar_progress.SetValue(val);
            shouldShowVolume = true;
        }
        #endregion
        #region Event Functions
        #region MEPlay Events
        private void Me_player_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (shouldRepeat)
            {
                me_player.Position = TimeSpan.FromMilliseconds(0);
                me_player.Play();
                return;
            }
            else if (shouldShuffle)
            {
                PlayRandom();
            }
            else
            {
                Btn_next_Click(null, null);
            }
        }
        #endregion
        #region Worker Events
        private void VolumeTimer_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (shouldShowVolume)
            {
                prgbar_progress.Opacity += 0.02F;
                if (prgbar_progress.Opacity > 1)
                {
                    prgbar_progress.Opacity = 1;
                }
                volumeAccum++;
                if (volumeAccum > 300)
                {
                    shouldShowVolume = false;
                    volumeAccum = 0;
                }
            }
            else
            {
                prgbar_progress.Opacity -= 0.02;
                if (prgbar_progress.Opacity < 0)
                {
                    prgbar_progress.Opacity = 0;
                }
            }
        }
        private void BackgroundTimer_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {

            if (me_backgr.Position.TotalMilliseconds >= backGroundLoopTime)
            {
                me_backgr.Stop();
                me_backgr.Position = TimeSpan.FromMilliseconds(10);
                me_backgr.Play();
            }
        }
        private void OpacityWorker_WorkerInterval(object sender, NDC.NDynamics.Arguments.AsyncWorkerArgs e)
        {
            if (opacityMod == 0 && opacityCmd != 3)
            {
                Opacity += 0.02;
                if (Opacity >= 1)
                {
                    Opacity = 1;
                    opacityCmd = 3;
                }
            }
            else if (opacityMod == 1 && opacityCmd != 3)
            {
                Opacity -= 0.02;
                if (Opacity <= 0)
                {
                    // Resize
                    if (opacityCmd == 0)
                    {
                        WindowState = nextState;
                        opacityMod = 0;
                    }
                    // Minimize
                    else if (opacityCmd == 1)
                    {
                        WindowState = WindowState.Minimized;
                        opacityMod = 0;
                    }
                    // Exit
                    else if (opacityCmd == 2)
                    {
                        Application.Current.Shutdown();
                    }
                    Opacity = 0;
                }
            }
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
        #region Window Events
        private void MainWindow_Maximizer(object sender, MouseButtonEventArgs e)
        {
            double y = Mouse.GetPosition(Application.Current.MainWindow).Y;
            if (y < 50)
            {
                if (WindowState == WindowState.Maximized)
                {
                    opacityCmd = 0;
                    opacityMod = 1;
                    nextState = WindowState.Normal;
                }
                else
                {
                    opacityCmd = 0;
                    opacityMod = 1;
                    nextState = WindowState.Maximized;
                }
            }
        }
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
            if (canTakeInput)
            {
                canTakeInput = false;
                Input i = new Input(this);
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

                btn_play.IsEnabled = true;
                btn_play.Visibility = Visibility.Visible;

                btn_pause.IsEnabled = false;
                btn_pause.Visibility = Visibility.Hidden;
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

                btn_play.IsEnabled = true;
                btn_play.Visibility = Visibility.Visible;

                btn_pause.IsEnabled = false;
                btn_pause.Visibility = Visibility.Hidden;
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
            opacityCmd = 1;
            opacityMod = 1;
        }
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            opacityCmd = 2;
            opacityMod = 1;

        }
        private void Btn_repeat_Click(object sender, RoutedEventArgs e)
        {
            shouldRepeat = !shouldRepeat;
            btn_repeat.isActive = !btn_repeat.isActive;
            btn_repeat.Update();
        }
        private void Btn_shuffle_Click(object sender, RoutedEventArgs e)
        {
            shouldShuffle = !shouldShuffle;
            btn_shuffle.isActive = !btn_shuffle.isActive;
            btn_shuffle.Update();
        }
        private void Btn_volUp_Click(object sender, RoutedEventArgs e)
        {
            me_player.Volume += 0.05F;
            if (me_player.Volume > 1)
            {
                me_player.Volume = 1;
            }
            ShowVolume((uint)(me_player.Volume * 100));
        }
        private void Btn_volDown_Click(object sender, RoutedEventArgs e)
        {
            me_player.Volume -= 0.05F;
            if (me_player.Volume < 0)
            {
                me_player.Volume = 0;
            }
            ShowVolume((uint)(me_player.Volume * 100));
        }

        #endregion
        #endregion
    }
}
