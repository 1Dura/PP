using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace Cesair
{
    public partial class Form1 : Form
    {
        Cesair Me = new Cesair();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Input_Textbox.Text = Me.Codeс(Input_Textbox.Text, -(int)numericUpDown1.Value);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Input_Textbox.Text = Me.Codeс(Input_Textbox.Text, (int)numericUpDown1.Value);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }



        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, Input_Textbox.Text);
            }
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Input_Textbox.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
    class Clent
    {
        string le;

        public Clent(string m)
        {
            le = m;
        }

        public string Repl(string m, int key) //замена символа на другой со смещением
        {
            int pos = le.IndexOf(m);
            if (pos == -1) return ""; // если в лентах нет такого символа
            pos = (pos + key) % le.Length; //если смещение больше одного круга
            if (pos < 0) pos += le.Length;
            return le.Substring(pos, 1);
        }
    }
    class Cesair : System.Collections.Generic.List<Clent>
    {
        public Cesair()
        { 
            this.Add(new Clent("abcdefghijklmnopqrstuvwxyz"));
            this.Add(new Clent("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            this.Add(new Clent("абвгдеёжзийклмнопрстуфхцчшщъыьэюя"));
            this.Add(new Clent("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"));
            this.Add(new Clent("0123456789"));
        }

        public string Codeс(string m, int key) //код и декод в зависимости от знака ключа
        {
            string res = "", tmp = "";
            for (int i = 0; i < m.Length; i++)
            {
                foreach (Clent v in this)
                {
                    tmp = v.Repl(m.Substring(i, 1), key);
                    if (tmp != "") //нужная лента найдена
                    {
                        res += tmp;
                        break;
                    }
                }
                if (tmp == "") res += m.Substring(i, 1); //если нет ленты для символа - то он не меняется
            }
            return res;
        }
    }
}
