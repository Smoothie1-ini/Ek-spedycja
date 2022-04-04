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
        public override bool DeleteData(Route value) {
            throw new NotImplementedException();
        }

        public override bool InsertData(Route route) {
            string insert = @"INSERT INTO [spedycja].[route] ([id_driver], [id_vehicle], [departure_date], 
                            [planed_arrival_date], [actual_arrival_date], [length], [bid])
                            VALUES
                            (@id_driver, @id_vehicle, @departure_date, @planned_arrival_date, @actual_arrival_date, @length, @bid)";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@id_driver", route.Driver.Id);
                command.Parameters.AddWithValue("@id_vehicle", route.Vehicle.Id);
                command.Parameters.AddWithValue("@departure_date", route.DepartureDate);
                command.Parameters.AddWithValue("@planned_arrival_date", route.PlannedArrivalDate);
                command.Parameters.AddWithValue("@actual_arrival_date", route.ActualArrivalDate);
                command.Parameters.AddWithValue("@length", route.Length);
                command.Parameters.AddWithValue("@bid", route.Bid);

                DataRow dataRow = dataSet.Tables[route.tableName].NewRow();
                dataRow["id_driver"] = route.Driver.Id;
                dataRow["id_vehicle"] = route.Vehicle.Id;
                dataRow["departure_date"] = route.DepartureDate;
                dataRow["planned_arrival_date"] = route.PlannedArrivalDate;
                dataRow["actual_arrival_date"] = route.ActualArrivalDate;
                dataRow["length"] = route.Length;
                dataRow["bid"] = route.Bid;
                dataSet.Tables[route.tableName].Rows.Add(dataRow);

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, route.tableName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable RefreshView() {
            DataTable routeView = new DataTable();
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(@"SELECT spedycja.route.id_route as ID, spedycja.driver.name + N' ' + spedycja.driver.surname AS Driver, 
                                                                spedycja.vehicle.brand + N' ' + spedycja.vehicle.model + N' ' + spedycja.vehicle.number AS Vehicle, 
                                                                spedycja.route.departure_date as 'Departure date',
                                                                spedycja.route.planed_arrival_date as 'Planed arrival date', 
                                                                spedycja.route.actual_arrival_date as 'Actual arrival date', 
                                                                spedycja.route.length as Length, 
                                                                spedycja.route.bid as Bid
                                                                FROM spedycja.route 
                                                                INNER JOIN spedycja.driver ON spedycja.route.id_driver = spedycja.driver.id_driver 
                                                                INNER JOIN spedycja.vehicle ON spedycja.route.id_vehicle = spedycja.vehicle.id_vehicle", base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(routeView);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return routeView;
        }

        public override DataTable RunCommandAndRefresh(Func<Route, bool> Func, Route route) {
            Func(route);
            return RefreshView();
        }

        public override bool UpdateData(Route value) {
            throw new NotImplementedException();
        }
    }
}
