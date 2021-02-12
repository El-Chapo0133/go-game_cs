using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Game.Cases
{
    class FillerCase
    {
        private int x, y;
        private string color;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public string Color
        {
            get { return color; }
        }
        public FillerCase(int x, int y, string color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
