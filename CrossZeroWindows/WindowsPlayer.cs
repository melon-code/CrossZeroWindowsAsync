using System;
using System.Drawing;
using CrossZeroAPI;

namespace CrossZeroWindows {
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