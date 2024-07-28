using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace College
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (index.Role == "Диспечер")
            {
                TableSwitch.SelectedIndex = 0;
                GoToChanged.IsEnabled = false;
            }
            else if (index.Role == "Класс_рук.")
            {
                TableSwitch.SelectedIndex = 5;
                GoToChanged.IsEnabled = false;

                foreach (ComboBoxItem i in TableSwitch.Items)
                {
                    if ((string)i.Content == "Marks" || (string)i.Content == "Student")
                    {
                        i.IsEnabled = true;
                    }
                    else
                    {
                        i.IsEnabled = false;
                    }
                }
            }
            else if (index.Role == "Студент")
            {
                TableSwitch.SelectedIndex = 5;
                GoToChanged.IsEnabled = false;

                foreach (ComboBoxItem i in TableSwitch.Items)
                {
                    if ((string)i.Content == "Marks")
                    {
                        i.IsEnabled = true;
                    }
                    else
                    {
                        i.IsEnabled = false;
                    }
                }
            }
        }

        private void GoToChanged_Click(object sender, RoutedEventArgs e)
        {
            GoToChanged.IsEnabled = false;
            TableChanged TC = new TableChanged();
            TC.Show();
            this.Close();
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DBManager TH = new DBManager();
            var records = new ArrayList();

            if (index.Role == "Препод")
            {
                foreach (ComboBoxItem i in TableSwitch.Items)
                {
                    if ((string)i.Content != "Marks")
                    {
                        GoToChanged.IsEnabled = false;
                    }
                }
            }

            switch (TableSwitch.SelectedIndex)
            {
                case 0:
                    index.switchIndex = 0;
                    records = TH.GiveTable("Dist");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 1:
                    index.switchIndex = 1;
                    records = TH.GiveTable("Expelled_students");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 2:
                    index.switchIndex = 2;
                    records = TH.GiveTable("GPA");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 3:
                    index.switchIndex = 3;
                    records = TH.GiveTable("Groups");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 4:
                    index.switchIndex = 4;
                    records = TH.GiveTable("Kat");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 5:
                    index.switchIndex = 5;
                    records = TH.GiveTable("Marks");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    if (index.Role == "Препод")
                    {
                        GoToChanged.IsEnabled = true;
                    }
                    break;

                case 6:
                    index.switchIndex = 6;
                    records = TH.GiveTable("RUP");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 7:
                    index.switchIndex = 7;
                    records = TH.GiveTable("Spec");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 8:
                    index.switchIndex = 8;
                    records = TH.GiveTable("Students");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;

                case 9:
                    index.switchIndex = 9;
                    records = TH.GiveTable("Trainer");
                    dgvData.ItemsSource = records;
                    StatusStrip.Content = "Количество: " + records.Count;
                    break;
            }

        }
    }
}
