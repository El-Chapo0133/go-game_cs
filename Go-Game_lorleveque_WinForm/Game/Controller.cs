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
using System;

namespace Go_Game_lorleveque_WinForm.Game
{
    class Controller
    {
        private GoGame mainForm;
        private UserSettings userSettings;
        private Calculator calculator;
        private Player playerWhite, playerBlack; // playerWhite plays white, playerBlack plays black
        private Bot bot;
        private Bot secondBot; // second bot for the machine learning
        private Goban goban;
        private GobanCalculator gobanCalculator;
        private CaseDico caseDictionnary;
        private bool playingNow; // true means black and false means white
        public bool gameStarted, gameEnded, gamePaused, playersLoaded;
        private uint round;

        public bool GameStarted
        {
            get { return gameStarted; }
            set { gameStarted = value; 
                if (gameStarted)
                {
                    gameEnded = false;
                }
            }
        }
        public bool GameEnded
        {
            get { return gameEnded; }
            set { gameEnded = value;
                if (gameEnded)
                {
                    gameStarted = false;
                }
            }
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
        public bool PlayingNow
        {
            get { return playingNow; }
            set { playingNow = value; }
        }
        public Player Player1 // for the json
        {
            get { return playerWhite; }
        }
        public Player Player2 // for the json
        {
            get { return playerBlack; }
        }
        public Goban GetGoban // for the json
        { 
            get { return goban; }
        }
        public bool PlayersLoaded
        {
            get { return playersLoaded; }
        }
        public bool GotBot
        {
            get { return bot != null; }
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
            // comment this for the machine learning training
            playingNow = playingNow ? false : true; // true means black and false means white
            round += 1;

            //if (round == 19 * 16)
            //{
            //    mainForm.resetGoban();

            //    //ApiCall api = new ApiCall("http://127.0.0.1:8080/fit");
            //    //api.Call(); // will fit the ml model

            //    // throw new Exception("End of the game");
            //}
            // comment this for the machine learning training
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
            return playingNow ? playerBlack : GotBot ? bot : playerWhite;
        }
        public Player GetOtherPlayer()
        {
            return playingNow ? GotBot ? bot : playerWhite : playerBlack;
        }
        public void LoadOnePlayer(string playerName)
        {
            playersLoaded = true;
            playerBlack = new Player(playerName, "Noir", this);
        }
        public void LoadTwoPlayers(string namePlayer1, string namePlayer2)
        {
            playersLoaded = true;
            playerWhite = new Player(namePlayer1, "Blanc", this);
            playerBlack = new Player(namePlayer2, "Noir", this);
        }
        public void LoadBot()
        {
            bot = new Bot("Ordinateur", "Blanc", this);
            UpdateLabelName("Ordinateur", 1);
        }
        public void LoadSecondBot()
        {
            secondBot = new Bot("Ordinateur", "Blanc", this);
            UpdateLabelName("Ordinateur", 2);
        }
        public void ResetGoban()
        {
            playersLoaded = false; // when we reset, the players are not loaded
            goban.ResetGoban();
            goban.initGoban();
        }

        /// <summary>
        /// Switch the timer (one starts, the other stops)
        /// </summary>
        public void SwitchTimerPlayer()
        {
            if (playingNow)
            {
                playerBlack.PauseTimer();
                if (!GotBot)
                {
                    playerWhite.ResumeTimer();
                }
                else
                {
                    bot.ResumeTimer();
                }
            }
            else
            {
                playerBlack.ResumeTimer();
                if (!GotBot)
                {
                    playerWhite.PauseTimer();
                }
                else
                {
                    bot.ResumeTimer();
                }
            }
        }
        public void StopTimers()
        {
            if (gameStarted)
            {
                if (playerWhite != null)
                {
                    playerWhite.StopTimer();
                }
                playerBlack.StopTimer();
            }
        }
        public void Pause()
        {
            gamePaused = true;
            if (gameStarted)
            {
                GetActualPlayer().PauseTimer();
            }
        }
        public void Resume()
        {
            gamePaused = false;
            if (gameStarted)
            {
                GetActualPlayer().ResumeTimer();
            }
        }

        public void DisposePlayers()
        {
            playerBlack.Dispose();
            playerWhite.Dispose();
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
            // comment this for the machine learning training


            goban.resetMultipleCases(listCases);
        }
        public void SetCase(Vector2D casePlayed)
        {
            GetActualPlayer().Score += 1; // comment this for the machine learning training
            goban.setOneCase(casePlayed, playingNow);
        }
        public void SetCaseWithColor(Vector2D casePlayed, string color)
        {
            goban.setOneCase(casePlayed, color == "black" ? true : false);
        }
        public byte GetCase(Vector2D caseWanted)
        {
            return goban.GetOneCase(caseWanted);
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

            playerWhite = new Player(hugeJson.playerName1, hugeJson.playerColor1, this);
            playerBlack = new Player(hugeJson.playerName2, hugeJson.playerColor2, this);
            playersLoaded = true;
            playerWhite.Score = hugeJson.playerScore1;
            playerBlack.Score = hugeJson.playerScore2;
            playerWhite.TimerValue = (int)hugeJson.playerTimerValue1;
            playerBlack.TimerValue = (int)hugeJson.playerTimerValue2;
            gamePaused = hugeJson.gamePaused;
            gameStarted = hugeJson.gameStarted;
        }

        public string GetPlaysFromBot(List<List<byte>> wholeGoban)
        {
            Vector2D result = bot.GetPlays(wholeGoban);
            return result.X + "." + result.Y;
        }
    }
}
