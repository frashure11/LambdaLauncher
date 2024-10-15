using System;
using LambdaLoader;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LambdaLoader
{
    public partial class ServerConfigUC : UserControl
    {
        public ServerConfigUC()
        {
            InitializeComponent();
        }

        private void ServerConfigWindow_Load(object sender, EventArgs e)
        {
            checkBoxSteam.Checked = Properties.Settings.Default.EnableSteamNet;
            textBoxHostname.Clear();
            textBoxHostname.Text = Properties.Settings.Default.Hostname;
            upDownMaxPlayers.Value = Properties.Settings.Default.MaxPlayers;
            textBoxServerPassword.Clear();
            textBoxServerPassword.Text = Properties.Settings.Default.ServerPassword;
            upDownFragLimit.Value = Properties.Settings.Default.FragLimit;
            upDownTimeLimit.Value = Properties.Settings.Default.TimeLimit;
            if(Properties.Settings.Default.FallingDamage == true)
            {
                radioFallDamageNormal.Checked = false;
                radioFallDamageRealistic.Checked = true;
            }
            else
            {
                radioFallDamageNormal.Checked = true;
                radioFallDamageRealistic.Checked = false;
            }
            if (Properties.Settings.Default.Teamplay >= 1)
            {
                checkBoxTeamplay.Checked = true;
            }
            else
            {
                checkBoxTeamplay.Checked = false;
            }
            checkBoxTMbarney.Checked = Properties.Settings.Default.barney;
            checkBoxTMgman.Checked = Properties.Settings.Default.gman;
            checkBoxTMgordon.Checked = Properties.Settings.Default.gordon;
            checkBoxTMhelmet.Checked = Properties.Settings.Default.helmet;
            checkBoxTMhgrunt.Checked = Properties.Settings.Default.hgrunt;
            checkBoxTMrecon.Checked = Properties.Settings.Default.recon;
            checkBoxTMrobo.Checked = Properties.Settings.Default.robo;
            checkBoxTMscientist.Checked = Properties.Settings.Default.scientist;
            checkBoxTMzombie.Checked = Properties.Settings.Default.zombie;
            checkBoxFriendlyFire.Checked = Properties.Settings.Default.FriendlyFire;
            checkBoxWeaponsStay.Checked = Properties.Settings.Default.WeaponsStay;
            checkBoxForceRespawn.Checked = Properties.Settings.Default.ForceRespawn;
            checkBoxFootsteps.Checked = Properties.Settings.Default.Footsteps;
            checkBoxFlashlight.Checked = Properties.Settings.Default.Flashlight;
            checkBoxAutocrosshair.Checked = Properties.Settings.Default.Autocrosshair;
            checkBoxShaders.Checked = Properties.Settings.Default.Shaders;
            UpDownMaxSpeed.Value = Properties.Settings.Default.MaxSpeed;
            UpDownGravity.Value = Properties.Settings.Default.Gravity;
            checkBoxMaxDownloadSpeed.Checked = Properties.Settings.Default.MaxDownloadSpeed;
            Enable_DisableTeamOptions();
            if(Properties.Settings.Default.Interlock_FF == true)
            {
                Properties.Settings.Default.FriendlyFire = false;
                checkBoxFriendlyFire.Checked = false;
            }
        }
        private void ServerConfigSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerConfigMethod = "Basic";
            ServerConfigSaveSettings();
        }
        private void ServerConfigLoad_Click(object sender, EventArgs e)
        {
            checkBoxSteam.Checked = true;
            textBoxHostname.Clear();
            textBoxHostname.Text = "A Lambda Loader Server";
            upDownMaxPlayers.Value = 8;
            textBoxServerPassword.Clear();
            checkBoxTMrobo.Checked = true;
            checkBoxTMhgrunt.Checked = true;
            upDownFragLimit.Value = 0;
            upDownTimeLimit.Value = 0;
            radioFallDamageNormal.Checked = true;
            radioFallDamageRealistic.Checked = false;
            checkBoxTeamplay.Checked = false;
            checkBoxFriendlyFire.Checked = false;
            checkBoxWeaponsStay.Checked = false;
            checkBoxForceRespawn.Checked = false;
            checkBoxFootsteps.Checked = true;
            checkBoxFlashlight.Checked = true;
            checkBoxAutocrosshair.Checked = false;
            checkBoxShaders.Checked = true;
            UpDownMaxSpeed.Value = 270;
            UpDownGravity.Value = 800;
            checkBoxMaxDownloadSpeed.Checked = false;
            Properties.Settings.Default.ServerConfigMethod = "Basic";
            ServerConfigSaveSettings();
        }
        private void ServerConfigSaveSettings()
        {
            if (checkBoxFriendlyFire.Checked == false)//If the player does not want friendly fire we can force the interlock off and set the teamplay value to 1. No reason to add other checks, keep code simple and just design it so that extra checks are not needed.
            {
                Properties.Settings.Default.Interlock_FF = false;
            }
            if(Properties.Settings.Default.Interlock_FF == false)//Do this check so that if the player intends to use friendly fire they know if it is not setup correctly. Else handles how we let them know.
            {
                // Create an array of the checkboxes
                CheckBox[] checkBoxes = { checkBoxTMbarney, checkBoxTMgman, checkBoxTMgordon, checkBoxTMhelmet, checkBoxTMhgrunt, checkBoxTMrecon, checkBoxTMrobo, checkBoxTMscientist, checkBoxTMzombie };
                // Count the number of checked checkboxes
                int checkedCount = checkBoxes.Count(cb => cb.Checked);

                // Check if at least two of the possible teams are checked
                if (checkedCount >= 2)
                {
                    //Save config
                    Properties.Settings.Default.Hostname = textBoxHostname.Text;
                    Properties.Settings.Default.MaxPlayers = Decimal.ToInt32(upDownMaxPlayers.Value);
                    Properties.Settings.Default.ServerPassword = textBoxServerPassword.Text;
                    Properties.Settings.Default.FragLimit = Decimal.ToInt32(upDownFragLimit.Value);
                    Properties.Settings.Default.TimeLimit = Decimal.ToInt32(upDownTimeLimit.Value);
                    Properties.Settings.Default.FallingDamage = radioFallDamageRealistic.Checked;
                    Properties.Settings.Default.Teamplay = checkBoxTeamplay.Checked ? 1 : 0;
                    Properties.Settings.Default.barney = checkBoxTMbarney.Checked;
                    Properties.Settings.Default.gman = checkBoxTMgman.Checked;
                    Properties.Settings.Default.gordon = checkBoxTMgordon.Checked;
                    Properties.Settings.Default.helmet = checkBoxTMhelmet.Checked;
                    Properties.Settings.Default.hgrunt = checkBoxTMhgrunt.Checked;
                    Properties.Settings.Default.recon = checkBoxTMrecon.Checked;
                    Properties.Settings.Default.robo = checkBoxTMrobo.Checked;
                    Properties.Settings.Default.scientist = checkBoxTMscientist.Checked;
                    Properties.Settings.Default.zombie = checkBoxTMzombie.Checked;
                    Properties.Settings.Default.FriendlyFire = checkBoxFriendlyFire.Checked;
                    Properties.Settings.Default.WeaponsStay = checkBoxWeaponsStay.Checked;
                    Properties.Settings.Default.ForceRespawn = checkBoxForceRespawn.Checked;
                    Properties.Settings.Default.Footsteps = checkBoxFootsteps.Checked;
                    Properties.Settings.Default.Flashlight = checkBoxFlashlight.Checked;
                    Properties.Settings.Default.Autocrosshair = checkBoxAutocrosshair.Checked;
                    Properties.Settings.Default.EnableSteamNet = checkBoxSteam.Checked;
                    Properties.Settings.Default.Shaders = checkBoxShaders.Checked;
                    Properties.Settings.Default.MaxSpeed = Decimal.ToInt32(UpDownMaxSpeed.Value);
                    Properties.Settings.Default.Gravity = Decimal.ToInt32(UpDownGravity.Value);
                    Properties.Settings.Default.MaxDownloadSpeed = checkBoxMaxDownloadSpeed.Checked;
                    if (checkBoxFriendlyFire.Checked == true && checkBoxTeamplay.Checked == true)
                    {
                        if(Properties.Settings.Default.UseFFTotal == true)
                        {
                            Properties.Settings.Default.Teamplay = Properties.Settings.Default.TeamplayTotal;
                        }
                        else
                        {
                            Properties.Settings.Default.Teamplay = 175381; //Use this as a default FF Value if a user does not go through the wizard.
                        }
                    }
                    else if(checkBoxTeamplay.Checked == true)
                    {
                        Properties.Settings.Default.Teamplay = 1;
                    }
                    else
                    {
                        Properties.Settings.Default.Teamplay = 0;
                    }
                }
                else {MessageBox.Show("Please select at least two teams", "Invalid Settings!", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
            }
            else{MessageBox.Show("Please disable the Friendly Fire option or redo the Friendly Fire Configuration Wizard. Settings didn't save or was exited out of early.", "Invalid Settings!", MessageBoxButtons.OK, MessageBoxIcon.Warning);}
        }
        private void ServerConfigUC_Click(object sender, EventArgs e)
        {
            //Anytime there is a click, update the vars in CurrentSettings so the Advanced settings window can show the previews
            UpdateLaunchArgsPreview();
        }
        static void UpdateLaunchArgsPreview()
        {
            ServerAdvancedConfig secondary = new ServerAdvancedConfig();
            secondary.AddRowstoLaunchArgs();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Interlock_FF = true;
            TeamplayConfigWizard wizardWindow = new TeamplayConfigWizard();
            wizardWindow.Show();
        }
        private void checkBoxFriendlyFire_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxFriendlyFire.Checked == true)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                Properties.Settings.Default.UseFFTotal = false;
            }
        }
        private void checkBoxTeamplay_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxTeamplay.Checked == false)
            {
                Properties.Settings.Default.UseFFTotal = false;
            }
            Enable_DisableTeamOptions();
        }
        private void Enable_DisableTeamOptions()
        {
            if (checkBoxTeamplay.Checked == true)
            {
                checkBoxTMbarney.Enabled = true;
                checkBoxTMgman.Enabled = true;
                checkBoxTMgordon.Enabled = true;
                checkBoxTMhelmet.Enabled = true;
                checkBoxTMhgrunt.Enabled = true;
                checkBoxTMrecon.Enabled = true;
                checkBoxTMrobo.Enabled = true;
                checkBoxTMscientist.Enabled = true;
                checkBoxTMzombie.Enabled = true;
                checkBoxFriendlyFire.Enabled = true;
                if (checkBoxFriendlyFire.Checked == true)
                {
                    button1.Enabled = true;
                }
            }
            else
            {
                checkBoxTMbarney.Enabled = false;
                checkBoxTMgman.Enabled = false;
                checkBoxTMgordon.Enabled = false;
                checkBoxTMhelmet.Enabled = false;
                checkBoxTMhgrunt.Enabled = false;
                checkBoxTMrecon.Enabled = false;
                checkBoxTMrobo.Enabled = false;
                checkBoxTMscientist.Enabled = false;
                checkBoxTMzombie.Enabled = false;
                checkBoxFriendlyFire.Enabled = false;
                button1.Enabled = false;
            }
        }
    }
}