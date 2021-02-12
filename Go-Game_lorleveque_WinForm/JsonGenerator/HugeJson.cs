using System.Collections.Generic;

namespace Go_Game_lorleveque_WinForm.JsonGenerator
{
    public class HugeJson
    {
        public byte gobanSize;
        public uint round, maxTimeForPlayer, playerTimerValue1, playerTimerValue2;
        public bool playingNow, gamePaused, gameStarted;
        public string playerName1, playerColor1, playerName2, playerColor2;
        public ushort playerScore1, playerScore2;

        public List<string> gameHistory;
        public List<List<byte>> goban;
        public List<int> caseDicoX, caseDicoY;
        public List<uint> caseDicoUsedAtRound;
        public List<bool> caseDicoUsed;

        public HugeJson(
            byte gobanSize,
            uint round,
            uint maxTimeForPlayer,
            bool playingNow,
            bool gamePaused,
            bool gameStarted,
            List<string> listHistory,
            List<List<byte>> goban,
            List<int> caseDicoX,
            List<int> caseDicoY,
            List<uint> caseDicoUsedAtRound,
            List<bool> caseDicoUsed,
            string playerName1,
            string playerName2,
            string playerColor1,
            string playerColor2,
            uint playerTimerValue1,
            uint playerTimerValue2,
            ushort playerScore1,
            ushort playerScore2
        )
        {
            this.gobanSize = gobanSize;
            this.round = round;
            this.maxTimeForPlayer = maxTimeForPlayer;
            this.playingNow = playingNow;
            this.gamePaused = gamePaused;
            this.gameStarted = gameStarted;
            this.gameHistory = listHistory;
            this.goban = goban;
            this.caseDicoX = caseDicoX;
            this.caseDicoY = caseDicoY;
            this.caseDicoUsedAtRound = caseDicoUsedAtRound;
            this.caseDicoUsed = caseDicoUsed;
            this.playerName1 = playerName1;
            this.playerName2 = playerName2;
            this.playerColor1 = playerColor1;
            this.playerColor2 = playerColor2;
            this.playerTimerValue1 = playerTimerValue1;
            this.playerTimerValue2 = playerTimerValue2;
            this.playerScore1 = playerScore1;
            this.playerScore2 = playerScore2;
        }
    }
}
