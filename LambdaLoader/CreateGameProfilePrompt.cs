using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LambdaLoader
{
    public partial class CreateGameProfilePrompt : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        public CreateGameProfilePrompt()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            ApplyCurrentTheme();
            this.Paint += MyForm_Paint;
        }
        private void CreateGameProfilePrompt_Load(object sender, EventArgs e)
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
                Text = "",
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
            Properties.Settings.Default.Interlock_FF = false;
            Properties.Settings.Default.UseFFTotal = false;
            this.Close();
        }
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void ApplyCurrentTheme()
        {
            if (TheThemeManager.CurrentTheme != null)
            {
                ApplyThemeToForm(this, TheThemeManager.CurrentTheme);
            }
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
        private void MyForm_Paint(object sender, PaintEventArgs e)
        {
            Color outlineColor = this.Tag is Color color ? color : Color.Yellow; // Default to yellow if not set
            using (Pen outlinePen = new Pen(outlineColor, 3)) // Set the color and width of the outline
            {
                e.Graphics.DrawRectangle(outlinePen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //File.Create(@"\data\test.llgp").Dispose();
            SaveFileWithTextboxName();

        }
        private void SaveFileWithTextboxName()
        {
            try
            {
                // Get the current working directory
                string currentDirectory = Directory.GetCurrentDirectory();

                // Create the data directory if it doesn't exist
                string dataDirectory = Path.Combine(currentDirectory, "data");
                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }
                // Get the text from the textbox
                string fileName = textBox1.Text.Trim();
                // Ensure the filename is valid and not empty
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("The textbox is empty. Please enter a valid filename.");
                    return;
                }
                // Define the full file path with .llgp extension
                string filePath = Path.Combine(dataDirectory, fileName + ".llgp");

                // Create an empty file
                using (FileStream fs = File.Create(filePath))
                {
                    // The file is created and will be closed automatically at the end of the using block
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
