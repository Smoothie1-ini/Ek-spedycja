using Ek_spedycja.DBAccess;
using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Ek_spedycja {
    public partial class MainPanel : Form {
        private DriverDataAccess driverDataAccess = new DriverDataAccess();
        Driver driver;
        int selectedDriverId;

        private VehicleDataAccess vehicleDataAccess = new VehicleDataAccess();
        Vehicle vehicle;
        int selectedVehicleId;

        private RouteDataAccess routeDataAccess = new RouteDataAccess();
        Route route;
        int selectedRouteId;
        Driver selectedDriver;
        int selectedMonth;
        int selectedYear;

        public MainPanel() {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
        }

        private void Form1_Load(object sender, EventArgs e) {
            FormConfiguration();
            ComboBoxHandlers();
        }
            

        private void FormConfiguration() {
            numericUpDownRouteLength.Maximum = decimal.MaxValue;
            numericUpDownRouteLength.Minimum = 0;
        }

        #region DRIVER

        private void tabPageDriver_Enter(object sender, EventArgs e) {
            dataGridViewDriver.DataSource = driverDataAccess.GetData();
            dataGridViewDriver.Columns[0].Visible = false;
        }

        private void buttonDriverAdd_Click(object sender, EventArgs e) {
            driver = new Driver(textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.InsertData, driver);
        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriverId, textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.UpdateData, driver);
        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriverId);
            dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.DeleteData, driver);
        }

        private void dataGridViewDriver_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewDriver.SelectedRows.Count > 0) {
                selectedDriverId = int.Parse(dataGridViewDriver.SelectedRows[0].Cells[0].Value.ToString());
                textBoxDriverName.Text = dataGridViewDriver.SelectedRows[0].Cells[1].Value.ToString();
                textBoxDriverSurname.Text = dataGridViewDriver.SelectedRows[0].Cells[2].Value.ToString();
                textBoxDriverPesel.Text = dataGridViewDriver.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePickerDriverBirthDate.Value = DateTime.Parse(dataGridViewDriver.SelectedRows[0].Cells[4].Value.ToString());
                dateTimePickerDriverHireDate.Value = DateTime.Parse(dataGridViewDriver.SelectedRows[0].Cells[5].Value.ToString());
            }
        }

        #endregion

        #region VEHICLE

        private void tabPageVehicle_Enter(object sender, EventArgs e) {
            dataGridViewVehicle.DataSource = vehicleDataAccess.GetData();
            dataGridViewVehicle.Columns[0].Visible = false;
        }

        private void buttonVehicleAdd_Click(object sender, EventArgs e) {
            vehicle = new Vehicle(textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, radioButtonVehicleAvailable.Checked);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.InsertData, vehicle);
        }

        private void buttonVehicleEdit_Click(object sender, EventArgs e) {
            vehicle = new Vehicle(selectedVehicleId, textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, radioButtonVehicleAvailable.Checked);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.UpdateData, vehicle);
        }

        private void buttonVehicleDelete_Click(object sender, EventArgs e) {
            vehicle = new Vehicle(selectedVehicleId);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.DeleteData, vehicle);
        }

        private void dataGridViewVehicle_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewVehicle.SelectedRows.Count > 0) {
                selectedVehicleId = int.Parse(dataGridViewVehicle.SelectedRows[0].Cells[0].Value.ToString());
                textBoxVehicleBrand.Text = dataGridViewVehicle.SelectedRows[0].Cells[1].Value.ToString();
                textBoxVehicleModel.Text = dataGridViewVehicle.SelectedRows[0].Cells[2].Value.ToString();
                textBoxVehicleNumber.Text = dataGridViewVehicle.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePickerVehicleService.Value = DateTime.Parse(dataGridViewVehicle.SelectedRows[0].Cells[4].Value.ToString());

                if ((bool)(dataGridViewVehicle.SelectedRows[0].Cells[5].Value)) {
                    radioButtonVehicleAvailable.Checked = true;
                    radioButtonVehicleNotAvailable.Checked = false;
                } else {
                    radioButtonVehicleAvailable.Checked = false;
                    radioButtonVehicleNotAvailable.Checked = true;
                }
            }
        }

        #endregion

        #region ROUTE

        private void tabPageRoute_Enter(object sender, EventArgs e) {
            dataGridViewRoute.DataSource = routeDataAccess.GetData();
            comboBoxRouteDriver.DataSource = driverDataAccess.GetDrivers();
            comboBoxRouteVehicle.DataSource = vehicleDataAccess.GetVehicles();
            dataGridViewRoute.Columns[0].Visible = false;
            dataGridViewRoute.Columns[8].Visible = false;
            dataGridViewRoute.Columns[9].Visible = false;
        }

        private void buttonRouteCost_Click(object sender, EventArgs e) {
            Form costsPanel = new CostsPanel(new Route(selectedRouteId));
            costsPanel.Show();
        }

        private void buttonRouteAdd_Click(object sender, EventArgs e) {
            route = new Route((Driver)comboBoxRouteDriver.SelectedItem, (Vehicle)comboBoxRouteVehicle.SelectedItem, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.InsertData, route);
        }

        private void buttonRouteEdit_Click(object sender, EventArgs e) {
            route = new Route(selectedRouteId, (Driver)comboBoxRouteDriver.SelectedItem, (Vehicle)comboBoxRouteVehicle.SelectedItem, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.UpdateData, route);
        }

        private void buttonRouteDelete_Click(object sender, EventArgs e) {
            route = new Route(selectedRouteId);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.DeleteData, route);
        }

        private void dataGridViewRoute_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewRoute.SelectedRows.Count > 0) {
                selectedRouteId = int.Parse(dataGridViewRoute.SelectedRows[0].Cells[0].Value.ToString());

                foreach (Driver driver in comboBoxRouteDriver.Items) {
                    if (driver.Id == int.Parse(dataGridViewRoute.SelectedRows[0].Cells[8].Value.ToString()))
                        comboBoxRouteDriver.SelectedItem = driver;
                }

                foreach (Vehicle vehicle in comboBoxRouteVehicle.Items) {
                    if (vehicle.Id == int.Parse(dataGridViewRoute.SelectedRows[0].Cells[9].Value.ToString()))
                        comboBoxRouteVehicle.SelectedItem = vehicle;
                }

                dateTimePickerRouteDeparture.Value = DateTime.Parse(dataGridViewRoute.SelectedRows[0].Cells[3].Value.ToString());
                dateTimePickerRoutePlannedArrival.Value = DateTime.Parse(dataGridViewRoute.SelectedRows[0].Cells[4].Value.ToString());
                dateTimePickerRouteActualArrival.Value = DateTime.Parse(dataGridViewRoute.SelectedRows[0].Cells[5].Value.ToString());
                numericUpDownRouteLength.Value = decimal.Parse(dataGridViewRoute.SelectedRows[0].Cells[6].Value.ToString());
            }
        }

        #endregion

        #region SALARY

        private void tabPageSalary_Enter(object sender, EventArgs e) {
            
            dataGridViewSalary.DataSource = routeDataAccess.GetSalaries();
            comboBoxSalaryDriver.Items.Clear();
            comboBoxSalaryMonth.Items.Clear();
            comboBoxSalaryYear.Items.Clear();

            routeDataAccess.GetRange("MONTH").ForEach(i => comboBoxSalaryMonth.Items.Add(i));
            routeDataAccess.GetRange("YEAR").ForEach(i => comboBoxSalaryYear.Items.Add(i));
            driverDataAccess.GetDrivers().ForEach(driver => comboBoxSalaryDriver.Items.Add(driver));
        }
        private void ComboBoxHandlers() {
            List<ComboBox> cbs = new List<ComboBox>() { comboBoxSalaryDriver, comboBoxSalaryMonth, comboBoxSalaryYear };

            foreach (ComboBox cb in cbs) {
                cb.SelectedIndexChanged += new EventHandler(ComboHandler);
            }
        }

        private void ComboHandler(object sender, EventArgs e) {
            dataGridViewSalary.DataSource = routeDataAccess.GetSalaries(selectedDriver, selectedMonth, selectedYear);
        }

        private void comboBoxSalaryDriver_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxSalaryDriver.Items.Count > 0) {
                selectedDriver = (Driver)comboBoxSalaryDriver.SelectedItem;
            }
        }

        private void comboBoxSalaryMonth_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxSalaryMonth.Items.Count > 0) {
                selectedMonth = (int)comboBoxSalaryMonth.SelectedItem;
            }
        }

        private void comboBoxSalaryYear_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxSalaryYear.Items.Count > 0) {
                selectedYear = (int)comboBoxSalaryYear.SelectedItem;
            }
        }
        #endregion
    }
}
