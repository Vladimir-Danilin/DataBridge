using System;
using System.Collections.Generic;
using System.Windows;

namespace College
{
    /// <summary>
    /// Логика взаимодействия для DialogUpdate.xaml
    /// </summary>
    public partial class DialogUpdate : Window
    {
        private List<string> columnValues { get; set; }
        private List<string> columnName { get; set; }

        private readonly int numTable = index.switchIndex;
        private readonly string[] Tables = index.TablesName;
        public int id { get; set; }
        public string colname { get; set; }

        private readonly Singleton singleton = Singleton.Intance;
        private readonly DBManager DBM = new DBManager();

        public DialogUpdate()
        {
            InitializeComponent();
            where.ItemsSource = singleton.rowsWhere;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            columnName = new List<string>();
            columnValues = new List<string>();

            foreach (Item item in singleton.rowsWhere)
            {
                columnName.Add(item.text);
                columnValues.Add(item.formul);
            }
            
            DBM.UpdateValues((string)TableName.Content, columnName, columnValues, $"{colname} = {id}");

            MessageBox.Show("Новые значения успешно добавлены");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var TC = TableChanged.TC;
            TC.IsEnabled = true;
            TC.Table.ItemsSource = DBM.GiveTable(Tables[numTable]);
        }
    }
}
