#include <ctime>
#include <iostream>
#include <stdlib.h>
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

template <class T> class Father {
public:
  T A, B;
  int nRows, nCols;
  T **Arr;

  Father(int n = 5, int m = 7, T a = -20, T b = 20) {
    cout << "\n Вызван конструктор класса-родителя Father";
    nRows = n;
    nCols = m;
    A = (T)a;
    B = (T)b;

    Arr = new T *[nRows];
    for (int i = 0; i < nRows; ++i) {
      Arr[i] = new T [nCols];
    }

    srand(time(0));

    for (int i = 0; i < nRows; ++i) {
      for (int j = 0; j < nCols; ++j) {
        float t = (float)(rand() % 100) / 100;
        Arr[i][j] = (T)(B - A) * t + A;
      }
    }
  }
  ~Father() {
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

template <class T> class Son : public Father<T> {
public:
  Son<T>(int n = 5, int m = 7, T a = -20, T b = 20) : Father<T>(n, m, a, b) {}
  ~Son() {}

  T min_element_matrix() {
    double min_el = 1e16;

    for (int i = 0; i < this->nRows; ++i) {
      for (int j = 0; j < this->nCols; ++j) {
        if (is_prime(i + j)) {
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
  Son<int> matrix(3, 2);
  matrix.display_arr();

  cout << "\n" << matrix.min_element_matrix();
  cout << "\n\n";
  system("pause");
  return 1;
}
