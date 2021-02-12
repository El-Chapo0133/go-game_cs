using Go_Game_lorleveque_WinForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Game.Cases
{
    class GobanFiller
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public GobanFiller()
        {

        }

        /// <summary>
        /// Will fill the goban to have no space
        /// </summary>
        /// <param name="goban">A copy of the whole goban</param>
        /// <returns>A list of case to update</returns>
        public List<FillerCase> FillGoban(List<List<byte>> goban)
        {
            List<FillerCase> casesToUpdate = new List<FillerCase>();
            List<Vector2D> caseDictionnary = new List<Vector2D>();

            for (int indexX = 0; indexX < goban.Count; indexX++)
            {
                for (int indexY = 0; indexY < goban[indexX].Count; indexY++)
                {
                    if (caseDictionnary.Contains(new Vector2D(indexX, indexY))) { continue; }
                    if (goban[indexX][indexY] == 0)
                    {
                        bool noEmptyNeightbors = true;
                        List<Vector2D> neighbors = getNeighbors(new Vector2D(indexX, indexY), goban.Count);
                        foreach (Vector2D neighbor in neighbors)
                        {
                            if (goban[neighbor.X][neighbor.Y] == 0)
                            {
                                noEmptyNeightbors = false;
                                break;
                            }
                        }
                            
                        if (noEmptyNeightbors)
                        {
                            casesToUpdate.Add(new FillerCase(indexX, indexY, countForNoEmptyNeighbors(goban, neighbors)));
                            caseDictionnary.Add(new Vector2D(indexX, indexY));
                        }
                        else
                        {
                            foreach (FillerCase caseToUpdate in countForMultipleEmptyNeighbors(goban, new Vector2D(indexX, indexY)))
                            {
                                casesToUpdate.Add(caseToUpdate);
                                caseDictionnary.Add(new Vector2D(caseToUpdate.X, caseToUpdate.Y));
                            }
                        }
                    }
                }
            }

            return casesToUpdate;
        }

        private List<Vector2D> getNeighbors(Vector2D caseBase, int gobanSize)
        {
            List<Vector2D> neightbors = new List<Vector2D>();

            if (caseBase.X > 0)
            {
                neightbors.Add(new Vector2D(caseBase.X - 1, caseBase.Y));
            }
            if (caseBase.Y > 0)
            {
                neightbors.Add(new Vector2D(caseBase.X, caseBase.Y - 1));
            }
            if (caseBase.Y < gobanSize - 1)
            {
                neightbors.Add(new Vector2D(caseBase.X, caseBase.Y + 1));
            }
            if (caseBase.X < gobanSize - 1)
            {
                neightbors.Add(new Vector2D(caseBase.X + 1, caseBase.Y));
            }

            return neightbors;
        }

        private string countForNoEmptyNeighbors(List<List<byte>> goban, List<Vector2D> neighbors)
        {
            int countBlack = 0, countWhite = 0;
            foreach (Vector2D neighbor in neighbors)
            {
                if (goban[neighbor.X][neighbor.Y] == 1)
                {
                    countBlack += 1;
                }
                else
                {
                    countWhite += 1;
                }
            }

            return countWhite > countBlack ? "white" : "black";
        }

        private List<FillerCase> countForMultipleEmptyNeighbors(List<List<byte>> goban, Vector2D pos)
        {
            List<FillerCase> caseToUpdate = new List<FillerCase>();
            int countBlack = 0, countWhite = 0;
            List<Vector2D> allEmptyNeighbors = getAllNeighborsEmpty(goban, pos);

            List<Vector2D> caseDictionnary = new List<Vector2D>();

            foreach (Vector2D caseToCheck in allEmptyNeighbors)
            {
                foreach (Vector2D neighbor in getNeighbors(caseToCheck, goban.Count))
                {
                    if (listContains(caseDictionnary, neighbor)) { continue; }
                    caseDictionnary.Add(neighbor);
                    if (goban[neighbor.X][neighbor.Y] == 0) { continue; }
                    if (goban[neighbor.X][neighbor.Y] == 1)
                    {
                        countBlack += 1;
                    }
                    else
                    {
                        countWhite += 1;
                    }
                }
            }

            foreach (Vector2D caseToCheck in allEmptyNeighbors)
            {
                caseToUpdate.Add(new FillerCase(caseToCheck.X, caseToCheck.Y, countWhite > countBlack ? "white" : "black"));
            }

            return caseToUpdate;
        }
        private List<Vector2D> getAllNeighborsEmpty(List<List<byte>> goban, Vector2D pos)
        {
            List<Vector2D> caseDictionnary = new List<Vector2D>();
            List<Vector2D> casesToCheck = new List<Vector2D>() { pos };
            int index = 0;
            

            while (true)
            {
                if (index == casesToCheck.Count) { break; }
                foreach (Vector2D neighbor in getNeighbors(casesToCheck[index], goban.Count))
                {
                    if (goban[neighbor.X][neighbor.Y] == 0)
                    {
                        if (listContains(caseDictionnary, neighbor))
                        {
                            continue;
                        }
                        casesToCheck.Add(neighbor);
                        caseDictionnary.Add(neighbor);
                    }
                }
                index += 1;
            }

            return casesToCheck;
        }
        private bool listContains(List<Vector2D> list, Vector2D input)
        {
            foreach (Vector2D cell in list)
            {
                if (cell.X == input.X && cell.Y == input.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
