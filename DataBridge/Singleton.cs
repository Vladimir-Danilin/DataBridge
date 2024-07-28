using System.Collections.ObjectModel;

namespace College
{
    public class Singleton
    {
        private static Singleton _intance;
        private ObservableCollection<Item> _rowsWhere;
        public ObservableCollection<Item> rowsWhere
        {
            get { return _rowsWhere; }
            set
            {
                if (_rowsWhere != value)
                {
                    _rowsWhere = value;
                }
            }
        }
        private Singleton()
        {
            rowsWhere = new ObservableCollection<Item>();
        }
        public static Singleton Intance
        {
            get
            {
                if (_intance == null)
                {
                    _intance = new Singleton();
                }
                return _intance;
            }
        }
    }
    public class Item
    {
        public string text { get; set; }
        public string formul { get; set; }
    }
}
