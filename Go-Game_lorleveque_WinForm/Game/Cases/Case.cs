using Go_Game_lorleveque_WinForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Game.Cases
{
    public class Case
    {
        private bool isUsed;
        private Vector2D position;
        private uint usedAtRound;

        public Vector2D Position
        {
            get { return position; }
        }
        public uint UsedAtRound
        {
            get { return usedAtRound; }
        }
        public bool IsUsed
        {
            get { return isUsed; }
        }
        public Case(Vector2D position, uint round)
        {
            this.position = position;
            isUsed = true;
            usedAtRound = round;
        }
        public void unUse()
        {
            isUsed = false;
        }
        public void actualiseUsedAtRound(uint round)
        {
            usedAtRound = round;
        }
    }
}
