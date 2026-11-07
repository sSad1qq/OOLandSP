using System;
using System.IO;

namespace lb2
{
    internal static class MatrixFile
    {
        internal static bool ValidSize(int rows, int columns)
        {
            // Ограничение для таблицы Windows Forms.
            return rows > 0 && columns > 0 && columns <= 65536 &&
                   (long)rows * columns <= 100000;
        }

        internal static void Write(string path, int[,] matrix)
        {
            string temp = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path)),
                                       Path.GetRandomFileName());
            try
            {
                using (var writer = new StreamWriter(temp))
                {
                    writer.WriteLine("{0} {1}", matrix.GetLength(0), matrix.GetLength(1));
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (j > 0) writer.Write(' ');
                            writer.Write(matrix[i, j]);
                        }
                        writer.WriteLine();
                    }
                }
                if (File.Exists(path)) File.Replace(temp, path, null);
                else File.Move(temp, path);
            }
            finally
            {
                if (File.Exists(temp)) File.Delete(temp);
            }
        }

        private static string[] ReadFields(StreamReader reader)
        {
            string line = reader.ReadLine();
            if (line == null) throw new FormatException("В файле недостаточно строк.");
            return line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static int[,] Read(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string[] size = ReadFields(reader);
                int rows, columns;
                if (size.Length != 2 || !int.TryParse(size[0], out rows) ||
                    !int.TryParse(size[1], out columns) || !ValidSize(rows, columns))
                    throw new FormatException("Неверные размеры матрицы в файле.");

                int[,] matrix = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    string[] fields = ReadFields(reader);
                    if (fields.Length != columns)
                        throw new FormatException("Неверное количество столбцов.");
                    for (int j = 0; j < columns; j++)
                        if (!int.TryParse(fields[j], out matrix[i, j]))
                            throw new FormatException("В матрице должно быть целое число.");
                }
                while (!reader.EndOfStream)
                    if (!string.IsNullOrWhiteSpace(reader.ReadLine()))
                        throw new FormatException("После матрицы обнаружены лишние данные.");
                return matrix;
            }
        }

        internal static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int d = 2; d <= n / d; d++)
                if (n % d == 0) return false;
            return true;
        }

        private static bool IsProductOfTwoPrimes(int value)
        {
            for (int divisor = 2; divisor <= value / divisor; divisor++)
            {
                // Первый делитель больше единицы всегда простой.
                if (value % divisor == 0)
                    return IsPrime(value / divisor);
            }
            return false;
        }

        internal static int CountProductsOfTwoPrimes(int[,] matrix)
        {
            int count = 0;
            foreach (int value in matrix)
                if (IsProductOfTwoPrimes(value)) count++;
            return count;
        }
    }
}
