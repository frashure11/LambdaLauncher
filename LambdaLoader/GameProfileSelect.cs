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
using System.Xml;
using System.Xml.Linq;

namespace LambdaLoader
{
    public partial class GameProfileSelect : Form
    {
        public GameProfileSelect()
        {
            InitializeComponent();
            Load += GameProfileSelect_Load; // Subscribe to the Load event
        }
        private void GameProfileSelect_Load(object sender, EventArgs e)
        {
            string datapath = Directory.GetCurrentDirectory() + @"\data";
            if (!Directory.Exists(datapath))
            {
                Directory.CreateDirectory(datapath);
                File.Create(datapath + @"\Half-Life (GoldSRC).llgp").Dispose();
                Directory.CreateDirectory(datapath + @"\Half-Life (GoldSRC)");
                Directory.CreateDirectory(datapath + @"\Themes");
                GenerateDefaultXml();
                GenerateFlatXml();
                GenerateGreenXml();
                GenerateMidnightXml();
                GenerateSlateXml();
                GenerateCobaltXml();
            }
            FindandDisplayGameProfiles();
        }
        private void FindandDisplayGameProfiles()
        {
            string directoryPath = Directory.GetCurrentDirectory() + @"\data";// Specify the directory to search for .llgp files
            string[] files = Directory.GetFiles(directoryPath, "*.llgp");// Get all .llgp files in the directory
            if (files.Length == 0)
            {
                File.Create("Half-Life (GoldSRC).llgp").Dispose();
            }
            int x = 10; // Starting x position for the first PictureBox
            int y = 10; // Starting y position for the first PictureBox
            int margin = 10; // Space between PictureBoxes and Labels
            //int pictureBoxHeight = 100; // Height of the PictureBox
            int labelHeight = 20; // Height of the Labeld
            int maxWidth = 0;
            int maxHeight = 0;
            int pictureBoxCounter = 0; // Counter to track the number of PictureBoxes in the current row
            foreach (string file in files)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(100, 100); 
                pictureBox.Location = new Point(x, y);
                pictureBox.Image = Properties.Resources.hlgsrc;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Click += (s, ev) => PictureBox_Click(file);
                Label label = new Label();
                label.Size = new Size(pictureBox.Width, labelHeight);
                label.Location = new Point(x, y + pictureBox.Height + margin);
                string fileName = Path.GetFileName(file);
                label.Text = fileName.Substring(0, fileName.Length - 5);
                label.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(pictureBox);
                Controls.Add(label);
                x += pictureBox.Width + margin;
                pictureBoxCounter++;
                if (pictureBoxCounter > 3)
                {
                    x = 10;
                    y += pictureBox.Height + label.Height + (2 * margin); // Adjust y position for the next row
                    pictureBoxCounter = 0;
                }
                // Update maxWidth and maxHeight
                maxWidth = Math.Max(maxWidth, x + pictureBox.Width);
                maxHeight = Math.Max(maxHeight, y + pictureBox.Height + label.Height + (2 * margin));
            }
            this.ClientSize = new Size(maxWidth + margin, maxHeight + margin);// Adjust the form size to fit all PictureBoxes and Labels
        }
        private void PictureBox_Click(string fileName)
        {
            Properties.Settings.Default.GameProfile = fileName;
            string directoryPath = Properties.Settings.Default.GameProfile;
            Properties.Settings.Default.GameProfileName = Path.GetFileName(directoryPath);
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
        public void GenerateSlateXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Slate.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Slate"),
                new XElement("Form",
                    new XElement("BackgroundColor", "#1e1e1e"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "#007BFF"),
                    new XElement("WindowOutlineColor", "#007BFF"),
                    new XElement("TitleBarColor", "#003153")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "#08030F"),
                    new XElement("ForeColor", "#007BFF"),
                    new XElement("OutlineColor", "#08030F")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "#1e1e1e"),
                    new XElement("ForeColor", "#007BFF")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "#007BFF")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#007BFF"),
                    new XElement("ActiveLinkColor", "#FF0000"),
                    new XElement("VisitedLinkColor", "#007BFF")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "#1e1e1e"),
                    new XElement("ForeColor", "#007BFF"),
                    new XElement("HeaderBackgroundColor", "#1e1e1e"),
                    new XElement("HeaderForeColor", "#007BFF"),
                    new XElement("HeaderOutlineColor", "#08030F"),
                    new XElement("RowBackgroundColor", "#323232"),
                    new XElement("AlternatingRowBackgroundColor", "#1e1e1e"),
                    new XElement("SelectionBackColor", "#5b5b5b"),
                    new XElement("SelectionForeColor", "#FFFFFF")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "#003153"),
                    new XElement("ForeColor", "#007BFF"),
                    new XElement("ItemBackColor", "#003153"),
                    new XElement("ItemForeColor", "#007BFF"),
                    new XElement("SelectedBackColor", "#003153"),
                    new XElement("SelectedForeColor", "#007BFF")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
        public void GenerateGreenXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Green.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Green"),
                new XElement("Form",
                    new XElement("BackgroundColor", "#09583B"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "#05A54B"),
                    new XElement("WindowOutlineColor", "#05A54B"),
                    new XElement("TitleBarColor", "#0c3F3A")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "#08030F"),
                    new XElement("ForeColor", "#05A54B"),
                    new XElement("OutlineColor", "#08030F")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "#09583B"),
                    new XElement("ForeColor", "#05A54B")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "#05A54B")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#05A54B"),
                    new XElement("ActiveLinkColor", "#05A54B"),
                    new XElement("VisitedLinkColor", "#05A54B")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "#09583B"),
                    new XElement("ForeColor", "#05A54B"),
                    new XElement("HeaderBackgroundColor", "#09583B"),
                    new XElement("HeaderForeColor", "#05A54B"),
                    new XElement("HeaderOutlineColor", "#08030F"),
                    new XElement("RowBackgroundColor", "#05F26C"),
                    new XElement("AlternatingRowBackgroundColor", "#098C50"),
                    new XElement("SelectionBackColor", "#F2B950"),
                    new XElement("SelectionForeColor", "#FFFFFF")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "#0c3F3A"),
                    new XElement("ForeColor", "#05A54B"),
                    new XElement("ItemBackColor", "#0c3F3A"),
                    new XElement("ItemForeColor", "#05A54B"),
                    new XElement("SelectedBackColor", "#0c3F3A"),
                    new XElement("SelectedForeColor", "#05A54B")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
        public void GenerateMidnightXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Midnight.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Midnight"),
                new XElement("Form",
                    new XElement("BackgroundColor", "#0B0F59"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "#7E49F2"),
                    new XElement("WindowOutlineColor", "#7E49F2"),
                    new XElement("TitleBarColor", "#050E40")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "#08030F"),
                    new XElement("ForeColor", "#7E49F2"),
                    new XElement("OutlineColor", "#08030F")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "#3B2559"),
                    new XElement("ForeColor", "#7E49F2")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "#7E49F2")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#7E49F2"),
                    new XElement("ActiveLinkColor", "#7E49F2"),
                    new XElement("VisitedLinkColor", "#7E49F2")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "#3B2559"),
                    new XElement("ForeColor", "#7E49F2"),
                    new XElement("HeaderBackgroundColor", "#3B2559"),
                    new XElement("HeaderForeColor", "#7E49F2"),
                    new XElement("HeaderOutlineColor", "#08030F"),
                    new XElement("RowBackgroundColor", "#00A4EF"),
                    new XElement("AlternatingRowBackgroundColor", "#4F328C"),
                    new XElement("SelectionBackColor", "#707019"),
                    new XElement("SelectionForeColor", "#FFFFFF")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "#050E40"),
                    new XElement("ForeColor", "#7E49F2"),
                    new XElement("ItemBackColor", "#050E40"),
                    new XElement("ItemForeColor", "#7E49F2"),
                    new XElement("SelectedBackColor", "#003153"),
                    new XElement("SelectedForeColor", "#7E49F2")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
        public void GenerateCobaltXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Cobalt.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Cobalt"),
                new XElement("Form",
                    new XElement("BackgroundColor", "#2953A6"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "#A68A56"),
                    new XElement("WindowOutlineColor", "#A68A56"),
                    new XElement("TitleBarColor", "#03258C")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "#08030F"),
                    new XElement("ForeColor", "#A68A56"),
                    new XElement("OutlineColor", "#A68A56")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "#03258C"),
                    new XElement("ForeColor", "#A68A56")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "#A68A56")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#A68A56"),
                    new XElement("ActiveLinkColor", "#A68A56"),
                    new XElement("VisitedLinkColor", "#A68A56")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "#03258C"),
                    new XElement("ForeColor", "#A68A56"),
                    new XElement("HeaderBackgroundColor", "#03258C"),
                    new XElement("HeaderForeColor", "#A68A56"),
                    new XElement("HeaderOutlineColor", "#08030F"),
                    new XElement("RowBackgroundColor", "#05F26C"),
                    new XElement("AlternatingRowBackgroundColor", "#2953A6"),
                    new XElement("SelectionBackColor", "#F2B950"),
                    new XElement("SelectionForeColor", "#FFFFFF")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "#03258C"),
                    new XElement("ForeColor", "#A68A56"),
                    new XElement("ItemBackColor", "#03258C"),
                    new XElement("ItemForeColor", "#A68A56"),
                    new XElement("SelectedBackColor", "#401122"),
                    new XElement("SelectedForeColor", "#A68A56")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
        public void GenerateDefaultXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Default.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Default"),
                new XElement("Form",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("WindowOutlineColor", "#0D0D0D"),
                    new XElement("TitleBarColor", "buttonface")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("OutlineColor", "")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "windowtext")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "buttontext")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#0000FF"),
                    new XElement("ActiveLinkColor", "Red"),
                    new XElement("VisitedLinkColor", "#800080")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("HeaderBackgroundColor", "buttonface"),
                    new XElement("HeaderForeColor", "windowtext"),
                    new XElement("HeaderOutlineColor", "#000000"),
                    new XElement("RowBackgroundColor", "#FFFFFF"),
                    new XElement("AlternatingRowBackgroundColor", "#F0F0F0"),
                    new XElement("SelectionBackColor", "highlight"),
                    new XElement("SelectionForeColor", "highlighttext")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("ItemBackColor", "buttonface"),
                    new XElement("ItemForeColor", "buttontext"),
                    new XElement("SelectedBackColor", "#000000"),
                    new XElement("SelectedForeColor", "#000000")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
        public void GenerateFlatXml()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string xmlfilePath = Path.Combine(currentDirectory, "data", "Themes", "Flate.xml");
            Directory.CreateDirectory(Path.GetDirectoryName(xmlfilePath));
            var theme = new XElement("Theme",
                new XAttribute("Name", "Flat"),
                new XElement("Form",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("FontFamily", "Microsoft Sans Serif"),
                    new XElement("FontSize", "8.25"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("WindowOutlineColor", "#0D0D0D"),
                    new XElement("TitleBarColor", "buttonface")
                ),
                new XElement("Button",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("OutlineColor", "")
                ),
                new XElement("RichTextBox",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "windowtext")
                ),
                new XElement("Label",
                    new XElement("ForeColor", "buttontext")
                ),
                new XElement("LinkLabel",
                    new XElement("LinkColor", "#0000FF"),
                    new XElement("ActiveLinkColor", "Red"),
                    new XElement("VisitedLinkColor", "#800080")
                ),
                new XElement("DataGridView",
                    new XElement("BackgroundColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("HeaderBackgroundColor", "buttonface"),
                    new XElement("HeaderForeColor", "windowtext"),
                    new XElement("HeaderOutlineColor", "#000000"),
                    new XElement("RowBackgroundColor", "#FFFFFF"),
                    new XElement("AlternatingRowBackgroundColor", "#F0F0F0"),
                    new XElement("SelectionBackColor", "highlight"),
                    new XElement("SelectionForeColor", "highlighttext")
                ),
                new XElement("MenuStrip",
                    new XElement("BackColor", "buttonface"),
                    new XElement("ForeColor", "buttontext"),
                    new XElement("ItemBackColor", "buttonface"),
                    new XElement("ItemForeColor", "buttontext"),
                    new XElement("SelectedBackColor", "#000000"),
                    new XElement("SelectedForeColor", "#000000")
                )
            );
            var declaration = new XDeclaration("1.0", "utf-8", null);
            var document = new XDocument(declaration, theme);
            document.Save(xmlfilePath);
        }
    }
    public partial class MainForm : Form
    {
        private string selectedFileName;

        public MainForm(string fileName)
        {
            selectedFileName = fileName;
            InitializeComponent();
        }
    }
}
