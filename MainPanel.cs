using Ek_spedycja.DBAccess;
using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ek_spedycja {
    public partial class MainPanel : Form {
        private DriverDataAccess driverDataAccess = new DriverDataAccess();
        Driver driver;
        int selectedDriver;

        private VehicleDataAccess vehicleDataAccess = new VehicleDataAccess();
        Vehicle vehicle;
        int selectedVehicle;

        private RouteDataAccess routeDataAccess = new RouteDataAccess();
        Route route;
        int selectedRoute;


        // Bug z data grid view przy wyborze itemu z indexem 0 

        public MainPanel() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        #region DRIVER

        private void tabPageDriver_Enter(object sender, EventArgs e) {
            dataGridViewDriver.DataSource = driverDataAccess.RefreshView();
        }

        private void buttonDriverAdd_Click(object sender, EventArgs e) {
            driver = new Driver(textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            dataGridViewDriver.DataSource = driverDataAccess.RunCommandAndRefresh(driverDataAccess.InsertData, driver);
        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriver, textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            dataGridViewDriver.DataSource = driverDataAccess.RunCommandAndRefresh(driverDataAccess.UpdateData, driver);
        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriver);
            dataGridViewDriver.DataSource = driverDataAccess.RunCommandAndRefresh(driverDataAccess.DeleteData, driver);
        }
        private void dataGridViewDriver_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridViewDriver.SelectedRows.Count > 0)
            {
                selectedDriver = int.Parse(dataGridViewDriver.SelectedRows[0].Cells[0].Value.ToString());
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
            dataGridViewVehicle.DataSource = vehicleDataAccess.RefreshView();
        }

        private void dataGridViewVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e) {
        }

        private void buttonVehicleAdd_Click(object sender, EventArgs e) {
            bool is_available = radioButtonVehicleAvailable.Checked == true ? true : false;
            vehicle = new Vehicle(textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, is_available);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunCommandAndRefresh(vehicleDataAccess.InsertData, vehicle);
        }

        private void buttonVehicleEdit_Click(object sender, EventArgs e) {
            bool is_available = radioButtonVehicleAvailable.Checked == true ? true : false;
            vehicle = new Vehicle(textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, is_available);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunCommandAndRefresh(vehicleDataAccess.UpdateData, vehicle);
        }

        private void buttonVehicleDelete_Click(object sender, EventArgs e) {
            vehicle = new Vehicle(selectedVehicle);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunCommandAndRefresh(vehicleDataAccess.DeleteData, vehicle);
        }

        private void dataGridViewVehicle_SelectionChanged(object sender, EventArgs e) {

            if (dataGridViewVehicle.SelectedRows.Count > 0) {
                selectedVehicle = int.Parse(dataGridViewVehicle.SelectedRows[0].Cells[0].Value.ToString());
                textBoxVehicleBrand.Text = dataGridViewVehicle.SelectedRows[0].Cells[1].Value.ToString();
                textBoxVehicleModel.Text = dataGridViewVehicle.SelectedRows[0].Cells[2].Value.ToString();
                textBoxVehicleNumber.Text = dataGridViewVehicle.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePickerVehicleService.Value = DateTime.Parse(dataGridViewVehicle.SelectedRows[0].Cells[4].Value.ToString());

                if ((bool)dataGridViewVehicle.SelectedRows[0].Cells[5].Value) {
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

        private void tabPage_Enter(object sender, EventArgs e) {
            dataGridViewRoute.DataSource = routeDataAccess.RefreshView();

            comboBoxRouteDriver.DataSource = driverDataAccess.RefreshComboBox();
            comboBoxRouteDriver.DisplayMember = "driver";

            comboBoxRouteVehicle.DataSource = vehicleDataAccess.RefreshComboBox();
            comboBoxRouteVehicle.DisplayMember = "vehicle";
        }

        private void buttonRouteCost_Click(object sender, EventArgs e) {
            Form costsPanel = new CostsPanel();
            costsPanel.Show();
        }

        private void buttonRouteAdd_Click(object sender, EventArgs e) {
            driver = driverDataAccess.GetDriverById(new Driver(selectedDriver));
            vehicle = vehicleDataAccess.GetVehicleById(new Vehicle(selectedVehicle));
            // TODO zmiana kontrolki z leave na departure
            route = new Route(driver, vehicle, dateTimePickerRouteLeave.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
            dataGridViewRoute.DataSource = routeDataAccess.RunCommandAndRefresh(routeDataAccess.InsertData, route);
        }

        private void buttonRouteEdit_Click(object sender, EventArgs e) {

        }

        private void buttonRouteDelete_Click(object sender, EventArgs e) {

        }

        private void comboBoxRouteDriver_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxRouteDriver.Items.Count > 0) {
                DataRowView s = (DataRowView)comboBoxRouteDriver.SelectedItem;
                selectedDriver = (int)s[0];
            }
        }
        private void comboBoxRouteVehicle_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxRouteVehicle.Items.Count > 0) {
                DataRowView s = (DataRowView)comboBoxRouteVehicle.SelectedItem;
                selectedVehicle = (int)s[0];
            }
        }

        #endregion
        #region SALARY

        private void tabPageCompensation_Enter(object sender, EventArgs e) {
        }


        #endregion


    }
}
