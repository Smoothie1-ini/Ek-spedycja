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
                        dataAdapter.Update(dataSet, driver.tableName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public bool UpdateData(Driver driver, int selectedDriver) {
            string update = @"update spedycja.driver
                              set name = @name, surname = @surname, pesel = @pesel, birth_date = @birthDate, hire_date = @hireDate
                              where id_driver = @id_driver";
            try {
                SqlCommand command = new SqlCommand(update, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.UpdateCommand = command;
                command.Parameters.AddWithValue("@name", driver.Name);
                command.Parameters.AddWithValue("@surname", driver.Surname);
                command.Parameters.AddWithValue("@pesel", driver.Pesel);
                command.Parameters.AddWithValue("@birthDate", driver.BirthDate);
                command.Parameters.AddWithValue("@hireDate", driver.HireDate);
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.AddWithValue("@id_driver", driver.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                dataSet.Tables[driver.tableName].Rows[selectedDriver]["name"] = driver.Name;
                dataSet.Tables[driver.tableName].Rows[selectedDriver]["surname"] = driver.Surname;
                dataSet.Tables[driver.tableName].Rows[selectedDriver]["pesel"] = driver.Pesel;
                dataSet.Tables[driver.tableName].Rows[selectedDriver]["birth_date"] = driver.BirthDate;
                dataSet.Tables[driver.tableName].Rows[selectedDriver]["hire_date"] = driver.HireDate;

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, driver.tableName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        //public bool InsertData(Vehicle vehicle) {
        //    try {
        //        DataRow dataRow = dataSet.Tables[vehicle.tableName].NewRow();
        //        dataRow[1] = vehicle.Brand;
        //        dataRow[2] = vehicle.Model;
        //        dataRow[3] = vehicle.Number;
        //        dataRow[4] = vehicle.ServiceDate;
        //        dataRow[5] = vehicle.IsAvailable;
        //        dataSet.Tables[vehicle.tableName].Rows.Add(dataRow);
        //        dataAdapter.Update(dataSet, vehicle.tableName);
        //    } catch (Exception ex) {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //public bool InsertData(Route route) {
        //    try {
        //        DataRow dataRow = dataSet.Tables[route.tableName].NewRow();
        //        dataRow[1] = route.Driver;
        //        dataRow[2] = route.Vehicle;
        //        dataRow[3] = route.DepartureDate;
        //        dataRow[4] = route.PlannedArrivalDate;
        //        dataRow[5] = route.ActualArrivalDate;
        //        dataRow[6] = route.Length;
        //        dataRow[7] = route.Bid;
        //        dataSet.Tables[route.tableName].Rows.Add(dataRow);
        //        dataAdapter.Update(dataSet, route.tableName);
        //    } catch (Exception ex) {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //public bool InsertData(Cost cost) {
        //    try {
        //        DataRow dataRow = dataSet.Tables[cost.tableName].NewRow();
        //        dataRow[1] = cost.CostType;
        //        dataRow[2] = cost.Description;
        //        dataRow[3] = cost.Amount;
        //        dataSet.Tables[cost.tableName].Rows.Add(dataRow);
        //        dataAdapter.Update(dataSet, cost.tableName);
        //    } catch (Exception ex) {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}
    }
}
