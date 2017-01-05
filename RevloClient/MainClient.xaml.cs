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

namespace RevloClient
{
    /// <summary>
    /// Lógica de interacción para MainClient.xaml
    /// </summary>
    public partial class MainClientWindow : Window
    {
        String revloAPIKey;
        LoginWindow loginWindow;
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
    }
}
