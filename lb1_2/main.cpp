#include <cstdlib>
#include <ctime>
#include <iostream>
using namespace std;

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

class Father {
public:
  float A, B;
  int nRows, nCols;
  float **Arr;

  Father(int n = 5, int m = 7, float a = -20, float b = 20) {
    cout << "\n Вызван конструктор класса-родителя Father";
    nRows = n;
    nCols = m;
    A = a;
    B = b;

    Arr = new float *[nRows];

    for (int i = 0; i < nRows; ++i) {
      Arr[i] = new float[nCols];
    }
    for (int i = 0; i < nRows; ++i) {
      for (int k = 0; k < nCols; ++k) {
        float t = (float)(rand() % 100) / 100;
        Arr[i][k] = (b - a) * t + a;
      }
    }
  }
  ~Father() {
    cout << "\n Деструктор класса-родителя Father";
    for (int i = 0; i < nRows; ++i) {
      delete[] Arr[i];
    }
    delete[] Arr;
  }
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

class Son : public Father {
public:
  Son(int n = 5, int m = 7, float a = -20, float b = 20) : Father(n, m, a, b) {
    cout << "\n Конструктор класса-наследника Son";
  }
  ~Son() { cout << "\n Деструктор класса-наследника"; }
  double min_element_matrix() {
    double min_el = 1.0e16;

    for (int i = 0; i < nRows; ++i) {
      for (int j = 0; j < nCols; ++j) {
        if (is_prime(i + j)) {
          if (Arr[i][j] < min_el) {
            min_el = Arr[i][j];
          }
        }
      }
    }
    return min_el;
  }
};

int main() {
  Son matrix(3, 2);
  matrix.display_arr();
  cout << "\n  Наименьший элемент двумерного массива, сумма индексов которого есть простое число = "
       << matrix.min_element_matrix();
  cout << "\n\n";
  system("pause");

  return 1;
}
