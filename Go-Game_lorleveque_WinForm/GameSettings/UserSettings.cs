



using Go_Game_lorleveque_WinForm.JsonGenerator;
/**
* Author : Loris Levêque
* Date : 04.02.2021
* Description : Get all the settings that the users can change
* *****************************************************/
namespace Go_Game_lorleveque_WinForm.GameSettings
{
    class UserSettings
    {
        private byte gobanSize;
        private uint maxTimeForPlayer;
        private bool somethingChanged;

        public byte GobanSize
        {
            get { return gobanSize; }
            set { gobanSize = value; somethingChanged = true; }
        }
        public uint MaxTimeForPlayer
        {
            get { return maxTimeForPlayer; }
            set { maxTimeForPlayer = value; somethingChanged = true; }
        }

        public UserSettings()
        {
            somethingChanged = false;
            gobanSize = 13;
            maxTimeForPlayer = 60 * 20; // 10min (60s * 10)
        }

        public void LoadFromHugeJson(HugeJson hugeJson)
        {
            gobanSize = hugeJson.gobanSize;
            maxTimeForPlayer = hugeJson.maxTimeForPlayer;
        }

        public bool SomethingChanged() // will say if something changed before the last call of this function
        {
            bool toReturn = somethingChanged;
            somethingChanged = false;
            return toReturn;
        }
    }
}
