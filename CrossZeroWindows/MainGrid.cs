using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CrossZeroAPI;

namespace CrossZeroWindows {
    public partial class MainGrid : Form {
        static string MarkToString(Cell mark) {
            if (mark == Cell.Cross)
                return crossButtonText;
            return zeroButtonText;
        }

        const string crossButtonText = "X";
        const string zeroButtonText = "O";
        const string crossWin = "Крестики победили!";
        const string zeroWin = "Нолики победили!";
        const string draw = "Ничья!";
        const int topStart = 20;
        const int leftStart = 20;
        const int verticalInterval = 5;
        const int horizontalInterval = 5;
        const int buttonHeight = 82;
        const int buttonWidth = 82;

        readonly int size;
        readonly IList<Button> buttons = new List<Button>();
        readonly bool allAI;
        readonly TTCWindowsProcessor processor;
        PlayerPoint selectedPoint = new PlayerPoint();
        int gridEndTop;

        delegate void UpdateGridDelegate(ReadOnlyTable grid);
        delegate void ShowResultDelegate(ReadOnlyTable grid, EndResult result);

        public MainGrid(int fieldSize, bool player1AI, bool player2AI) {
            InitializeComponent();
            size = fieldSize;
            allAI = player1AI && player2AI;
            CreateGrid();
            processor = new TTCWindowsProcessor(fieldSize, CreatePlayer(player1AI), CreatePlayer(player2AI), allAI);
            processor.RenderField += UpdateGridAfterTurn;
            processor.RenderResult += ShowResult;
            processor.PlayAsync();
        }

        void CreateGrid() {
            int top = topStart;
            int left = leftStart;
            SuspendLayout();
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Button button = CreateButton(top, left, new Point(i, j), !allAI);
                    Controls.Add(button);
                    buttons.Add(button);
                    left += buttonWidth + horizontalInterval;
                }
                left = leftStart;
                top += buttonHeight + verticalInterval;
            }
            gridEndTop = top;
            Width = buttonWidth * size + horizontalInterval * (size - 1) + leftStart * 3;
            if (allAI) 
                MakeVisible(allAIButton, false);
            ResumeLayout(false);
        }

        IPlayer CreatePlayer(bool isAI) {
            if (isAI)
                return new AI();
            return new WindowsPlayer(selectedPoint);
        }

        void ButtonClick(object sender, EventArgs e) {
            if (!(sender is Button button))
                throw new ArgumentNullException();
            selectedPoint.Value = button.GetPoint();
            selectedPoint.Selected = true;
        }

        void DisableButton(Button button, string updatedText) {
            button.Text = updatedText;
            button.Enabled = false;
        }

        void MakeVisible(Control control, bool updateGridEndTop) {
            control.Location = new Point(AlignCenter(control.Width), gridEndTop);
            control.Visible = true;
            if (updateGridEndTop)
                gridEndTop += control.Height + verticalInterval;
        }

        public void ShowResult(ReadOnlyTable grid, EndResult result) {
            if (!InvokeRequired) {
                UpdateGridAfterTurn(grid);
                UpdateGrid((i, j) => {
                    var button = buttons[i * size + j];
                    if (button.Enabled)
                        button.Enabled = false;
                });
                if (allAI)
                    allAIButton.Visible = false;
                switch (result) {
                    case EndResult.CrossWin:
                        endGameLabel.Text = crossWin;
                        break;
                    case EndResult.ZeroWin:
                        endGameLabel.Text = zeroWin;
                        break;
                    default:
                        endGameLabel.Text = draw;
                        break;
                }
                MakeVisible(endGameLabel, true);
                MakeVisible(repeatButton, true);
                MakeVisible(toMainMenuButton, false);
            }
            else
                Invoke(new ShowResultDelegate(ShowResult), new object[] { grid, result });
        }

        int AlignCenter(int width) {
            return (Width - width) / 2;
        }

        void UpdateGrid(Action<int, int> updateFunc) {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    updateFunc(i, j);
        }

        public void UpdateGridAfterTurn(ReadOnlyTable grid) {
            if (!InvokeRequired) {
                UpdateGrid((i, j) => {
                    var button = buttons[i * size + j];
                    var cell = grid[i, j];
                    if (button.IsEmpty() && cell != Cell.Empty)
                        DisableButton(button, MarkToString(cell));
                });
            }
            else 
                Invoke(new UpdateGridDelegate(UpdateGridAfterTurn), new object[] { grid });
        }

        Button CreateButton(int top, int left, Point point, bool enabled) {
            int currentNumber = point.X * size + point.Y;
            Button button = new Button();
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 255, 192);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Microsoft Sans Serif", 45F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(left, top);
            button.Name = $"button{currentNumber}";
            button.Size = new Size(buttonWidth, buttonHeight);
            button.TabIndex = currentNumber;
            button.Text = string.Empty;
            button.UseVisualStyleBackColor = true;
            button.Visible = true;
            button.Enabled = enabled;
            button.Tag = point;
            button.Click += ButtonClick;
            return button;
        }

        void CloseWindow(DialogResult result) {
            DialogResult = result;
            Close();
        }

        private void allAIButton_Click(object sender, EventArgs e) {
            processor.NextTurn = true;
        }

        private void repeatButton_Click(object sender, EventArgs e) {
            CloseWindow(DialogResult.Retry);
        }

        private void toMainMenuButton_Click(object sender, EventArgs e) {
            CloseWindow(DialogResult.Ignore);
        }
    }
}