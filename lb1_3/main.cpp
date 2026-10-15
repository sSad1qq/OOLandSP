#include <ctime> // для функции time
#include <iostream>
#include <stdlib.h> // для функций srand и rand
using namespace std;

// Проверка, является ли число простым.
bool is_prime(int num) {
  if (num < 2) {
    return false;
  }
  for (int i = 2; i < (int)(pow(num, 0.5)) + 1; ++i) {
    if (num % i == 0) {
      return false;
    }
  }
  return true;
}

// Шаблон родительского класса.
template <class T> class Father {
public:
  T A, B; // концы отрезка случайных значений
  int nRows, nCols; // число строк и столбцов
  T **Arr; // динамический двумерный массив

  // Конструктор с параметрами по умолчанию.
  Father(int n = 5, int m = 7, T a = -20, T b = 20) {
    cout << "\n Вызван конструктор класса-родителя Father";
    nRows = n;
    nCols = m;
    A = (T)a;
    B = (T)b;

    // Выделение памяти для указателей на строки и для каждой строки.
    Arr = new T *[nRows];
    for (int i = 0; i < nRows; ++i) {
      Arr[i] = new T [nCols];
    }

    // Установка начального значения генератора случайных чисел.
    srand(time(0));

    // Заполнение двумерного массива случайными значениями.
    for (int i = 0; i < nRows; ++i) {
      for (int j = 0; j < nCols; ++j) {
        float t = (float)(rand() % 100) / 100; // число из отрезка [0; 1)
        Arr[i][j] = (T)(B - A) * t + A;
      }
    }
  }
  // Деструктор освобождает память массива.
  ~Father() {
    cout << "\n Вызван деструктор класса-родителя Father";
    for (int i = 0; i < nRows; ++i) {
      delete[] Arr[i];
    }
    delete[] Arr;
  }

  // Вывод элементов массива на экран.
  void display_arr() {
    cout << "\n Массив Arr \n";
    for (int i = 0; i < nRows; ++i) {
      for (int k = 0; k < nCols; ++k) {
        cout << Arr[i][k] << " ";
      }
      cout << endl;
    }
  }
};

// Шаблон класса-наследника.
template <class T> class Son : public Father<T> {
public:
  // Конструктор наследника вызывает конструктор родителя.
  Son<T>(int n = 5, int m = 7, T a = -20, T b = 20) : Father<T>(n, m, a, b) {
    cout << "\n Вызван конструктор класса-наследника Son";}
  // Деструктор родителя вызывается автоматически после деструктора наследника.
  ~Son() {cout << "\nВызван деструктор класса-наследника Son";}

  // Поиск наименьшего элемента с простой суммой индексов.
  T min_element_matrix() {
    double min_el = 1e16;

    for (int i = 0; i < this->nRows; ++i) {
      for (int j = 0; j < this->nCols; ++j) {
        if (is_prime(i + j)) { // индексы строк и столбцов считаются с нуля
          if (this->Arr[i][j] < min_el) {
            min_el = this->Arr[i][j];
          }
        }
      }
    }
    return (T)min_el;
  }
};

int main() {
  // Проверка шаблона на массивах целых и вещественных чисел.
  Son<int> matrix_int(4, 2);
  Son<float> matrix_float(5, 3);
  
  cout << "\nРезультат выполнения для массива тип int: " << '\n';
  matrix_int.display_arr();
  
  cout << "\nНаименьший элемент двумерного массива, сумма индексов которого есть простое число = " << matrix_int.min_element_matrix();

  cout << '\n';

  cout << "\nРезультат выполнения для массива тип float: " << '\n';
  matrix_float.display_arr();
  cout << "\nНаименьший элемент двумерного массива, сумма индексов которого есть простое число = " << matrix_float.min_element_matrix();

  cout << "\n\n";
  system("pause");
  return 1;
}
