using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrossZeroWindows {
    public partial class SettingsForm : Form {
        const int playerIndex = 0;
        const int aiIndex = 1;

        public SettingsForm() {
            InitializeComponent();
            comboBox1.SelectedIndex = playerIndex;
            comboBox2.SelectedIndex = aiIndex;
        }

        private void exitButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void startButton_Click(object sender, EventArgs e) {
            Hide();
            MainGrid grid = new MainGrid((int)numericUpDown1.Value);
            switch (grid.ShowDialog()) {
                case DialogResult.Cancel:
                    Close();
                    break;
                case DialogResult.Retry:
                    Show();
                    break;
                case DialogResult.Yes:
                    Show();
                    break;
                default:
                    Show();
                    break;
            }
        }
    }
}
