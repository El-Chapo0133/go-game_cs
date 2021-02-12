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

        public CaseDico()
        {
            listCasesUsed = new List<Case>();
        }

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
            System.Diagnostics.Debug.WriteLine("Could not find the case :c");
        }

        public bool isCaseUsed(Vector2D input)
        {
            var output =
                from caseGoban in listCasesUsed
                where caseGoban.Position.X == input.X && caseGoban.Position.Y == input.Y && caseGoban.IsUsed
                select caseGoban;

            return output.Count<Case>() != 0;
        }
        public bool isCaseBlocked(Vector2D input)
        {
            var output =
                from caseGoban in listCasesUsed
                where caseGoban.Position.X == input.X && caseGoban.Position.Y == input.Y && !caseGoban.IsUsed
                select caseGoban;

            return output.Count<Case>() != 0;
        }
        public void resetDico()
        {
            listCasesUsed = new List<Case>();
        }
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
            foreach (int index in indexToRemove)
            {
                listCasesUsed.RemoveAt(index);
            }
        }
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
