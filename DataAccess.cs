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
            SqlDataAdapter dataAdapter;
            try {
                dataSet = new DataSet();
                List<string> tables = new List<string> { "driver", "vehicle", "route", "salary", "cost" };
                List<string> views = new List<string> { "v_driver", "v_vehicle", "v_route", "v_salary", "v_cost" };
                foreach (string table in tables) {
                    dataAdapter = new SqlDataAdapter($"SELECT * FROM spedycja.{table}", connectionString);
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataSet, table);
                }
                foreach (string view in views) {
                    dataAdapter = new SqlDataAdapter($"SELECT * FROM spedycja.dbo.{view}", connectionString);
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataSet, view);
                }
            } catch (Exception) {
                return false;
            }
            return true;
        }

        public bool InsertData(Driver driver) {
            string select = "SELECT * FROM spedycja.driver";
            string insert = @"INSERT INTO spedycja.driver 
                            (name, surname, pesel, birth_date, hire_date) 
                            VALUES 
                            ('@name', '@surname', '@pesel', '@birthDate', '@hireDate')";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, connection);
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.Add("@name", SqlDbType.VarChar, 100, "name");
                command.Parameters.Add("@surname", SqlDbType.VarChar, 100, "surname");
                command.Parameters.Add("@pesel", SqlDbType.VarChar, 100, "pesel");
                command.Parameters.Add("@birthDate", SqlDbType.Date, 100, "birth_date");
                command.Parameters.Add("@hireDate", SqlDbType.Date, 100, "hire_date");

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

        public bool UpdateData(Driver driver) {
            string select = "";
            string update = "";

            try {
                SqlCommand command = new SqlCommand(update, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, connection);
                dataAdapter.UpdateCommand = command;
                command.Parameters.Add("@name", SqlDbType.VarChar, 100, "name");
                command.Parameters.Add("@surname", SqlDbType.VarChar, 100, "")
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 100, "id_driver");

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
