using Go_Game_lorleveque_WinForm.GameSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Game
{
    class Win
    {
        private UserSettings userSettings;
        private GeneralSettings generalSettings;

        /// <summary>
        /// Contructor
        /// </summary>
        public Win(UserSettings settings)
        {
            userSettings = settings;
            generalSettings = new GeneralSettings();
        }

        public bool CheckWintimer(int timerPlayer)
        {
            return timerPlayer == userSettings.MaxTimeForPlayer;
        }

        /// <summary>
        /// Count all the case to get the winner
        /// </summary>
        /// <param name="goban">the whole goban</param>
        /// <returns>the winner, true mean black and false mean white</returns>
        public string CountCase(List<List<byte>> goban)
        {
            double countBlack = 0, countWhite = generalSettings.Komi;
            for (int indexX = 0; indexX < goban.Count; indexX++)
            {
                for (int indexY = 0; indexY < goban[indexX].Count; indexY++)
                {
                    if (goban[indexX][indexY] == 1)
                    {
                        countBlack += 1;
                    }
                    else
                    {
                        countWhite += 1;
                    }
                }
            }
            return countWhite > countBlack ? "Les Blancs sont vinqueurs avec " + countWhite + " contre " + countBlack : "Les Noirs sont vinqueurs avec " + countBlack + " contre " + countWhite;
        }
    }
}
