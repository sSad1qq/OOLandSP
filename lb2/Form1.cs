using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace lb2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void лБ22ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string q = textBox1.Text;

            if (q.Length == 0)
            {
                textBox2.Text = "Введите строчку";
                return;
            }

            for (int i = 0; i < q.Length; ++i)
            {
                if (abc.IndexOf(q[i]) == -1)
                {
                    textBox2.Text = "Допустимы только строчные русские буквы";
                    return;
                }
            }

            bool isOrdered = true;

            for (int i = 1; i < q.Length; ++i)
            {
                if (abc.IndexOf(q[i - 1]) > abc.IndexOf(q[i]))
                {
                    isOrdered = false;
                    break;
                }
            }

            if (isOrdered)
            {
                textBox2.Text = q;
            }
            else
            {
                char[] Letters = q.ToCharArray();

                Array.Sort(Letters, (a, b) => abc.IndexOf(a).CompareTo(abc.IndexOf(b)));

                textBox2.Text = new string(Letters);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
