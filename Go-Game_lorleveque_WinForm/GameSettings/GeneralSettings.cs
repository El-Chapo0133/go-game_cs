/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : Get all the settings that won't change over the time
 * *****************************************************/


namespace Go_Game_lorleveque_WinForm.GameSettings
{
    class GeneralSettings
    {
        private int maxCase, minCase, komi;

        public int MaxCase
        {
            get { return maxCase; }
        }
        public int MinCase
        {
            get { return minCase; }
        }
        public int Komi
        {
            get { return komi; }
        }

        public GeneralSettings()
        {
            maxCase = 38;
            minCase = 6;
            komi = 7;
        }
    }
}
