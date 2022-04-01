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
    class DriverDataAccess : DataAccess<Driver>
    {
        public override bool DeleteData(Driver driver)
        {
            string delete = @"DELETE FROM spedycja.driver 
                            WHERE id_driver = @id_driver";
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(delete, connection);
                DataSet dataSetChanges;
                dataAdapter.DeleteCommand = command;

                SqlParameter sqlParameter = dataAdapter.DeleteCommand.Parameters.AddWithValue("@id_driver", driver.Id);
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.SourceVersion = DataRowVersion.Original;

                DataRow dataRow = dataSet.Tables["driver"].Rows.Find(driver.Id);
                dataRow.Delete();

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override bool InsertData(Driver driver)
        {
            string insert = @"INSERT INTO spedycja.driver 
                            (name, surname, pesel, birth_date, hire_date) 
                            VALUES 
                            (@name, @surname, @pesel, @birthDate, @hireDate)";
            try
            {
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

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }
            return true;
        }

        public override bool UpdateData(Driver driver)
        {
            string update = @"UPDATE spedycja.driver 
                            SET name = @name, surname = @surname, pesel = @pesel, birth_date = @birth_date, hire_date = @hire_date 
                            WHERE id_driver = @id_driver";
            try
            {
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

                if (dataSet.HasChanges())
                {
                    dataSetChanges = dataSet.GetChanges();
                    if (dataSet.HasErrors)
                        dataSet.RejectChanges();
                    else
                        dataAdapter.Update(dataSet, "driver");
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
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT id_driver as ID, name as Name, surname as Surname, pesel as PESEL, hire_date as 'Date of employment' , birth_date as 'Date of birth' FROM spedycja.driver", base.connection);
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dataAdapter.Fill(driverView);
            return driverView;
        }

        public override DataTable RefreshViewAfterCommand(Func<Driver, bool> DriverFunc, Driver driver)
        {
            DriverFunc(driver);
            return RefreshView();
        }
    }
}
