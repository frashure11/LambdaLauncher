using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace LambdaLoader
{
    public partial class ServerAdvancedConfig : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        public ServerAdvancedConfig()
        {
            InitializeComponent();
            dataGridViewCMDargs.Columns.Add("newColumnName", "Arguments are run top to bottom");
            dataGridViewConfigPreview.Columns.Add("newColumnName", "Saved to config files in order they are listed");
            dataGridViewCMDargs.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewConfigPreview.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void ServerAdvancedConfig_Load(object sender, EventArgs e)
        {
            LoadFilesorDefaults();
            InitializeCustomTitleBar();
            this.Paint += MyForm_Paint;
        }
        private void InitializeCustomTitleBar()
        {
            // Create the custom title bar panel
            titleBar.Name = "titleBar";
            titleBar.BackColor = Color.DarkBlue; // Default color
            titleBar.Dock = DockStyle.Top;
            titleBar.Height = 24;

            titleBar.MouseDown += TitleBar_MouseDown;
            titleBar.MouseMove += TitleBar_MouseMove;
            titleBar.MouseUp += TitleBar_MouseUp;

            // Create the minimize button
            Button minimizeButton = new Button
            {
                Text = "_",
                ForeColor = Color.White,
                BackColor = Color.Gray,
                Dock = DockStyle.Right,
                Width = 27
            };
            minimizeButton.Click += MinimizeButton_Click;
            titleBar.Controls.Add(minimizeButton);

            // Create the close button
            Button closeButton = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Red,
                Dock = DockStyle.Right,
                Width = 27
            };
            closeButton.Click += CloseButton_Click;
            titleBar.Controls.Add(closeButton);

            Label titleLabel = new Label
            {
                Text = "Advanced Server Setup",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            titleBar.Controls.Add(titleLabel);

            // Position the title label in the center of the title bar
            titleLabel.Location = new Point((titleBar.Width - titleLabel.Width) / 2, (titleBar.Height - titleLabel.Height) / 2);
            titleBar.Resize += (s, e) =>
            {
                titleLabel.Location = new Point((titleBar.Width - titleLabel.Width) / 2, (titleBar.Height - titleLabel.Height) / 2);
            };

            // Add the title bar panel to the form
            this.Controls.Add(titleBar);
            this.Padding = new Padding(1);
            this.BackColor = Color.Black;
        }
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void LoadFilesorDefaults()
        {
            string filePatharg = Properties.Settings.Default.GameProfileNoExt + @"\GP.arg";
            if (!File.Exists(filePatharg))
            {
                AddRowstoLaunchArgs();
            }
            else
            {
                string[] linesarg = File.ReadAllLines(filePatharg);
                dataGridViewCMDargs.Rows.Clear();

                // Add each line as a new row in the DataGridView
                int WhiteSpaceHandlerarg = 0;//This will limit how many empty lines there are. Without this it will double the empty lines with every save. Do not know why so this is the solution
                foreach (string line in linesarg)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Assuming each line in the file is a single string to be added to the first column of the DataGridView
                        dataGridViewCMDargs.Rows.Add(line);
                    }
                    else
                    {
                        WhiteSpaceHandlerarg++;
                        if (WhiteSpaceHandlerarg == 1)
                        {
                            dataGridViewCMDargs.Rows.Add("");
                        }
                        else
                        {
                            WhiteSpaceHandlerarg = 0;
                        }
                    }
                }
            }
            string filePathcmd = Properties.Settings.Default.GameProfileNoExt + @"\GP.cfg";
            if (!File.Exists(filePathcmd))
            {
                AddRowstoConfigPreview();
            }
            else
            {
                // Read all lines from the file
                string[] linescmd = File.ReadAllLines(filePathcmd);

                // Clear existing rows in the DataGridView
                dataGridViewConfigPreview.Rows.Clear();

                // Add each line as a new row in the DataGridView
                int WhiteSpaceHandlercmd = 0;//This will limit how many empty lines there are. Without this it will double the empty lines with every save. Do not know why so this is the solution
                foreach (string line in linescmd)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Assuming each line in the file is a single string to be added to the first column of the DataGridView
                        dataGridViewConfigPreview.Rows.Add(line);
                    }
                    else
                    {
                        WhiteSpaceHandlercmd++;
                        if (WhiteSpaceHandlercmd == 1)
                        {
                            dataGridViewConfigPreview.Rows.Add("");
                        }
                        else
                        {
                            WhiteSpaceHandlercmd = 0;
                        }
                    }
                }
            }
        }
        public void AddRowstoLaunchArgs()
        {
            dataGridViewCMDargs.Rows.Clear();
            dataGridViewCMDargs.Rows.Add(Properties.Settings.Default.UserSelectedFolderPath + @"\hl.exe");
            dataGridViewCMDargs.Rows.Add("-game -valve");
            dataGridViewCMDargs.Rows.Add($"+hostname \"{Properties.Settings.Default.Hostname}\"");
            dataGridViewCMDargs.Rows.Add($"+map \"{Properties.Settings.Default.LastRanMap}\"");
            dataGridViewCMDargs.Rows.Add("-maxplayers " + Properties.Settings.Default.MaxPlayers);
            dataGridViewCMDargs.Rows.Add("deathmatch 1");
            dataGridViewCMDargs.Rows.Add("+exec listenserver.cfg");
            dataGridViewCMDargs.Rows.Add("-novid");
            dataGridViewCMDargs.Rows.Add("-steam");
        }
        private void AddRowstoConfigPreview()
        {
            dataGridViewConfigPreview.Rows.Clear();
            dataGridViewConfigPreview.Rows.Add("//Enable or Disable players turning on autoaim");
            dataGridViewConfigPreview.Rows.Add($"sv_allow_autoaim {(Properties.Settings.Default.Autocrosshair ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("//Allow Shaders, disable this if you care about preventing players from cheating by modifying shaders");
            dataGridViewConfigPreview.Rows.Add($"sv_allow_shaders {(Properties.Settings.Default.Shaders ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("//Player bounding box. Collisions, not clipping");
            dataGridViewConfigPreview.Rows.Add("sv_clienttrace 3.5");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add($"sv_gravity {Properties.Settings.Default.Gravity}");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("//Disable clients ability to pause the server");
            dataGridViewConfigPreview.Rows.Add("pausable 0");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("//Maximum client movement speed");
            dataGridViewConfigPreview.Rows.Add($"sv_maxspeed {Properties.Settings.Default.MaxSpeed}");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("//Server Settings");
            dataGridViewConfigPreview.Rows.Add($"mp_teamplay {(Properties.Settings.Default.Teamplay)}");
            if (Properties.Settings.Default.Teamplay >= 1)
            {
                //If Teamplay enabled, let's build our string
                List<string> enabledSettings = new List<string>();
                if (Properties.Settings.Default.barney) enabledSettings.Add("barney");
                if (Properties.Settings.Default.gman) enabledSettings.Add("gman");
                if (Properties.Settings.Default.gordon) enabledSettings.Add("gordon");
                if (Properties.Settings.Default.helmet) enabledSettings.Add("helmet");
                if (Properties.Settings.Default.hgrunt) enabledSettings.Add("hgrunt");
                if (Properties.Settings.Default.recon) enabledSettings.Add("recon");
                if (Properties.Settings.Default.robo) enabledSettings.Add("robo");
                if (Properties.Settings.Default.scientist) enabledSettings.Add("scientist");
                if (Properties.Settings.Default.zombie) enabledSettings.Add("zombie");
                string result = string.Join(";", enabledSettings);
                Properties.Settings.Default.ValidTeams = result;
                dataGridViewConfigPreview.Rows.Add("mp_teamlist \"" + result + "\"");
            }
            //
            if (Properties.Settings.Default.ServerPassword != null && Properties.Settings.Default.ServerPassword != "")
            {
                dataGridViewConfigPreview.Rows.Add($"sv_password \"{Properties.Settings.Default.ServerPassword}\"");
            }
            dataGridViewConfigPreview.Rows.Add($"mp_fraglimit {Properties.Settings.Default.FragLimit}");
            dataGridViewConfigPreview.Rows.Add($"mp_timelimit {Properties.Settings.Default.TimeLimit}");
            dataGridViewConfigPreview.Rows.Add($"mp_falldamage {(Properties.Settings.Default.FallingDamage ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_friendlyfire {(Properties.Settings.Default.FriendlyFire ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_weaponstay {(Properties.Settings.Default.WeaponsStay ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_forcerespawn {(Properties.Settings.Default.ForceRespawn ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_footsteps {(Properties.Settings.Default.Footsteps ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_flashlight {(Properties.Settings.Default.Flashlight ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mp_autocrosshair {(Properties.Settings.Default.Autocrosshair ? 1 : 0)}");
            dataGridViewConfigPreview.Rows.Add($"mapcyclefile \"llmapcycle.txt\"");
            dataGridViewConfigPreview.Rows.Add("sv_cheats 0");
            dataGridViewConfigPreview.Rows.Add("sv_allowdownload 1");
            dataGridViewConfigPreview.Rows.Add("sv_allowupload 1");
            dataGridViewConfigPreview.Rows.Add("");
            dataGridViewConfigPreview.Rows.Add("sv_lan 0");
        }
        public string ConcatenateCmdArgs()
        {
            // Use StringBuilder for better performance with string concatenations
            StringBuilder cmdArgsContent = new StringBuilder();

            // Loop through the DataGridView rows
            foreach (DataGridViewRow row in dataGridViewCMDargs.Rows)
            {
                if (!row.IsNewRow) // Ignore the new row placeholder
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellContent = cell.Value?.ToString() ?? string.Empty;

                        // Append the content to cmdArgsContent with a space
                        cmdArgsContent.Append(cellContent + " ");
                    }
                }
            }

            // Remove the trailing space if there is any
            if (cmdArgsContent.Length > 0)
            {
                cmdArgsContent.Length--; // Remove the last space
            }

            return cmdArgsContent.ToString();
        }
        private void ArgsToFile()
        {
            string profileArgPath = Properties.Settings.Default.GameProfileNoExt + @"GP.arg";
            StringBuilder profileArgContent = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViewCMDargs.Rows)
            {
                if (!row.IsNewRow) // Ignore the new row placeholder
                {
                    bool isEmptyRow = true;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellContent = cell.Value?.ToString() ?? string.Empty;
                        if (!string.IsNullOrEmpty(cellContent))
                        {
                            isEmptyRow = false;
                        }

                        // Append the content to the respective StringBuilder
                        profileArgContent.AppendLine(cellContent);
                    }

                    if (isEmptyRow)
                    {
                        // Append an empty line to the respective StringBuilder if the row is empty
                        profileArgContent.AppendLine(string.Empty);
                    }
                }
            }
            File.WriteAllText(profileArgPath, profileArgContent.ToString());
        }
        private void ConfigFileMake()
        {
            // Define the file paths
            string gameCfgPath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg";
            string listenserverCfgPath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg";
            string serverCfgPath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg";
            string profileCfgPath = Properties.Settings.Default.GameProfileNoExt + @"GP.cfg";

            // Use StringBuilder for better performance with string concatenations
            StringBuilder gameCfgContent = new StringBuilder();
            StringBuilder listenserverCfgContent = new StringBuilder();
            StringBuilder serverCfgContent = new StringBuilder();
            StringBuilder profileCfgContent = new StringBuilder();

            foreach (DataGridViewRow row in dataGridViewConfigPreview.Rows)
            {
                if (!row.IsNewRow) // Ignore the new row placeholder
                {
                    bool isEmptyRow = true;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellContent = cell.Value?.ToString() ?? string.Empty;
                        if (!string.IsNullOrEmpty(cellContent))
                        {
                            isEmptyRow = false;
                        }

                        // Append the content to the respective StringBuilder
                        gameCfgContent.AppendLine(cellContent);
                        listenserverCfgContent.AppendLine(cellContent);
                        serverCfgContent.AppendLine(cellContent);
                        profileCfgContent.AppendLine(cellContent);
                    }

                    if (isEmptyRow)
                    {
                        // Append an empty line to the respective StringBuilder if the row is empty
                        gameCfgContent.AppendLine(string.Empty);
                        listenserverCfgContent.AppendLine(string.Empty);
                        serverCfgContent.AppendLine(string.Empty);
                        profileCfgContent.AppendLine(string.Empty);
                    }
                }
            }
            File.WriteAllText(gameCfgPath, gameCfgContent.ToString());
            File.WriteAllText(listenserverCfgPath, listenserverCfgContent.ToString());
            File.WriteAllText(serverCfgPath, serverCfgContent.ToString());
            File.WriteAllText(profileCfgPath, profileCfgContent.ToString());
        }
        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        private void buttonCMD_Up_Click(object sender, EventArgs e)
        {
            if (dataGridViewCMDargs.SelectedRows.Count == 0)
            {
                return;
            }

            // Get the index of the selected row
            int selectedIndex = dataGridViewCMDargs.SelectedRows[0].Index;

            // Check if the selected row is not the first row
            if (selectedIndex > 0)
            {
                int newIndex = selectedIndex - 1;

                // Copy the selected row
                DataGridViewRow selectedRow = dataGridViewCMDargs.SelectedRows[0];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                }

                // Remove the selected row
                dataGridViewCMDargs.Rows.RemoveAt(selectedIndex);

                // Insert the new row at the new index
                dataGridViewCMDargs.Rows.Insert(newIndex, newRow);

                // Select the moved row
                dataGridViewCMDargs.ClearSelection();
                dataGridViewCMDargs.Rows[newIndex].Selected = true;
            }
        }
        private void buttonCMD_Down_Click(object sender, EventArgs e)
        {
            if (dataGridViewCMDargs.SelectedRows.Count == 0)
            {
                return;
            }

            // Get the index of the selected row
            int selectedIndex = dataGridViewCMDargs.SelectedRows[0].Index;

            // Check if the selected row is not the last row
            if (selectedIndex < dataGridViewCMDargs.Rows.Count - 1)
            {
                int newIndex = selectedIndex + 1;

                // Copy the selected row
                DataGridViewRow selectedRow = dataGridViewCMDargs.SelectedRows[0];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                }

                // Remove the selected row
                dataGridViewCMDargs.Rows.RemoveAt(selectedIndex);

                // Insert the new row at the new index
                dataGridViewCMDargs.Rows.Insert(newIndex, newRow);

                // Select the moved row
                dataGridViewCMDargs.ClearSelection();
                dataGridViewCMDargs.Rows[newIndex].Selected = true;
            }
        }
        private void buttonCMD_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCMDargs.SelectedRows.Count == 0)
            {
                return;
            }
            // Get the index of the selected row
            int selectedIndex = dataGridViewCMDargs.SelectedRows[0].Index;
            dataGridViewCMDargs.Rows.RemoveAt(selectedIndex);
        }
        private void buttonConf_Up_Click(object sender, EventArgs e)
        {
            if (dataGridViewConfigPreview.SelectedRows.Count == 0)
            {
                return;
            }

            // Get the index of the selected row
            int selectedIndex = dataGridViewConfigPreview.SelectedRows[0].Index;

            // Check if the selected row is not the first row
            if (selectedIndex > 0)
            {
                int newIndex = selectedIndex - 1;

                // Copy the selected row
                DataGridViewRow selectedRow = dataGridViewConfigPreview.SelectedRows[0];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                }

                // Remove the selected row
                dataGridViewConfigPreview.Rows.RemoveAt(selectedIndex);

                // Insert the new row at the new index
                dataGridViewConfigPreview.Rows.Insert(newIndex, newRow);

                // Select the moved row
                dataGridViewConfigPreview.ClearSelection();
                dataGridViewConfigPreview.Rows[newIndex].Selected = true;
            }
        }
        private void buttonConf_Down_Click(object sender, EventArgs e)
        {
            if (dataGridViewConfigPreview.SelectedRows.Count == 0)
            {
                return;
            }

            // Get the index of the selected row
            int selectedIndex = dataGridViewConfigPreview.SelectedRows[0].Index;

            // Check if the selected row is not the last row
            if (selectedIndex < dataGridViewConfigPreview.Rows.Count - 1)
            {
                int newIndex = selectedIndex + 1;

                // Copy the selected row
                DataGridViewRow selectedRow = dataGridViewConfigPreview.SelectedRows[0];
                DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                }

                // Remove the selected row
                dataGridViewConfigPreview.Rows.RemoveAt(selectedIndex);

                // Insert the new row at the new index
                dataGridViewConfigPreview.Rows.Insert(newIndex, newRow);

                // Select the moved row
                dataGridViewConfigPreview.ClearSelection();
                dataGridViewConfigPreview.Rows[newIndex].Selected = true;
            }
        }
        private void buttonConf_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridViewConfigPreview.SelectedRows.Count == 0)
            {
                return;
            }
            // Get the index of the selected row
            int selectedIndex = dataGridViewConfigPreview.SelectedRows[0].Index;
            dataGridViewConfigPreview.Rows.RemoveAt(selectedIndex);
        }
        private void dataGridViewConfigPreview_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewCMDargs.ClearSelection();
        }
        private void dataGridViewCMDargs_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewConfigPreview.ClearSelection();
        }
        private void SaveSettings()
        {
            Properties.Settings.Default.ServerConfigMethod = "Advanced";
            Properties.Settings.Default.AdvCMDargs = ConcatenateCmdArgs();
            ConfigFileMake();
            ArgsToFile();
        }
        private void buttonLoadDefaults_Click(object sender, EventArgs e)
        {
            AddRowstoLaunchArgs();
            AddRowstoConfigPreview();
            SaveSettings();
        }
        private void button_CreateGameProfile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to create a new profile?", "Continue?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                CreateGameProfilePrompt ProfilePromptWindow = new CreateGameProfilePrompt();
                ProfilePromptWindow.Show();
            }
            else if (dialogResult == DialogResult.No)
            {
                //Do nothing. 
            }
        }
        private void buttonCMD_Import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + Properties.Settings.Default.GameProfileName;  // Set initial directory (optional)
                openFileDialog.Filter = "LL-Arg files (*.arg)|*.arg|All files (*.*)|*.*";  // Filter for .arg files

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    try
                    {
                        // Read all lines from the selected .txt file
                        string[] lines = File.ReadAllLines(selectedFilePath);

                        // Clear existing rows in dataGridViewCopiedFiles
                        dataGridViewCMDargs.Rows.Clear();

                        // Add each line as a new row in dataGridViewCopiedFiles
                        foreach (string line in lines)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                //Add code here to searcha gainst data in grid1
                                //row.DefaultCellStyle.BackColor = Color.Red; // Change to red
                                dataGridViewCMDargs.Rows.Add(line.Trim()); // Trim whitespace from each line
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void buttonConf_Import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + Properties.Settings.Default.GameProfileName;  // Set initial directory (optional)
                openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";  // Filter for .cfg files

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    try
                    {
                        // Read all lines from the selected .txt file
                        string[] lines = File.ReadAllLines(selectedFilePath);
                        // Clear existing rows in dataGridViewCopiedFiles
                        dataGridViewConfigPreview.Rows.Clear();
                        int WhiteSpaceHandler = 0;//This will limit how many empty lines there are. Without this it will double the empty lines with every save. Do not know why so this is the solution
                        foreach (string line in lines)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                dataGridViewConfigPreview.Rows.Add(line.Trim()); // Trim whitespace from each line
                            }
                            else
                            {
                                WhiteSpaceHandler++;
                                if (WhiteSpaceHandler == 1)
                                {
                                    dataGridViewConfigPreview.Rows.Add("");
                                }
                                else
                                {
                                    WhiteSpaceHandler = 0;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void buttonCMD_Export_Click(object sender, EventArgs e)
        {
            // Create a new SaveFileDialog instance
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "LL-Arg files (*.arg)|*.arg|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Open the selected file path for writing
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            // Iterate through each row in dataGridViewCopiedFiles
                            foreach (DataGridViewRow row in dataGridViewCMDargs.Rows)
                            {
                                if (!row.IsNewRow) // Check if the row is not a new row
                                {
                                    // Get the value from the first column (index 0)
                                    string entry = row.Cells[0].Value?.ToString();

                                    if (!string.IsNullOrEmpty(entry))
                                    {
                                        // Write the entry to the file
                                        writer.WriteLine(entry);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void buttonConf_Export_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Configuration File (*.cfg)|*.cfg";
                saveFileDialog.Title = "Save Configuration File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = saveFileDialog.FileName;

                    // Use StringBuilder for better performance with string concatenations
                    StringBuilder combinedContent = new StringBuilder();

                    foreach (DataGridViewRow row in dataGridViewConfigPreview.Rows)
                    {
                        if (!row.IsNewRow) // Ignore the new row placeholder
                        {
                            bool isEmptyRow = true;

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string cellContent = cell.Value?.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(cellContent))
                                {
                                    isEmptyRow = false;
                                }

                                // Append the content to the combined StringBuilder
                                combinedContent.AppendLine(cellContent);
                            }

                            if (isEmptyRow)
                            {
                                // Append an empty line to the combined StringBuilder if the row is empty
                                combinedContent.AppendLine(string.Empty);
                            }
                        }
                    }

                    // Write the combined content to the selected file
                    File.WriteAllText(selectedFilePath, combinedContent.ToString());
                }
            }
        }
        private void MyForm_Paint(object sender, PaintEventArgs e)
        {
            Color outlineColor = this.Tag is Color color ? color : Color.Yellow; // Default to yellow if not set
            using (Pen outlinePen = new Pen(outlineColor, 3)) // Set the color and width of the outline
            {
                e.Graphics.DrawRectangle(outlinePen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

    }
}
