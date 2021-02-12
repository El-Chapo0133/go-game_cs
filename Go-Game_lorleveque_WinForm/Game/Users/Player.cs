/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Class for the player "Player"
* *****************************************************/


//using System.Timers;
using System;
using System.Windows.Forms;

namespace Go_Game_lorleveque_WinForm.Game.Users
{
    class Player
    {
        private Controller gameController;

        private string name, color;
        private Timer timer;
        private ushort score;
        private ushort intervalTimer = 1000;
        private int timerValue;

        /// <summary>
        /// Getter for the name of the player
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Getter for the color of the player
        /// </summary>
        public string Color
        {
            get { return color; }
        }
        /// <summary>
        /// Get and Set the score
        /// When the score is setted, update the label in the view
        /// </summary>
        public int Score
        {
            get { return score; }
            set {
                score = (ushort)value;
                gameController.UpdateLabelScore(score, color == "Noir" ? 2 : 1);
            }
        }
        /// <summary>
        /// Get or Set the timerValue
        /// When timervalue is setted, update the label in the view
        /// </summary>
        public int TimerValue
        {
            get { return timerValue; }
            set
            {
                timerValue = value;
                gameController.UpdateLabelTimer(timerValue);
            }
        }

        public Player(string name, string color, Controller controller)
        {
            gameController = controller;
            this.name = name;
            this.color = color;
            initTimer();
            score = 0;
            gameController.UpdateLabelName(name, color == "Noir" ? 2 : 1);
        }


        /// <summary>
        /// Initialize a timer
        /// </summary>
        private void initTimer()
        {
            timer = new Timer();
            timer.Interval = intervalTimer;
            timer.Tick += Timer_Tick;
        }
        /// <summary>
        /// Event for the tick, will increment the TimerValue and update the label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            TimerValue += intervalTimer / 1000;
        }
        /// <summary>
        /// Start the timer
        /// </summary>
        public void startTimer()
        {
            timer.Start();
        }
        /// <summary>
        /// Pause the timer
        /// </summary>
        public void pauseTimer()
        {
            timer.Stop();
        }
        /// <summary>
        /// Resume the timer
        /// </summary>
        public void resumeTimer()
        {
            timer.Start();
        }
        /// <summary>
        /// Reset the timer
        /// </summary>
        public void resetTimer()
        {
            initTimer();
        }
    }
}
