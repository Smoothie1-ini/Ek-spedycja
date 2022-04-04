using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ek_spedycja.DBAccess
{
    class VehicleDataAccess : DataAccess<Vehicle>
    {
        public override bool DeleteData(Vehicle vehicle)
        {
            string delete = @"DELETE FROM spedycja.vehicle 
                            WHERE id_vehicle = @id_vehicle";
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_vehicle", vehicle.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[vehicle.tableName].Rows.Find(vehicle.Id);
                dataRow.Delete();

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, vehicle.tableName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override bool InsertData(Vehicle vehicle)
        {
            string insert = @"INSERT INTO spedycja.vehicle 
                            (brand, model, number, service_date, is_available) 
                            VALUES 
                            (@brand, @model, @number, @service_date, @is_available)";
            try
            {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@brand", vehicle.Brand);
                command.Parameters.AddWithValue("@model", vehicle.Model);
                command.Parameters.AddWithValue("@number", vehicle.Number);
                command.Parameters.AddWithValue("@service_date", vehicle.ServiceDate);
                command.Parameters.AddWithValue("@is_available", vehicle.IsAvailable);

                DataRow dataRow = dataSet.Tables[vehicle.tableName].NewRow();
                dataRow["brand"] = vehicle.Brand;
                dataRow["model"] = vehicle.Model;
                dataRow["number"] = vehicle.Number;
                dataRow["service_date"] = vehicle.ServiceDate;
                dataRow["is_available"] = vehicle.IsAvailable;
                dataSet.Tables[vehicle.tableName].Rows.Add(dataRow);

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, vehicle.tableName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable RefreshView()
        {
            DataTable driverView = new DataTable();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT id_vehicle as ID, brand as Brand, model as Model, number as Number, service_date as 'Last service date', is_available as 'Availability' FROM spedycja.vehicle", base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(driverView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return driverView;
        }

        public override DataTable RunCommandAndRefresh(Func<Vehicle, bool> Func, Vehicle vehicle)
        {
            Func(vehicle);
            return RefreshView();
        }

        public override bool UpdateData(Vehicle vehicle)
        {
            string update = @"UPDATE spedycja.vehicle 
                            SET brand = @brand, model = @model, number = @number, service_date = @service_date, is_available = @is_available
                            WHERE id_vehicle = @id_vehicle";
            try
            {
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

                DataRow dataRow = dataSet.Tables[vehicle.tableName].Rows.Find(vehicle.Id);
                dataRow["brand"] = vehicle.Brand;
                dataRow["model"] = vehicle.Model;
                dataRow["number"] = vehicle.Number;
                dataRow["service_date"] = vehicle.ServiceDate;
                dataRow["is_available"] = vehicle.IsAvailable;

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, vehicle.tableName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }
    }
}
