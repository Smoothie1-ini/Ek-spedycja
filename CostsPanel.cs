using Ek_spedycja.DBAccess;
using Ek_spedycja.Model;
using System;
using System.Windows.Forms;

namespace Ek_spedycja {
    public partial class CostsPanel : Form {
        private CostDataAccess costDataAccess = new CostDataAccess();
        Cost cost;
        Route route;
        int selectedCostId;

        public CostsPanel(Route route) {
            InitializeComponent();
            this.route = route;
            numericUpDownCostValue.Maximum = decimal.MaxValue;
            numericUpDownCostValue.Minimum = 0;
        }

        private void CostsPanel_Load(object sender, EventArgs e) {
            dataGridViewCost.DataSource = costDataAccess.GetData(new Cost(route));
            comboBoxCostType.DataSource = costDataAccess.GetCostTypes();
            comboBoxCostType.ValueMember = "id_cost_type";
            comboBoxCostType.DisplayMember = "name";
        }

        private void buttonCostAdd_Click(object sender, EventArgs e) {
            cost = new Cost(route, (int)comboBoxCostType.SelectedValue, richTextBoxCostDescription.Text, numericUpDownCostValue.Value);
            dataGridViewCost.DataSource = costDataAccess.RunMethodAndRefresh(costDataAccess.InsertData, cost);
        }

        private void buttonCostEdit_Click(object sender, EventArgs e) {
            cost = new Cost(selectedCostId, route, (int)comboBoxCostType.SelectedValue, richTextBoxCostDescription.Text, numericUpDownCostValue.Value);
            dataGridViewCost.DataSource = costDataAccess.RunMethodAndRefresh(costDataAccess.UpdateData, cost);
        }

        private void buttonCostDelete_Click(object sender, EventArgs e) {
            cost = new Cost(selectedCostId, route);
            dataGridViewCost.DataSource = costDataAccess.RunMethodAndRefresh(costDataAccess.DeleteData, cost);
        }

        private void dataGridViewCost_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewCost.SelectedRows.Count > 0) {
                selectedCostId = int.Parse(dataGridViewCost.SelectedRows[0].Cells[0].Value.ToString());
                comboBoxCostType.SelectedItem = dataGridViewCost.SelectedRows[0].Cells[1].Value.ToString();
                richTextBoxCostDescription.Text = dataGridViewCost.SelectedRows[0].Cells[2].Value.ToString();
                numericUpDownCostValue.Value = decimal.Parse(dataGridViewCost.SelectedRows[0].Cells[3].Value.ToString());
            }
        }
    }
}
