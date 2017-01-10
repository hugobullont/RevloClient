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
using RevloClient.Entities;


namespace RevloClient
{
    /// <summary>
    /// Lógica de interacción para MainClient.xaml
    /// </summary>
    public partial class MainClientWindow : Window
    {
        String revloAPIKey;
        int srRewardId = 0;
        LoginWindow loginWindow;
        RewardResponse allRewards;
        public MainClientWindow()
        {
            InitializeComponent();
        }

        public MainClientWindow(String key, LoginWindow loginWindow)
        {
            this.revloAPIKey = key;
            this.loginWindow = loginWindow;
            InitializeComponent();
        }

        private void MainClient_Closed(object sender, EventArgs e)
        {
            this.loginWindow.Show();
        }

        private void MainClient_Loaded(object sender, RoutedEventArgs e)
        {
            RevloRepository revlo = new RevloRepository();
            allRewards = revlo.GetAllRewards(revloAPIKey);
            List<Reward> listRewards = allRewards.rewards.ToList();
            
            foreach(Reward temp in listRewards)
            {
                if(temp.title.ToLower() == "song request" || temp.title.ToLower()=="songrequest")
                {
                    srRewardId = temp.reward_id;
                }
            }

            if(srRewardId ==0)
            {
                labelAdvice.Visibility = Visibility.Visible;
                btnGoSongRequest.IsEnabled = false;

            }

            
        }

        private void btnGoSongRequest_Click(object sender, RoutedEventArgs e)
        {
            String ircKey = txtIRC.Text;
            String chatUsername = txtChatUsername.Text;
            String channel = txtChannel.Text;

            if(ValidateFields())
            {

            }
            else
            {
                MessageBox.Show("You forgot to enter some data.");
            }

        }

        bool ValidateFields()
        {     
            if (String.IsNullOrEmpty(txtIRC.Text))
            {
                return false;
            }

            if(String.IsNullOrEmpty(txtChatUsername.Text))
            {
                return false;
            }

            if(String.IsNullOrEmpty(txtChannel.Text))
            {
                return false;
            }

            return true;
        }
    }
}
