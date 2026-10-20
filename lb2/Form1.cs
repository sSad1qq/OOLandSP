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
            groupBox1.Visible = false;
            groupBox2.Visible = false;
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

        private void лБ23ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Показываем элементы ЛБ 2.3 при выборе пункта меню.
            groupBox2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            int N;
            // Проверяем, что введено целое число не меньше двух.
            if (!int.TryParse(textBox3.Text, out N) || N < 2)
            {
                MessageBox.Show("Введите целое число не меньше 2.");
                return;
            }

            // Очищаем результаты предыдущего вычисления.
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            Random random = new Random();
            // Границы случайных значений для варианта 6.
            int a = -100 - 6, b = 100 + 6;
            int[] Source = new int[N];
            // Для N элементов существует N - 1 пара соседей.
            int[] Result = new int[N-1];

            // Заполняем исходный массив и выводим его в первый список.
            for (int i = 0; i < N; ++i)
            {
                Source[i] = random.Next(a, b + 1);
                listBox1.Items.Add(Source[i].ToString());
            }
            // Вычисляем суммы соседних элементов и выводим во второй список.
            for (int i = 0; i < N - 1; ++i)
            {
                Result[i] = Source[i] + Source[i + 1];
                listBox2.Items.Add(Result[i].ToString());
            }
        }
    }
}
