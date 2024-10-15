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

//Please checkout the devs_README file
namespace LambdaLoader
{
    public partial class MainForm : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        private List<Theme> themes = new List<Theme>();
        private HashSet<int> favoriteRows = new HashSet<int>();
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            LoadThemes();
            LoadSavedTheme();
            this.Paint += MyForm_Paint; // Subscribe to the Paint event
            if (Debugger.IsAttached)
            {
                //Properties.Settings.Default.Reset();
            }
        }
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
            Properties.Settings.Default.GameProfileNoExt = Properties.Settings.Default.GameProfile.Substring(0, Properties.Settings.Default.GameProfile.Length - 5) + @"\";
            //if (Properties.Settings.Default.GameProfile != "Half-Life (GoldSRC)")
            //{
            //    RemoveAdvancedServerConfigToolStripMenuItem();
            //}
            if (Properties.Settings.Default.UserSelectedFolderPath.Length < 5)//On startup check if a user has already selected the HL directory previously. It's a decent assumption their directory path will be less than 5 characters.
            {
                ChooseGameDirectory();
            }
            else
            {
                RefreshMapList();
            }
            BackupConfigFiles();
            if (Properties.Settings.Default.AutoImportRan == false)
            {
                AutoExtractExtras();
            }

            LoadSavedTheme();
        }
        //                                                                                              REGION: Title Bar
        #region CustomTitleBar
        private void InitializeCustomTitleBar()
        {
            titleBar.Name = "titleBar";
            titleBar.BackColor = Color.DarkBlue; // Default color
            titleBar.Dock = DockStyle.Top;
            titleBar.Height = 24;

            titleBar.MouseDown += TitleBar_MouseDown;
            titleBar.MouseMove += TitleBar_MouseMove;
            titleBar.MouseUp += TitleBar_MouseUp;

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
                Text = "Lambda Loader Beta 1",
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
        private void RemoveAdvancedServerConfigToolStripMenuItem()
        {
            // Check if the item exists in the ToolStrip
            if (menuStrip1.Items.Contains(advancedServerConfigToolStripMenuItem))
            {
                // Remove the item from the ToolStrip
                menuStrip1.Items.Remove(advancedServerConfigToolStripMenuItem);
            }
            else
            {
                // Optionally, you can add some code to handle the case where the item is not found
                MessageBox.Show("Item not found in the ToolStrip.");
            }
        }//Should be safe to delete this now, but I don't have the time to verify it won't cause issues.
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
                if(Properties.Settings.Default.UseFFTotal == true)
                {
                    Properties.Settings.Default.Teamplay = Properties.Settings.Default.TeamplayTotal;
                }
                if (Properties.Settings.Default.Teamplay >= 1)
                {
                    configContent.AppendLine($"mp_teamplay {(Properties.Settings.Default.Teamplay)}");
                    if (Properties.Settings.Default.Teamplay > 1)
                    {
                        configContent.AppendLine("mp_friendlyfire 1");
                    }
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
                configContent.AppendLine("cl_dlmax 1024");
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.DefaultCellStyle.BackColor == Color.Yellow)
                    {
                        // Reset to the theme color based on whether it's an alternating row or not
                        if (row.Index % 2 == 0)
                        {
                            row.DefaultCellStyle.BackColor = TheThemeManager.CurrentTheme.DataGridViewRowBackgroundColor;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = TheThemeManager.CurrentTheme.DataGridViewAlternatingRowBackgroundColor;
                        }
                        favoriteRows.Remove(row.Index); // Remove from favorites
                    }
                    else
                    {
                        // Change to yellow
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        favoriteRows.Add(row.Index); // Add to favorites
                    }
                }
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
                            favoriteRows.Add(row.Index); // Add to the favorite rows set
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
            ApplyThemeByName(Properties.Settings.Default.SelectedTheme);
        }
        private void ApplyThemeByName(string themeName)
        {
            Properties.Settings.Default.SelectedTheme = themeName;
            TheThemeManager.CurrentTheme = themes.FirstOrDefault(t => t.Name == themeName);
            if (TheThemeManager.CurrentTheme != null)
            {
                ApplyThemeToForm(this, TheThemeManager.CurrentTheme);
            }
        }
        public void ApplyThemeToAllOpenWindows()
        {
            ApplyThemeToAllForms(Properties.Settings.Default.SelectedTheme);
        }
        private void ThemeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem != null)
            {
                ApplyThemeByName(clickedItem.Text);
            }
        }
        private void ApplyThemeToAllForms(string themeName)
        {
            Theme selectedTheme = themes.FirstOrDefault(t => t.Name == themeName);
            if (selectedTheme != null)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm != this)
                    {
                        ApplyThemeToForm(openForm, selectedTheme);
                    }
                }
            }
        }
        private void LoadThemes()
        {
            string themesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data", "Themes");
            if (!Directory.Exists(themesDirectory))
            {
                MessageBox.Show("Themes directory not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            themesToolStripMenuItem.DropDownItems.Clear();
            themes.Clear();

            foreach (string themeFile in Directory.GetFiles(themesDirectory, "*.xml"))
            {
                try
                {
                    Theme theme = ThemeLoader.LoadThemeFromFile(themeFile);
                    if (theme != null)
                    {
                        themes.Add(theme);
                        AddThemeToMenu(theme.Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading theme from file {themeFile}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AddThemeToMenu(string themeName)
        {
            ToolStripMenuItem themeMenuItem = new ToolStripMenuItem(themeName);
            themeMenuItem.Click += ThemeMenuItem_Click;
            themesToolStripMenuItem.DropDownItems.Add(themeMenuItem);
        }
        private void ApplyThemeToForm(Form form, Theme theme)
        {
            form.BackColor = theme.BackgroundColor;
            form.ForeColor = theme.ForegroundColor;

            foreach (Control control in form.Controls)
            {
                ApplyControlTheme(control, theme);
            }

            if (form.MainMenuStrip != null)
            {
                ApplyMenuStripTheme(form.MainMenuStrip, theme);
            }

            form.Tag = theme.WindowOutlineColor; // Set the outline color to the form's Tag property
            form.Invalidate();

            ApplyTitleBarTheme(form, theme);

            // Apply theme to DataGridView rows
            ApplyThemeToDataGridViewRows(dataGridView1, theme);
        }
        private void ApplyThemeToDataGridViewRows(DataGridView dataGridView, Theme theme)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (favoriteRows.Contains(row.Index))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (row.Index % 2 == 0)
                    {
                        row.DefaultCellStyle.BackColor = theme.DataGridViewRowBackgroundColor;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = theme.DataGridViewAlternatingRowBackgroundColor;
                    }
                }
            }
        }
        private void ApplyControlTheme(Control control, Theme theme)
        {
            switch (control)
            {
                case Button button:
                    ApplyButtonTheme(button, theme);
                    break;
                case RichTextBox richTextBox:
                    ApplyRichTextBoxTheme(richTextBox, theme);
                    break;
                case LinkLabel linkLabel:
                    ApplyLinkLabelTheme(linkLabel, theme);
                    break;
                case Label label:
                    ApplyLabelTheme(label, theme);
                    break;
                case DataGridView dataGridView:
                    ApplyDataGridViewTheme(dataGridView, theme);
                    break;
                case MenuStrip menuStrip:
                    ApplyMenuStripTheme(menuStrip, theme);
                    break;
            }

            foreach (Control childControl in control.Controls)
            {
                ApplyControlTheme(childControl, theme);
            }
        }
        private void ApplyButtonTheme(Button button, Theme theme)
        {
            button.FlatStyle = Properties.Settings.Default.SelectedTheme == "Default" ? FlatStyle.Standard : FlatStyle.Flat;
            button.BackColor = theme.ButtonColor;
            button.ForeColor = theme.ButtonForeColor;
            button.FlatAppearance.BorderColor = theme.ButtonOutlineColor;
            button.FlatAppearance.BorderSize = 1;
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

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = theme.DataGridViewHeaderBackgroundColor;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = theme.DataGridViewHeaderForeColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = theme.DataGridViewHeaderOutlineColor;
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.RowsDefaultCellStyle.BackColor = theme.DataGridViewRowBackgroundColor;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = theme.DataGridViewAlternatingRowBackgroundColor;
            dataGridView.DefaultCellStyle.SelectionBackColor = theme.DataGridViewSelectionBackColor;
            dataGridView.DefaultCellStyle.SelectionForeColor = theme.DataGridViewSelectionForeColor;
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
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    ApplyMenuStripItemTheme(subMenuItem, theme);
                }
            }
        }
        private void ApplyTitleBarTheme(Form form, Theme theme)
        {
            Panel titleBar = form.Controls.OfType<Panel>().FirstOrDefault(c => c.Name == "titleBar");
            if (titleBar != null)
            {
                titleBar.BackColor = theme.TitleBarColor;
            }
        }
        private void InitializeCustomTitleBarinOtherForms(Form form)
        {
            Panel titleBar = new Panel
            {
                Name = "titleBar",
                BackColor = Color.DarkBlue, // Default color
                Dock = DockStyle.Top,
                Height = 24
            };

            titleBar.MouseDown += TitleBar_MouseDown;
            titleBar.MouseMove += TitleBar_MouseMove;
            titleBar.MouseUp += TitleBar_MouseUp;

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

            form.Controls.Add(titleBar);
            form.Padding = new Padding(1);
            form.BackColor = Color.Black;
        }
        //                                                                                                       END REGION
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
            extraWindow.Show(); // Use ShowDialog() if you want it to be a modal dialog
            ApplyThemeToAllOpenWindows();
        }//                                          Menustrip       OPEN Install Extras Window
        private void serverConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServConfigWindow configWindow = new ServConfigWindow();
            configWindow.Show();
            ApplyThemeToAllOpenWindows();
        }//                                     Menustrip       Configure Server Settings
        private void mapFolderControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature hasn't been implemented yet, check back in future updates", "Under Construction", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//                                       Menustrip       Map Folder Controls Window
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twhl.info/user/view/8914");
        }//                                         Menustrip       Show Check for Updates 
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm AboutWindow = new AboutForm();
            AboutWindow.Show();
            ApplyThemeToAllOpenWindows();
        }//                                                   Menustrip       Show About Window
        private void advancedServerConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServConfigWindow configWindow = new ServConfigWindow();
            configWindow.Show();
            ApplyThemeToAllOpenWindows();
        }//                                    Menustrip       Show Server Config Window
        private void advancedServerConfigToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ServerAdvancedConfig advancedconfigWindow = new ServerAdvancedConfig();
            advancedconfigWindow.Show();
            ApplyThemeToAllOpenWindows();
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
    public static class TheThemeManager
    {
        public static Theme CurrentTheme { get; set; }
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
    }
    public static class ThemeLoader
    {
        public static Theme LoadThemeFromFile(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode themeNode = xmlDoc.SelectSingleNode("/Theme");
            if (themeNode == null || themeNode.Attributes["Name"] == null)
            {
                throw new Exception($"The theme file {filePath} does not contain a valid 'Name' attribute or is not structured correctly.");
            }

            Theme theme = new Theme { Name = themeNode.Attributes["Name"].Value };

            XmlNode formNode = themeNode.SelectSingleNode("Form");
            if (formNode != null)
            {
                theme.BackgroundColor = GetColorFromNode(formNode["BackgroundColor"]);
                theme.ForegroundColor = GetColorFromNode(formNode["ForeColor"]);
                theme.TitleBarColor = GetColorFromNode(formNode["TitleBarColor"]);
                theme.WindowOutlineColor = GetColorFromNode(formNode["WindowOutlineColor"]);
            }

            theme.ButtonColor = GetColorFromNode(themeNode.SelectSingleNode("Button/BackgroundColor"));
            theme.ButtonForeColor = GetColorFromNode(themeNode.SelectSingleNode("Button/ForeColor"));
            theme.ButtonOutlineColor = GetColorFromNode(themeNode.SelectSingleNode("Button/OutlineColor"));

            theme.RichTextBoxBackgroundColor = GetColorFromNode(themeNode.SelectSingleNode("RichTextBox/BackgroundColor"));
            theme.RichTextBoxForeColor = GetColorFromNode(themeNode.SelectSingleNode("RichTextBox/ForeColor"));

            theme.LabelForeColor = GetColorFromNode(themeNode.SelectSingleNode("Label/ForeColor"));

            theme.LinkLabelLinkColor = GetColorFromNode(themeNode.SelectSingleNode("LinkLabel/LinkColor"));
            theme.LinkLabelActiveLinkColor = GetColorFromNode(themeNode.SelectSingleNode("LinkLabel/ActiveLinkColor"));
            theme.LinkLabelVisitedLinkColor = GetColorFromNode(themeNode.SelectSingleNode("LinkLabel/VisitedLinkColor"));

            theme.DataGridViewBackgroundColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/BackgroundColor"));
            theme.DataGridViewForeColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/ForeColor"));
            theme.DataGridViewHeaderBackgroundColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/HeaderBackgroundColor"));
            theme.DataGridViewHeaderForeColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/HeaderForeColor"));
            theme.DataGridViewHeaderOutlineColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/HeaderOutlineColor"));
            theme.DataGridViewRowBackgroundColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/RowBackgroundColor"));
            theme.DataGridViewAlternatingRowBackgroundColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/AlternatingRowBackgroundColor"));
            theme.DataGridViewSelectionBackColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/SelectionBackColor"));
            theme.DataGridViewSelectionForeColor = GetColorFromNode(themeNode.SelectSingleNode("DataGridView/SelectionForeColor"));

            theme.MenuStripBackColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/BackColor"));
            theme.MenuStripForeColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/ForeColor"));
            theme.MenuStripItemBackColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/ItemBackColor"));
            theme.MenuStripItemForeColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/ItemForeColor"));
            theme.MenuStripSelectedBackColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/SelectedBackColor"));
            theme.MenuStripSelectedForeColor = GetColorFromNode(themeNode.SelectSingleNode("MenuStrip/SelectedForeColor"));

            return theme;
        }
        private static Color GetColorFromNode(XmlNode node)
        {
            if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
            {
                return Color.Empty;
            }
            string colorText = node.InnerText.Trim();
            try
            {
                // Check if the color is a system color
                if (colorText.StartsWith("sys:", StringComparison.OrdinalIgnoreCase))
                {
                    string systemColorName = colorText.Substring(4);
                    return Color.FromName(systemColorName);
                }

                // Otherwise, try to parse as an HTML color
                return ColorTranslator.FromHtml(colorText);
            }
            catch (System.FormatException)
            {
                Console.WriteLine($"Unable to parse '{colorText}' as an HTML color.");
                return Color.Empty; // Return default or empty color
            }
        }
    }
}