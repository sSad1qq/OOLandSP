#include <iostream>
#include <clocale>
using namespace std;

class tab_function {
private:
  int a, b, h;

public:
  // Вычисление функции Мёбиуса
  int f(int n) {
    if (n == 1)
      return 1;

    int count = 0;

    for (int p = 2; p * p <= n; ++p) {
      if (n % p == 0) {
        // Если число делится на квадрат простого делителя
        if (n % (p * p) == 0)
          return 0;

        ++count;

        while (n % p == 0) {
          n /= p;
        }
      }
    }

    // Учитываем оставшийся простой делитель
    if (n > 1)
      ++count;

    return (count % 2 == 0) ? 1 : -1;
  }

  // Инициализация границ и шага табулирования
  tab_function(int a = 1, int b = 10, int h = 1) {
    this->a = a;
    this->b = b;
    this->h = h;
  }

  // Табулирование функции на отрезке [a, b]
  void tabular(void) {
    for (int i = a; i <= b; i += h) {
      cout << "\n f( " << i << " ) = " << f(i);
    }
  }
};

int main() {
  setlocale(LC_CTYPE, "");

  // Экземпляр класса с параметрами по умолчанию
  tab_function tf;

  cout << "Тестирование f(n)" << endl;

  cout << "\nf(1) = " << tf.f(1) << endl;
  cout << "\nf(2) = " << tf.f(2) << endl;
  cout << "\nf(4) = " << tf.f(4) << endl;
  cout << "\nf(6) = " << tf.f(6) << endl;
  cout << "\nf(9) = " << tf.f(9) << endl;
  cout << "\nf(30) = " << tf.f(30) << endl;

  cout << "\ntf - экземпляр типа класс";
  tf.tabular();

  cout << "\n\nptF - экземпляр типа указатель на класс";
  // Экземпляр типа указатель на класс с явными параметрами
  tab_function *ptF;
  ptF = new tab_function(10, 34, 5);
  ptF->tabular();
  delete ptF;

  cout << endl;
  return 0;
}
