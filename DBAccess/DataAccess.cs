using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ek_spedycja.DBAccess {
    abstract class DataAccess<T> {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Ek-spedycja"].ConnectionString;
        internal SqlConnection connection = new SqlConnection(connectionString);
        public static DataSet dataSet = GetDataSet();

        private static DataSet GetDataSet() {
            string select = @"SELECT name
                            FROM sys.Tables
                            WHERE name <> 'sysdiagrams'";
            DataTable dtTableNames = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, connectionString);
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dataAdapter.Fill(dtTableNames);
            try {
                dataSet = new DataSet();
                foreach (DataRow row in dtTableNames.Rows) {
                    select = $"SELECT * FROM spedycja.{row[0]}";
                    dataAdapter = new SqlDataAdapter(select, connectionString);
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataSet, row[0].ToString());
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            return dataSet;
        }

        public abstract bool InsertData(T value);
        public abstract bool UpdateData(T value);
        public abstract bool DeleteData(T value);
        public abstract DataTable RunMethodAndRefresh(Func<T, bool> Func, T value);
        public abstract DataTable GetData();
        public virtual DataTable GetData(T value) { return new DataTable(); }
    }
}
