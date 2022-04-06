using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// do cost type trzeba zbudować słownik id:value na podstawie danych pobranych z bazy - GetCostTypes()
/// metody napisane z palca, nic nie jest przetestowane
/// </summary>

namespace Ek_spedycja.DBAccess {
    internal class CostDataAccess : DataAccess<Cost> {
        public override bool DeleteData(Cost cost) {
            string delete = @"DELETE FROM spedycja.cost 
                            WHERE id_cost = @id_cost";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_cost", cost.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Cost.TABLE_NAME].Rows.Find(cost.Id);
                dataRow.Delete();

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Driver.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable GetData() {
            string select = @"SELECT id_cost as id_cost,
                              CT.name as 'Cost Type',
                              description as Description,
                              amount as Amount
                              FROM spedycja.cost AS C
                              INNER JOIN spedycja.cost_type AS CT
                              ON C.id_cost_type = CT.id_cost_type";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, base.connection);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DataTable costView = new DataTable();
                dataAdapter.Fill(costView);
                return costView;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            return new DataTable();
        }

        public override bool InsertData(Cost cost) {
            string insert = @"INSERT INTO spedycja.cost 
                            (id_route, id_cost_type, description, amount) 
                            VALUES 
                            (@id_route, @id_cost_type, @description, @amount)";
            try {
                SqlCommand command = new SqlCommand(insert, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSetChanges;
                dataAdapter.InsertCommand = command;
                command.Parameters.AddWithValue("@id_route", cost.Route.Id);
                command.Parameters.AddWithValue("@id_cost_type", cost.CostType);
                command.Parameters.AddWithValue("@description", cost.Description);
                command.Parameters.AddWithValue("@amount", cost.Amount);

                DataRow dataRow = dataSet.Tables[Cost.TABLE_NAME].NewRow();
                dataRow["id_route"] = cost.Route.Id;
                dataRow["id_cost_type"] = cost.CostType;
                dataRow["description"] = cost.Description;
                dataRow["amount"] = cost.Amount;
                dataSet.Tables[Cost.TABLE_NAME].Rows.Add(dataRow);

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Cost.TABLE_NAME);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override DataTable RunMethodAndRefresh(Func<Cost, bool> Func, Cost cost) {
            Func(cost);
            return GetData();
        }

        public override bool UpdateData(Cost cost) {
            string update = @"UPDATE spedycja.cost 
                            SET id_cost_type = @id_cost_type, description = @description, amount = @amount
                            WHERE id_cost = @id_cost";
            try {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(update, connection);
                DataSet dataSetChanges;
                dataAdapter.UpdateCommand = command;

                command.Parameters.AddWithValue("@id_route", cost.Route.Id);
                command.Parameters.AddWithValue("@id_cost_type", cost.CostType);
                command.Parameters.AddWithValue("@description", cost.Description);
                command.Parameters.AddWithValue("@amount", cost.Amount);
                SqlParameter sqlParameter = dataAdapter.UpdateCommand.Parameters.AddWithValue("@id_cost", cost.Id);

                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables[Cost.TABLE_NAME].Rows.Find(cost.Id);
                dataRow["id_route"] = cost.Route.Id;
                dataRow["id_cost_type"] = cost.CostType;
                dataRow["description"] = cost.Description;
                dataRow["amount"] = cost.Amount;

                if (dataSet.HasChanges()) {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, Cost.TABLE_NAME);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public List<Cost> GetCostTypes() {
            return new List<Cost>();
        }
    }
}
