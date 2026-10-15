#include <cstdlib> // Для функций rand и system.
#include <ctime>
#include <iostream>
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
  Son matrix(6, 4); // Объявление экземпляра класса-наследника.
  // Значения наследованных полей: nRows = 3, nCols = 2, A = -20, B = 20.
  matrix.display_arr(); // Вывод элементов массива Arr.
  // Вывод результата решения задачи варианта 6:
  cout << "\n  Наименьший элемент двумерного массива, сумма индексов которого есть простое число = "
       << matrix.min_element_matrix();
  cout << "\n\n";
  system("pause");

  return 1;
}
