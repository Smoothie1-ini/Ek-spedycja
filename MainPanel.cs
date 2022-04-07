using Ek_spedycja.DBAccess;
using Ek_spedycja.Model;
using System;
using System.Collections.Generic;
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
        bool vehicleAvailability;

        private RouteDataAccess routeDataAccess = new RouteDataAccess();
        Route route;
        int selectedRouteId;

        //nie da się na tej samej zmiennej zrobić? (driver)
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

        #region PRIVATE METHODS

        private void FormConfiguration() {
            numericUpDownRouteLength.Maximum = decimal.MaxValue;
            numericUpDownRouteLength.Minimum = 0;
        }

        private void resetControlsDriver() {
            textBoxDriverName.Text = "";
            textBoxDriverSurname.Text = "";
            textBoxDriverPesel.Text = "";
            dateTimePickerDriverBirthDate.Value = DateTime.Now;
            dateTimePickerDriverHireDate.Value = DateTime.Now;
        }

        private void resetControlsVehicle() {
            textBoxVehicleBrand.Text = "";
            textBoxVehicleModel.Text = "";
            textBoxVehicleNumber.Text = "";
            dateTimePickerVehicleService.Value = DateTime.Now;
        }

        private void resetControlsRoute() {
            if (comboBoxRouteDriver.Items.Count > 0) comboBoxRouteDriver.SelectedIndex = 0;
            if (comboBoxRouteVehicle.Items.Count > 0) comboBoxRouteVehicle.SelectedIndex = 0;
            dateTimePickerRouteDeparture.Value = DateTime.Now;
            dateTimePickerRoutePlannedArrival.Value = DateTime.Now;
            dateTimePickerRouteActualArrival.Value = DateTime.Now;
            numericUpDownRouteLength.Value = 0;
        }

        private void checkVehicleAvailability() {
            if (dateTimePickerVehicleService.Value.AddYears(1) > DateTime.Now)
                vehicleAvailability = true;
            else
                vehicleAvailability = false;
        }

        #endregion

        #region DRIVER

        private void tabPageDriver_Enter(object sender, EventArgs e) {
            dataGridViewDriver.DataSource = driverDataAccess.GetData();
            dataGridViewDriver.Columns[0].Visible = false;
        }

        private void buttonDriverAdd_Click(object sender, EventArgs e) {
            try {
                driver = new Driver(textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
                dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.InsertData, driver);
                resetControlsDriver();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {
            try {
                driver = new Driver(selectedDriverId, textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
                dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.UpdateData, driver);
                resetControlsDriver();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriverId);
            dataGridViewDriver.DataSource = driverDataAccess.RunMethodAndRefresh(driverDataAccess.DeleteData, driver);
            resetControlsDriver();
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

        private void textBoxDriverPesel_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion

        #region VEHICLE

        private void tabPageVehicle_Enter(object sender, EventArgs e) {
            dataGridViewVehicle.DataSource = vehicleDataAccess.GetData();
            dataGridViewVehicle.Columns[0].Visible = false;
        }

        private void buttonVehicleAdd_Click(object sender, EventArgs e) {
            try {
                checkVehicleAvailability();
                vehicle = new Vehicle(textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, vehicleAvailability);
                dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.InsertData, vehicle);
                resetControlsVehicle();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonVehicleEdit_Click(object sender, EventArgs e) {
            try {
                checkVehicleAvailability();
                vehicle = new Vehicle(selectedVehicleId, textBoxVehicleBrand.Text, textBoxVehicleModel.Text, textBoxVehicleNumber.Text, dateTimePickerVehicleService.Value, vehicleAvailability);
                dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.UpdateData, vehicle);
                resetControlsVehicle();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonVehicleDelete_Click(object sender, EventArgs e) {
            vehicle = new Vehicle(selectedVehicleId);
            dataGridViewVehicle.DataSource = vehicleDataAccess.RunMethodAndRefresh(vehicleDataAccess.DeleteData, vehicle);
            resetControlsVehicle();
        }

        private void dataGridViewVehicle_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewVehicle.SelectedRows.Count > 0) {
                selectedVehicleId = int.Parse(dataGridViewVehicle.SelectedRows[0].Cells[0].Value.ToString());
                textBoxVehicleBrand.Text = dataGridViewVehicle.SelectedRows[0].Cells[1].Value.ToString();
                textBoxVehicleModel.Text = dataGridViewVehicle.SelectedRows[0].Cells[2].Value.ToString();
                textBoxVehicleNumber.Text = dataGridViewVehicle.SelectedRows[0].Cells[3].Value.ToString();
                dateTimePickerVehicleService.Value = DateTime.Parse(dataGridViewVehicle.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void textBoxVehicleNumber_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            try {
                route = new Route((Driver)comboBoxRouteDriver.SelectedItem, (Vehicle)comboBoxRouteVehicle.SelectedItem, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
                dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.InsertData, route);
                resetControlsRoute();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonRouteEdit_Click(object sender, EventArgs e) {
            try {
                route = new Route(selectedRouteId, (Driver)comboBoxRouteDriver.SelectedItem, (Vehicle)comboBoxRouteVehicle.SelectedItem, dateTimePickerRouteDeparture.Value, dateTimePickerRoutePlannedArrival.Value, dateTimePickerRouteActualArrival.Value, numericUpDownRouteLength.Value);
                dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.UpdateData, route);
                resetControlsRoute();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonRouteDelete_Click(object sender, EventArgs e) {
            route = new Route(selectedRouteId);
            dataGridViewRoute.DataSource = routeDataAccess.RunMethodAndRefresh(routeDataAccess.DeleteData, route);
            resetControlsRoute();
        }

        private void dataGridViewRoute_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewRoute.SelectedRows.Count > 0) {
                selectedRouteId = int.Parse(dataGridViewRoute.SelectedRows[0].Cells[0].Value.ToString());
                buttonRouteCost.Enabled = true;

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
            if (dataGridViewRoute.SelectedRows.Count > 0) {
                dataGridViewSalary.DataSource = routeDataAccess.GetSalaries();
                comboBoxSalaryDriver.Items.Clear();
                comboBoxSalaryMonth.Items.Clear();
                comboBoxSalaryYear.Items.Clear();

                routeDataAccess.GetRange("MONTH").ForEach(i => comboBoxSalaryMonth.Items.Add(i));
                routeDataAccess.GetRange("YEAR").ForEach(i => comboBoxSalaryYear.Items.Add(i));
                driverDataAccess.GetDrivers().ForEach(driver => comboBoxSalaryDriver.Items.Add(driver));
            }
        }
        private void ComboBoxHandlers() {
            List<ComboBox> cbs = new List<ComboBox>() { comboBoxSalaryDriver, comboBoxSalaryMonth, comboBoxSalaryYear };
            foreach (ComboBox cb in cbs) {
                cb.SelectedIndexChanged += new EventHandler(ComboHandler);
            }
        }

        private void ComboHandler(object sender, EventArgs e) {
            dataGridViewSalary.DataSource = routeDataAccess.GetSalaries(driver, selectedMonth, selectedYear);
        }

        private void comboBoxSalaryDriver_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxSalaryDriver.Items.Count > 0) {
                driver = (Driver)comboBoxSalaryDriver.SelectedItem;
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
