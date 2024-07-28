using System.Collections.Generic;
using System.Windows;

namespace College
{
    /// <summary>
    /// Логика взаимодействия для DialogCreate.xaml
    /// </summary>
    public partial class DialogCreate : Window
    {
        private List<string> columnValues { get; set; }
        private List<string> columnName { get; set; }

        private readonly int numTable = index.switchIndex;
        private static readonly string[] Tables = index.TablesName;

        private readonly Singleton singleton = Singleton.Intance;
        private readonly DBManager DBM = new DBManager();

        public DialogCreate()
        {
            InitializeComponent();
            where.ItemsSource = singleton.rowsWhere;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            columnName = new List<string>();
            columnValues = new List<string>();

            foreach (Item item in singleton.rowsWhere)
            {
                columnName.Add(item.text);
                columnValues.Add(item.formul);
            }

            DBM.InsertValues((string)TableName.Content, columnName, columnValues);

            MessageBox.Show("Новые значения успешно добавлены");
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            var TC = TableChanged.TC;
            TC.IsEnabled = true;
            TC.Table.ItemsSource = DBM.GiveTable(Tables[numTable]);
        }
    }
}