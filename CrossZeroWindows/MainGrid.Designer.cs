namespace CrossZeroWindows {
    partial class MainGrid {
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
            this.referenceButton = new System.Windows.Forms.Button();
            this.endGameLabel = new System.Windows.Forms.Label();
            this.allAIButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // referenceButton
            // 
            this.referenceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.referenceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.referenceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.referenceButton.Location = new System.Drawing.Point(259, 124);
            this.referenceButton.Name = "referenceButton";
            this.referenceButton.Size = new System.Drawing.Size(82, 82);
            this.referenceButton.TabIndex = 0;
            this.referenceButton.Text = "X";
            this.referenceButton.UseVisualStyleBackColor = true;
            this.referenceButton.Visible = false;
            // 
            // endGameLabel
            // 
            this.endGameLabel.AutoSize = true;
            this.endGameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.endGameLabel.Location = new System.Drawing.Point(350, 286);
            this.endGameLabel.Name = "endGameLabel";
            this.endGameLabel.Size = new System.Drawing.Size(90, 26);
            this.endGameLabel.TabIndex = 1;
            this.endGameLabel.Text = "Победа";
            this.endGameLabel.Visible = false;
            // 
            // allAIButton
            // 
            this.allAIButton.AutoSize = true;
            this.allAIButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.allAIButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.allAIButton.Location = new System.Drawing.Point(293, 352);
            this.allAIButton.Name = "allAIButton";
            this.allAIButton.Size = new System.Drawing.Size(191, 47);
            this.allAIButton.TabIndex = 2;
            this.allAIButton.Text = "Следующий ход";
            this.allAIButton.UseVisualStyleBackColor = true;
            this.allAIButton.Visible = false;
            this.allAIButton.Click += new System.EventHandler(this.allAIButton_Click);
            // 
            // MainGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.allAIButton);
            this.Controls.Add(this.endGameLabel);
            this.Controls.Add(this.referenceButton);
            this.Name = "MainGrid";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CrossZero";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button referenceButton;
        private System.Windows.Forms.Label endGameLabel;
        private System.Windows.Forms.Button allAIButton;
    }
}