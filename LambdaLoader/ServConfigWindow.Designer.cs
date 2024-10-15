
namespace LambdaLoader
{
    partial class ServConfigWindow
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
            this.serverConfigUC1 = new LambdaLoader.ServerConfigUC();
            this.SuspendLayout();
            // 
            // serverConfigUC1
            // 
            this.serverConfigUC1.Location = new System.Drawing.Point(13, 40);
            this.serverConfigUC1.Name = "serverConfigUC1";
            this.serverConfigUC1.Size = new System.Drawing.Size(293, 696);
            this.serverConfigUC1.TabIndex = 0;
            // 
            // ServConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 740);
            this.Controls.Add(this.serverConfigUC1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "ServConfigWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServConfigWindow_FormClosing);
            this.Load += new System.EventHandler(this.ServConfigWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ServerConfigUC serverConfigUC1;
    }
}