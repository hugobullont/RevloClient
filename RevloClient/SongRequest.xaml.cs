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
using TwitchLib;
using TwitchLib.Events.Client;

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
        TwitchClient client;
        RevloRepository revloRepo;

        public SongRequest()
        {
            InitializeComponent();
        }

        public SongRequest(int srRedID, String revloKey, String ircKey, String chatUsername, String channel, LoginWindow login)
        {
            this.srRedID = srRedID;
            this.revloKey = revloKey;
            this.ircKey = ircKey;
            this.chatUsername = chatUsername.ToLower();
            this.channel = channel.ToLower();
            this.login = login;
            revloRepo = new RevloRepository();
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
            client = new TwitchClient(new TwitchLib.Models.Client.ConnectionCredentials(chatUsername, ircKey), channel);
            client.OnConnected += clientConnected;
            client.OnJoinedChannel += clientJoinedChannel;
            client.OnMessageSent += clientMessageSent;
            client.Connect();
            songsDataGrid.ItemsSource = songList;

            if(songList.Count==0)
            {
                songsDataGrid.Visibility = Visibility.Collapsed;
                labelAdvice.Visibility = Visibility.Visible;
            }
            else
            {
                songsDataGrid.Visibility = Visibility.Visible;
                labelAdvice.Visibility = Visibility.Collapsed;
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

            if (songList.Count == 0)
            {
                songsDataGrid.Visibility = Visibility.Collapsed;
                labelAdvice.Visibility = Visibility.Visible;
            }
            else
            {
                songsDataGrid.Visibility = Visibility.Visible;
                labelAdvice.Visibility = Visibility.Collapsed;
            }

            SentSongRequests(songList);
        }

        public void SetLabelConsoleContent(String content)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            new Action(() => this.labelConsole.Content = content));
        }

        public void clientConnected(object sender, OnConnectedArgs e)
        {
            String content = "Client connected to Twitch!";
            SetLabelConsoleContent(content);
    
        }

        public void clientJoinedChannel(object sender, OnJoinedChannelArgs e)
        {

            String content = "Client joined channel: " +e.Channel;
            SetLabelConsoleContent(content);
        }

        public void clientMessageSent(object sender, OnMessageSentArgs e)
        {
            String content = "SongRequest sended: " + e.SentMessage.Message;
            SetLabelConsoleContent(content);
        }

        public void SentSongRequests(List<Song> songList)
        {
            foreach(Song temp in songList)
            {
                String request = temp.song.ToLower();
                if (!(request.Contains("gemidos")||request.Contains("porno")||request.Contains("antaurus")||request.Contains("cachame")))
                {
                    client.SendMessage("!songs request " + request);
                }
                
                revloRepo.SetRedemptionCompleted(revloKey, temp.redemption_id);
            }
        }

    }
}
