/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : Control everything with link to the go game (players, moves, rules, etc..)
 * *****************************************************/




using Go_Game_lorleveque_WinForm.Game.Users;
using Go_Game_lorleveque_WinForm.GameSettings;
using Go_Game_lorleveque_WinForm.Engine;
using System.Windows.Forms;
using System.Collections.Generic;
using Go_Game_lorleveque_WinForm.Utils;
using System.Drawing;
using Go_Game_lorleveque_WinForm.Game.Cases;

namespace Go_Game_lorleveque_WinForm.Game
{
    class Controller
    {
        private GoGame mainForm;
        private UserSettings userSettings;
        private Calculator calculator;
        private Player player1, player2; // player1 plays white, player2 plays black
        private Goban goban;
        private GobanCalculator gobanCalculator;
        private CaseDico caseDictionnary;
        private bool playingNow, gameStarted, gamePaused, playersLoaded; // true means black and false means white
        private uint round;

        public bool GameStarted
        {
            get { return gameStarted; }
            set { gameStarted = value; }
        }
        public bool GamePaused
        {
            get { return gamePaused; }
            set { gamePaused = value; }
        }
        public uint Round
        {
            get { return round; }
            set { round = value; }
        }
        public bool PlayingNow // for the json
        {
            get { return playingNow; }
        }
        public Player Player1 // for the json
        {
            get { return player1; }
        }
        public Player Player2 // for the json
        {
            get { return player2; }
        }
        public Goban GetGoban // for the json
        { 
            get { return goban; }
        }
        public bool PlayersLoaded
        {
            get { return playersLoaded; }
        }

        public Controller(GoGame goGame, UserSettings userSettings, CaseDico caseDico)
        {
            mainForm = goGame;
            this.userSettings = userSettings;
            caseDictionnary = caseDico;


            calculator = new Calculator();
            playingNow = true;
            goban = new Goban(userSettings);
            gobanCalculator = new GobanCalculator(this);
        }

        public void Played()
        {
            SwitchTimerPlayer(); // pause the timer of the player who played, and start the timer of the other player
            playingNow = playingNow ? false : true; // true means black and false means white
            round += 1;
        }

        public string WhoIsPlaying()
        {
            return playingNow ? "Noir" : "Blanc";
        }
        /// <summary>
        /// Get the player who is playing
        /// True mean black and false mean white
        /// </summary>
        /// <returns>boolean of the result</returns>
        public bool WhoIsPlayingBool()
        {
            return playingNow;
        }
        public string WhoIsPlayingForImage()
        {
            return playingNow ? "black" : "white";
        }
        public Player GetActualPlayer()
        {
            return playingNow ? player2 : player1;
        }
        public Player GetOtherPlayer()
        {
            return playingNow ? player1 : player2;
        }
        public void LoadTwoPlayers(string namePlayer1, string namePlayer2)
        {
            playersLoaded = true;
            player1 = new Player(namePlayer1, "Blanc", this);
            player2 = new Player(namePlayer2, "Noir", this);
        }
        public void ResetGoban()
        {
            playersLoaded = false; // when we reset, the players are not loaded
            goban.ResetGoban();
        }

        public void SwitchTimerPlayer()
        {
            if (playingNow)
            {
                player2.pauseTimer();
                player1.resumeTimer();
            }
            else
            {
                player1.pauseTimer();
                player2.resumeTimer();
            }
        }
        public void StopTimers()
        {
            player1.pauseTimer();
            player2.pauseTimer();
        }
        public void Pause()
        {
            gamePaused = true;
            GetActualPlayer().pauseTimer();
        }
        public void Resume()
        {
            gamePaused = false;
            GetActualPlayer().resumeTimer();
        }

        public void ResetMultipleCases(List<Vector2D> listCases)
        {
            ImageAjuster imageAjuster = new ImageAjuster();
            foreach (Vector2D caseToReset in listCases)
            {
                caseDictionnary.unUseCase(caseToReset, round);
                ((PictureBox)mainForm.Controls.Find(caseToReset.X + "." + caseToReset.Y, true)[0]).Image = Image.FromFile(imageAjuster.getImageGobanFromPos(caseToReset.X, caseToReset.Y, userSettings.GobanSize) + ".png");
            }

            int score = listCases.Count;

            GetActualPlayer().Score = GetActualPlayer().Score + score;
            GetOtherPlayer().Score = GetOtherPlayer().Score - score;


            goban.resetMultipleCases(listCases);
        }
        public void SetCase(Vector2D casePlayed)
        {
            GetActualPlayer().Score += 1;
            goban.setOneCase(casePlayed, playingNow);
        }
        public void CalculateGoban(Vector2D casePlayed)
        {
            gobanCalculator.Calculate(goban.AllGoban, casePlayed, playingNow, userSettings.GobanSize);
        }

        public void UpdateLabelTimer(int timePassed)
        {
            mainForm.UpdateLabelTimer(GetTimeLeft(timePassed));
        }
        public void UpdateLabelScore(int score, int player)
        {
            mainForm.UpdateLabelScore(score, player);
        }
        public void UpdateLabelName(string name, int player)
        {
            mainForm.UpdateLabelName(name, player);
        }
        private string GetTimeLeft(int timePassed)
        {
            return calculator.getTimeLeft(timePassed, userSettings.MaxTimeForPlayer);
        }
        public bool TimerGoesDown()
        {
            return GetActualPlayer().TimerValue == userSettings.MaxTimeForPlayer;
        }
        public uint GetRoundLeftForCase(Vector2D casePos)
        {
            Case actualCase = caseDictionnary.getCase(casePos);
            return (actualCase.UsedAtRound - round) + 4;
        }

        public void LoadFromHugeJson(JsonGenerator.HugeJson hugeJson)
        {
            round = hugeJson.round;

            playingNow = hugeJson.playingNow;
            gamePaused = hugeJson.gamePaused;
            gameStarted = hugeJson.gameStarted;
            goban.AllGoban = hugeJson.goban;

            player1 = new Player(hugeJson.playerName1, hugeJson.playerColor1, this);
            player2 = new Player(hugeJson.playerName2, hugeJson.playerColor2, this);
            playersLoaded = true;
            player1.Score = hugeJson.playerScore1;
            player2.Score = hugeJson.playerScore2;
            player1.TimerValue = (int)hugeJson.playerTimerValue1;
            player2.TimerValue = (int)hugeJson.playerTimerValue2;
            gamePaused = hugeJson.gamePaused;
            gameStarted = hugeJson.gameStarted;
        }
    }
}
