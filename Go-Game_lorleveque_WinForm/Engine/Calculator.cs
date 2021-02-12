

using System;
/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Making some relativly complex calculs
* *****************************************************/
namespace Go_Game_lorleveque_WinForm.Engine
{
    class Calculator
    {
        public int getCaseSize(int gobanSize)
        {
            return 770 / gobanSize;
        }

        public string getTimeLeft(int timePassed, uint maxTimeForPlayer)
        {
            TimeSpan time = TimeSpan.FromSeconds(maxTimeForPlayer - timePassed);
            return time.ToString(@"hh\:mm\:ss");
        }
        public string getMaxTimeBase(uint maxTimeForPlayer)
        {
            TimeSpan time = TimeSpan.FromSeconds(maxTimeForPlayer);
            return time.ToString(@"hh\:mm\:ss");
        }
        public int getHours(uint maxTimeForPlayer)
        {
            TimeSpan time = TimeSpan.FromSeconds(maxTimeForPlayer);
            return time.Hours;
        }
        public int getMinutes(uint maxTimeForPlayer)
        {
            TimeSpan time = TimeSpan.FromSeconds(maxTimeForPlayer);
            return time.Minutes;
        }
        public int getSeconds(uint maxTimeForPlayer)
        {
            TimeSpan time = TimeSpan.FromSeconds(maxTimeForPlayer);
            return time.Seconds;
        }
    }
}
