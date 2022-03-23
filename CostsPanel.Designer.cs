
namespace Ek_spedycja {
    partial class CostsPanel {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonCostDelete = new System.Windows.Forms.Button();
            this.buttonCostEdit = new System.Windows.Forms.Button();
            this.buttonCostAdd = new System.Windows.Forms.Button();
            this.comboBoxCostType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownCostValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxCostDescription = new System.Windows.Forms.RichTextBox();
            this.dataGridViewCost = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCostValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCost)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCostDelete
            // 
            this.buttonCostDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCostDelete.Location = new System.Drawing.Point(674, 291);
            this.buttonCostDelete.Name = "buttonCostDelete";
            this.buttonCostDelete.Size = new System.Drawing.Size(114, 44);
            this.buttonCostDelete.TabIndex = 47;
            this.buttonCostDelete.Text = "Usuń";
            this.buttonCostDelete.UseVisualStyleBackColor = true;
            this.buttonCostDelete.Click += new System.EventHandler(this.buttonCostDelete_Click);
            // 
            // buttonCostEdit
            // 
            this.buttonCostEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCostEdit.Location = new System.Drawing.Point(554, 291);
            this.buttonCostEdit.Name = "buttonCostEdit";
            this.buttonCostEdit.Size = new System.Drawing.Size(114, 44);
            this.buttonCostEdit.TabIndex = 46;
            this.buttonCostEdit.Text = "Edytuj";
            this.buttonCostEdit.UseVisualStyleBackColor = true;
            this.buttonCostEdit.Click += new System.EventHandler(this.buttonCostEdit_Click);
            // 
            // buttonCostAdd
            // 
            this.buttonCostAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCostAdd.Location = new System.Drawing.Point(434, 291);
            this.buttonCostAdd.Name = "buttonCostAdd";
            this.buttonCostAdd.Size = new System.Drawing.Size(114, 44);
            this.buttonCostAdd.TabIndex = 45;
            this.buttonCostAdd.Text = "Dodaj";
            this.buttonCostAdd.UseVisualStyleBackColor = true;
            this.buttonCostAdd.Click += new System.EventHandler(this.buttonCostAdd_Click);
            // 
            // comboBoxCostType
            // 
            this.comboBoxCostType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxCostType.FormattingEnabled = true;
            this.comboBoxCostType.Location = new System.Drawing.Point(118, 184);
            this.comboBoxCostType.Name = "comboBoxCostType";
            this.comboBoxCostType.Size = new System.Drawing.Size(197, 28);
            this.comboBoxCostType.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Typ kosztu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(347, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 51;
            this.label2.Text = "Kwota kosztu";
            // 
            // numericUpDownCostValue
            // 
            this.numericUpDownCostValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownCostValue.Location = new System.Drawing.Point(457, 184);
            this.numericUpDownCostValue.Name = "numericUpDownCostValue";
            this.numericUpDownCostValue.Size = new System.Drawing.Size(197, 26);
            this.numericUpDownCostValue.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "Opis kosztu";
            // 
            // richTextBoxCostDescription
            // 
            this.richTextBoxCostDescription.Location = new System.Drawing.Point(118, 220);
            this.richTextBoxCostDescription.Name = "richTextBoxCostDescription";
            this.richTextBoxCostDescription.Size = new System.Drawing.Size(670, 53);
            this.richTextBoxCostDescription.TabIndex = 54;
            this.richTextBoxCostDescription.Text = "";
            // 
            // dataGridViewCost
            // 
            this.dataGridViewCost.AllowUserToAddRows = false;
            this.dataGridViewCost.AllowUserToDeleteRows = false;
            this.dataGridViewCost.AllowUserToResizeRows = false;
            this.dataGridViewCost.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewCost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCost.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewCost.Name = "dataGridViewCost";
            this.dataGridViewCost.RowHeadersVisible = false;
            this.dataGridViewCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCost.Size = new System.Drawing.Size(776, 166);
            this.dataGridViewCost.TabIndex = 55;
            // 
            // CostsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 345);
            this.Controls.Add(this.dataGridViewCost);
            this.Controls.Add(this.richTextBoxCostDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownCostValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCostType);
            this.Controls.Add(this.buttonCostDelete);
            this.Controls.Add(this.buttonCostEdit);
            this.Controls.Add(this.buttonCostAdd);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 384);
            this.MinimumSize = new System.Drawing.Size(816, 384);
            this.Name = "CostsPanel";
            this.Text = "Zarządzanie kosztami kursu";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCostValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCostDelete;
        private System.Windows.Forms.Button buttonCostEdit;
        private System.Windows.Forms.Button buttonCostAdd;
        private System.Windows.Forms.ComboBox comboBoxCostType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownCostValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxCostDescription;
        private System.Windows.Forms.DataGridView dataGridViewCost;
    }
}