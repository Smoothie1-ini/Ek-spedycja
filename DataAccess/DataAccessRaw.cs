using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ek_spedycja {
    class DataAccessRaw
    {
        string connectionString;
        public SqlConnection connection;
        public DataSet dataSet;

        public DataAccessRaw() {
            connectionString = ConfigurationManager.ConnectionStrings["Ek-spedycja"].ConnectionString;
            connection = new SqlConnection(connectionString);
            FillDataSet();
        }

        //DataReader czy DataSet?
        private bool FillDataSet() {
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
            } catch (Exception) {
                return false;
            }
            return true;
        }
    }
}
