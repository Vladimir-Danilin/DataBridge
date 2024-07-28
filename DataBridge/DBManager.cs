using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace College
{
    internal class DBManager
    {
        private protected static SqlConnection _con = new SqlConnection();
        private protected static DataTable _sqlBD = new DataTable("Colledge");
        private protected static SqlCommand _cmd = new SqlCommand();
        public DBManager()
        {
            _con.ConnectionString = DataBridge.Properties.Settings.Default.ConString;
        }

        public ArrayList GiveTable(string Table)
        {
            _cmd.Connection = _con;
            _cmd.CommandType = System.Data.CommandType.Text;
            _cmd.CommandText = "SELECT * FROM " + Table;

            _con.Open();

            SqlDataReader rdr = _cmd.ExecuteReader();
            ArrayList records = new ArrayList();
            if (rdr.HasRows)
            {
                foreach (DbDataRecord rec in rdr)
                {
                    records.Add(rec);
                }
            }
            _con.Close();
            return records;
        }
        public ArrayList GiveTableInfo(string Table)
        {
            _cmd.Connection = _con;
            _cmd.CommandType = System.Data.CommandType.Text;
            _cmd.CommandText = "SELECT * FROM " + Table;

            _con.Open();

            SqlDataReader rdr = _cmd.ExecuteReader();

            ArrayList info = new ArrayList();
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                info.Add(rdr.GetName(i));
            }
            _con.Close();
            return info;
        }

        public void InsertValues(string tableName, List<string> columnNames, List<string> columnValues)
        {
            string strColumnNames = string.Join(",", columnNames);
            string strColumnValues = string.Join(",", columnValues.Select(column => $"'{column}'"));

            if (columnNames.Count != columnValues.Count)
                throw new Exception("Количество столбцов не совпадает с количством значений");

            string sqlQuery = $"INSERT INTO {tableName} ({strColumnNames}) VALUES ({strColumnValues})";
            _con.Open();
            SqlCommand command = _con.CreateCommand();

            try
            {
                command.CommandText = sqlQuery;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(_sqlBD);
                sda.Dispose();
            }
            finally
            {
                command.Dispose();
                _con.Close();
            }
        }

        public void UpdateValues(string tableName, List<string> columnNames, List<string> columnValues, string condition)
        {
            if (columnNames.Count != columnValues.Count)
                throw new Exception("Количество столбцов не совпадает с количством значений");

            string setClause = "";
            for (int i = 0; i < columnNames.Count; i++)
            {
                setClause += columnNames[i] + " = ";
                setClause += "'" + columnValues[i] + "', ";
            }
            setClause = setClause.Remove(setClause.Length - 2, 2);

            string sqlQuery = $"UPDATE {tableName} SET {setClause} WHERE {condition}";

            _con.Open();
            SqlCommand command = _con.CreateCommand();

            try
            {
                command.CommandText = sqlQuery;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(_sqlBD);
                sda.Dispose();
            }
            finally
            {
                command.Dispose();
                _con.Close();
            }
        }

        public void DeleteValues(string tableName, string condition)
        {
            string sqlQuery = $"DELETE FROM {tableName} WHERE {condition}";

            _con.Open();
            SqlCommand command = _con.CreateCommand();

            try
            {
                command.CommandText = sqlQuery;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(_sqlBD);
                sda.Dispose();
            }
            finally
            {
                command.Dispose();
                _con.Close();
            }
        }
    }
}
