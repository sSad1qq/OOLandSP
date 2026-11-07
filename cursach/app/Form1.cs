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
            timer2.Stop();
            groupBox4.Visible = false;
            groupBox5.Visible = false;
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
            timer2.Stop();
            groupBox4.Visible = false;
            groupBox5.Visible = false;
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
            timer2.Stop();
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private readonly string workMatrixPath = System.IO.Path.GetFullPath("matrix-work.txt");
        private string currentMatrixPath;

        private void button3_Click(object sender, EventArgs e)
        {
            int rows, columns;
            if (!int.TryParse(textBox4.Text, out rows) ||
                !int.TryParse(textBox5.Text, out columns) || !MatrixFile.ValidSize(rows, columns))
            {
                MessageBox.Show("Введите положительные размеры: до 65536 столбцов и 100000 ячеек.");
                return;
            }

            int[,] matrix = new int[rows, columns];
            Random random = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = random.Next(-20, 21);

            try
            {
                MatrixFile.Write(workMatrixPath, matrix);
            }
            catch (Exception ex) when (ex is System.IO.IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Ошибка записи: " + ex.Message);
                return;
            }

            DisplayMatrix(matrix, workMatrixPath);
        }

        private void DisplayMatrix(int[,] matrix, string path)
        {
            int rows = matrix.GetLength(0), columns = matrix.GetLength(1);
            textBox4.Text = rows.ToString();
            textBox5.Text = columns.ToString();
            dataGridView1.ColumnCount = columns;
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.RowHeadersVisible = true;
            for (int j = 0; j < columns; j++)
            {
                dataGridView1.Columns[j].HeaderText = j.ToString();
                dataGridView1.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                for (int j = 0; j < columns; j++)
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
            }
            currentMatrixPath = path;
            label5.Text = path;
            label4.Visible = false;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            try
            {
                int[,] matrix = MatrixFile.Read(currentMatrixPath);
                label4.Text = MatrixFile.CountProductsOfTwoPrimes(matrix).ToString();
                label4.Visible = true;
            }
            catch (Exception ex) when (ex is System.IO.IOException ||
                                       ex is UnauthorizedAccessException || ex is FormatException)
            {
                MessageBox.Show("Ошибка чтения матрицы: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            string path = saveFileDialog1.FileName;
            int[,] matrix = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (!int.TryParse(Convert.ToString(dataGridView1.Rows[i].Cells[j].Value), out matrix[i, j]))
                    {
                        MessageBox.Show("В каждой ячейке должно быть целое число.");
                        return;
                    }

            try
            {
                MatrixFile.Write(path, matrix);
                currentMatrixPath = path;
                label5.Text = path;
                label4.Visible = false;
            }
            catch (Exception ex) when (ex is System.IO.IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Ошибка записи матрицы: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            string path = openFileDialog1.FileName;
            try
            {
                int[,] matrix = MatrixFile.Read(path);
                DisplayMatrix(matrix, path);
            }
            catch (Exception ex) when (ex is System.IO.IOException ||
                                       ex is UnauthorizedAccessException || ex is FormatException)
            {
                MessageBox.Show("Ошибка чтения матрицы: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void лБ26ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            groupBox5.Visible = false;
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

        

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int nC; // глобальная переменная для запоминания координаты частицы. 
        Random rnd; // для генерации случайных чисел. 
        private void лБ27ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Останавливаем таймеры и скрываем блоки других работ.
            timer1.Stop();
            timer2.Stop();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = true;
            // Изменяем свойства dataGridView2:
            dataGridView2.ColumnCount = 78;
            dataGridView2.RowCount = 1;
            // Стираем частицу, оставшуюся от прошлого запуска.
            for (int c = 0; c < dataGridView2.ColumnCount; c++)
                dataGridView2.Rows[0].Cells[c].Style.BackColor = Color.White;
            // Скрываем заголовки: 
            dataGridView2.ColumnHeadersVisible = false;    // столбцов, 
            dataGridView2.RowHeadersVisible = false;         //  строк, 
            for (int c = 0; c < dataGridView2.ColumnCount; c++)
                dataGridView2.Columns[c].Width = 7; // ширина столбца = 7, 
                                                    // начальное положение частицы: 
            nC = (dataGridView2.ColumnCount - 1) / 2; // координата точки, 
                                                // цвет ячейки: 
            dataGridView2.Rows[0].Cells[nC].Style.BackColor = Color.Red;
            dataGridView2.ClearSelection(); // убрали выделение, закрывающее частицу.
            rnd = new Random(12345); // инициализация генератора случайных чисел.
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // [Л] button8 "Старт" 
            timer2.Enabled = true; // включили timer2 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // [Л] button9 "Стоп" 
            timer2.Enabled = false; // выключили timer2.
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Прошла 0,1 с., моделируем смещение частицы: 
            bool moveLeft = rnd.Next(2) == 0;
            // в ячейке, где частица, меняем цвет на белый:
            dataGridView2.Rows[0].Cells[nC].Style.BackColor = Color.White;
            // За один тик выполняется ровно одна ветка, частица смещается на 1.
            if (moveLeft)
            {// выпало смещение влево:
                if (nC > 0)
                    --nC; // смещение влево,
                else
                    ++nC; // отражение частицы от левого экрана вправо.
            }
            else
            {// выпало смещение вправо:
                if (nC < dataGridView2.ColumnCount - 1)
                    ++nC; // смещение вправо,
                else
                    --nC; // отражение частицы от правого экрана влево.
            }
            // в ячейке, в которую сместилась частица, меняем цвет на красный:
            dataGridView2.Rows[0].Cells[nC].Style.BackColor = Color.Red;
        }
    }
}
