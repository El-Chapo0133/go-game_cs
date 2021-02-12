using Go_Game_lorleveque_WinForm.GameSettings;
using Go_Game_lorleveque_WinForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Game
{
    class Goban
    {
        private UserSettings userSettings;
        private List<List<byte>> goban; // 0 mean unused, 1 black and 2 white

        public List<List<byte>> AllGoban
        {
            get { return goban; }
            set { goban = value; }
        }
        public Goban(UserSettings userSettings)
        {
            this.userSettings = userSettings;
            goban = new List<List<byte>>();
            initGoban();
        }

        public void initGoban()
        {
            for (int index_x = 0; index_x < userSettings.GobanSize; index_x++)
            {
                goban.Add(new List<byte>());
                for (int index_y = 0; index_y < userSettings.GobanSize; index_y++)
                {
                    goban[index_x].Add(0);
                }
            }
        }
        public void ResetGoban()
        {
            for (int indexX = 0; indexX < userSettings.GobanSize; indexX++)
            {
                for (int indexY = 0; indexY < userSettings.GobanSize; indexY++)
                {
                    goban[indexX][indexY] = 0;
                }
            }
        }
        public void setOneCase(Vector2D caseGoban, bool color) // true mean black and false white
        {
            if (color)
            {
                goban[caseGoban.X][caseGoban.Y] = 1;
            }
            else
            {
                goban[caseGoban.X][caseGoban.Y] = 2;
            }
        }
        public void resetMultipleCases(List<Vector2D> casesGoban)
        {
            foreach (Vector2D caseGoban in casesGoban)
            {
                goban[caseGoban.X][caseGoban.Y] = 0;
            }
        }
    }
}
