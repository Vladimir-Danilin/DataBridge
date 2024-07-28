using DataBridge;
using System.Windows;
using System.Windows.Controls;

namespace College
{
    /// <summary>
    /// Логика взаимодействия для Registry.xaml
    /// </summary>
    public partial class Registry : Window
    {
        ColledgeEntities model = new ColledgeEntities();

        public Registry()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextLogin.Visibility = Visibility.Hidden;
            if(Login.Text == "")
            {
                TextLogin.Visibility = Visibility.Visible;
            }
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextPassword.Visibility = Visibility.Hidden;
            if (Password.Password == "")
            {
                TextPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in model.DB_Users)
            {
                if (Login.Text == user.Login && Password.Password == user.Password)
                {
                    index.Role = user.Role;
                    MainWindow MW = new MainWindow();
                    MW.Show();
                    Close();
                }
            }
        }
    }
}
