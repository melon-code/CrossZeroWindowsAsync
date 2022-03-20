using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        const string crossWin = "Крустики победили!";
        const string zeroWin = "Нолики победили!";
        const string draw = "Ничья!";
        const int topStart = 20;
        const int leftStart = 20;
        const int verticalInterval = 10;
        const int horizontalInterval = 10;

        readonly int size;
        readonly IList<Button> buttons = new List<Button>();
        Point selectedPoint;
        TTCGame game;
        int gridEndTop;

        int ButtonHeight => referenceButton.Height;
        int ButtonWidth => referenceButton.Width;

        public MainGrid(int fieldSize) {
            InitializeComponent();
            size = fieldSize;
            CreateGrid();
            game = new TTCGame(fieldSize, new WindowsPlayer(() => selectedPoint), new AI());
        }

        void CreateGrid() {
            int top = topStart;
            int left = leftStart;
            SuspendLayout();
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Button button = CreateButton(top, left, new Point(i, j));
                    Controls.Add(button);
                    buttons.Add(button);
                    left += ButtonWidth + horizontalInterval;
                }
                left = leftStart;
                top += ButtonHeight + verticalInterval;
            }
            gridEndTop = top;
            ResumeLayout(false);
        }

        void ButtonClick(object sender, EventArgs e) {
            if (!(sender is Button button))
                throw new ArgumentNullException();
            selectedPoint = button.GetPoint();
            DisableButton(button, game.CurrentMark == Marks.Cross ? crossButtonText : zeroButtonText);
            if (MakeTurn(null))
                MakeTurn(UpdateGrid);
        }

        void DisableButton(Button button, string updatedText) {
            button.Text = updatedText;
            button.Enabled = false;
        }

        bool MakeTurn(Action update) {
            game.MakePlayerTurn();
            update?.Invoke();
            if (game.IsEnd) {
                //disable buttons
                endGameLabel.Location = new Point(leftStart, gridEndTop);
                game.TryGetEndResult(out EndResult result);
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
                endGameLabel.Visible = true;
                return false;
            }
            return true;
        }

        void UpdateGrid() {
            var grid = game.GameField;
            for (int i = 0; i < size; i++) 
                for (int j = 0; j < size; j++) {
                    var button = buttons[i * size + j];
                    var cell = grid[i, j];
                    if (button.IsEmpty() && cell != Cell.Empty) 
                        DisableButton(button, MarkToString(cell));
                }
        }

        Button CreateButton(int top, int left, Point point) {
            int currentNumber = point.X * size + point.Y;
            Button button = new Button();
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 255, 192);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Microsoft Sans Serif", 45F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Location = new Point(left, top);
            button.Name = $"button{currentNumber}";
            button.Size = new Size(82, 82); //const size
            button.TabIndex = currentNumber;
            button.Text = string.Empty;
            button.UseVisualStyleBackColor = true;
            button.Visible = true;
            button.Tag = point;
            button.Click += ButtonClick;
            return button;
        }
    }

    public static class ButtonExtension {
        public static Point GetPoint(this Button button) {
            if (!(button.Tag is Point point))
                throw new ArgumentException();
            return point;
        }

        public static bool IsEmpty(this Button button) {
            return string.IsNullOrEmpty(button.Text);
        }
    }

    public class WindowsPlayer : IPlayer {
        readonly Func<Point> makeTurn;

        public WindowsPlayer(Func<Point> getPoint) {
            makeTurn = getPoint;
        }

        public Coordinate MakeTurn() {
            Point point = makeTurn();
            return new Coordinate(point.X, point.Y);
        }
    }
}