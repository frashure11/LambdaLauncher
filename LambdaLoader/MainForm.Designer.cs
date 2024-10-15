
namespace LambdaLoader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.buttonAddToPlaylist = new System.Windows.Forms.Button();
            this.buttonRemoveFromPlaylist = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSavePlaylist = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAddPrimaryMap = new System.Windows.Forms.Button();
            this.buttonLoadMapRotationFile = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonLoadDefaultMapCycle = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedServerConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedServerConfigToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mapFolderControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadExtrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonFavorite = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(687, 243);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(281, 433);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(687, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 176);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Enabled = false;
            this.buttonLaunch.Location = new System.Drawing.Point(880, 684);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(88, 23);
            this.buttonLaunch.TabIndex = 3;
            this.buttonLaunch.Text = "Launch Game";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.buttonLaunch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 63);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(316, 501);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(389, 123);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(292, 441);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // buttonAddToPlaylist
            // 
            this.buttonAddToPlaylist.Location = new System.Drawing.Point(335, 288);
            this.buttonAddToPlaylist.Name = "buttonAddToPlaylist";
            this.buttonAddToPlaylist.Size = new System.Drawing.Size(47, 23);
            this.buttonAddToPlaylist.TabIndex = 6;
            this.buttonAddToPlaylist.Text = ">>";
            this.buttonAddToPlaylist.UseVisualStyleBackColor = true;
            this.buttonAddToPlaylist.Click += new System.EventHandler(this.buttonAddToPlaylist_Click);
            // 
            // buttonRemoveFromPlaylist
            // 
            this.buttonRemoveFromPlaylist.Location = new System.Drawing.Point(335, 318);
            this.buttonRemoveFromPlaylist.Name = "buttonRemoveFromPlaylist";
            this.buttonRemoveFromPlaylist.Size = new System.Drawing.Size(47, 23);
            this.buttonRemoveFromPlaylist.TabIndex = 7;
            this.buttonRemoveFromPlaylist.Text = "<<";
            this.buttonRemoveFromPlaylist.UseVisualStyleBackColor = true;
            this.buttonRemoveFromPlaylist.Click += new System.EventHandler(this.buttonRemoveFromPlaylist_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Maps Found: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Map Rotation";
            // 
            // buttonSavePlaylist
            // 
            this.buttonSavePlaylist.Location = new System.Drawing.Point(536, 571);
            this.buttonSavePlaylist.Name = "buttonSavePlaylist";
            this.buttonSavePlaylist.Size = new System.Drawing.Size(145, 23);
            this.buttonSavePlaylist.TabIndex = 10;
            this.buttonSavePlaylist.Text = "Export Map Rotation to file";
            this.buttonSavePlaylist.UseVisualStyleBackColor = true;
            this.buttonSavePlaylist.Click += new System.EventHandler(this.buttonSavePlaylist_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(389, 63);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(292, 41);
            this.dataGridView3.TabIndex = 11;
            this.dataGridView3.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            this.dataGridView3.SelectionChanged += new System.EventHandler(this.dataGridView3_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Map to Run on Launch";
            // 
            // buttonAddPrimaryMap
            // 
            this.buttonAddPrimaryMap.Location = new System.Drawing.Point(335, 72);
            this.buttonAddPrimaryMap.Name = "buttonAddPrimaryMap";
            this.buttonAddPrimaryMap.Size = new System.Drawing.Size(47, 23);
            this.buttonAddPrimaryMap.TabIndex = 13;
            this.buttonAddPrimaryMap.Text = ">>";
            this.buttonAddPrimaryMap.UseVisualStyleBackColor = true;
            this.buttonAddPrimaryMap.Click += new System.EventHandler(this.buttonAddPrimaryMap_Click);
            // 
            // buttonLoadMapRotationFile
            // 
            this.buttonLoadMapRotationFile.Location = new System.Drawing.Point(389, 571);
            this.buttonLoadMapRotationFile.Name = "buttonLoadMapRotationFile";
            this.buttonLoadMapRotationFile.Size = new System.Drawing.Size(145, 23);
            this.buttonLoadMapRotationFile.TabIndex = 14;
            this.buttonLoadMapRotationFile.Text = "Import Map Rotation File";
            this.buttonLoadMapRotationFile.UseVisualStyleBackColor = true;
            this.buttonLoadMapRotationFile.Click += new System.EventHandler(this.buttonLoadMapRotationFile_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(152, 576);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(177, 13);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Not finding a map you downloaded?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonLoadDefaultMapCycle
            // 
            this.buttonLoadDefaultMapCycle.Location = new System.Drawing.Point(389, 601);
            this.buttonLoadDefaultMapCycle.Name = "buttonLoadDefaultMapCycle";
            this.buttonLoadDefaultMapCycle.Size = new System.Drawing.Size(292, 23);
            this.buttonLoadDefaultMapCycle.TabIndex = 16;
            this.buttonLoadDefaultMapCycle.Text = "Load Valve\'s Default Map Rotation";
            this.buttonLoadDefaultMapCycle.UseVisualStyleBackColor = true;
            this.buttonLoadDefaultMapCycle.Click += new System.EventHandler(this.buttonLoadDefaultMapCycle_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(113, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gamePathToolStripMenuItem,
            this.serverConfigurationToolStripMenuItem,
            this.mapFolderControlsToolStripMenuItem,
            this.themesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // gamePathToolStripMenuItem
            // 
            this.gamePathToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gamePathToolStripMenuItem.Name = "gamePathToolStripMenuItem";
            this.gamePathToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.gamePathToolStripMenuItem.Text = "Game Path";
            this.gamePathToolStripMenuItem.Click += new System.EventHandler(this.gamePathToolStripMenuItem_Click);
            // 
            // serverConfigurationToolStripMenuItem
            // 
            this.serverConfigurationToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.serverConfigurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedServerConfigToolStripMenuItem,
            this.advancedServerConfigToolStripMenuItem1});
            this.serverConfigurationToolStripMenuItem.Name = "serverConfigurationToolStripMenuItem";
            this.serverConfigurationToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.serverConfigurationToolStripMenuItem.Text = "Server Configuration";
            // 
            // advancedServerConfigToolStripMenuItem
            // 
            this.advancedServerConfigToolStripMenuItem.Name = "advancedServerConfigToolStripMenuItem";
            this.advancedServerConfigToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.advancedServerConfigToolStripMenuItem.Text = "Standard Server Config";
            this.advancedServerConfigToolStripMenuItem.Click += new System.EventHandler(this.advancedServerConfigToolStripMenuItem_Click);
            // 
            // advancedServerConfigToolStripMenuItem1
            // 
            this.advancedServerConfigToolStripMenuItem1.Name = "advancedServerConfigToolStripMenuItem1";
            this.advancedServerConfigToolStripMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.advancedServerConfigToolStripMenuItem1.Text = "Advanced Server Config";
            this.advancedServerConfigToolStripMenuItem1.Click += new System.EventHandler(this.advancedServerConfigToolStripMenuItem1_Click);
            // 
            // mapFolderControlsToolStripMenuItem
            // 
            this.mapFolderControlsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mapFolderControlsToolStripMenuItem.Name = "mapFolderControlsToolStripMenuItem";
            this.mapFolderControlsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.mapFolderControlsToolStripMenuItem.Text = "Map Folder Controls";
            this.mapFolderControlsToolStripMenuItem.Click += new System.EventHandler(this.mapFolderControlsToolStripMenuItem_Click);
            // 
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.themesToolStripMenuItem.Text = "Themes";
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.downloadExtrasToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.extrasToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.aboutToolStripMenuItem.Text = "Check for Updates";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "About Lambda Launcher";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // downloadExtrasToolStripMenuItem
            // 
            this.downloadExtrasToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.downloadExtrasToolStripMenuItem.Name = "downloadExtrasToolStripMenuItem";
            this.downloadExtrasToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.downloadExtrasToolStripMenuItem.Text = "Install Extras";
            this.downloadExtrasToolStripMenuItem.Click += new System.EventHandler(this.downloadExtrasToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(887, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Click to Enlarge";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonFavorite
            // 
            this.buttonFavorite.Location = new System.Drawing.Point(13, 571);
            this.buttonFavorite.Name = "buttonFavorite";
            this.buttonFavorite.Size = new System.Drawing.Size(133, 23);
            this.buttonFavorite.TabIndex = 19;
            this.buttonFavorite.Text = "★ Favorite";
            this.buttonFavorite.UseVisualStyleBackColor = true;
            this.buttonFavorite.Click += new System.EventHandler(this.buttonFavorite_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(980, 719);
            this.Controls.Add(this.buttonFavorite);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonLoadDefaultMapCycle);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.buttonLoadMapRotationFile);
            this.Controls.Add(this.buttonAddPrimaryMap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.buttonSavePlaylist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRemoveFromPlaylist);
            this.Controls.Add(this.buttonAddToPlaylist);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonLaunch);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cleverly done, Mr. Freeman, but you\'re not supposed to be here. (There has been a" +
    "n issue with loading the window title)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonAddToPlaylist;
        private System.Windows.Forms.Button buttonRemoveFromPlaylist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSavePlaylist;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAddPrimaryMap;
        private System.Windows.Forms.Button buttonLoadMapRotationFile;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonLoadDefaultMapCycle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gamePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem mapFolderControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadExtrasToolStripMenuItem;
        private System.Windows.Forms.Button buttonFavorite;
        private System.Windows.Forms.ToolStripMenuItem advancedServerConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedServerConfigToolStripMenuItem1;
    }
}

