using System;
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
    public partial class ServConfigWindow : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        public ServConfigWindow()
        {
            InitializeComponent();
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
                Text = "Server Setup",
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
        private void ServConfigWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void ServConfigWindow_Load(object sender, EventArgs e)
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
