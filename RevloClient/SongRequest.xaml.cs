using RevloClient.Entities.ExtraEntities;
using RevloClient.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RevloClient
{
    /// <summary>
    /// Lógica de interacción para SongRequest.xaml
    /// </summary>
    public partial class SongRequest : Window
    {
        int srRedID;
        String revloKey;
        String ircKey;
        String chatUsername;
        String channel;
        LoginWindow login;
        List<Song> songList;

        DispatcherTimer dispatcherTimer;
        
        public SongRequest()
        {
            InitializeComponent();
        }

        public SongRequest(int srRedID, String revloKey, String ircKey, String chatUsername, String channel, LoginWindow login)
        {
            this.srRedID = srRedID;
            this.revloKey = revloKey;
            this.ircKey = ircKey;
            this.chatUsername = chatUsername;
            this.channel = channel;
            this.login = login;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
            InitializeComponent();
        }

        private void SongRequest_Loaded(object sender, RoutedEventArgs e)
        {
            SongRepository songRepo = new SongRepository();
            songList = songRepo.GetSongs(srRedID, revloKey);

            songsDataGrid.ItemsSource = songList;

            if(songList.Count==0)
            {
                songsDataGrid.Visibility = Visibility.Collapsed;
                labelAdvice.Visibility = Visibility.Visible;
            }
        }

        private void SongRequestWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SongRepository songRepo = new SongRepository();
            songList = songRepo.GetSongs(srRedID, revloKey);
            songsDataGrid.ItemsSource = songList;
        }

    }
}
