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
    public partial class TeamplayConfigWizard : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel titleBar = new Panel();
        public TeamplayConfigWizard()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            ApplyCurrentTheme();
            this.Paint += MyForm_Paint;
            Properties.Settings.Default.Interlock_FF = true;//This is so if the program crashes, the user exits out early, or just doesn't finish this that the program won't try to make the config file with half completed data.
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
                Text = "Friendly Fire Setup",
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
        private void TeamplayConfigWizard_Load(object sender, EventArgs e)
        {
            TMCW_Vars.StartedWizard = true;//Is this needed? May be obsolete now.
            TMCW_Vars.Page = 1;
            buttonPrevious.Enabled = false;
            UpdateTextBox();
        }
        private void UpdateTextBox()
        {
            switch(TMCW_Vars.Page)
            {
                case 1:
                    richTextBox1.Text = "Does friendly fire hurt teammates?";
                    TMCW_Vars.CQValue1 = 2;
                    TMCW_Vars.CQValue2 = 4;
                    break;
                case 2:
                    richTextBox1.Text = "Does friendly fire from explosives hurt teammates?";
                    TMCW_Vars.CQValue1 = 8;
                    TMCW_Vars.CQValue2 = 16;
                    break;
                case 3:
                    richTextBox1.Text = "Does friendly fire damage armor?";
                    TMCW_Vars.CQValue1 = 128;
                    TMCW_Vars.CQValue2 = 256;
                    break;
                case 4:
                    richTextBox1.Text = "Does friendly fire from explosives damage armor?";
                    TMCW_Vars.CQValue1 = 512;
                    TMCW_Vars.CQValue2 = 1024;
                    break;
                case 5:
                    richTextBox1.Text = "If someone shoots a teammate do they take damage?";
                    TMCW_Vars.CQValue1 = 2048;
                    TMCW_Vars.CQValue2 = 4096;
                    break;
                case 6:
                    richTextBox1.Text = "If someone hits a teammate with explosive weapon fire do they take damage?";
                    TMCW_Vars.CQValue1 = 8192;
                    TMCW_Vars.CQValue2 = 16384;
                    break;
                case 7:
                    richTextBox1.Text = "If someone shoots a teammate does their armor take damage?";
                    TMCW_Vars.CQValue1 = 32768;
                    TMCW_Vars.CQValue2 = 65536;
                    break;
                case 8:
                    richTextBox1.Text = "If someone hits a teammate with explosive weapon fire does their armor take damage?";
                    TMCW_Vars.CQValue1 = 131072;
                    TMCW_Vars.CQValue2 = 262144;
                    break;
            }
        }
        private void FinishedWithWizard()
        {
            TMCW_Vars.QVTotal = TMCW_Vars.Q1V + TMCW_Vars.Q2V + TMCW_Vars.Q3V + TMCW_Vars.Q4V + TMCW_Vars.Q5V + TMCW_Vars.Q6V + TMCW_Vars.Q7V + TMCW_Vars.Q8V +1;
            Properties.Settings.Default.TeamplayTotal = TMCW_Vars.QVTotal;
            TMCW_Vars.Page = 1;
            Properties.Settings.Default.Interlock_FF = false;
            Properties.Settings.Default.UseFFTotal = true;
            this.Close();
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            //Separate updating values from the actions so things don't get confusing with too much going on
            switch (TMCW_Vars.Page)
            {
                case 1:
                    if(radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q1V = TMCW_Vars.CQValue1;
                    }
                    else//Using else in case the user doesn't select an answer. Don't feel like coding in message boxes or disabling the next button until an answer is chosen.
                    {
                        TMCW_Vars.Q1V = TMCW_Vars.CQValue2;
                    }
                    buttonPrevious.Enabled = true;
                    break;
                case 2:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q2V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q2V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 3:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q3V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q3V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 4:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q4V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q4V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 5:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q5V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q5V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 6:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q6V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q6V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 7:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q7V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q7V = TMCW_Vars.CQValue2;
                    }
                    buttonNext.Text = "Finish";
                    break;
                case 8:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q8V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q8V = TMCW_Vars.CQValue2;
                    }
                    FinishedWithWizard();
                    break;
            }
            radioButtonHalf.Checked = false;
            radioButtonNone.Checked = true;
            TMCW_Vars.Page++;
            UpdateTextBox();
        }
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            switch (TMCW_Vars.Page)
            {
                case 1:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q1V = TMCW_Vars.CQValue1;
                    }
                    else//Using else in case the user doesn't select an answer. Don't feel like coding in message boxes or disabling the next button until an answer is chosen.
                    {
                        TMCW_Vars.Q1V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 2:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q2V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q2V = TMCW_Vars.CQValue2;
                    }
                    buttonPrevious.Enabled = false;
                    break;
                case 3:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q3V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q3V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 4:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q4V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q4V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 5:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q5V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q5V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 6:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q6V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q6V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 7:
                    if (radioButtonHalf.Checked == true)
                    {
                        TMCW_Vars.Q7V = TMCW_Vars.CQValue1;
                    }
                    else
                    {
                        TMCW_Vars.Q7V = TMCW_Vars.CQValue2;
                    }
                    break;
                case 8:
                    buttonNext.Text = "Next";
                    break;
            } //Do the action
            radioButtonHalf.Checked = false;
            radioButtonNone.Checked = true;
            TMCW_Vars.Page--;
            UpdateTextBox();
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
    }
    public static class TMCW_Vars
    {
        public static bool StartedWizard = false;
        public static int Page = 1;
        public static int CQValue1 = 0;
        public static int CQValue2 = 0;
        public static int Q1V = 0;
        public static int Q2V = 0;
        public static int Q3V = 0;
        public static int Q4V = 0;
        public static int Q5V = 0;
        public static int Q6V = 0;
        public static int Q7V = 0;
        public static int Q8V = 0;
        public static int QVTotal = 0;
    }
}
