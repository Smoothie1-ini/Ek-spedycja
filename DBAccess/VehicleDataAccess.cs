using Ek_spedycja.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ek_spedycja.DBAccess {
    class VehicleDataAccess : DataAccess<Vehicle> {
        public override bool DeleteData(Vehicle vehicle) {
            string delete = @"DELETE FROM spedycja.vehicle 
                            WHERE id_vehicle = @id_vehicle";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_vehicle", vehicle.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Vehicle.TABLE_NAME].Rows.Find(vehicle.Id);
                dataRow.Delete();

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Vehicle.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override bool InsertData(Vehicle vehicle) {
            string insert = @"INSERT INTO spedycja.vehicle 
                            (brand, model, number, service_date, is_available) 
                            VALUES 
                            (@brand, @model, @number, @service_date, @is_available)";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@brand", vehicle.Brand);
                command.Parameters.AddWithValue("@model", vehicle.Model);
                command.Parameters.AddWithValue("@number", vehicle.Number);
                command.Parameters.AddWithValue("@service_date", vehicle.ServiceDate);
                command.Parameters.AddWithValue("@is_available", vehicle.IsAvailable);

                DataRow dataRow = dataSet.Tables[Vehicle.TABLE_NAME].NewRow();
                dataRow["brand"] = vehicle.Brand;
                dataRow["model"] = vehicle.Model;
                dataRow["number"] = vehicle.Number;
                dataRow["service_date"] = vehicle.ServiceDate;
                dataRow["is_available"] = vehicle.IsAvailable;
                dataSet.Tables[Vehicle.TABLE_NAME].Rows.Add(dataRow);

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Vehicle.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable GetData() {
            string select = @"SELECT id_vehicle as id_vehicle, 
                            brand as Brand, 
                            model as Model, 
                            number as Number, 
                            service_date as 'Last service date', 
                            is_available as 'Availability' 
                            FROM spedycja.vehicle";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataTable driverView = new DataTable();
                dataAdapter.Fill(driverView);
                return driverView;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return new DataTable();
        }

        public override DataTable RunMethodAndRefresh(Func<Vehicle, bool> Func, Vehicle vehicle) {
            Func(vehicle);
            return GetData();
        }

        public override bool UpdateData(Vehicle vehicle) {
            string update = @"UPDATE spedycja.vehicle 
                            SET brand = @brand, model = @model, number = @number, service_date = @service_date, is_available = @is_available
                            WHERE id_vehicle = @id_vehicle";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(update, connection);
                DataSet dataSetChanges;
                dataAdapter.UpdateCommand = command;

                command.Parameters.AddWithValue("@brand", vehicle.Brand);
                command.Parameters.AddWithValue("@model", vehicle.Model);
                command.Parameters.AddWithValue("@number", vehicle.Number);
                command.Parameters.AddWithValue("@service_date", vehicle.ServiceDate);
                command.Parameters.AddWithValue("@is_available", vehicle.IsAvailable);
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.AddWithValue("@id_vehicle", vehicle.Id);

                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Vehicle.TABLE_NAME].Rows.Find(vehicle.Id);
                dataRow["brand"] = vehicle.Brand;
                dataRow["model"] = vehicle.Model;
                dataRow["number"] = vehicle.Number;
                dataRow["service_date"] = vehicle.ServiceDate;
                dataRow["is_available"] = vehicle.IsAvailable;

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Vehicle.TABLE_NAME);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public DataTable RefreshComboBox() {
            DataTable vehicleTable = new DataTable();
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT id_vehicle, brand + N' ' + model + N' ' + number as 'vehicle' FROM spedycja.vehicle", base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(vehicleTable);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return vehicleTable;
        }

        public Vehicle GetVehicleById(Vehicle vehicleID) {
            DataRow dataRow = dataSet.Tables[Vehicle.TABLE_NAME].Rows.Find(vehicleID.Id);
            Vehicle vehicle = new Vehicle((int)dataRow[0], dataRow[1].ToString(), dataRow[2].ToString(), dataRow[3].ToString(), DateTime.Parse(dataRow[4].ToString()), (bool)dataRow[5]);
            return vehicle;
        }
    }
}
