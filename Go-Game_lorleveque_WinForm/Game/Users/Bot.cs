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
        private const string BASEAPIURL = "http://127.0.0.1:8080/";
        private ApiCall api;

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

            api = new ApiCall("http://127.0.0.1:8080/");
            api.AddAcceptType("application/json");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="goban"></param>
        /// <returns></returns>
        public Vector2D GetPlays(List<List<byte>> goban)
        {
            string param = "";
            for (int indexX = 0; indexX < goban.Count; indexX++)
            {
                for (int indexY = 0; indexY < goban[indexX].Count; indexY++)
                {
                    if (gameController.GetCase(new Vector2D(indexX, indexY)) == 0)
                    {
                        param += "&" + indexX + "." + indexY + "=0";
                        continue;
                    }
                    param += "&" + indexX + "." + indexY + "=" + (gameController.WhoIsPlayingBool() ? gameController.GetCase(new Vector2D(indexX, indexY)) == 1 ? "1" : "-1" : gameController.GetCase(new Vector2D(indexX, indexY)) == 2 ? "1" : "-1");
                }
            }

            api.callWithGetParams("?" + shift(param));

            var result = api.getResultBot();
            if (result == null)
            {
                throw new System.Exception("Result of the api is null");
            }

            return new Vector2D(result.x, result.y);

            //System.Random rng = new System.Random();
            //Vector2D temp;
            //do
            //{
            //    temp = new Vector2D(rng.Next(goban.Count), rng.Next(goban.Count));
            //}
            //while (goban[temp.X][temp.Y] != 0);
            //return temp;
        }

        private string shift(string input)
        {
            int inputLength = input.Length;
            return input.Substring(1, inputLength - 1);
        }
    }
}
