﻿using System;
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
            DialogResult result;
            do {
                MainGrid grid = new MainGrid((int)numericUpDown1.Value, comboBox1.SelectedIndex == aiIndex, comboBox2.SelectedIndex == aiIndex);
                result = grid.ShowDialog();
            } while (result == DialogResult.Retry);
            switch (result) {
                case DialogResult.Cancel:
                    Close();
                    break;
                case DialogResult.Ignore:
                    Show();
                    break;
                default:
                    Show();
                    break;
            }
        }
    }
}
