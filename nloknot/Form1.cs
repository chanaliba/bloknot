using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nloknot
{
    public partial class Form1 : Form
    {
        string[] status_string = { "Страница: ", " из ", "Столбец: ", "Строка: " };
        FontStyle[] font_style = { FontStyle.Bold, FontStyle.Italic, FontStyle.Underline };
        int count_string = 10;

        public Form1()
        {
            InitializeComponent();

        }

        private void position()
        {
            int position = richTextBox1.SelectionStart;                       //позиция курсора
            int row = richTextBox1.GetLineFromCharIndex(position) + 1;     //строка 
                                                                           // int firstCharIndex = richTextBox1.GetFirstCharIndexFromLine(row);  // индекс первого символа в стр.
            int column = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
            int page = richTextBox1.Lines.Count() / count_string + Convert.ToInt32(richTextBox1.Lines.Count() % count_string > 0);

            toolStripStatusLabel1.Text = status_string[0] + (row / count_string + 1) + status_string[1] + page.ToString();
            toolStripStatusLabel2.Text = status_string[2] + column.ToString();
            toolStripStatusLabel3.Text = status_string[3] + row.ToString() + status_string[1] + richTextBox1.Lines.Count().ToString();

            if (richTextBox1.SelectionFont != null)
            {
                toolStripButton1.Checked = (richTextBox1.SelectionFont.Style & FontStyle.Bold) == FontStyle.Bold;
                toolStripButton2.Checked =
          (richTextBox1.SelectionFont.Style & FontStyle.Italic) == FontStyle.Italic;
                toolStripButton3.Checked =
          (richTextBox1.SelectionFont.Style & FontStyle.Underline) == FontStyle.Underline;
            }
            // toolStripComboBox1.SelectedIndex = toolStripComboBox1.FindString(richTextBox1.SelectionFont.FontFamily.ToString());
            // toolStripComboBox2.SelectedIndex = toolStripComboBox2.FindString(richTextBox1.SelectionFont.Size.ToString());
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(toolStripComboBox1.Text, richTextBox1.SelectionFont.Size);
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, Convert.ToInt32(toolStripComboBox2.Text));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ToolStripButton but = (ToolStripButton)sender;
            int tag = Convert.ToInt32(but.Tag);
            //but.Checked = !but.Checked;
            Font current = richTextBox1.SelectionFont;
            if (but.Checked)
                richTextBox1.SelectionFont = new Font(current.FontFamily, current.Size, font_style[tag] | current.Style);
            else
                richTextBox1.SelectionFont = new Font(current.FontFamily, current.Size, font_style[tag] ^ current.Style);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily item in FontFamily.Families)
                toolStripComboBox1.Items.Add(item.Name);
            toolStripComboBox1.SelectedIndex = toolStripComboBox1.FindString("Calibri");

            richTextBox1.Font = new Font("Calibri", 8);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size + 2);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, richTextBox1.SelectionFont.Size - 2);
        }
        
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            //this.BackColor = colorDialog1.Color;
            richTextBox1.SelectionColor = colorDialog1.Color;
        }
        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            position();
        }
        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            position();
        }
       

        private void страницаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ToolStripMenuItem item =  (ToolStripMenuItem)sender;
            int tag = Convert.ToInt32(item.Tag);
            item.Checked = !item.Checked;
            switch (tag)
            {
                case 0: toolStripStatusLabel1.Visible = item.Checked; break;
                case 1: toolStripStatusLabel2.Visible = item.Checked; break;
                case 2: toolStripStatusLabel3.Visible = item.Checked; break;
            }

        }
        

    }
}
