/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Class for the player "Player"
* *****************************************************/


using System.Windows.Forms;

namespace Go_Game_lorleveque_WinForm.Game.Users
{
    class Player
    {
        protected Controller gameController;

        protected string name, color;
        protected Timer timer;
        protected ushort score;
        protected ushort intervalTimer = 1000;
        protected int timerValue;

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

        /// <summary>
        /// Constructor
        /// </summary>
        public Player() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="color">The color the player is</param>
        /// <param name="controller">The gameController</param>
        public Player(string name, string color, Controller controller)
        {
            gameController = controller;
            this.name = name;
            this.color = color;
            InitTimer();
            score = 0;
            gameController.UpdateLabelName(name, color == "Noir" ? 2 : 1);
        }

        /// <summary>
        /// Initialize a timer
        /// </summary>
        protected void InitTimer()
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
        public void StartTimer()
        {
            timer.Start();
        }

        /// <summary>
        /// Pause the timer
        /// </summary>
        public void PauseTimer()
        {
            timer.Stop();
        }

        /// <summary>
        /// Stop definitly the timer
        /// </summary>
        public void StopTimer()
        {
            timer.Dispose();
        }

        /// <summary>
        /// Resume the timer
        /// </summary>
        public void ResumeTimer()
        {
            timer.Start();
        }

        /// <summary>
        /// Reset the timer
        /// </summary>
        public void ResetTimer()
        {
            InitTimer();
        }

        /// <summary>
        /// Dispose the player
        /// </summary>
        public void Dispose()
        {
            name = "";
            color = "";
            timer.Stop();
            timer.Dispose();
            timerValue = 0;
            score = 0;
        }
    }
}
