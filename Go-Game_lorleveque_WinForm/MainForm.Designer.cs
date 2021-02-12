
namespace Go_Game_lorleveque_WinForm
{
    partial class GoGame
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoGame));
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonCharge = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.pictureBoxPlaynowBlack = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlaynowWhite = new System.Windows.Forms.PictureBox();
            this.labelScoreValue2 = new System.Windows.Forms.Label();
            this.labelScore2 = new System.Windows.Forms.Label();
            this.labelScoreValue1 = new System.Windows.Forms.Label();
            this.labelScore1 = new System.Windows.Forms.Label();
            this.labelTimer2 = new System.Windows.Forms.Label();
            this.labelTimer1 = new System.Windows.Forms.Label();
            this.labelTimeLeft2 = new System.Windows.Forms.Label();
            this.labelTimeLeft1 = new System.Windows.Forms.Label();
            this.labelPlayBlacks = new System.Windows.Forms.Label();
            this.labelPlayWhites = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.panelUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlaynowBlack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlaynowWhite)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(972, 808);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 35);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Démarrer";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(760, 808);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 35);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Redémarrer";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(866, 808);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(100, 35);
            this.buttonPause.TabIndex = 2;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonCharge
            // 
            this.buttonCharge.Location = new System.Drawing.Point(611, 808);
            this.buttonCharge.Name = "buttonCharge";
            this.buttonCharge.Size = new System.Drawing.Size(100, 35);
            this.buttonCharge.TabIndex = 3;
            this.buttonCharge.Text = "Charger";
            this.buttonCharge.UseVisualStyleBackColor = true;
            this.buttonCharge.Click += new System.EventHandler(this.buttonCharge_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(505, 808);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 35);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Sauvegarder";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(288, 808);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(100, 35);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "Paramètres";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // panelUsers
            // 
            this.panelUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUsers.Controls.Add(this.pictureBoxPlaynowBlack);
            this.panelUsers.Controls.Add(this.pictureBoxPlaynowWhite);
            this.panelUsers.Controls.Add(this.labelScoreValue2);
            this.panelUsers.Controls.Add(this.labelScore2);
            this.panelUsers.Controls.Add(this.labelScoreValue1);
            this.panelUsers.Controls.Add(this.labelScore1);
            this.panelUsers.Controls.Add(this.labelTimer2);
            this.panelUsers.Controls.Add(this.labelTimer1);
            this.panelUsers.Controls.Add(this.labelTimeLeft2);
            this.panelUsers.Controls.Add(this.labelTimeLeft1);
            this.panelUsers.Controls.Add(this.labelPlayBlacks);
            this.panelUsers.Controls.Add(this.labelPlayWhites);
            this.panelUsers.Controls.Add(this.labelPlayer2);
            this.panelUsers.Controls.Add(this.labelPlayer1);
            this.panelUsers.Location = new System.Drawing.Point(13, 36);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(269, 147);
            this.panelUsers.TabIndex = 7;
            // 
            // pictureBoxPlaynowBlack
            // 
            this.pictureBoxPlaynowBlack.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlaynowBlack.Image")));
            this.pictureBoxPlaynowBlack.Location = new System.Drawing.Point(222, 70);
            this.pictureBoxPlaynowBlack.Name = "pictureBoxPlaynowBlack";
            this.pictureBoxPlaynowBlack.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxPlaynowBlack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlaynowBlack.TabIndex = 13;
            this.pictureBoxPlaynowBlack.TabStop = false;
            // 
            // pictureBoxPlaynowWhite
            // 
            this.pictureBoxPlaynowWhite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxPlaynowWhite.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlaynowWhite.Image")));
            this.pictureBoxPlaynowWhite.Location = new System.Drawing.Point(222, 3);
            this.pictureBoxPlaynowWhite.Name = "pictureBoxPlaynowWhite";
            this.pictureBoxPlaynowWhite.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxPlaynowWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlaynowWhite.TabIndex = 12;
            this.pictureBoxPlaynowWhite.TabStop = false;
            this.pictureBoxPlaynowWhite.Visible = false;
            // 
            // labelScoreValue2
            // 
            this.labelScoreValue2.AutoSize = true;
            this.labelScoreValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelScoreValue2.Location = new System.Drawing.Point(111, 110);
            this.labelScoreValue2.Name = "labelScoreValue2";
            this.labelScoreValue2.Size = new System.Drawing.Size(16, 17);
            this.labelScoreValue2.TabIndex = 11;
            this.labelScoreValue2.Text = "0";
            // 
            // labelScore2
            // 
            this.labelScore2.AutoSize = true;
            this.labelScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelScore2.Location = new System.Drawing.Point(4, 110);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Size = new System.Drawing.Size(53, 17);
            this.labelScore2.TabIndex = 10;
            this.labelScore2.Text = "Score :";
            // 
            // labelScoreValue1
            // 
            this.labelScoreValue1.AutoSize = true;
            this.labelScoreValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelScoreValue1.Location = new System.Drawing.Point(111, 43);
            this.labelScoreValue1.Name = "labelScoreValue1";
            this.labelScoreValue1.Size = new System.Drawing.Size(16, 17);
            this.labelScoreValue1.TabIndex = 9;
            this.labelScoreValue1.Text = "0";
            // 
            // labelScore1
            // 
            this.labelScore1.AutoSize = true;
            this.labelScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelScore1.Location = new System.Drawing.Point(4, 43);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Size = new System.Drawing.Size(53, 17);
            this.labelScore1.TabIndex = 8;
            this.labelScore1.Text = "Score :";
            // 
            // labelTimer2
            // 
            this.labelTimer2.AutoSize = true;
            this.labelTimer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelTimer2.Location = new System.Drawing.Point(111, 93);
            this.labelTimer2.Name = "labelTimer2";
            this.labelTimer2.Size = new System.Drawing.Size(64, 17);
            this.labelTimer2.TabIndex = 7;
            this.labelTimer2.Text = "00:00:00";
            // 
            // labelTimer1
            // 
            this.labelTimer1.AutoSize = true;
            this.labelTimer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelTimer1.Location = new System.Drawing.Point(111, 26);
            this.labelTimer1.Name = "labelTimer1";
            this.labelTimer1.Size = new System.Drawing.Size(64, 17);
            this.labelTimer1.TabIndex = 6;
            this.labelTimer1.Text = "00:00:00";
            // 
            // labelTimeLeft2
            // 
            this.labelTimeLeft2.AutoSize = true;
            this.labelTimeLeft2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelTimeLeft2.Location = new System.Drawing.Point(3, 93);
            this.labelTimeLeft2.Name = "labelTimeLeft2";
            this.labelTimeLeft2.Size = new System.Drawing.Size(107, 17);
            this.labelTimeLeft2.TabIndex = 5;
            this.labelTimeLeft2.Text = "Temps restant :";
            // 
            // labelTimeLeft1
            // 
            this.labelTimeLeft1.AutoSize = true;
            this.labelTimeLeft1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelTimeLeft1.Location = new System.Drawing.Point(3, 26);
            this.labelTimeLeft1.Name = "labelTimeLeft1";
            this.labelTimeLeft1.Size = new System.Drawing.Size(107, 17);
            this.labelTimeLeft1.TabIndex = 4;
            this.labelTimeLeft1.Text = "Temps restant :";
            // 
            // labelPlayBlacks
            // 
            this.labelPlayBlacks.AutoSize = true;
            this.labelPlayBlacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelPlayBlacks.Location = new System.Drawing.Point(88, 76);
            this.labelPlayBlacks.Name = "labelPlayBlacks";
            this.labelPlayBlacks.Size = new System.Drawing.Size(96, 17);
            this.labelPlayBlacks.TabIndex = 3;
            this.labelPlayBlacks.Text = "Joue les noirs";
            // 
            // labelPlayWhites
            // 
            this.labelPlayWhites.AutoSize = true;
            this.labelPlayWhites.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelPlayWhites.Location = new System.Drawing.Point(88, 9);
            this.labelPlayWhites.Name = "labelPlayWhites";
            this.labelPlayWhites.Size = new System.Drawing.Size(106, 17);
            this.labelPlayWhites.TabIndex = 2;
            this.labelPlayWhites.Text = "Joue les blancs";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.BackColor = System.Drawing.SystemColors.Control;
            this.labelPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelPlayer2.Location = new System.Drawing.Point(4, 76);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = "Joueur 2";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelPlayer1.Location = new System.Drawing.Point(4, 9);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Joueur 1";
            // 
            // listViewHistory
            // 
            this.listViewHistory.HideSelection = false;
            this.listViewHistory.Location = new System.Drawing.Point(12, 189);
            this.listViewHistory.MultiSelect = false;
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.Size = new System.Drawing.Size(270, 654);
            this.listViewHistory.TabIndex = 8;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.List;
            // 
            // GoGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 861);
            this.Controls.Add(this.listViewHistory);
            this.Controls.Add(this.panelUsers);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCharge);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoGame";
            this.Text = "Go Game";
            this.Load += new System.EventHandler(this.GoGame_Load);
            this.panelUsers.ResumeLayout(false);
            this.panelUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlaynowBlack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlaynowWhite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonCharge;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Label labelTimeLeft2;
        private System.Windows.Forms.Label labelTimeLeft1;
        private System.Windows.Forms.Label labelPlayBlacks;
        private System.Windows.Forms.Label labelPlayWhites;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.Label labelTimer2;
        private System.Windows.Forms.Label labelTimer1;
        private System.Windows.Forms.Label labelScoreValue2;
        private System.Windows.Forms.Label labelScore2;
        private System.Windows.Forms.Label labelScoreValue1;
        private System.Windows.Forms.Label labelScore1;
        private System.Windows.Forms.PictureBox pictureBoxPlaynowBlack;
        private System.Windows.Forms.PictureBox pictureBoxPlaynowWhite;
    }
}

