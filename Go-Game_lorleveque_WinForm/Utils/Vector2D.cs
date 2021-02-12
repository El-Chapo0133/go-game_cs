/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : Provide a class that has 2 properties
 * *****************************************************/



namespace Go_Game_lorleveque_WinForm.Utils
{
    public class Vector2D
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
