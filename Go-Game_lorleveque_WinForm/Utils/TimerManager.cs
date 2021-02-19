

using Go_Game_lorleveque_WinForm.Game.Users;
using System.Timers;
/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Class to manage the timers
* *****************************************************/
namespace Go_Game_lorleveque_WinForm.Utils
{
    class TimerForPlayer
    {
        private Timer timer;
        private Player player;

        /// <summary>
        /// Constructor
        /// </summary>
        public TimerForPlayer()
        {
            
        }
        public void Dispose()
        {
            timer.Dispose();
            player.Dispose();
        }
    }
    class TimerManager
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public TimerManager()
        {

        }


    }
}
