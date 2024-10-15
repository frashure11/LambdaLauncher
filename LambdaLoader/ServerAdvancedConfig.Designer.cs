
namespace LambdaLoader
{
    partial class ServerAdvancedConfig
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
            this.dataGridViewCMDargs = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCMD_Delete = new System.Windows.Forms.Button();
            this.buttonCMD_Down = new System.Windows.Forms.Button();
            this.buttonCMD_Up = new System.Windows.Forms.Button();
            this.buttonConf_Up = new System.Windows.Forms.Button();
            this.buttonConf_Down = new System.Windows.Forms.Button();
            this.buttonConf_Delete = new System.Windows.Forms.Button();
            this.buttonCMD_Import = new System.Windows.Forms.Button();
            this.buttonCMD_Export = new System.Windows.Forms.Button();
            this.buttonConf_Import = new System.Windows.Forms.Button();
            this.buttonConf_Export = new System.Windows.Forms.Button();
            this.dataGridViewConfigPreview = new System.Windows.Forms.DataGridView();
            this.buttonLoadDefaults = new System.Windows.Forms.Button();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.button_CreatGameProfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCMDargs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfigPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCMDargs
            // 
            this.dataGridViewCMDargs.AllowUserToResizeRows = false;
            this.dataGridViewCMDargs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCMDargs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCMDargs.Location = new System.Drawing.Point(20, 53);
            this.dataGridViewCMDargs.MultiSelect = false;
            this.dataGridViewCMDargs.Name = "dataGridViewCMDargs";
            this.dataGridViewCMDargs.RowHeadersVisible = false;
            this.dataGridViewCMDargs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCMDargs.Size = new System.Drawing.Size(248, 558);
            this.dataGridViewCMDargs.TabIndex = 1;
            this.dataGridViewCMDargs.SelectionChanged += new System.EventHandler(this.dataGridViewCMDargs_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Launch Arguments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Config File Preview";
            // 
            // buttonCMD_Delete
            // 
            this.buttonCMD_Delete.Location = new System.Drawing.Point(124, 617);
            this.buttonCMD_Delete.Name = "buttonCMD_Delete";
            this.buttonCMD_Delete.Size = new System.Drawing.Size(41, 23);
            this.buttonCMD_Delete.TabIndex = 5;
            this.buttonCMD_Delete.Text = "✕";
            this.buttonCMD_Delete.UseVisualStyleBackColor = true;
            this.buttonCMD_Delete.Click += new System.EventHandler(this.buttonCMD_Delete_Click);
            // 
            // buttonCMD_Down
            // 
            this.buttonCMD_Down.Location = new System.Drawing.Point(77, 617);
            this.buttonCMD_Down.Name = "buttonCMD_Down";
            this.buttonCMD_Down.Size = new System.Drawing.Size(41, 23);
            this.buttonCMD_Down.TabIndex = 7;
            this.buttonCMD_Down.Text = "▼";
            this.buttonCMD_Down.UseVisualStyleBackColor = true;
            this.buttonCMD_Down.Click += new System.EventHandler(this.buttonCMD_Down_Click);
            // 
            // buttonCMD_Up
            // 
            this.buttonCMD_Up.Location = new System.Drawing.Point(171, 617);
            this.buttonCMD_Up.Name = "buttonCMD_Up";
            this.buttonCMD_Up.Size = new System.Drawing.Size(41, 23);
            this.buttonCMD_Up.TabIndex = 8;
            this.buttonCMD_Up.Text = "▲";
            this.buttonCMD_Up.UseVisualStyleBackColor = true;
            this.buttonCMD_Up.Click += new System.EventHandler(this.buttonCMD_Up_Click);
            // 
            // buttonConf_Up
            // 
            this.buttonConf_Up.Location = new System.Drawing.Point(433, 617);
            this.buttonConf_Up.Name = "buttonConf_Up";
            this.buttonConf_Up.Size = new System.Drawing.Size(41, 23);
            this.buttonConf_Up.TabIndex = 12;
            this.buttonConf_Up.Text = "▲";
            this.buttonConf_Up.UseVisualStyleBackColor = true;
            this.buttonConf_Up.Click += new System.EventHandler(this.buttonConf_Up_Click);
            // 
            // buttonConf_Down
            // 
            this.buttonConf_Down.Location = new System.Drawing.Point(342, 617);
            this.buttonConf_Down.Name = "buttonConf_Down";
            this.buttonConf_Down.Size = new System.Drawing.Size(41, 23);
            this.buttonConf_Down.TabIndex = 11;
            this.buttonConf_Down.Text = "▼";
            this.buttonConf_Down.UseVisualStyleBackColor = true;
            this.buttonConf_Down.Click += new System.EventHandler(this.buttonConf_Down_Click);
            // 
            // buttonConf_Delete
            // 
            this.buttonConf_Delete.Location = new System.Drawing.Point(389, 617);
            this.buttonConf_Delete.Name = "buttonConf_Delete";
            this.buttonConf_Delete.Size = new System.Drawing.Size(41, 23);
            this.buttonConf_Delete.TabIndex = 9;
            this.buttonConf_Delete.Text = "✕";
            this.buttonConf_Delete.UseVisualStyleBackColor = true;
            this.buttonConf_Delete.Click += new System.EventHandler(this.buttonConf_Delete_Click);
            // 
            // buttonCMD_Import
            // 
            this.buttonCMD_Import.Location = new System.Drawing.Point(77, 646);
            this.buttonCMD_Import.Name = "buttonCMD_Import";
            this.buttonCMD_Import.Size = new System.Drawing.Size(63, 23);
            this.buttonCMD_Import.TabIndex = 13;
            this.buttonCMD_Import.Text = "Import";
            this.buttonCMD_Import.UseVisualStyleBackColor = true;
            this.buttonCMD_Import.Click += new System.EventHandler(this.buttonCMD_Import_Click);
            // 
            // buttonCMD_Export
            // 
            this.buttonCMD_Export.Location = new System.Drawing.Point(146, 646);
            this.buttonCMD_Export.Name = "buttonCMD_Export";
            this.buttonCMD_Export.Size = new System.Drawing.Size(66, 23);
            this.buttonCMD_Export.TabIndex = 14;
            this.buttonCMD_Export.Text = "Export";
            this.buttonCMD_Export.UseVisualStyleBackColor = true;
            this.buttonCMD_Export.Click += new System.EventHandler(this.buttonCMD_Export_Click);
            // 
            // buttonConf_Import
            // 
            this.buttonConf_Import.Location = new System.Drawing.Point(342, 646);
            this.buttonConf_Import.Name = "buttonConf_Import";
            this.buttonConf_Import.Size = new System.Drawing.Size(63, 23);
            this.buttonConf_Import.TabIndex = 15;
            this.buttonConf_Import.Text = "Import";
            this.buttonConf_Import.UseVisualStyleBackColor = true;
            this.buttonConf_Import.Click += new System.EventHandler(this.buttonConf_Import_Click);
            // 
            // buttonConf_Export
            // 
            this.buttonConf_Export.Location = new System.Drawing.Point(411, 646);
            this.buttonConf_Export.Name = "buttonConf_Export";
            this.buttonConf_Export.Size = new System.Drawing.Size(63, 23);
            this.buttonConf_Export.TabIndex = 16;
            this.buttonConf_Export.Text = "Export";
            this.buttonConf_Export.UseVisualStyleBackColor = true;
            this.buttonConf_Export.Click += new System.EventHandler(this.buttonConf_Export_Click);
            // 
            // dataGridViewConfigPreview
            // 
            this.dataGridViewConfigPreview.AllowUserToResizeRows = false;
            this.dataGridViewConfigPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewConfigPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConfigPreview.Location = new System.Drawing.Point(285, 53);
            this.dataGridViewConfigPreview.MultiSelect = false;
            this.dataGridViewConfigPreview.Name = "dataGridViewConfigPreview";
            this.dataGridViewConfigPreview.RowHeadersVisible = false;
            this.dataGridViewConfigPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConfigPreview.Size = new System.Drawing.Size(248, 558);
            this.dataGridViewConfigPreview.TabIndex = 17;
            this.dataGridViewConfigPreview.SelectionChanged += new System.EventHandler(this.dataGridViewConfigPreview_SelectionChanged);
            // 
            // buttonLoadDefaults
            // 
            this.buttonLoadDefaults.Location = new System.Drawing.Point(219, 617);
            this.buttonLoadDefaults.Name = "buttonLoadDefaults";
            this.buttonLoadDefaults.Size = new System.Drawing.Size(115, 23);
            this.buttonLoadDefaults.TabIndex = 18;
            this.buttonLoadDefaults.Text = "Load Defaults";
            this.buttonLoadDefaults.UseVisualStyleBackColor = true;
            this.buttonLoadDefaults.Click += new System.EventHandler(this.buttonLoadDefaults_Click);
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Location = new System.Drawing.Point(219, 646);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(115, 23);
            this.buttonSaveSettings.TabIndex = 19;
            this.buttonSaveSettings.Text = "Apply Settings";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // button_CreatGameProfile
            // 
            this.button_CreatGameProfile.Enabled = false;
            this.button_CreatGameProfile.Location = new System.Drawing.Point(219, 675);
            this.button_CreatGameProfile.Name = "button_CreatGameProfile";
            this.button_CreatGameProfile.Size = new System.Drawing.Size(115, 23);
            this.button_CreatGameProfile.TabIndex = 20;
            this.button_CreatGameProfile.Text = "Create New Profile";
            this.button_CreatGameProfile.UseVisualStyleBackColor = true;
            this.button_CreatGameProfile.Visible = false;
            this.button_CreatGameProfile.Click += new System.EventHandler(this.button_CreateGameProfile_Click);
            // 
            // ServerAdvancedConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 704);
            this.Controls.Add(this.button_CreatGameProfile);
            this.Controls.Add(this.buttonSaveSettings);
            this.Controls.Add(this.buttonLoadDefaults);
            this.Controls.Add(this.dataGridViewConfigPreview);
            this.Controls.Add(this.buttonConf_Export);
            this.Controls.Add(this.buttonConf_Import);
            this.Controls.Add(this.buttonCMD_Export);
            this.Controls.Add(this.buttonCMD_Import);
            this.Controls.Add(this.buttonConf_Up);
            this.Controls.Add(this.buttonConf_Down);
            this.Controls.Add(this.buttonConf_Delete);
            this.Controls.Add(this.buttonCMD_Up);
            this.Controls.Add(this.buttonCMD_Down);
            this.Controls.Add(this.buttonCMD_Delete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewCMDargs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "ServerAdvancedConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Server Configuration";
            this.Load += new System.EventHandler(this.ServerAdvancedConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCMDargs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfigPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewCMDargs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCMD_Delete;
        private System.Windows.Forms.Button buttonCMD_Down;
        private System.Windows.Forms.Button buttonCMD_Up;
        private System.Windows.Forms.Button buttonConf_Up;
        private System.Windows.Forms.Button buttonConf_Down;
        private System.Windows.Forms.Button buttonConf_Delete;
        private System.Windows.Forms.Button buttonCMD_Import;
        private System.Windows.Forms.Button buttonCMD_Export;
        private System.Windows.Forms.Button buttonConf_Import;
        private System.Windows.Forms.Button buttonConf_Export;
        private System.Windows.Forms.DataGridView dataGridViewConfigPreview;
        private System.Windows.Forms.Button buttonLoadDefaults;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.Button button_CreatGameProfile;
    }
}