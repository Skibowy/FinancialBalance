using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilansPieniezny
{
    public partial class SettingsForm : Form
    {
        public string filePath;
        public decimal creditCardLimit;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            this.filePath = txtFilePath.Text;
            this.creditCardLimit = decimal.Parse(txtCreditCardLimit.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
