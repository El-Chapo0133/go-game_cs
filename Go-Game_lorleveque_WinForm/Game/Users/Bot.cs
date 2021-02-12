/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Class for the player "bot" which isn't a real player but don't tell him
* *****************************************************/


using Go_Game_lorleveque_WinForm.Utils;
using System.Collections.Generic;


namespace Go_Game_lorleveque_WinForm.Game.Users
{
    class Bot : Player
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public Bot() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="controller"></param>
        public Bot(string name, string color, Controller controller)
        {
            gameController = controller;
            this.name = name;
            this.color = color;
            InitTimer();
            score = 0;
            gameController = controller;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="goban"></param>
        /// <returns></returns>
        public Vector2D GetPlays(List<List<byte>> goban)
        {
            System.Random rng = new System.Random();
            Vector2D temp;
            do
            {
                temp = new Vector2D(rng.Next(goban.Count), rng.Next(goban.Count));
            }
            while (goban[temp.X][temp.Y] != 0);
            return temp;
        }
    }
}
