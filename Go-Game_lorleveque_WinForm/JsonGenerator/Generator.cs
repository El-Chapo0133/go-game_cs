using Go_Game_lorleveque_WinForm.Game;
using Go_Game_lorleveque_WinForm.GameSettings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace Go_Game_lorleveque_WinForm.JsonGenerator
{
    class Generator
    {
        private HugeJson hugeJson;
        public void createHugeJson(
            Controller gameController, 
            UserSettings userSettings, 
            List<string> listHistory, 
            List<Game.Cases.Case> caseDico, 
            Goban goban
        )
        {
            List<int> listCaseX = new List<int>();
            List<int> listCaseY = new List<int>();
            List<uint> listCaseUsedAtRound = new List<uint>();
            List<bool> listCaseUsed = new List<bool>();

            foreach (Game.Cases.Case newCase in caseDico)
            {
                listCaseX.Add(newCase.Position.X);
                listCaseY.Add(newCase.Position.Y);
                listCaseUsedAtRound.Add(newCase.UsedAtRound);
                listCaseUsed.Add(newCase.IsUsed);
            }

            hugeJson = new HugeJson(
                userSettings.GobanSize,
                gameController.Round,
                userSettings.MaxTimeForPlayer,
                gameController.PlayingNow,
                gameController.GamePaused,
                gameController.GameStarted,
                listHistory,
                goban.AllGoban,
                listCaseX,
                listCaseY,
                listCaseUsedAtRound,
                listCaseUsed,
                gameController.Player1.Name,
                gameController.Player2.Name,
                gameController.Player1.Color,
                gameController.Player2.Color,
                (uint)gameController.Player1.TimerValue,
                (uint)gameController.Player2.TimerValue,
                (ushort)gameController.Player1.Score,
                (ushort)gameController.Player2.Score
            );
        }
        public void serializeHugeJson(string filename)
        {
            if (hugeJson == null)
            {
                throw new Exception("Huge json isn't initialized");
            }
            string jsonString = JsonConvert.SerializeObject(hugeJson);
            File.WriteAllText(filename, jsonString);
        }
    }
}
