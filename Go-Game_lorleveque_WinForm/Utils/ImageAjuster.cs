/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : Class to get the right images in the right time
 * *****************************************************/



using System;

namespace Go_Game_lorleveque_WinForm.Utils
{
    class ImageAjuster
    {
        public String getImageGobanFromPos(int x, int y, int maxSize)
        {
            if (x == 0 && y == 0)
            {
                return "..\\..\\Images\\Goban\\top_left";
            }
            else if (x == 0 && y == maxSize - 1)
            {
                return "..\\..\\Images\\Goban\\top_right";
            }
            else if (x == maxSize - 1 && y == 0)
            {
                return "..\\..\\Images\\Goban\\bottom_left";
            }
            else if (x == maxSize - 1 && y == maxSize - 1)
            {
                return "..\\..\\Images\\Goban\\bottom_right";
            }
            else if (x == 0)
            {
                return "..\\..\\Images\\Goban\\top";
            }
            else if (x == maxSize - 1)
            {
                return "..\\..\\Images\\Goban\\bottom";
            }
            else if (y == 0)
            {
                return "..\\..\\Images\\Goban\\left";
            }
            else if (y == maxSize - 1)
            {
                return "..\\..\\Images\\Goban\\right";
            }
            else if (maxSize % 2 == 1 && maxSize / 2 >= 6 && ((x == maxSize / 4 && y == maxSize / 4) || (x == (int)(((float)maxSize / 4) * 3) && y == maxSize / 4) || (y == (int)(((float)maxSize / 4) * 3) && x == maxSize / 4) || (x == (int)(((float)maxSize / 4) * 3) && y == (int)(((float)maxSize / 4) * 3))))
            {
                return "..\\..\\Images\\Goban\\center_point";
            }
            else if (maxSize % 2 == 0 && maxSize / 2 >= 6 && ((x == maxSize / 4 && y == maxSize / 4) || (x == maxSize / 4 && y == maxSize - 1 - maxSize / 4) || (x == maxSize - 1 - maxSize / 4 && y == maxSize / 4) || (x == maxSize - 1 - maxSize / 4 && y == maxSize - 1 - maxSize / 4)))
            {
                return "..\\..\\Images\\Goban\\center_point";
            }
            else if (maxSize % 2 == 1 && x == maxSize / 2 && y == maxSize / 2)
            {
                return "..\\..\\Images\\Goban\\center_point";
            }
            else
            {
                return "..\\..\\Images\\Goban\\center";
            }
        }
    }
}
