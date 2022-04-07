using System;
using System.Drawing;
using System.Threading;
using CrossZeroAPI;

namespace CrossZeroWindows {
    public class WindowsPlayer : IPlayer {
        const int interval = 50;

        readonly PlayerPoint point;

        public WindowsPlayer(PlayerPoint playerPoint) {
            point = playerPoint;
        }

        public Coordinate MakeTurn() {
            while (!point.Selected) {
                Thread.Sleep(interval);
            }
            point.Selected = false;
            return new Coordinate(point.Value.X, point.Value.Y);
        }
    }
}