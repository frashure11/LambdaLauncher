using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace LambdaLoader
{
    public partial class MainForm : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            this.Paint += MyForm_Paint; // Subscribe to the Paint event
            if (Debugger.IsAttached)
            {
                //Properties.Settings.Default.Reset();
            }
        }//Enable debug settings reset here
        private List<string> exclusionList = new List<string>
        {
            "c4a3.bsp", "c5a1.bsp", "c0a0.bsp", "c0a0a.bsp", "c0a0b.bsp", "c0a0c.bsp", "c0a0d.bsp", "c0a0e.bsp",
            "c1a0.bsp", "c1a0a.bsp", "c1a0b.bsp", "c1a0c.bsp", "c1a0d.bsp", "c1a0e.bsp", "c1a1.bsp", "c1a1a.bsp",
            "c1a1b.bsp", "c1a1c.bsp", "c1a1d.bsp", "c1a1f.bsp", "c1a2.bsp", "c1a2a.bsp", "c1a2b.bsp", "c1a2c.bsp",
            "c1a2d.bsp", "c1a3.bsp", "c1a3a.bsp", "c1a3b.bsp", "c1a3c.bsp", "c1a3d.bsp", "c1a4.bsp", "c1a4b.bsp",
            "c1a4d.bsp", "c1a4e.bsp", "c1a4f.bsp", "c1a4g.bsp", "c1a4i.bsp", "c1a4j.bsp", "c1a4k.bsp", "c2a1.bsp",
            "c2a1a.bsp", "c2a1b.bsp", "c2a2.bsp", "c2a2a.bsp", "c2a2b1.bsp", "c2a2b2.bsp", "c2a2c.bsp", "c2a2d.bsp",
            "c2a2e.bsp", "c2a2f.bsp", "c2a2g.bsp", "c2a2h.bsp", "c2a3.bsp", "c2a3a.bsp", "c2a3b.bsp", "c2a3c.bsp",
            "c2a3d.bsp", "c2a3e.bsp", "c2a4.bsp", "c2a4a.bsp", "c2a4b.bsp", "c2a4c.bsp", "c2a4d.bsp", "c2a4e.bsp",
            "c2a4f.bsp", "c2a4g.bsp", "c2a5.bsp", "c2a5a.bsp", "c2a5b.bsp", "c2a5c.bsp", "c2a5d.bsp", "c2a5e.bsp",
            "c2a5f.bsp", "c2a5g.bsp", "c2a5w.bsp", "c2a5x.bsp", "c3a1.bsp", "c3a1a.bsp", "c3a1b.bsp", "c3a2.bsp",
            "c3a2a.bsp", "c3a2b.bsp", "c3a2c.bsp", "c3a2d.bsp", "c3a2e.bsp", "c3a2f.bsp", "c4a1.bsp", "c4a1a.bsp",
            "c4a1b.bsp", "c4a1c.bsp", "c4a1d.bsp", "c4a1e.bsp", "c4a1f.bsp", "c4a2.bsp", "c4a2a.bsp", "c4a2b.bsp",
            "hldemo1.bsp", "hldemo2.bsp", "hldemo3.bsp", "t0a0.bsp", "t0a0a.bsp", "t0a0b.bsp", "t0a0b1.bsp", "t0a0b2.bsp",
            "t0a0c.bsp", "t0a0d.bsp"
        };//                                                                  List            Ignore these maps when running RefreshMapList(). Single player maps and demo maps.
        private void Form1_Load(object sender, EventArgs e)
        {
            //Put anything here that needs setup in the beginning
            LaunchSettings.OriginalWindowWidth = this.Width;
            LaunchSettings.OriginalWindowHeight = this.Height;
            dataGridView1.Columns.Add("newColumnName", "Map Name");
            dataGridView1.Columns.Add("newColumnName2", "Folder");
            dataGridView2.Columns.Add("newColumnName", "Map Name");
            dataGridView2.Columns.Add("newColumnName2", "Folder");
            dataGridView3.Columns.Add("newColumnName", "Map Name");
            dataGridView3.Columns.Add("newColumnName2", "Folder");
            //Do something for the GameProfile Here
            Properties.Settings.Default.GameProfileNoExt = Properties.Settings.Default.GameProfile.Substring(0, Properties.Settings.Default.GameProfile.Length - 5) + @"\";
            if (Properties.Settings.Default.GameProfile != "default")
            {
                //Am I supposed to do something here?
            }
            if (Properties.Settings.Default.UserSelectedFolderPath.Length < 5)//On startup check if a user has already selected the HL directory previously
            {
                ChooseGameDirectory();
            }
            else
            {
                this.Text = ("Lambda Launcher | Game Path: " + Properties.Settings.Default.UserSelectedFolderPath); //Set the window title. This loads from the saved setting
                RefreshMapList();
            }
            BackupConfigFiles();
            if (Properties.Settings.Default.AutoImportRan == false)
            {
                AutoExtractExtras();
            }
            LoadThemes();
            LoadSavedTheme();
        }
        //                                                                                              REGION: Title Bar
        #region CustomTitleBar
        private void InitializeCustomTitleBar()
        {
            // Create the custom title bar panel
            titleBar.BackColor = Color.DarkBlue; // Change the color as needed
            titleBar.Dock = DockStyle.Top;
            titleBar.Height = 24;
            titleBar.MouseDown += TitleBar_MouseDown;
            titleBar.MouseMove += TitleBar_MouseMove;
            titleBar.MouseUp += TitleBar_MouseUp;

            // Create the minimize button
            Button minimizeButton = new Button();
            minimizeButton.Text = "_";
            minimizeButton.ForeColor = Color.White;
            minimizeButton.BackColor = Color.Gray;
            minimizeButton.Dock = DockStyle.Right;
            minimizeButton.Width = 27;
            minimizeButton.Click += MinimizeButton_Click;
            titleBar.Controls.Add(minimizeButton);

            // Create the close button
            Button closeButton = new Button();
            closeButton.Text = "X";
            closeButton.ForeColor = Color.White;
            closeButton.BackColor = Color.Red;
            closeButton.Dock = DockStyle.Right;
            closeButton.Width = 27;
            closeButton.Click += CloseButton_Click;
            titleBar.Controls.Add(closeButton);

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
        #endregion
        private void BackupConfigFiles()
        {
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg"))
            {
                System.IO.File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg"))
            {
                System.IO.File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg"))
            {
                System.IO.File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg");
            }
        }//                                                                                        EVENT           Backup Config Files, if they exist
        private void ChooseGameDirectory()
        {
            MessageBox.Show("Please select the folder Half-Life is installed in", "Game Directory Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string selectedFolderPath = "";
            // Create a new instance of the FolderBrowserDialog
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Show the dialog and get the result
                DialogResult result = folderBrowserDialog.ShowDialog();

                // If the user clicked OK, save the selected folder path
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    selectedFolderPath = folderBrowserDialog.SelectedPath;
                }
            }

            // Output the selected folder path
            this.Text = ("Lambda Launcher | Game Path: " + selectedFolderPath);
            //Save to settings
            Properties.Settings.Default.UserSelectedFolderPath = selectedFolderPath; //Set the window title. 
            RefreshMapList();
        }//                                                                                      EVENT           Opens folder browser window to choose game directory
        private void RefreshMapList()
        {
            //Scan the MAIN VALVE MAPS FOLDER
            string directoryToScan = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps";
            int nMap = 0;
            if (Directory.Exists(directoryToScan))
            {
                // Get all .BSP files in the directory
                string[] bspFiles = Directory.GetFiles(directoryToScan, "*.BSP", SearchOption.AllDirectories);

                //Output the found .BSP files to the ListBox
                foreach (string bspFile in bspFiles)
                {
                    string fileName = Path.GetFileName(bspFile); // Extract the file name
                    if (!exclusionList.Contains(fileName))
                    {
                        // Add the file to the DataGridView without the .bsp extension
                        string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 4);
                        dataGridView1.Rows.Add(fileNameWithoutExtension, @"\valve\maps");
                        nMap++;
                    }
                }
                //Scan the DOWNLAOD_VALVE FOLDER
                string secondarydirectoryToScan = Properties.Settings.Default.UserSelectedFolderPath + @"\valve_downloads\maps";
                if (Directory.Exists(secondarydirectoryToScan))
                {
                    // Get all .BSP files in the directory
                    bspFiles = Directory.GetFiles(secondarydirectoryToScan, "*.BSP", SearchOption.AllDirectories);

                    //Output the found .BSP files to the ListBox
                    foreach (string bspFile in bspFiles)
                    {
                        string fileName = Path.GetFileName(bspFile); // Extract the file name
                        string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 4);
                        dataGridView1.Rows.Add(fileNameWithoutExtension, @"\valve_downloads\maps");
                        nMap++;
                    }
                }
            }
            else
            {
                MessageBox.Show("The specified directory does not exist." + directoryToScan, "Error: Is the Game Directory correct?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.Rows.Clear();
                ChooseGameDirectory();
            }
            LoadFavoriteMapList();
            Properties.Settings.Default.nMapFinal = nMap;
            label1.Text = "Maps Found: " + nMap;
        }//                                                                                           EVENT           Refresh Grid 1
        private void AutoExtractExtras()
        {
            try
            {
                string extractPath = Directory.GetCurrentDirectory() + @"\Extras.zip";
                string destinationPath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps";
                if (File.Exists(extractPath))
                {
                    ZipFile.ExtractToDirectory(extractPath, destinationPath);
                    MessageBox.Show("Found Extras.zip file and extracted it. If you have more, use the 'Help>Install Extras' page", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Did not find a Extras.zip file to import. If you have one, use the 'Help>Install Extras' page to add it", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Properties.Settings.Default.AutoImportRan = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting Extras.zip: {ex.Message}");
            }
        }//                                                                                        EVENT           If Extras.zip is found, auto extract
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Clear();
            // Check if a cell is clicked
            if (e.RowIndex >= 0)
            {
                string selectedFileName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); //This is my BSP file
                string txtFilePath = Path.ChangeExtension(selectedFileName, ".txt");
                string selectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + txtFilePath;
                if (File.Exists(selectedFilePath))
                {
                    // Read the contents of the .txt file
                    string fileContents = File.ReadAllText(selectedFilePath);

                    // Display contents in RichTextBox
                    richTextBox1.Text = fileContents;
                }
                else
                {
                    richTextBox1.Text = "No description available.";
                }
                string imageFilePath = Path.ChangeExtension(selectedFileName, ".jpg");
                string imageselectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + imageFilePath;
                if (File.Exists(imageselectedFilePath))
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    pictureBox1.Image = Image.FromFile(imageselectedFilePath);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
            }
        }//                                        EVENT           Cell in Grid 1 clicked
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Clear();
            // Check if a cell is clicked
            if (e.RowIndex >= 0)
            {
                string selectedFileName = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString(); //This is my BSP file
                string txtFilePath = Path.ChangeExtension(selectedFileName, ".txt");
                string selectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + txtFilePath;
                if (File.Exists(selectedFilePath))
                {
                    // Read the contents of the .txt file
                    string fileContents = File.ReadAllText(selectedFilePath);

                    // Display contents in RichTextBox
                    richTextBox1.Text = fileContents;
                }
                else
                {
                    richTextBox1.Text = "No description available.";
                }
                string imageFilePath = Path.ChangeExtension(selectedFileName, ".jpg");
                string imageselectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + imageFilePath;
                if (File.Exists(imageselectedFilePath))
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    pictureBox1.Image = Image.FromFile(imageselectedFilePath);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
            }
        }//                                        EVENT           Cell in Grid 2 clicked
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Clear();
            // Check if a cell is clicked
            if (e.RowIndex >= 0)
            {
                string selectedFileName = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString(); //This is my BSP file
                string txtFilePath = Path.ChangeExtension(selectedFileName, ".txt");
                string selectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + txtFilePath;
                if (File.Exists(selectedFilePath))
                {
                    // Read the contents of the .txt file
                    string fileContents = File.ReadAllText(selectedFilePath);

                    // Display contents in RichTextBox
                    richTextBox1.Text = fileContents;
                }
                else
                {
                    richTextBox1.Text = "No description available.";
                }
                string imageFilePath = Path.ChangeExtension(selectedFileName, ".jpg");
                string imageselectedFilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\" + imageFilePath;
                if (File.Exists(imageselectedFilePath))
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    pictureBox1.Image = Image.FromFile(imageselectedFilePath);
                }
                else
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
            }
        }//                                        EVENT           Cell in Grid 3 clicked
        private void buttonAddPrimaryMap_Click(object sender, EventArgs e)
        {

            // Check if a row is selected
            if (dataGridView1.CurrentRow != null)
            {
                int selectedRowIndex = dataGridView1.CurrentRow.Index;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                // Extract values from the selected row
                string fileName = selectedRow.Cells[0].Value.ToString();
                string path = selectedRow.Cells[1].Value.ToString();

                // Clear existing rows in the second DataGridView
                dataGridView3.Rows.Clear();

                // Add the values to the second DataGridView
                dataGridView3.Rows.Add(fileName, path);
                buttonLaunch.Enabled = true;
                Properties.Settings.Default.LastRanMap = fileName;
            }
            else
            {
                MessageBox.Show("Please select a row to copy.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }//                                                      Button          Add map selected in Grid 1 to Grid 3
        private void buttonAddToPlaylist_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.CurrentRow != null)
            {
                int selectedRowIndex = dataGridView1.CurrentRow.Index;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                // Extract values from the selected row
                string fileName = selectedRow.Cells[0].Value.ToString();
                string path = selectedRow.Cells[1].Value.ToString();

                // Add the values to the second DataGridView
                dataGridView2.Rows.Add(fileName, path);
            }
            else
            {
                MessageBox.Show("Please select a row to copy.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }//                                                      Button          Add map selected in Grid 1 to Grid 2
        private void buttonRemoveFromPlaylist_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView2.CurrentRow != null)
            {
                int selectedRowIndex = dataGridView2.CurrentRow.Index;
                dataGridView2.Rows.RemoveAt(selectedRowIndex);
            }
            else
            {
                MessageBox.Show("Please select a row to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }//                                                 Button          Remove map from Grid 2
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView2.ClearSelection();
                dataGridView3.ClearSelection();
            }
        }//                                                 EVENT           Grid 1 selected, deselect maps in 2 and 3
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView3.ClearSelection();
            }
        }//                                                 EVENT           Grid 2 selected, deselect maps in 1 and 3
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView2.ClearSelection();
            }
        }//                                                 EVENT           Grid 3 selected, deselect maps in 1 and 2
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Make sure your map .bsp file is at: \n \n" + Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps\", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//                                     Button          Displays message box telling user where map files should be placed
        private void buttonLoadMapRotationFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.UserSelectedFolderPath + @"\valve";  // Set initial directory (optional)
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";  // Filter for .txt files

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    try
                    {
                        // Read all lines from the selected .txt file
                        string[] lines = File.ReadAllLines(selectedFilePath);

                        // Clear existing rows in dataGridViewCopiedFiles
                        dataGridView2.Rows.Clear();

                        // Add each line as a new row in dataGridViewCopiedFiles
                        foreach (string line in lines)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                //Add code here to searcha gainst data in grid1
                                //row.DefaultCellStyle.BackColor = Color.Red; // Change to red
                                dataGridView2.Rows.Add(line.Trim()); // Trim whitespace from each line
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }//                                                Button          Load a map rotation file from disk
        private void buttonSavePlaylist_Click(object sender, EventArgs e)
        {
            // Create a new SaveFileDialog instance
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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
                            foreach (DataGridViewRow row in dataGridView2.Rows)
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
        }//                                                       Button          Save a map rotation file to disk
        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            //Save the current primary map to a variable for the next time the program is launched
            Properties.Settings.Default.LastRanMap = dataGridView3.Rows[0].Cells[0].Value.ToString();
            //Overwrite map rotation file.
            string mapcyclefilePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\" + "mapcycle.txt"; // Specify the path to the mapcycle.txt file
            try
            {
                // Open mapcycle.txt for writing (this will overwrite the existing file)
                using (StreamWriter writer = new StreamWriter(mapcyclefilePath))
                {
                    // Iterate through each row in dataGridViewCopiedFiles
                    foreach (DataGridViewRow row in dataGridView2.Rows)
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
                CreateOrUpdatelistenservercfg();
                BuildLaunchArguments();
                RunExe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//                                                             Button          LAUNCH Map(s)
        private static void BuildLaunchArguments()
        {
            if (Properties.Settings.Default.ServerConfigMethod == "Basic")
            {
                StringBuilder args = new StringBuilder();
                args.Append(" -game -valve ");
                if (!string.IsNullOrEmpty(Properties.Settings.Default.Hostname))
                {
                    args.Append($"+hostname \"{Properties.Settings.Default.Hostname}\" ");
                }
                args.Append("-maxplayers " + Properties.Settings.Default.MaxPlayers + " ");
                args.Append("deathmatch 1 ");
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastRanMap))
                {
                    args.Append($"+map \"{Properties.Settings.Default.LastRanMap}\" ");
                }
                args.Append("+exec listenserver.cfg -novid ");
                if (Properties.Settings.Default.EnableSteamNet == true)
                {
                    args.Append("-steam ");
                }
                // Trim any trailing whitespace and set the Arguments property
                LaunchSettings.Arguments = args.ToString().Trim();
            }
            else
            {
                LaunchSettings.Arguments = Properties.Settings.Default.AdvCMDargs;
            }
        }//                                                                              EVENT           Create Launch Arguments
        public static void CreateOrUpdatelistenservercfg()
        {
            if (Properties.Settings.Default.ServerConfigMethod == "Basic")
            {
                string basePath = Properties.Settings.Default.UserSelectedFolderPath + @"\valve\";
                List<string> configFiles = new List<string>
                {
                    Path.Combine(basePath, "listenserver.cfg"),
                    Path.Combine(basePath, "server.cfg"),
                    Path.Combine(basePath, "game.cfg")
                };
                StringBuilder configContent = new StringBuilder();
                configContent.AppendLine("//Enable or Disable players turning on autoaim");
                configContent.AppendLine($"sv_allow_autoaim {(Properties.Settings.Default.Autocrosshair ? 1 : 0)}");
                configContent.AppendLine("");
                configContent.AppendLine("//Allow Shaders, disable this if you care about preventing players from cheating by modifying shaders");
                configContent.AppendLine($"sv_allow_shaders {(Properties.Settings.Default.Shaders ? 1 : 0)}");
                configContent.AppendLine("");
                configContent.AppendLine("//Player bounding box. Collisions, not clipping");
                configContent.AppendLine("sv_clienttrace 3.5");
                configContent.AppendLine("");
                configContent.AppendLine($"sv_gravity {Properties.Settings.Default.Gravity}");
                configContent.AppendLine("");
                configContent.AppendLine("//Disable clients ability to pause the server");
                configContent.AppendLine("pausable 0");
                configContent.AppendLine("");
                configContent.AppendLine("//Maximum client movement speed");
                configContent.AppendLine($"sv_maxspeed {Properties.Settings.Default.MaxSpeed}");
                configContent.AppendLine("");
                configContent.AppendLine("//Server Settings");
                configContent.AppendLine($"mp_teamplay {(Properties.Settings.Default.Teamplay)}");
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
                    configContent.AppendLine("mp_teamlist \"" + result + "\"");
                }
                //
                if (Properties.Settings.Default.ServerPassword != null && Properties.Settings.Default.ServerPassword != "")
                {
                    configContent.AppendLine($"sv_password \"{Properties.Settings.Default.ServerPassword}\"");
                }
                configContent.AppendLine($"mp_fraglimit {Properties.Settings.Default.FragLimit}");
                configContent.AppendLine($"mp_timelimit {Properties.Settings.Default.TimeLimit}");
                configContent.AppendLine($"mp_falldamage {(Properties.Settings.Default.FallingDamage ? 1 : 0)}");
                configContent.AppendLine($"mp_friendlyfire {(Properties.Settings.Default.FriendlyFire ? 1 : 0)}");
                configContent.AppendLine($"mp_weaponstay {(Properties.Settings.Default.WeaponsStay ? 1 : 0)}");
                configContent.AppendLine($"mp_forcerespawn {(Properties.Settings.Default.ForceRespawn ? 1 : 0)}");
                configContent.AppendLine($"mp_footsteps {(Properties.Settings.Default.Footsteps ? 1 : 0)}");
                configContent.AppendLine($"mp_flashlight {(Properties.Settings.Default.Flashlight ? 1 : 0)}");
                configContent.AppendLine($"mp_autocrosshair {(Properties.Settings.Default.Autocrosshair ? 1 : 0)}");
                configContent.AppendLine($"mapcyclefile \"mapcycle.txt\"");
                configContent.AppendLine("sv_cheats 0");
                configContent.AppendLine("sv_allowdownload 1");
                configContent.AppendLine("sv_allowupload 1");
                configContent.AppendLine("");
                if (Properties.Settings.Default.EnableSteamNet == true)
                {
                    configContent.AppendLine("sv_lan 0");
                }
                else
                {
                    configContent.AppendLine("sv_lan 1");
                }

                // Write the same content to all configuration files
                foreach (string configFile in configFiles)
                {
                    File.WriteAllText(configFile, configContent.ToString());
                }
            }
        }//                                                                      EVENT           Create config files needed
        private static void RunExe()
        {
            if (Properties.Settings.Default.ServerConfigMethod == "Basic")
            {
                Properties.Settings.Default.ExePath = Properties.Settings.Default.UserSelectedFolderPath + @"\hl.exe"; // Replace with the actual path to hl.exe
                Properties.Settings.Default.LaunchCMDargs = LaunchSettings.Arguments;
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = Properties.Settings.Default.ExePath,
                        Arguments = Properties.Settings.Default.LaunchCMDargs,
                        UseShellExecute = false
                    };

                    using (Process process = Process.Start(startInfo))
                    {
                        //    //Console.WriteLine("Half-Life deathmatch game launched successfully.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error launching Half-Life", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Properties.Settings.Default.ExePath = Properties.Settings.Default.UserSelectedFolderPath + @"\hl.exe"; // Replace with the actual path to hl.exe
                Properties.Settings.Default.LaunchCMDargs = LaunchSettings.Arguments;
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe", // Use cmd.exe to execute the command on Windows
                        Arguments = $"/C {Properties.Settings.Default.LaunchCMDargs}", // /C to carry out the command and then terminate
                        UseShellExecute = false
                    };

                    using (Process process = Process.Start(startInfo))
                    {
                        // Optionally handle process output or wait for exit
                        // process.WaitForExit();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error launching Half-Life", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }//                                                                                            EVENT           Run the defined .exe file. This was added for 
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            // Toggle between StretchImage and AutoSize
            //Disabled due to it covering up the richTextBox and also it increases the window size, but does not automatically set it back to the correct size when image is shrunk.
            if (pictureBox1.SizeMode == PictureBoxSizeMode.StretchImage)//Grows the image and sets label for shrink
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                //buttonLaunch.Visible = false;
                label4.Text = "Click to Shrink";
                richTextBox1.Location = new System.Drawing.Point(13, 630); // New location
                richTextBox1.Size = new System.Drawing.Size(550, 160); // New size
                buttonLaunch.Location = new System.Drawing.Point(593, 684);
                this.Height = 820;
            }
            else if (pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize)//Shrinks the image and sets label for grow
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //buttonLaunch.Visible = true;
                label4.Text = "Click to Enlarge";
                richTextBox1.Location = new System.Drawing.Point(687, 243); // Original location
                richTextBox1.Size = new System.Drawing.Size(281, 433); // Original size
                buttonLaunch.Location = new System.Drawing.Point(880, 684);
                this.Width = LaunchSettings.OriginalWindowWidth;
                this.Height = LaunchSettings.OriginalWindowHeight;
            }
            this.Invalidate();
        }//                                                              PictureBox Click     Grow or Shrink map preview
        private void buttonFavorite_Click(object sender, EventArgs e)
        {
            // Change the color of the currently selected row in dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.DefaultCellStyle.BackColor == Color.Yellow)
                    {
                        //row.DefaultCellStyle.BackColor = Color.White; // Change back to white
                        if (row.Index % 2 == 0)
                        {
                            row.DefaultCellStyle.BackColor = ThemeLoader.DataGridViewRowBackgroundColor;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = ThemeLoader.DataGridViewAlternatingRowBackgroundColor;
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow; // Change to yellow
                    }
                }
            }
            else
            {

            }
        }//                                                           Button          Favorite currently selected map
        private void SaveFavoriteMapList()
        {
            string directoryPath = Properties.Settings.Default.GameProfileNoExt;
            string filename = "FavoriteMaps.txt";
            string filePath = Path.Combine(directoryPath, filename);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.DefaultCellStyle.BackColor == Color.Yellow)
                    {
                        writer.WriteLine(row.Cells[0].Value.ToString());
                    }
                }
            }
        }//                                                                                      EVENT           Save Favorite Maps list
        private void LoadFavoriteMapList()
        {
            if (File.Exists(Properties.Settings.Default.GameProfileNoExt + "FavoriteMaps.txt"))
            {
                string[] yellowRowValues = File.ReadAllLines(Properties.Settings.Default.GameProfileNoExt + "FavoriteMaps.txt");
                foreach (string value in yellowRowValues)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == value)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            break;
                        }
                    }
                }
            }
        }//                                                                                      EVENT           Load Favorite Maps list and apply color to rows
        private void buttonLoadDefaultMapCycle_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add("frenzy", @"\valve\maps");
            dataGridView2.Rows.Add("contamination", @"\valve\maps");
            dataGridView2.Rows.Add("gasworks", @"\valve\maps");
            dataGridView2.Rows.Add("disposal", @"\valve\maps");
            dataGridView2.Rows.Add("subtransit", @"\valve\maps");
            dataGridView2.Rows.Add("pool_party", @"\valve\maps");
            dataGridView2.Rows.Add("crossfire", @"\valve\maps");
            dataGridView2.Rows.Add("rocket_frenzy", @"\valve\maps");
        }//                                                Button          Load Valves Map Rotation
        //                                                                                              REGION     Theme Code
        #region Theme_Crap
        public void LoadSavedTheme()
        {
            LoadTheme(Properties.Settings.Default.SelectedTheme);
        }
        private void LoadTheme(string themeName)
        {
            Properties.Settings.Default.SelectedTheme = themeName;
            Theme selectedTheme = themes.FirstOrDefault(t => t.Name == themeName);
            if (selectedTheme != null)
            {
                ApplyTheme(selectedTheme);
            }
        }
        public void LoadThemetoNewWindow()
        {
            ApplyThemeToAllOpenForms(Properties.Settings.Default.SelectedTheme);
        }
        private void ThemeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                Debug.WriteLine($"Attempting to load theme: {clickedItem.Text}");
                Properties.Settings.Default.SelectedTheme = clickedItem.Text;
                Theme selectedTheme = themes.FirstOrDefault(t => t.Name == clickedItem.Text);
                if (selectedTheme != null)
                {
                    ApplyTheme(selectedTheme);
                }
            }
        }
        private void ApplyThemeToAllOpenForms(string themeName)
        {
            Theme selectedTheme = themes.FirstOrDefault(t => t.Name == themeName);
            if (selectedTheme != null)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm != this) // Skip the current form if already themed
                    {
                        ApplyTheme(openForm, selectedTheme);
                    }
                }
            }
        }
        private List<Theme> themes = new List<Theme>();
        private void LoadThemes()
        {
            string themesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data", "Themes");
            if (!Directory.Exists(themesDirectory))
            {
                MessageBox.Show("Themes directory not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear existing items
            themesToolStripMenuItem.DropDownItems.Clear();
            themes.Clear();

            // Get all XML files in the themes directory
            string[] themeFiles = Directory.GetFiles(themesDirectory, "*.xml");

            foreach (string themeFile in themeFiles)
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.Load(themeFile);
                    XmlNode themeNode = xmlDoc.SelectSingleNode("/Theme");
                    if (themeNode != null && themeNode.Attributes["Name"] != null)
                    {
                        string themeName = themeNode.Attributes["Name"].Value;

                        // Create a new Theme object
                        Theme theme = new Theme { Name = themeName };

                        // Parse Form colors
                        XmlNode formNode = themeNode.SelectSingleNode("Form");
                        if (formNode != null)
                        {
                            theme.BackgroundColor = GetColorFromNode(formNode["BackgroundColor"]);
                            theme.ForegroundColor = GetColorFromNode(formNode["ForeColor"]);
                            theme.TitleBarColor = GetColorFromNode(formNode["TitleBarColor"]);
                            theme.WindowOutlineColor = GetColorFromNode(formNode["WindowOutlineColor"]);
                        }

                        // Parse Button colors
                        XmlNode buttonNode = themeNode.SelectSingleNode("Button");
                        if (buttonNode != null)
                        {
                            theme.ButtonColor = GetColorFromNode(buttonNode["BackgroundColor"]);
                            theme.ButtonForeColor = GetColorFromNode(buttonNode["ForeColor"]);
                            theme.ButtonOutlineColor = GetColorFromNode(buttonNode["TitleBarColor"]);
                        }

                        // Parse RichTextBox colors
                        XmlNode richTextBoxNode = themeNode.SelectSingleNode("RichTextBox");
                        if (richTextBoxNode != null)
                        {
                            theme.RichTextBoxBackgroundColor = GetColorFromNode(richTextBoxNode["BackgroundColor"]);
                            theme.RichTextBoxForeColor = GetColorFromNode(richTextBoxNode["ForeColor"]);
                        }

                        // Parse Label colors
                        XmlNode labelNode = themeNode.SelectSingleNode("Label");
                        if (labelNode != null)
                        {
                            theme.LabelForeColor = GetColorFromNode(labelNode["ForeColor"]);
                        }
                        // Parse LinkLabel colors
                        XmlNode linkLabelNode = themeNode.SelectSingleNode("LinkLabel");
                        if (linkLabelNode != null)
                        {
                            theme.LinkLabelLinkColor = GetColorFromNode(linkLabelNode["LinkColor"]);
                            theme.LinkLabelActiveLinkColor = GetColorFromNode(linkLabelNode["ActiveLinkColor"]);
                            theme.LinkLabelVisitedLinkColor = GetColorFromNode(linkLabelNode["VisitedLinkColor"]);
                        }
                        // Parse DataGridView colors
                        XmlNode dataGridViewNode = themeNode.SelectSingleNode("DataGridView");
                        if (dataGridViewNode != null)
                        {
                            theme.DataGridViewBackgroundColor = GetColorFromNode(dataGridViewNode["BackgroundColor"]);
                            theme.DataGridViewForeColor = GetColorFromNode(dataGridViewNode["ForeColor"]);
                            theme.DataGridViewHeaderBackgroundColor = GetColorFromNode(dataGridViewNode["HeaderBackgroundColor"]);
                            theme.DataGridViewHeaderForeColor = GetColorFromNode(dataGridViewNode["HeaderForeColor"]);
                            theme.DataGridViewHeaderOutlineColor = GetColorFromNode(dataGridViewNode["HeaderOutlineColor"]);
                            theme.DataGridViewRowBackgroundColor = GetColorFromNode(dataGridViewNode["RowBackgroundColor"]);
                            theme.DataGridViewAlternatingRowBackgroundColor = GetColorFromNode(dataGridViewNode["AlternatingRowBackgroundColor"]);
                            theme.DataGridViewSelectionBackColor = GetColorFromNode(dataGridViewNode["SelectionBackColor"]);
                            theme.DataGridViewSelectionForeColor = GetColorFromNode(dataGridViewNode["SelectionForeColor"]);
                        }
                        // Parse MenuStrip colors
                        XmlNode menuStripNode = themeNode.SelectSingleNode("MenuStrip");
                        if (menuStripNode != null)
                        {
                            theme.MenuStripBackColor = GetColorFromNode(menuStripNode["BackColor"]);
                            theme.MenuStripForeColor = GetColorFromNode(menuStripNode["ForeColor"]);
                            theme.MenuStripItemBackColor = GetColorFromNode(menuStripNode["ItemBackColor"]);
                            theme.MenuStripItemForeColor = GetColorFromNode(menuStripNode["ItemForeColor"]);
                            theme.MenuStripSelectedBackColor = GetColorFromNode(menuStripNode["SelectedBackColor"]);
                            theme.MenuStripSelectedForeColor = GetColorFromNode(menuStripNode["SelectedForeColor"]);
                        }
                        themes.Add(theme);
                        // Create a new ToolStripMenuItem for the theme
                        ToolStripMenuItem themeMenuItem = new ToolStripMenuItem(themeName);
                        themeMenuItem.Click += ThemeMenuItem_Click;

                        // Add the theme item to the Themes menu
                        themesToolStripMenuItem.DropDownItems.Add(themeMenuItem);
                    }
                    else
                    {
                        MessageBox.Show($"The theme file {themeFile} does not contain a valid 'Name' attribute or is not structured correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading theme from file {themeFile}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private Color GetColorFromNode(XmlNode node)
        {
            if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
            {
                return Color.Empty; // Or handle as you see fit
            }

            try
            {
                return ColorTranslator.FromHtml(node.InnerText);
            }
            catch
            {
                // Handle named colors (e.g., ButtonFace)
                return Color.FromName(node.InnerText);
            }
        }
        private void ApplyTheme(Theme theme)//This one is used by LoadTheme
        {
            this.BackColor = theme.BackgroundColor;
            this.ForeColor = theme.ForegroundColor;
            this.Tag = theme.WindowOutlineColor;
            // Iterate through all controls on the form and apply the theme
            foreach (Control control in this.Controls)
            {
                ApplyControlTheme(control, theme);//Although there are two ApplyTheme funcs, they both run this func to actually apply
            }

            // Apply the theme to the menu strip separately if you have one
            if (this.MainMenuStrip != null)
            {
                ApplyMenuStripTheme(this.MainMenuStrip, theme);
            }
            this.Invalidate();
            ApplyTitleBarTheme(titleBar, theme);
            Debug.WriteLine("Ran ApplyTitleBarTheme Manually");
        }
        private void ApplyTheme(Form form, Theme theme)//This one is used by ApplyThemeToAllOpenForms
        {
            form.BackColor = theme.BackgroundColor;
            form.ForeColor = theme.ForegroundColor;

            foreach (Control control in form.Controls)
            {
                ApplyControlTheme(control, theme);//Although there are two ApplyTheme funcs, they both run this func to actually apply
            }
            ApplyTitleBarTheme(titleBar, theme);
            Debug.WriteLine("Ran ApplyTitleBarTheme Manually");
        }
        private void ApplyControlTheme(Control control, Theme theme)
        {
            //titleBar is changed under ApplyTitleBarTheme and is called under ele if (control is MenuStrip)
            //titleBar.BackColor = theme.MenuStripBackColor;
            //SomeFunction();
            if (control is Button)
            {
                ApplyButtonTheme((Button)control, theme);
            }
            else if (control is RichTextBox)
            {
                ApplyRichTextBoxTheme((RichTextBox)control, theme);
            }
            else if (control is Label)
            {
                ApplyLabelTheme((Label)control, theme);
            }
            else if (control is LinkLabel)
            {
                ApplyLinkLabelTheme((LinkLabel)control, theme);
            }
            else if (control is DataGridView)
            {
                ApplyDataGridViewTheme((DataGridView)control, theme);
            }
            else if (control is MenuStrip)
            {
                ApplyMenuStripTheme((MenuStrip)control, theme);
            }
            else if (control is Panel && control.Name == "titleBar")
            {
                ApplyTitleBarTheme((Panel)control, theme);
                Debug.WriteLine("Ran Apply Control Theme and found a titleBar");
            }
            // Recursively apply the theme to child controls
            foreach (Control childControl in control.Controls)
            {
                ApplyControlTheme(childControl, theme);
            }
        }//Used by both ApplyTheme Funcs
        private void ApplyButtonTheme(Button button, Theme theme)
        {
            if(Properties.Settings.Default.SelectedTheme == "Default")
            {
                button.FlatStyle = FlatStyle.Standard;
            }
            else
            {
                button.FlatStyle = FlatStyle.Flat;
            }
            button.BackColor = theme.ButtonColor;
            button.ForeColor = theme.ButtonForeColor;

            // Apply the outline color
            button.FlatAppearance.BorderColor = theme.ButtonOutlineColor;
            button.FlatAppearance.BorderSize = 1; // Adjust the border size as needed
        }
        private void ApplyRichTextBoxTheme(RichTextBox richTextBox, Theme theme)
        {
            richTextBox.BackColor = theme.RichTextBoxBackgroundColor;
            richTextBox.ForeColor = theme.RichTextBoxForeColor;
        }
        private void ApplyLabelTheme(Label label, Theme theme)
        {
            label.ForeColor = theme.LabelForeColor;
        }
        private void ApplyLinkLabelTheme(LinkLabel linkLabel, Theme theme)
        {
            linkLabel.LinkColor = theme.LinkLabelLinkColor;
            linkLabel.ActiveLinkColor = theme.LinkLabelActiveLinkColor;
            linkLabel.VisitedLinkColor = theme.LinkLabelVisitedLinkColor;
        }
        private void ApplyDataGridViewTheme(DataGridView dataGridView, Theme theme)
        {
            dataGridView.BackgroundColor = theme.DataGridViewBackgroundColor;
            dataGridView.ForeColor = theme.DataGridViewForeColor;

            // Apply header styles
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = theme.DataGridViewHeaderBackgroundColor;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = theme.DataGridViewHeaderForeColor;

            if (theme.DataGridViewHeaderOutlineColor != Color.Empty)
            {
                dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = theme.DataGridViewHeaderOutlineColor;
            }

            // Ensure visual styles are disabled to apply custom colors
            dataGridView.EnableHeadersVisualStyles = false;

            // Apply row styles
            dataGridView.RowsDefaultCellStyle.BackColor = theme.DataGridViewRowBackgroundColor;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = theme.DataGridViewAlternatingRowBackgroundColor;

            if (theme.DataGridViewSelectionBackColor != Color.Empty)
            {
                dataGridView.DefaultCellStyle.SelectionBackColor = theme.DataGridViewSelectionBackColor;
            }

            if (theme.DataGridViewSelectionForeColor != Color.Empty)
            {
                dataGridView.DefaultCellStyle.SelectionForeColor = theme.DataGridViewSelectionForeColor;
            }

            // Apply grid line color (if needed)
            dataGridView.GridColor = theme.DataGridViewHeaderOutlineColor;

            dataGridView.ClearSelection();
            dataGridView.Refresh();
        }
        private void ApplyMenuStripTheme(MenuStrip menuStrip, Theme theme)
        {
            menuStrip.BackColor = theme.MenuStripBackColor;
            menuStrip.ForeColor = theme.MenuStripForeColor;

            foreach (ToolStripMenuItem menuItem in menuStrip.Items)
            {
                ApplyMenuStripItemTheme(menuItem, theme);
            }
        }
        private void ApplyMenuStripItemTheme(ToolStripMenuItem menuItem, Theme theme)
        {
            menuItem.BackColor = theme.MenuStripItemBackColor;
            menuItem.ForeColor = theme.MenuStripItemForeColor;

            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem)
                {
                    ApplyMenuStripItemTheme((ToolStripMenuItem)subItem, theme);
                }
            }
        }
        private void ApplyTitleBarTheme(Panel titleBar, Theme theme)
        {
            if (titleBar != null)
            {
                titleBar.BackColor = theme.TitleBarColor;
                Debug.WriteLine("Ran ApplyTitleBarTheme and title bar is not null");
                // Apply other properties as needed
            }
            else
            {
                Debug.WriteLine("Ran ApplyTitleBarTheme and title bar is null");

            }
        }//                                                                                                       END REGION
        #endregion
        private void gamePathToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Do you want to choose a new game directory?", "Continue?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ChooseGameDirectory();
            }
            else if (dialogResult == DialogResult.No)
            {
                //Do nothing. 
            }
        }//                                                Menustrip       Change Game Path
        private void downloadExtrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallExtrasForm extraWindow = new InstallExtrasForm();
            Theme savedTheme = themes.FirstOrDefault(t => t.Name == Properties.Settings.Default.SelectedTheme);
            extraWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
            LoadThemetoNewWindow();
            //NO CODE RUNS HERE UNTIL WINDOW CLOSED if in modal mode

        }//                                                                                                                          Menustrip       OPEN Install Extras Window
        private void serverConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("The server settings do not currently work, but you're welcome to checkout the configuration window if you want.", "Broken Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Create an instance of the ServerConfigWindow form
            ServConfigWindow configWindow = new ServConfigWindow();

            // Show the ServerConfigWindow form
            configWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
            LoadThemetoNewWindow();
        }//                                     Menustrip       Configure Server Settings
        private void mapFolderControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature hasn't been implemented yet, check back in future updates", "Under Construction", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//                                       Menustrip       Map Folder Controls Window
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the ServerConfigWindow form
            AboutForm AboutWindow = new AboutForm();

            // Show the ServerConfigWindow form
            AboutWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
        }//                                         Menustrip       Show Check for Updates 
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature hasn't been implemented yet, check back in future updates", "Under Construction", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//                                                   Menustrip       Show About Window
        private void advancedServerConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServConfigWindow configWindow = new ServConfigWindow();
            //if (currentTheme != null)
            //{
            //    ApplyTheme(configWindow, currentTheme);
            //}
            // Show the ServerConfigWindow form
            configWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
            LoadThemetoNewWindow();
        }//                                    Menustrip       Show Server Config Window
        private void advancedServerConfigToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ServerAdvancedConfig advancedconfigWindow = new ServerAdvancedConfig();
            advancedconfigWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
            LoadThemetoNewWindow();
        }//                                   Menustrip       Show Advanced Server Config Window
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFavoriteMapList();
            Properties.Settings.Default.Save();
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg") && File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg"))
            {
                File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg");
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg");
            }
            else if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPserver.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\server.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg") && File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg"))
            {
                File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg");
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg");
            }
            else if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPlistenserver.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\listenserver.cfg");
            }
            if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg") && File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg"))
            {
                File.Delete(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg");
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg");
            }
            else if (File.Exists(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg"))
            {
                System.IO.File.Move(Properties.Settings.Default.UserSelectedFolderPath + @"\valve\BACKUPgame.cfg", Properties.Settings.Default.UserSelectedFolderPath + @"\valve\game.cfg");
            }
            Environment.Exit(1);
        }//                                                   EVENT           MainForm is Closing
        private void MyForm_Paint(object sender, PaintEventArgs e)
        {
            Color outlineColor = this.Tag is Color color ? color : Color.Yellow; // Default to yellow if not set
            using (Pen outlinePen = new Pen(outlineColor, 3)) // Set the color and width of the outline
            {
                e.Graphics.DrawRectangle(outlinePen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
        
    }
    public class LaunchSettings
    {
        public static int OriginalWindowWidth = 0;
        public static int OriginalWindowHeight = 0;
        public static string TeamString = "";
        public static string Arguments = "";
        public static string ArgumentsConstants = @"hl.exe -game valve -novid -deathmatch 1 +exec listenserver.cfg +map contamination";
        //public static string ArgumentsConstants = @"hl.exe -game valve +deathmatch 1 +mp_footsteps 0 +exec listenserver.cfg +map contamination"; //Should work as failsafe if needed for troubleshooting
    }
    public static class ThemeLoader
    {
        public static Color DataGridViewRowBackgroundColor { get; internal set; }
        public static Color DataGridViewAlternatingRowBackgroundColor { get; internal set; }
        public static Color DataGridViewHeaderOutlineColor { get; internal set; }
        public static Color ButtonOutlineColor { get; internal set; }
        public static void SaveTheme(string filePath, Form form)
        {
            var xmlDoc = new XDocument(
                new XElement("Theme",
                    new XElement("Form",
                        new XElement("BackgroundColor", ColorTranslator.ToHtml(form.BackColor)),
                        new XElement("FontFamily", form.Font.FontFamily.Name),
                        new XElement("FontSize", form.Font.Size),
                        new XElement("ForeColor", ColorTranslator.ToHtml(form.ForeColor))
                    ),
                    new XElement("Button",
                        new XElement("BackgroundColor", GetControlBackColor<Button>(form)),
                        new XElement("ForeColor", GetControlForeColor<Button>(form)),
                        new XElement("TitleBarColor", ButtonOutlineColor != null ? ColorTranslator.ToHtml(ButtonOutlineColor) : "")
                    ),
                    new XElement("RichTextBox",
                        new XElement("BackgroundColor", GetControlBackColor<RichTextBox>(form)),
                        new XElement("ForeColor", GetControlForeColor<RichTextBox>(form))
                    ),
                    new XElement("Label",
                        new XElement("ForeColor", GetControlForeColor<Label>(form))
                    ),
                    new XElement("LinkLabel",
                        new XElement("LinkColor", GetLinkLabelProperty(form, "LinkColor")),
                        new XElement("ActiveLinkColor", GetLinkLabelProperty(form, "ActiveLinkColor")),
                        new XElement("VisitedLinkColor", GetLinkLabelProperty(form, "VisitedLinkColor"))
                    ),
                    new XElement("DataGridView",
                        new XElement("BackgroundColor", GetControlBackColor<DataGridView>(form)),
                        new XElement("ForeColor", GetControlForeColor<DataGridView>(form)),
                        new XElement("HeaderBackgroundColor", GetDataGridViewHeaderProperty(form, "BackColor")),
                        new XElement("HeaderForeColor", GetDataGridViewHeaderProperty(form, "ForeColor")),
                        new XElement("HeaderOutlineColor", DataGridViewHeaderOutlineColor != null ? ColorTranslator.ToHtml(DataGridViewHeaderOutlineColor) : ""),
                        new XElement("RowBackgroundColor", DataGridViewRowBackgroundColor != null ? ColorTranslator.ToHtml(DataGridViewRowBackgroundColor) : ""),
                        new XElement("AlternatingRowBackgroundColor", DataGridViewAlternatingRowBackgroundColor != null ? ColorTranslator.ToHtml(DataGridViewAlternatingRowBackgroundColor) : ""),
                        new XElement("SelectionBackColor", GetDataGridViewProperty(form, "SelectionBackColor")),
                        new XElement("SelectionForeColor", GetDataGridViewProperty(form, "SelectionForeColor"))
                    ),
                    new XElement("MenuStrip",
                        new XElement("BackColor", GetControlBackColor<MenuStrip>(form)),
                        new XElement("ForeColor", GetControlForeColor<MenuStrip>(form)),
                        new XElement("ItemBackColor", GetMenuStripItemProperty(form, "BackColor")),
                        new XElement("ItemForeColor", GetMenuStripItemProperty(form, "ForeColor")),
                        new XElement("SelectedBackColor", GetMenuStripItemProperty(form, "SelectedBackColor")),
                        new XElement("SelectedForeColor", GetMenuStripItemProperty(form, "SelectedForeColor"))
                    )
                )
            );

            xmlDoc.Save(filePath);
        }
        private static string GetControlBackColor<T>(Form form) where T : Control
        {
            var control = form.Controls.OfType<T>().FirstOrDefault();
            return control != null ? ColorTranslator.ToHtml(control.BackColor) : "#FFFFFF";
        }
        private static string GetControlForeColor<T>(Form form) where T : Control
        {
            var control = form.Controls.OfType<T>().FirstOrDefault();
            return control != null ? ColorTranslator.ToHtml(control.ForeColor) : "#000000";
        }
        private static string GetLinkLabelProperty(Form form, string propertyName)
        {
            var linkLabel = form.Controls.OfType<LinkLabel>().FirstOrDefault();
            if (linkLabel != null)
            {
                var property = typeof(LinkLabel).GetProperty(propertyName);
                if (property != null)
                {
                    var color = (Color)property.GetValue(linkLabel);
                    return ColorTranslator.ToHtml(color);
                }
            }
            return "#000000";
        }
        private static string GetDataGridViewHeaderProperty(Form form, string propertyName)
        {
            var dgv = form.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv != null)
            {
                var headerStyle = dgv.ColumnHeadersDefaultCellStyle;
                var property = typeof(DataGridViewCellStyle).GetProperty(propertyName);
                if (property != null)
                {
                    var color = (Color)property.GetValue(headerStyle);
                    return ColorTranslator.ToHtml(color);
                }
            }
            return "#000000";
        }
        private static string GetDataGridViewProperty(Form form, string propertyName)
        {
            var dgv = form.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv != null)
            {
                var property = typeof(DataGridViewCellStyle).GetProperty(propertyName);
                if (property != null)
                {
                    var color = (Color)property.GetValue(dgv.DefaultCellStyle);
                    return ColorTranslator.ToHtml(color);
                }
            }
            return "#000000";
        }
        private static string GetMenuStripItemProperty(Form form, string propertyName)
        {
            var menuStrip = form.Controls.OfType<MenuStrip>().FirstOrDefault();
            if (menuStrip != null)
            {
                var item = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault();
                if (item != null)
                {
                    var property = typeof(ToolStripMenuItem).GetProperty(propertyName);
                    if (property != null)
                    {
                        var color = (Color)property.GetValue(item);
                        return ColorTranslator.ToHtml(color);
                    }
                }
            }
            return "#000000";
        }
    }
    public class Theme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public Color TitleBarColor { get; set; }
        public Color WindowOutlineColor { get; set; }
        public Color ButtonColor { get; set; }
        public Color ButtonForeColor { get; set; }
        public Color ButtonOutlineColor { get; set; }
        public Color RichTextBoxBackgroundColor { get; set; }
        public Color RichTextBoxForeColor { get; set; }
        public Color LabelForeColor { get; set; }
        public Color LinkLabelLinkColor { get; set; }
        public Color LinkLabelActiveLinkColor { get; set; }
        public Color LinkLabelVisitedLinkColor { get; set; }
        public Color DataGridViewBackgroundColor { get; set; }
        public Color DataGridViewForeColor { get; set; }
        public Color DataGridViewHeaderBackgroundColor { get; set; }
        public Color DataGridViewHeaderForeColor { get; set; }
        public Color DataGridViewHeaderOutlineColor { get; set; }
        public Color DataGridViewRowBackgroundColor { get; set; }
        public Color DataGridViewAlternatingRowBackgroundColor { get; set; }
        public Color DataGridViewSelectionBackColor { get; set; }
        public Color DataGridViewSelectionForeColor { get; set; }
        public Color MenuStripBackColor { get; set; }
        public Color MenuStripForeColor { get; set; }
        public Color MenuStripItemBackColor { get; set; }
        public Color MenuStripItemForeColor { get; set; }
        public Color MenuStripSelectedBackColor { get; set; }
        public Color MenuStripSelectedForeColor { get; set; }
        public Theme(string name, Color backgroundColor, Color foregroundColor, Color buttonColor)
        {
            Name = name;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            ButtonColor = buttonColor;
        }
        public Theme() { }
    }
}