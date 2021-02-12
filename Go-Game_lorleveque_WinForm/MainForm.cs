/**
 * Author : Loris Levêque
 * Date : 04.02.2021
 * Description : It's litherally the base file or the program
 * *****************************************************/

using Go_Game_lorleveque_WinForm.Engine;
using Go_Game_lorleveque_WinForm.Game;
using Go_Game_lorleveque_WinForm.GameSettings;
using Go_Game_lorleveque_WinForm.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Go_Game_lorleveque_WinForm
{
    public partial class GoGame : Form
    {
        private GeneralSettings generalSettings;
        private UserSettings userSettings;
        private ImageAjuster imageAjuster;
        private Controller gameController;
        private CaseDico casesDictionnary;

        public GoGame()
        {
            InitializeComponent();
            generalSettings = new GeneralSettings();
            userSettings = new UserSettings();
            imageAjuster = new ImageAjuster();
            casesDictionnary = new CaseDico();
            gameController = new Controller(this, userSettings, casesDictionnary);

            //gameController.LoadTwoPlayers("Blanc", "Noir");
        }

        /// <summary>
        /// at the load of the app, will generate the goban
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoGame_Load(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator();
            string maxTimeSpan = calculator.getMaxTimeBase(userSettings.MaxTimeForPlayer);
            Panel goban = new Panel();
            goban.Size = new Size(770, 770);
            goban.Location = new Point(300, 20);
            goban.Name = "Goban";

            labelTimer1.Text = maxTimeSpan;
            labelTimer2.Text = maxTimeSpan;

            this.Controls.Add(initGoban(goban));
        }

        /// <summary>
        /// Event when a case of the goban is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoGameCase_Click(object sender, EventArgs e)
        {
            if (!gameController.GameStarted)
            {
                MessageBox.Show("La partie n'a pas encore démarrée, utilisez le bouton \"démarrer\" pour démarrer", "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (gameController.GamePaused)
            {
                MessageBox.Show("La partie est en pause, utilisez le bouton \"Reprendre\" pour reprendre la partie", "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            String[] buttonNameSplitted = ((PictureBox)sender).Name.Split('.');
            Vector2D casePos = new Vector2D(Convert.ToInt32(buttonNameSplitted[0]), Convert.ToInt32(buttonNameSplitted[1]));

            if (casesDictionnary.isCaseBlocked(casePos))
            {
                MessageBox.Show("Vous ne pouvez pas poser de caillou ici, la case est bloquée pour " + gameController.GetRoundLeftForCase(casePos) + " tours.", "Mouvement interdit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (casesDictionnary.isCaseUsed(casePos))
            {
                MessageBox.Show("Vous ne pouvez pas poser de caillou ici, il y en a déjà un", "Mouvement interdit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            casesDictionnary.useCase(casePos, gameController.Round);
            ((PictureBox)sender).Image = Image.FromFile(imageAjuster.getImageGobanFromPos(casePos.X, casePos.Y, userSettings.GobanSize) + "_" + gameController.WhoIsPlayingForImage() + ".png");

            int last_score = gameController.GetActualPlayer().Score;
            gameController.SetCase(casePos);
            gameController.CalculateGoban(casePos);

            if (gameController.WhoIsPlayingBool())
            {
                pictureBoxPlaynowBlack.Visible = false;
                pictureBoxPlaynowWhite.Visible = true;
            }
            else
            {
                pictureBoxPlaynowBlack.Visible = true;
                pictureBoxPlaynowWhite.Visible = false;
            }


            listViewHistory.Items.Add(gameController.Round + ". Le joueur " + gameController.WhoIsPlaying() + " a joué " + casePos.X + ":" + casePos.Y + " et a gagné: " + (gameController.GetActualPlayer().Score- last_score) + "points");
            listViewHistory.EnsureVisible(listViewHistory.Items.Count - 1); // set the index to the last one, it always display the last move

            casesDictionnary.updateDicoFromRound(gameController.Round);
            gameController.Played();
        }

        /// <summary>
        /// Reset the goban
        /// </summary>
        private void resetGoban()
        {
            Calculator calculator = new Calculator();
            Panel panel = searchGobanPanelAndRemoveAllChilds();

            listViewHistory.Clear();
            System.Diagnostics.Debug.WriteLine("|- ListView réinitialisées");
            gameController.Round = 0;
            gameController.GamePaused = false;
            gameController.GameStarted = false;
            System.Diagnostics.Debug.WriteLine("|- controlleur réinitialisés");
            gameController.StopTimers();
            gameController.LoadTwoPlayers("Blanc", "Noir");
            System.Diagnostics.Debug.WriteLine("|- Joueurs réinitialisés");
            labelScoreValue1.Text = "0";
            labelScoreValue2.Text = "0";
            string maxTimeSpan = calculator.getMaxTimeBase(userSettings.MaxTimeForPlayer);
            labelTimer1.Text = maxTimeSpan;
            labelTimer2.Text = maxTimeSpan;
            System.Diagnostics.Debug.WriteLine("|- Labels réinitialisés");
            buttonStart.Enabled = true;
            buttonPause.Text = "Pause";
            System.Diagnostics.Debug.WriteLine("|- Boutons réinitialisés");
            casesDictionnary.resetDico();
            System.Diagnostics.Debug.WriteLine("|- Dictionnaire réinitialisé");
            initGoban(panel);               // reset the displaying goban
            gameController.ResetGoban();    // reset the virtual goban in the controller (without this you can make "ghost kills")
            System.Diagnostics.Debug.WriteLine("|- Panel initialisé");
        }


        private Panel searchGobanPanelAndRemoveAllChilds()
        {
            Searching searcher = new Searching();
            Panel panel = searcher.searchPanelByName(getAllControls(), "Goban");
            int controlsCount = panel.Controls.Count;

            for (int _ = 0; _ < controlsCount; _++)  // I call every var "_" when i won't use it
            {
                panel.Controls.RemoveAt(0);
            }

            return panel;
        }

        /// <summary>
        /// Event for the start of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // if no players aare loaded, display a form the ask the two player name
            if (!gameController.PlayersLoaded)
            {
                bool playersNameWrong = true, cancel = false;
                string[] playersName = { "", "" };
                do
                {
                    const int FORMSETTNGSIZEHEIGHT = 300;
                    const int FORMSETTNGSIZEWIDTH = 500;
                    Form prompt = new Form();
                    prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
                    prompt.Width = FORMSETTNGSIZEWIDTH;
                    prompt.Height = FORMSETTNGSIZEHEIGHT;
                    prompt.Text = "Joueurs";
                    prompt.Icon = new Icon("..\\..\\Images\\Icons\\main.ico");
                    prompt.MaximizeBox = false;
                    prompt.MinimizeBox = false;
                    prompt.ControlBox = false;

                    Label labelPlayer1 = new Label() { Top = 24, Left = 50 };
                    Label labelPlayer2 = new Label() { Top = 124, Left = 50 };
                    labelPlayer1.Text = "Nom du joueur 1";
                    labelPlayer2.Text = "Nom du joueur 2";
                    TextBox textBoxPlayer1 = new TextBox() { Top = 20, Left = 150, Width = 200, };
                    TextBox textBoxPlayer2 = new TextBox() { Top = 120, Left = 150, Width = 200, };

                    Label labelBot = new Label() { Top = 124, Left = 370, Width = 30 };
                    labelBot.Text = "Bot:";
                    CheckBox checkBoxBot = new CheckBox() { Top = 120, Left = 400 };

                    checkBoxBot.CheckedChanged += (sender_, e_) =>
                    {
                        textBoxPlayer2.Enabled = ((CheckBox)sender_).Checked ? false : true;
                    };

                    prompt.Controls.Add(labelPlayer1);
                    prompt.Controls.Add(textBoxPlayer1);
                    prompt.Controls.Add(labelPlayer2);
                    prompt.Controls.Add(textBoxPlayer2);
                    prompt.Controls.Add(labelBot);
                    prompt.Controls.Add(checkBoxBot);


                    Button terminated = new Button() { Text = "Terminé", Left = FORMSETTNGSIZEWIDTH - 150, Width = 100, Top = FORMSETTNGSIZEHEIGHT - 100 };
                    terminated.Click += (_sender, _e) => {
                        playersName[0] = textBoxPlayer2.Text; // play the black
                        playersName[1] = textBoxPlayer1.Text; // play the white
                        prompt.Close();

                        if (checkBoxBot.Checked && playersName[0] == "")
                        {
                            playersNameWrong = true;
                            if (MessageBox.Show("Les nom du joueur ne peut pas être vide", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                            {
                                cancel = true;
                            }
                        }
                        else if (!checkBoxBot.Checked && (playersName[0] == "" || playersName[1] == ""))
                        {
                            playersNameWrong = true;
                            if (MessageBox.Show("Les noms des joueurs ne peuvent pas être vides", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                            {
                                cancel = true;
                            }
                        }
                        else if (playersName[0] == playersName[1])
                        {
                            playersNameWrong = true;
                            if (MessageBox.Show("Les noms des joueurs ne peuvent pas être identiques", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                            {
                                cancel = true;
                            }
                        }
                        else
                        {
                            playersNameWrong = false;
                        }
                    };

                    prompt.Controls.Add(terminated);

                    prompt.ShowDialog();

                    if (cancel) { return; }
                    
                } while (playersNameWrong);
                gameController.LoadTwoPlayers(playersName[0], playersName[1]);
            }

            buttonSave.Enabled = true;
            gameController.GameStarted = true;
            gameController.GetActualPlayer().startTimer();
            ((Button)sender).Enabled = false;
        }

        /// <summary>
        /// Event for the reset of the goban
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment reset la partie ?", "Reset la partie", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                buttonSave.Enabled = false;
                resetGoban();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (!gameController.GamePaused)
            {
                gameController.Pause();
                ((Button)sender).Text = togglePauseButtonText(gameController.GamePaused);
            }
            else
            {
                gameController.Resume();
                ((Button)sender).Text = togglePauseButtonText(gameController.GamePaused);
            }            
        }
        private string togglePauseButtonText(bool gamePaused)
        {
            return gamePaused ? "Reprendre" : "Pause";
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (gameController.GameStarted)
            {
                MessageBox.Show("Vous ne pouvez pas changer les paramètres durant la partie", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            showFormSettings();

            if (userSettings.SomethingChanged())
            {
                if (MessageBox.Show("Voulez vous recharger le goban pour que le paramètre [taille du goban] s'applique ?", "Informations", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    resetGoban();
                }
                else
                {
                    Calculator calculator = new Calculator();
                    UpdateLabelsTimer(calculator.getMaxTimeBase(userSettings.MaxTimeForPlayer));
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json Files | *.json";
            saveFileDialog.DefaultExt = "json";
            gameController.StopTimers(); // Pause the timers when saving
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;

                List<string> listViewItems = listViewHistory.Items.Cast<ListViewItem>()
                                                                  .Select(item => item.Text)
                                                                  .ToList();

                JsonGenerator.Generator generator = new JsonGenerator.Generator();
                generator.createHugeJson(
                    gameController,
                    userSettings,
                    listViewItems,
                    casesDictionnary.GetAllDico,
                    gameController.GetGoban
                );
                generator.serializeHugeJson(filename);
            }
            gameController.Resume(); // then the game continue
        }

        private void buttonCharge_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json Files | *.json";
            openFileDialog.DefaultExt = "json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;

                JsonGenerator.Loader loader = new JsonGenerator.Loader();
                loader.DeserializeFile(filename);
                loader.LoadFromJson(gameController, userSettings, this);
            }
        }



        // ======================================================================================================

        public void UpdateLabelTimer(string text)
        {
            string whichtoUpdate = gameController.WhoIsPlaying() == "Noir" ? "labelTimer2" : "labelTimer1";
            ((Label)Controls.Find(whichtoUpdate, true)[0]).Text = text;
        }
        public void UpdateLabelsTimer(string text)
        {
            ((Label)Controls.Find("labelTimer1", true)[0]).Text = text;
            ((Label)Controls.Find("labelTimer2", true)[0]).Text = text;
        }
        public void UpdateLabelName(string name, int player)
        {
            string whichtoUpdate = "labelPlayer" + player;
            ((Label)Controls.Find(whichtoUpdate, true)[0]).Text = name;
        }
        public void UpdateLabelScore(int score, int player)
        {
            string whichtoUpdate = "labelScoreValue" + player;

            ((Label)Controls.Find(whichtoUpdate, true)[0]).Text = score.ToString();
        }
        private Panel initGoban(Panel inputPanel)
        {
            Calculator calculator = new Calculator();
            int caseSize = calculator.getCaseSize(userSettings.GobanSize);

            for (int index_x = 0; index_x < userSettings.GobanSize; index_x++)
            {
                for (int index_y = 0; index_y < userSettings.GobanSize; index_y++)
                {
                    PictureBox tempObject = new PictureBox();
                    tempObject.Name = index_x + "." + index_y;
                    tempObject.Size = new Size(caseSize, caseSize);
                    tempObject.Location = new Point(caseSize * index_y, caseSize * index_x);

                    tempObject.Image = Image.FromFile(imageAjuster.getImageGobanFromPos(index_x, index_y, userSettings.GobanSize) + ".png");

                    tempObject.SizeMode = PictureBoxSizeMode.StretchImage;

                    tempObject.Click += GoGameCase_Click;

                    inputPanel.Controls.Add(tempObject);
                }
            }

            return inputPanel;
        }

        /// <summary>
        /// Get all controls of the MainForm
        /// </summary>
        /// <returns></returns>
        private object[] getAllControls()
        {
            object[] allControls = new object[this.Controls.Count];
            foreach (object temp in this.Controls)
            {
                allControls[0] = temp;
            }
            return allControls;
        }

        private void showFormSettings()
        {
            Calculator calculator = new Calculator();
            const int FORMSETTNGSIZEHEIGHT = 300;
            const int FORMSETTNGSIZEWIDTH = 500;
            Form prompt = new Form();
            prompt.FormBorderStyle = FormBorderStyle.Fixed3D;
            prompt.Width = FORMSETTNGSIZEWIDTH;
            prompt.Height = FORMSETTNGSIZEHEIGHT;
            prompt.Text = "Paramètres";
            prompt.Icon = new Icon("..\\..\\Images\\Icons\\main.ico");

            Label labelGobanSize = new Label() { Left = 50, Top = 23, Text = "Taille du Goban", Width = 100 };
            NumericUpDown inputGobanSize = new NumericUpDown() { Left = 250, Top = 20, Width = 200 };
            inputGobanSize.Maximum = 38;
            inputGobanSize.Minimum = 6;
            inputGobanSize.Value = userSettings.GobanSize;
            inputGobanSize.ValueChanged += (sender, e) => { userSettings.GobanSize = (byte)inputGobanSize.Value; };
            prompt.Controls.Add(labelGobanSize);
            prompt.Controls.Add(inputGobanSize);
            Label labelTimeForPlayer = new Label() { Left = 50, Top = 73, Text = "Temps par joueur" };
            Label labelTimeHours = new Label() { Left = 300, Top = 73, Text = "h", Width = 20 };
            Label labelTimeMinutes = new Label() { Left = 370, Top = 73, Text = "min", Width = 30 };
            Label labelTimeSeconds = new Label() { Left = 450, Top = 73, Text = "s", Width = 30 };
            NumericUpDown inputTimeHours = new NumericUpDown() { Left = 250, Top = 70, Width = 50 };
            inputTimeHours.Maximum = 99;
            inputTimeHours.Minimum = 0;
            inputTimeHours.Value = calculator.getHours(userSettings.MaxTimeForPlayer);
            NumericUpDown inputTimeMinutes = new NumericUpDown() { Left = 320, Top = 70, Width = 50 };
            inputTimeMinutes.Maximum = 99;
            inputTimeMinutes.Minimum = 0;
            inputTimeMinutes.Value = calculator.getMinutes(userSettings.MaxTimeForPlayer);
            NumericUpDown inputTimeSeconds = new NumericUpDown() { Left = 400, Top = 70, Width = 50 };
            inputTimeSeconds.Maximum = 99;
            inputTimeSeconds.Minimum = 0;
            inputTimeSeconds.Value = calculator.getSeconds(userSettings.MaxTimeForPlayer);
            inputTimeHours.ValueChanged += (sender, e) => { calculateMaxTimeForPlayer((int)inputTimeHours.Value, (int)inputTimeMinutes.Value, (int)inputTimeSeconds.Value); };
            inputTimeMinutes.ValueChanged += (sender, e) => { calculateMaxTimeForPlayer((int)inputTimeHours.Value, (int)inputTimeMinutes.Value, (int)inputTimeSeconds.Value); };
            inputTimeSeconds.ValueChanged += (sender, e) => { calculateMaxTimeForPlayer((int)inputTimeHours.Value, (int)inputTimeMinutes.Value, (int)inputTimeSeconds.Value); };
            prompt.Controls.Add(labelTimeForPlayer);
            prompt.Controls.Add(labelTimeHours);
            prompt.Controls.Add(labelTimeMinutes);
            prompt.Controls.Add(labelTimeSeconds);
            prompt.Controls.Add(inputTimeHours);
            prompt.Controls.Add(inputTimeMinutes);
            prompt.Controls.Add(inputTimeSeconds);


            Button terminated = new Button() { Text = "Terminé", Left = FORMSETTNGSIZEWIDTH - 150, Width = 100, Top = FORMSETTNGSIZEHEIGHT - 100 };
            terminated.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(terminated);
            prompt.ShowDialog();
        }

        private void calculateMaxTimeForPlayer(int hours, int minutes, int seconds)
        {
            userSettings.MaxTimeForPlayer = (uint)(hours * 60 * 60 + minutes * 60 + seconds);
        }


        public void LoadFromHugeJson(JsonGenerator.HugeJson hugeJson)
        {
            // uncomment when the player name can be setted by the users
            labelPlayer1.Text = hugeJson.playerName1;
            labelPlayer2.Text = hugeJson.playerName2;

            buttonSave.Enabled = true;

            // The labels for the timers and scores will be automatically updated when timervalue will be setted

            casesDictionnary.resetDico();
            for (int index = 0; index < hugeJson.caseDicoX.Count; index++)
            {
                casesDictionnary.useCase(new Vector2D(hugeJson.caseDicoX[index], hugeJson.caseDicoY[index]), hugeJson.caseDicoUsedAtRound[index]);
                if (!hugeJson.caseDicoUsed[index])
                {
                    casesDictionnary.unUseCase(new Vector2D(hugeJson.caseDicoX[index], hugeJson.caseDicoY[index]), hugeJson.caseDicoUsedAtRound[index]);
                }
            }

            foreach (string text in hugeJson.gameHistory)
            {
                listViewHistory.Items.Add(text);
            }
            if (listViewHistory.Items.Count > 0) // if Items.Count == 0 it throw an error (index can't be '-1')
            {
                listViewHistory.EnsureVisible(listViewHistory.Items.Count - 1); // set the index to the last one, it always display the last move
            }

            if (hugeJson.gameStarted)
            {
                buttonStart.Enabled = false;
            }
            else
            {
                buttonStart.Enabled = true;
            }

            if (hugeJson.gamePaused)
            {
                buttonPause.Text = "Reprendre";
            }
            else
            {
                buttonPause.Text = "Pause";
            }
            if (gameController.WhoIsPlayingBool())
            {
                pictureBoxPlaynowBlack.Visible = true;
                pictureBoxPlaynowWhite.Visible = false;
            }
            else
            {
                pictureBoxPlaynowBlack.Visible = false;
                pictureBoxPlaynowWhite.Visible = true;
            }

            // reset the goban with the new layout
            Panel panel = searchGobanPanelAndRemoveAllChilds();
            initGoban(panel);


            for (int indexX = 0; indexX < hugeJson.goban.Count; indexX++)
            {
                for (int indexY = 0; indexY < hugeJson.goban[indexX].Count; indexY++)
                {
                    if (hugeJson.goban[indexX][indexY] == 0)
                    {
                        continue;
                    }
                    if (hugeJson.goban[indexX][indexY] == 1)
                    {
                        ((PictureBox)Controls.Find(indexX + "." + indexY, true)[0]).Image = Image.FromFile(imageAjuster.getImageGobanFromPos(indexX, indexY, userSettings.GobanSize) + "_black.png");
                    }
                    else
                    {
                        ((PictureBox)Controls.Find(indexX + "." + indexY, true)[0]).Image = Image.FromFile(imageAjuster.getImageGobanFromPos(indexX, indexY, userSettings.GobanSize) + "_white.png");
                    }
                }
            }
        }
    }
}
