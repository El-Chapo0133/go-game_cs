using Go_Game_lorleveque_WinForm.Game;
using Go_Game_lorleveque_WinForm.GameSettings;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Go_Game_lorleveque_WinForm.JsonGenerator
{
    class Loader
    {
        private HugeJson hugeJson;

        public void DeserializeFile(string filename)
        {
            string jsonFile = File.ReadAllText(filename);
            hugeJson = JsonConvert.DeserializeObject<HugeJson>(jsonFile);
        }
        public void LoadFromJson(Controller gameController, UserSettings userSettings, GoGame mainForm)
        {
            if (hugeJson == null)
            {
                throw new Exception("Huge json isn't initialized");
            }

            gameController.LoadFromHugeJson(hugeJson);
            userSettings.LoadFromHugeJson(hugeJson);
            mainForm.LoadFromHugeJson(hugeJson);
        }
    }
}
