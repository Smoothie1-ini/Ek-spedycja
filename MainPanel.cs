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
        public MainPanel() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        #region DRIVER

        private void buttonDriverAdd_Click(object sender, EventArgs e) {

        }

        private void buttonDriverEdit_Click(object sender, EventArgs e) {

        }

        private void buttonDriverDelete_Click(object sender, EventArgs e) {

        }

        #endregion

        #region VEHICLE

        private void buttonVehicleAdd_Click(object sender, EventArgs e) {

        }

        private void buttonVehicleEdit_Click(object sender, EventArgs e) {

        }

        private void buttonVehicleDelete_Click(object sender, EventArgs e) {

        }

        #endregion

        #region ROUTE

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
    }
}
