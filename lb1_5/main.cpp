#include <cstdlib> // Для функции rand.
#include <ctime>
#include <iostream>
#include <fstream> // Для файловых потоков ofstream и ifstream.
using namespace std;

bool is_prime(int num) { // Проверка числа на простоту.
  if (num < 2) {
    return false;
  }
  // Проверяем делители от 2 до квадратного корня из числа.
  for (int i = 2; i < (int)(pow(num, 0.5)) + 1; ++i) {
    if (num % i == 0) {
      return false;
    }
  }
  return true;
}

class Father { // Класс-родитель.
public:
  float A, B; // Концы отрезка.
  int nRows, nCols; // Число строк и столбцов.
  float **Arr; // Динамический двумерный массив.

  // Конструктор с параметрами по умолчанию:
  Father(int n = 5, int m = 7, float a = -20, float b = 20) {
    cout << "\n Вызван конструктор класса-родителя Father";
    nRows = n;
    nCols = m;
    A = a;
    B = b;

    // Выделение памяти для массива указателей на строки:
    Arr = new float *[nRows];

    // Выделение памяти для каждой строки:
    for (int i = 0; i < nRows; ++i) {
      Arr[i] = new float[nCols];
    }
    // Инициализация двумерного массива случайными числами:
    for (int i = 0; i < nRows; ++i) {
      for (int k = 0; k < nCols; ++k) {
        float t = (float)(rand() % 100) / 100;
        Arr[i][k] = (b - a) * t + a;
      }
    }
  }
  ~Father() { // Деструктор класса-родителя.
    cout << "\n Деструктор класса-родителя Father";
    // Освобождаем память каждой строки:
    for (int i = 0; i < nRows; ++i) {
      delete[] Arr[i];
    }
    delete[] Arr; // Освобождаем массив указателей.
  }
  // Запись массива в файл; path - имя файла, true - успешная запись.
  bool write_file(const char *path){
    ofstream fout(path); // Создаём файл или перезаписываем существующий.
    if (!fout.is_open()){
      return false; // Не удалось открыть файл для записи.
    }

    // Первая строка файла содержит размеры массива.
    fout << nRows << " " << nCols << '\n';
    // Записываем элементы построчно, разделяя их пробелами.
    for (int i = 0; i < nRows; ++i){
      for (int j = 0; j < nCols; ++j){
        fout << Arr[i][j] << " ";
      }
      fout << '\n';
    }
    fout.close(); // Закрываем файл после записи.
    return !fout.fail(); // Проверяем ошибки записи и закрытия файла.
  }

  // Чтение в уже выделенный массив; true - успешное чтение.
  bool read_file(const char *path){
    ifstream fin(path); // Открываем существующий файл только для чтения.
    if (!fin.is_open()){
      return false; // Не удалось открыть файл для чтения.
    }

    int rows, cols;

    // Сначала читаем размеры, записанные в первой строке файла.
    if (!(fin >> rows >> cols)){
      return false;
    }
    // Размеры должны совпадать с размерами массива объекта.
    if (rows != nRows || cols != nCols){
      return false;
    }

    // Восстанавливаем элементы и проверяем каждое извлечение из файла.
    for (int i = 0; i < nRows; ++i){
      for (int j = 0; j < nCols; ++j){
        if (!(fin >> Arr[i][j])){
          return false; // Недостаточно чисел или неверный формат данных.
        }
      }
    }
    fin.close(); // При раннем return поток закроется автоматически.
    return true; // Все элементы массива успешно прочитаны.
  }
  void display_arr() { // Вывод элементов массива.
    cout << "\n Массив Arr \n";
    for (int i = 0; i < nRows; ++i) {
      for (int k = 0; k < nCols; ++k) {
        cout << Arr[i][k] << " ";
      }
      cout << endl;
    }
  }
};

class Son : public Father { // Класс-наследник.
public:
  // В конструкторе наследника явно вызываем конструктор родителя:
  Son(int n = 5, int m = 7, float a = -20, float b = 20) : Father(n, m, a, b) {
    cout << "\n Конструктор класса-наследника Son";
  }
  // Деструктор класса-наследника:
  ~Son() { cout << "\n Деструктор класса-наследника"; }
  // Минимум среди элементов с простой суммой индексов - вариант 6.
  double min_element_matrix() {
    double min_el = 1.0e16;

    for (int i = 0; i < nRows; ++i) {
      for (int j = 0; j < nCols; ++j) {
        // Индексы считаются с нуля; проверяем их сумму на простоту.
        if (is_prime(i + j)) {
          if (Arr[i][j] < min_el) {
            min_el = Arr[i][j]; // Запоминаем меньший элемент.
          }
        }
      }
    }
    return min_el;
  }
};

int main() {
  // Полное имя файла для сохранения и восстановления массива.
  const char *path = "/Users/daniildyatlov/develop/university/OOLandSP/lb1_5/matrix.txt";
  Son matrix(6, 4); // Создаём случайный массив из 6 строк и 4 столбцов.

  // Наследованный метод записывает массив; при ошибке завершаем программу.
  if (!matrix.write_file(path)){
      cout << "\n Ошибка записи массива в файл. \n";
      return 1;
  }

  Son matrix_from_file(6, 4); // Создаём второй объект с такими же размерами.
  // Заменяем его случайные элементы данными из файла.
  if (!matrix_from_file.read_file(path)){
      cout << "\n Ошибка чтения массива или несовпадение размеров. \n";
      return 1;
  }
  matrix_from_file.display_arr(); // Выводим восстановленный массив.

  // Решаем задачу варианта 6 для массива, прочитанного из файла.
  cout << "\n Наименьший элемент с простой суммой индексов = " 
      << matrix_from_file.min_element_matrix();


}
