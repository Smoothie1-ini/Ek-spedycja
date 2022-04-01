﻿using Ek_spedycja.DBAccess;
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
            dataGridViewDriver.DataSource = driverDataAccess.RefreshViewAfterCommand(driverDataAccess.InsertData, driver);
        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriver, textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            // To musi być ?
            driver.Id = selectedDriver;
            dataGridViewDriver.DataSource = driverDataAccess.RefreshViewAfterCommand(driverDataAccess.UpdateData, driver);
        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {
            driver = new Driver(selectedDriver);
            dataGridViewDriver.DataSource = driverDataAccess.RefreshViewAfterCommand(driverDataAccess.DeleteData, driver);
        }
        private void dataGridViewDriver_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDriver.CurrentCell.RowIndex < 0)
                return;

            if (dataGridViewDriver.Focused)
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
        }

        private void dataGridViewVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void buttonVehicleAdd_Click(object sender, EventArgs e) {

        }

        private void buttonVehicleEdit_Click(object sender, EventArgs e) {

        }

        private void buttonVehicleDelete_Click(object sender, EventArgs e) {

        }

        #endregion

        #region ROUTE

        private void tabPage_Enter(object sender, EventArgs e) {
        }

        private void dataGridViewRoute_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void buttonRouteCost_Click(object sender, EventArgs e) {
            Form costsPanel = new CostsPanel();
            costsPanel.Show();
        }

        private void buttonRouteAdd_Click(object sender, EventArgs e) {

        }

        private void buttonRouteEdit_Click(object sender, EventArgs e) {

        }

        private void buttonRouteDelete_Click(object sender, EventArgs e) {

        }
        #endregion

        #region SALARY

        private void tabPageCompensation_Enter(object sender, EventArgs e) {
        }

        #endregion
 
    }
}
