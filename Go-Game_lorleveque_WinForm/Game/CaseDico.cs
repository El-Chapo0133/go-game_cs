/**
* Author : Loris Levêque
* Date : 11.02.2021
* Description : Class dictionnary-like for the cases of the goban
* *****************************************************/



using Go_Game_lorleveque_WinForm.Game.Cases;
using Go_Game_lorleveque_WinForm.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Go_Game_lorleveque_WinForm.Game
{
    class CaseDico
    {
        private List<Case> listCasesUsed;

        public List<Case> GetAllDico
        {
            get { return listCasesUsed; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CaseDico()
        {
            listCasesUsed = new List<Case>();
        }

        /// <summary>
        /// Use a case from his pos and set the round
        /// </summary>
        /// <param name="input">The position of the case</param>
        /// <param name="round">The round when the case has been used</param>
        public void useCase(Vector2D input, uint round)
        {
            listCasesUsed.Add(new Case(input, round));
        }
        public void unUseCase(Vector2D input, uint round)
        {
            for (int index = 0; index < listCasesUsed.Count; index++)
            {
                if (listCasesUsed[index].Position.X == input.X && listCasesUsed[index].Position.Y == input.Y)
                {
                    listCasesUsed[index].unUse();
                    listCasesUsed[index].actualiseUsedAtRound(round);
                    return;
                }
            }
            //System.Diagnostics.Debug.WriteLine("Could not find the case :c");
        }

        /// <summary>
        /// Get if a case is used from his position
        /// </summary>
        /// <param name="input">The position of the case</param>
        /// <returns>If the case is used</returns>
        public bool isCaseUsed(Vector2D input)
        {
            var output =
                from caseGoban in listCasesUsed
                where caseGoban.Position.X == input.X && caseGoban.Position.Y == input.Y && caseGoban.IsUsed
                select caseGoban;

            return output.Count<Case>() != 0;
        }

        /// <summary>
        /// Check if a case is blocked from his position (not used but can't sue it)
        /// </summary>
        /// <param name="input">The position of the case</param>
        /// <returns>If the case is blocked</returns>
        public bool isCaseBlocked(Vector2D input)
        {
            var output =
                from caseGoban in listCasesUsed
                where caseGoban.Position.X == input.X && caseGoban.Position.Y == input.Y && !caseGoban.IsUsed
                select caseGoban;

            return output.Count<Case>() != 0;
        }

        /// <summary>
        /// Reset the whole dico
        /// </summary>
        public void resetDico()
        {
            listCasesUsed = new List<Case>();
        }

        /// <summary>
        /// Update the whole dico, when a case is blocked but his round is >3 round last, the dico removes it
        /// </summary>
        /// <param name="round">The actual round</param>
        public void updateDicoFromRound(uint round)
        {
            List<int> indexToRemove = new List<int>();
            if (round < 3)
            {
                return;
            }
            for (int index = 0; index < listCasesUsed.Count; index++)
            {
                if (listCasesUsed[index].UsedAtRound + 3 == round && !listCasesUsed[index].IsUsed)
                {
                    indexToRemove.Add(index);
                }
            }
            indexToRemove.Reverse();
            foreach (int index in indexToRemove)
            {
                listCasesUsed.RemoveAt(index);
            }
        }

        /// <summary>
        /// Get a case from his position
        /// </summary>
        /// <param name="input">The position of the case</param>
        /// <returns>The case found</returns>
        public Case getCase(Vector2D input)
        {
            var output =
                from caseGoban in listCasesUsed
                where caseGoban.Position.X == input.X && caseGoban.Position.Y == input.Y
                select caseGoban;
            return output.First();
        }
    }
}
