using Go_Game_lorleveque_WinForm.Game;
using Go_Game_lorleveque_WinForm.GameSettings;
using Go_Game_lorleveque_WinForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Game_lorleveque_WinForm.Engine
{
    class GobanCalculator
    {
        private Controller gameController;
        private CaseDico caseDico;

        public GobanCalculator(Controller controller)
        {
            gameController = controller;
            caseDico = new CaseDico();
        }


        //private const int neighborsCount = 4;
        public void Calculate(List<List<byte>> goban, Vector2D casePlayed, bool whoPlayed, int gobanSize) // true mean black, false mean white
        {
            bool /* isCerned = false, */ destroyedSomething = false;
            byte playerToCheck = getPlayerToCheck(whoPlayed);
            List<Vector2D> baseNeighbors = getNeighbors(casePlayed, gobanSize);

            List<Vector2D> casesToCheck = new List<Vector2D>();

            // ===============================================
            // Je dois check pour les bords du goban
            // ===============================================

            foreach (Vector2D baseNeighbor in baseNeighbors)
            {
                if (goban[baseNeighbor.X][baseNeighbor.Y] == playerToCheck)
                {
                    casesToCheck.Add(baseNeighbor);
                }
            }
            // base cases to check if they are deleted
            // if there is nothing to check, just return
            //if (casesToCheck.Count == 4)
            //{
            //    isCerned = true;
            //}
            if (casesToCheck.Count == 0)
            {
                return;
            }


            

            foreach (Vector2D caseToCheckRoot in casesToCheck)
            {
                if (calculateIfShouldDestroye(new List<Vector2D>() { caseToCheckRoot }, goban, playerToCheck, gobanSize) && !destroyedSomething)
                {
                    destroyedSomething = false;
                }
            }

            if (destroyedSomething)
            {
                return;
            }

            calculateIfShouldDestroye(new List<Vector2D>() { casePlayed }, goban, getPlayerToCheck(!whoPlayed), gobanSize);
        }


        private bool calculateIfShouldDestroye(List<Vector2D> casesToCheck, List<List<byte>> goban, byte playerToCheck, int gobanSize)
        {
            List<Vector2D> tempCasesToCheck = new List<Vector2D>();

            while (true)
            {
                foreach (Vector2D caseToCheck in casesToCheck)
                {
                    if (goban[caseToCheck.X][caseToCheck.Y] == 0) // case empty
                    {
                        caseDico.resetDico();
                        return false;
                    }
                    else if (goban[caseToCheck.X][caseToCheck.Y] == playerToCheck)
                    {
                        foreach (Vector2D caseToAdd in getNeighbors(caseToCheck, gobanSize))
                        {
                            if ((goban[caseToAdd.X][caseToAdd.Y] == playerToCheck || goban[caseToAdd.X][caseToAdd.Y] == 0) && !caseDico.isCaseUsed(caseToAdd))
                            {
                                tempCasesToCheck.Add(caseToAdd);
                                caseDico.useCase(caseToAdd, 0);
                            }
                        }
                    }
                }
                if (tempCasesToCheck.Count == 0)
                {
                    break;
                }
                foreach (Vector2D caseToAdd in tempCasesToCheck)
                {
                    casesToCheck.Add(caseToAdd);
                }
                tempCasesToCheck = new List<Vector2D>();
            }

            gameController.ResetMultipleCases(casesToCheck);
            return true;
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
        private byte getPlayerToCheck(bool whoPlayed)
        {
            if (whoPlayed)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
