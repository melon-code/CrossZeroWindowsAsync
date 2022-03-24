using System;
using System.Drawing;
using System.Windows.Forms;

namespace CrossZeroWindows {
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
}