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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevloClient
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String key = txtKey.Text;
            MainClientWindow mainClient = new MainClientWindow(key,this);
            AuthRepository auth = new AuthRepository();
            if(auth.Validate(key))
            {
                this.Hide();
                mainClient.Show();
            }
            else
            {
                MessageBox.Show("You entered an incorrect API Key.");
            }
            
        }
    }
}
