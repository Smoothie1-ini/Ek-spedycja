using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ek_spedycja {
    class DataAccess {
        string connectionString;
        SqlConnection connection;
        public DataSet dataSet;

        public DataAccess() {
            connectionString = ConfigurationManager.ConnectionStrings["Ek-spedycja"].ConnectionString;
            connection = new SqlConnection(connectionString);
            FillDataSet();
        }

        public bool Connect() {
            using (SqlConnection con = new SqlConnection(connectionString)) {
                try {
                    con.Open();
                } catch (Exception) {
                    return false;
                }
                return true;
            }
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

        #region Driver

        public bool InsertData(Driver driver) {
            string insert = @"INSERT INTO spedycja.driver 
                            (name, surname, pesel, birth_date, hire_date) 
                            VALUES 
                            (@name, @surname, @pesel, @birthDate, @hireDate)";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@name", driver.Name);
                command.Parameters.AddWithValue("@surname", driver.Surname);
                command.Parameters.AddWithValue("@pesel", driver.Pesel);
                command.Parameters.AddWithValue("@birthDate", driver.BirthDate);
                command.Parameters.AddWithValue("@hireDate", driver.HireDate);

                DataRow dataRow = dataSet.Tables[driver.tableName].NewRow();
                dataRow["name"] = driver.Name;
                dataRow["surname"] = driver.Surname;
                dataRow["pesel"] = driver.Pesel;
                dataRow["birth_date"] = driver.BirthDate;
                dataRow["hire_date"] = driver.HireDate;
                dataSet.Tables[driver.tableName].Rows.Add(dataRow);

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public bool UpdateData(Driver driver) {
            string update = @"UPDATE spedycja.driver 
                            SET name = @name, surname = @surname, pesel = @pesel, birth_date = @birth_date, hire_date = @hire_date 
                            WHERE id_driver = @id_driver";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(update, connection);
                DataSet dataSetChanges;
                dataAdapter.UpdateCommand = command;

                command.Parameters.AddWithValue("@name", driver.Name);
                command.Parameters.AddWithValue("@surname", driver.Surname);
                command.Parameters.AddWithValue("@pesel", driver.Pesel);
                command.Parameters.AddWithValue("@birth_date", driver.BirthDate);
                command.Parameters.AddWithValue("@hire_date", driver.HireDate);
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.AddWithValue("@id_driver", driver.Id);

                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables["driver"].Rows.Find(driver.Id);
                dataRow["name"] = driver.Name;
                dataRow["surname"] = driver.Surname;
                dataRow["pesel"] = driver.Pesel;
                dataRow["birth_date"] = driver.BirthDate;
                dataRow["hire_date"] = driver.HireDate;

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public bool DeleteData(Driver driver) {
            string delete = @"DELETE FROM spedycja.driver 
                            WHERE id_driver = @id_driver";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_driver", driver.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables["driver"].Rows.Find(driver.Id);
                dataRow.Delete();

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        #endregion
    }
}
