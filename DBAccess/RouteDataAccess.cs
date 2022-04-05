using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ek_spedycja.DBAccess {
    class RouteDataAccess : DataAccess<Route> {
        public override bool DeleteData(Route route) {
            string delete = @"DELETE FROM spedycja.route 
                            WHERE id_route = @id_route";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_route", route.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Route.TABLE_NAME].Rows.Find(route.Id);
                dataRow.Delete();

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Route.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override bool InsertData(Route route) {
            string insert = @"INSERT INTO [spedycja].[route] ([id_driver], [id_vehicle], [departure_date], 
                            [planed_arrival_date], [actual_arrival_date], [length], [bid])
                            VALUES
                            (@id_driver, @id_vehicle, @departure_date, @planed_arrival_date, @actual_arrival_date, @length, @bid)";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@id_driver", route.Driver.Id);
                command.Parameters.AddWithValue("@id_vehicle", route.Vehicle.Id);
                command.Parameters.AddWithValue("@departure_date", route.DepartureDate);
                command.Parameters.AddWithValue("@planed_arrival_date", route.PlannedArrivalDate);
                command.Parameters.AddWithValue("@actual_arrival_date", route.ActualArrivalDate);
                command.Parameters.AddWithValue("@length", route.Length);
                command.Parameters.AddWithValue("@bid", route.Bid);

                DataRow dataRow = dataSet.Tables[Route.TABLE_NAME].NewRow();
                dataRow["id_driver"] = route.Driver.Id;
                dataRow["id_vehicle"] = route.Vehicle.Id;
                dataRow["departure_date"] = route.DepartureDate;
                dataRow["planed_arrival_date"] = route.PlannedArrivalDate;
                dataRow["actual_arrival_date"] = route.ActualArrivalDate;
                dataRow["length"] = route.Length;
                dataRow["bid"] = route.Bid;
                dataSet.Tables[Route.TABLE_NAME].Rows.Add(dataRow);

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Route.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable GetData() {
            string select = @"SELECT spedycja.route.id_route as id_route, 
                            spedycja.driver.name + N' ' + spedycja.driver.surname AS Driver, 
                            spedycja.vehicle.brand + N' ' + spedycja.vehicle.model + N' ' + spedycja.vehicle.number AS Vehicle, 
                            spedycja.route.departure_date as 'Departure date',
                            spedycja.route.planed_arrival_date as 'Planed arrival date', 
                            spedycja.route.actual_arrival_date as 'Actual arrival date', 
                            spedycja.route.length as Length, 
                            spedycja.route.bid as Bid,
                            spedycja.route.id_driver as id_driver,
                            spedycja.route.id_vehicle as id_vehicle
                            FROM spedycja.route 
                            INNER JOIN spedycja.driver ON spedycja.route.id_driver = spedycja.driver.id_driver 
                            INNER JOIN spedycja.vehicle ON spedycja.route.id_vehicle = spedycja.vehicle.id_vehicle";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataTable routeView = new DataTable();
                dataAdapter.Fill(routeView);
                return routeView;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return new DataTable();
        }
        
        //usuwa ale nie odświeża
        public override DataTable RunMethodAndRefresh(Func<Route, bool> Func, Route route) {
            Func(route);
            return GetData();
        }

        public override bool UpdateData(Route route) { 
            string update = @"UPDATE spedycja.route
                            SET id_driver = @id_driver, id_vehicle = @id_vehicle, departure_date = @departure_date, planed_arrival_date = @planed_arrival_date, actual_arrival_date = @actual_arrival_date, length = @length, bid = @bid
                            WHERE id_route = @id_route";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(update, connection);
                DataSet dataSetChanges;
                dataAdapter.UpdateCommand = command;

                command.Parameters.AddWithValue("@id_driver", route.Driver.Id);
                command.Parameters.AddWithValue("@id_vehicle", route.Vehicle.Id);
                command.Parameters.AddWithValue("@departure_date", route.DepartureDate);
                command.Parameters.AddWithValue("@planed_arrival_date", route.PlannedArrivalDate);
                command.Parameters.AddWithValue("@actual_arrival_date", route.ActualArrivalDate);
                command.Parameters.AddWithValue("@length", route.Length);
                command.Parameters.AddWithValue("@bid", route.Bid);
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.AddWithValue("@id_route", route.Id);

                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Route.TABLE_NAME].Rows.Find(route.Id);
                dataRow["id_driver"] = route.Driver.Id;
                dataRow["id_vehicle"] = route.Vehicle.Id;
                dataRow["departure_date"] = route.DepartureDate;
                dataRow["planed_arrival_date"] = route.PlannedArrivalDate;
                dataRow["actual_arrival_date"] = route.ActualArrivalDate;
                dataRow["length"] = route.Length;
                dataRow["bid"] = route.Bid;

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Route.TABLE_NAME);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }
    }
}
