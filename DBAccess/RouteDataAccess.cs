using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                            spedycja.driver.name + N' ' + spedycja.driver.surname AS Kierowca, 
                            spedycja.vehicle.brand + N' ' + spedycja.vehicle.model + N' (' + spedycja.vehicle.number + N')' AS Pojazd, 
                            spedycja.route.departure_date as 'Data wyjazdu',
                            spedycja.route.planed_arrival_date as 'Planowana data przyjazdu', 
                            spedycja.route.actual_arrival_date as 'Faktyczna data przyjazdu', 
                            spedycja.route.length as 'Długość trasy (km)', 
                            spedycja.route.bid as Należność,
                            spedycja.route.id_driver as id_driver,
                            spedycja.route.id_vehicle as id_vehicle
                            FROM spedycja.route 
                            INNER JOIN spedycja.driver ON spedycja.route.id_driver = spedycja.driver.id_driver 
                            INNER JOIN spedycja.vehicle ON spedycja.route.id_vehicle = spedycja.vehicle.id_vehicle";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataTable dtRoute = new DataTable();
                dataAdapter.Fill(dtRoute);
                return dtRoute;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return new DataTable();
        }

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

        public List<int> GetRange(string param) {
            string select = $"select MIN({param}(departure_date)), MAX({param}(departure_date)) from [spedycja].[route]";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, base.connection);
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataTable routeView = new DataTable();
            dataAdapter.Fill(routeView);
            string start_range = routeView.Rows[0][0].ToString();
            string end_range = routeView.Rows[0][1].ToString();

            if (routeView.Rows.Count > 0 && start_range != "" && end_range != "") {
                return Enumerable.Range(int.Parse(start_range), int.Parse(end_range) - int.Parse(start_range) + 1).ToList<int>();
            } else
                return new List<int>();
        }

        public DataTable GetSalaries(Driver driver = null, int month = 0, int year = 0) {
            string str_driver = driver != null ? $" AND route.id_driver = {driver.Id} " : " ";
            string str_month = month != 0 ? $" AND MONTH(route.departure_date) = {month} " : " ";
            string str_year = year != 0 ? $" AND YEAR(route.departure_date) = {year} " : " ";

            string select = $@"SET LANGUAGE polish
							SELECT driver.name + N' ' + driver.surname AS 'Kierowca',
                            DATENAME(month, Dateadd(month, MONTH(route.departure_date), -1)) AS 'Miesiąc',
                            YEAR(route.departure_date) AS 'Rok',
                            ROUND(SUM(route.bid), 2) AS 'Wypłata'
                            FROM spedycja.route
                            INNER JOIN spedycja.driver ON spedycja.route.id_driver = spedycja.driver.id_driver
                            WHERE (driver.name + N' ' + driver.surname) <> ''
                            {str_driver}
                            {str_month}
                            {str_year}
                            GROUP BY driver.name + N' ' + driver.surname, MONTH(route.departure_date), YEAR(route.departure_date)";
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
    }
}
