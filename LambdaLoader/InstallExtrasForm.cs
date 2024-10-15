using System;
using System.IO;
using System.IO.Compression;
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
    public partial class InstallExtrasForm : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        private string droppedFilePath;
        public InstallExtrasForm()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            InitializeDragAndDrop();
            this.Paint += MyForm_Paint;
        }
        private void InitializeDragAndDrop()
        {
            this.panelDropArea.AllowDrop = true;
        }
        private void InstallExtrasForm_Load(object sender, EventArgs e)
        {

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
                Text = "Install Extras",
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
        private void panelDropArea_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the data being dragged is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Check if the file is a .zip file
                if (files.Length > 0 && Path.GetExtension(files[0]).Equals(".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void panelDropArea_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the file path and name
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string filePath = files[0];
                if (Path.GetExtension(filePath).Equals(".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    droppedFilePath = filePath;
                    //ExtractZipFile(droppedFilePath, @"C:\");
                    ExtractZipFile(droppedFilePath, Properties.Settings.Default.UserSelectedFolderPath + @"\valve\maps");
                }
            }
        }
        private void ExtractZipFile(string zipPath, string extractPath)
        {
            try
            {
                // Ensure the extract path exists
                if (!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                }

                // Extract the zip file to the specified directory
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting zip file: {ex.Message}");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Extras contain .jpg and/or .txt files that share the same name as map files.\nThese are used by the Launcher to display during map browsing.");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Create a collection of .jpg/and or .txt files that share the names of the maps they correspond to, select all of them, and compress to a zip file.\nYou do not want to select and compress the folder they are in, select the files themselves. Then you are done!");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("I do have an official Extras.zip file that has .jpg previews for over 400 maps and maybe if this program becomes popular other people will make their own.\nYou can find mine at blahblah.com under Frashure11.\n\nOr you can create your own!");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("The program specifically expects .jpg files only for images. Open a map in game and disable all hud elements. commands 'hud_draw 0', 'crosshair 0.0', and 'r_drawviewmodel 0'\n\nThen find a good spot in the map and take a screenshot. Sometimes use 'sv_gravity 20' to be able to jump to get a good view\n\nThen name the screenshot the exact same name as the map and prefferably set the image size to 1280x720.");
        }

        private void panelDropArea_Paint(object sender, PaintEventArgs e)
        {

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