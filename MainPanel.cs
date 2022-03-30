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
        private DataAccess dataAccess = new DataAccess();

        public MainPanel() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        #region DRIVER

        private void tabPageDriver_Enter(object sender, EventArgs e) {
            dataGridViewDriver.DataSource = dataAccess.dataSet.Tables["v_driver"];
        }

        private void dataGridViewDriver_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void buttonDriverAdd_Click(object sender, EventArgs e) {
            Driver driver = new Driver(textBoxDriverName.Text, textBoxDriverSurname.Text, textBoxDriverPesel.Text, dateTimePickerDriverBirthDate.Value, dateTimePickerDriverHireDate.Value);
            dataAccess.InsertData(driver);
        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {

        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {

        }

        #endregion

        #region VEHICLE

        private void tabPageVehicle_Enter(object sender, EventArgs e) {
            dataGridViewVehicle.DataSource = dataAccess.dataSet.Tables["v_vehicle"];
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
            dataGridViewRoute.DataSource = dataAccess.dataSet.Tables["v_route"];
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
            dataGridViewSalary.DataSource = dataAccess.dataSet.Tables["v_salary"];
        }

        #endregion
    }
}
