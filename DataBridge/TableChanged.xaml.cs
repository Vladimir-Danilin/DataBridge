using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace College
{
    /// <summary>
    /// Логика взаимодействия для TableChanged.xaml
    /// </summary>
    public partial class TableChanged : Window
    {
        private readonly int numTable = index.switchIndex;
        public static string[] Tables = index.TablesName;

        private readonly Singleton singleton = Singleton.Intance;
        private readonly DBManager DBM = new DBManager();

        public static TableChanged TC { get; set; }

        public TableChanged()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            TC = this;
            if (index.Role == "Препод")
            {
                CreateQuery.IsEnabled = false;
                DeleteQuery.IsEnabled = false;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ArrayList records = DBM.GiveTable(Tables[numTable]);

            Table.ItemsSource = records;
            StatusStrip.Content = "Количество: " + records.Count;
        }

        private void backToMain_Click(object sender, RoutedEventArgs e)
        {
            backToMain.IsEnabled = false;
            MainWindow MW = new MainWindow();
            MW.Show();
            Close();
        }

        private void CreateQuery_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            singleton.rowsWhere.Clear();

            DialogCreate DC = new DialogCreate();
            DC.TableName.Content = Tables[numTable];

            var records = DBM.GiveTableInfo(Tables[numTable]);
            for (int i = 1; i < records.Count; i++)
            {
                singleton.rowsWhere.Add(new Item { text = (string)records[i] });
            }

            DC.where.DataContext = singleton.rowsWhere;
            DC.Show();
        }

        private void UpdateQuery_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            singleton.rowsWhere.Clear();

            DialogUpdate DU = new DialogUpdate();
            DU.TableName.Content = Tables[numTable];

            DataGridCellInfo selectedcell = Table.SelectedCells[0];
            TextBlock content = (TextBlock)selectedcell.Column.GetCellContent(selectedcell.Item);

            int index = Convert.ToInt32(content.Text);
            string column = (string)Table.Columns[0].Header;

            var records = DBM.GiveTableInfo(Tables[numTable]);
            for (int i = 1; i < records.Count; i++)
            {
                singleton.rowsWhere.Add(new Item { text = (string)records[i] });
            }

            DU.id = index;
            DU.colname = column;

            DU.where.DataContext = singleton.rowsWhere;
            DU.Show();
        }

        private void DeleteQuery_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Внимание пожарная тревога", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                DataGridCellInfo selectedcell = Table.SelectedCells[0];
                TextBlock content = (TextBlock)selectedcell.Column.GetCellContent(selectedcell.Item);

                int index = Convert.ToInt32(content.Text);
                string column = (string)Table.Columns[0].Header;

                DBM.DeleteValues(tableName: Tables[numTable], condition: $"{column} = {index}");
                Table.ItemsSource = DBM.GiveTable(Tables[numTable]);
            }
        }
    }
}