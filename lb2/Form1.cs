using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace lb2
{
    public partial class Form1 : Form
    {
        public object Line { get; private set; }

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyDown += textBox1_KeyDown;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }
        private void лБ22ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            groupBox4.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, EventArgs.Empty);
            }
        }

        private void лБ23ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            groupBox4.Visible = false;
            // Показываем элементы ЛБ 2.3 при выборе пункта меню.
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
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

        private void лБ24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            groupBox4.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // [Л] button3 
            // Инициализация таблицы 
            dataGridView1.RowCount = int.Parse(textBox4.Text);    //  число строк, 
            dataGridView1.ColumnCount = int.Parse(textBox5.Text); //  число  столбцов, 
                                                                  // скрываем:  
            dataGridView1.ColumnHeadersVisible = false;  //  заголовки столбцов, 
            dataGridView1.RowHeadersVisible = false;   //  заголовки строк, 
                                                       // устанавливаем ширину столбцов = 30: 
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                dataGridView1.Columns[i].Width = 30;
            // Отображаем элементы массива в ячейках таблицы: 
            Random random = new Random();
            int a = -20, b = 20, num;
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    num = random.Next(a, b + 1); // случайное целое число из [a, b], 
                    dataGridView1.Rows[i].Cells[j].Value = num.ToString();
                }
            button4.Enabled = true; // кнопка «решить» стала доступна.
            button5.Enabled = true;
            button6.Enabled = true;
        }

        private bool is_prime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }

        private bool is_product_of_two_primes(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0 && is_prime(i) && is_prime(n / i))
                    return true;
            }
            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            // Решение задачи варианта 0. 
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (is_product_of_two_primes(Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value)))
                        count++;

            label4.Visible = true;
            label4.Text = count.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {   // [Л] button5 “Запись”. 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = openFileDialog1.FileName; // полное имя файла, 
                System.IO.StreamWriter file = new System.IO.StreamWriter(name);
                file.WriteLine(dataGridView1.RowCount.ToString()); // запись строки,  
                file.WriteLine(dataGridView1.ColumnCount.ToString());
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        file.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                file.Close();
                // модальное окно7: 
                MessageBox.Show("Данные таблицы записаны в файл.");
            }
            else
                MessageBox.Show("Запись данных таблицы в файл не удалась.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // [Л] button6 “Чтение”. 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = openFileDialog1.FileName;
                System.IO.StreamReader file = new System.IO.StreamReader(name);
                dataGridView1.RowCount = int.Parse(file.ReadLine()); // чтение строки. 
                textBox4.Text = dataGridView1.RowCount.ToString();
                dataGridView1.ColumnCount = int.Parse(file.ReadLine());
                textBox5.Text = dataGridView1.ColumnCount.ToString();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        dataGridView1.Rows[i].Cells[j].Value = file.ReadLine();
                file.Close();
            }
            else MessageBox.Show("Данные прочитать не удалось.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void лБ26ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            chart1.Legends.Clear(); // удалили легенду из chart1.
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        double phi;

        private void button7_Click(object sender, EventArgs e)
        {
            // [Л] button7 "Нарисовать" 
            chart1.Visible = true;
            chart1.Series[0].Points.Clear(); // очистка диаграммы, 
            timer1.Stop();
            phi = 0.0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Угол по номеру точки: без накопления погрешности шага.
            phi = chart1.Series[0].Points.Count * Math.PI / 100;
            double r = 2 + Math.Cos(3 * phi) * Math.Sin(2 * phi),
                   // преобразование функции из полярной СС в прямоугольную: 
                   x = r * Math.Cos(phi),
                   y = r * Math.Sin(phi);
            chart1.Series[0].Points.AddXY(x, y);
            // Включаем обе граничные точки: 0 и 2π.
            if (chart1.Series[0].Points.Count == 201)
                timer1.Stop();
        }
    }
}
