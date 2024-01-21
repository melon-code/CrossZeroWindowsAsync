using System.Drawing;

namespace CrossZeroWindows {
    public class PlayerPoint {
        public bool Selected { get; set; }
        public Point Value { get; set; } 

        public PlayerPoint() {
            Selected = false;
        }
    }
}