using System.Threading.Tasks;
using CrossZeroAPI;

namespace CrossZeroWindows {
    public class TTCWindowsProcessor : TTCGameProcessor {
        const int sleepInterval = 50;

        public delegate void RenderFieldHandler(ReadOnlyTable field);
        public event RenderFieldHandler RenderField;
        public delegate void RenderResultHandler(ReadOnlyTable field, EndResult result);
        public event RenderResultHandler RenderResult;

        readonly bool delayTurn;

        public bool NextTurn { private get; set; } = true;

        public TTCWindowsProcessor(int size, IPlayer player1, IPlayer player2, bool allAI) : base(size, player1, player2, Marks.Cross) {
            delayTurn = allAI;
        }

        public override void RenderGameField(ReadOnlyTable gameField) {
            RenderField(gameField);
            if (delayTurn) {
                while (!NextTurn)
                    Task.Delay(sleepInterval);
                NextTurn = false;
            }
        }

        public override void RenderLastFieldAndResult(ReadOnlyTable gameField, EndResult result) {
            RenderResult(gameField, result);
        }
    }
}