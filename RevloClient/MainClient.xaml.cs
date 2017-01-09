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
        RedemptionResponse allRedemptions;
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
    }
}
