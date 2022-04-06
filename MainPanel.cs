using Ek_spedycja.DBAccess;
using Ek_spedycja.Model;
using System;
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
        Vehicle selectedVehicle;

        public MainPanel() {
            InitializeComponent();
            numericUpDownRouteLength.Maximum = decimal.MaxValue;
            numericUpDownRouteLength.Minimum = 0;
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        #region DRIVER

        private void tabPageDriver_Enter(object sender, EventArgs e) {
            dataGridViewDriver.DataSource = driverDataAccess.GetData();
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
        }

        private void dataGridViewVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e) {

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
        }

        private void buttonRouteCost_Click(object sender, EventArgs e) {
            Form costsPanel = new CostsPanel(new Route(selectedRouteId));
            costsPanel.Show();
        }

        private void buttonRouteAdd_Click(object sender, EventArgs e) {
            route = new Route(selectedDriver, selectedVehicle, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.InsertData, route);
        }

        private void buttonRouteEdit_Click(object sender, EventArgs e) {
            route = new Route(selectedRouteId, selectedDriver, selectedVehicle, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.UpdateData, route);
        }

        private void buttonRouteDelete_Click(object sender, EventArgs e) {
            route = new Route(selectedRouteId);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.DeleteData, route);
        }

        private void comboBoxRouteDriver_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxRouteDriver.Items.Count > 0) {
                selectedDriver = (Driver)comboBoxRouteDriver.SelectedItem;
            }
        }

        private void comboBoxRouteVehicle_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxRouteVehicle.Items.Count > 0) {
                selectedVehicle = (Vehicle)comboBoxRouteVehicle.SelectedItem;
            }
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

        }

        #endregion

    }
}
