using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ek_spedycja.DBAccess
{
    abstract class DataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Ek-spedycja"].ConnectionString;
        internal static SqlConnection connection =  new SqlConnection(connectionString);
        public static DataSet dataSet = GetDataSet();


        private static DataSet GetDataSet() {
            string select;
            SqlDataAdapter dataAdapter;
            try {
                dataSet = new DataSet();
                List<string> tableNames = new List<string> { "driver", "vehicle", "route", "cost", "salary" };

                foreach (string tableName in tableNames) {
                    select = $"SELECT * FROM spedycja.{tableName}";
                    dataAdapter = new SqlDataAdapter(select, connectionString);
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataSet, tableName);
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            return dataSet;
        }


        public abstract bool InsertData(Object object);
        public abstract bool UpdateData(Object object);
        public abstract bool DeleteData(Object object);
    }
}
