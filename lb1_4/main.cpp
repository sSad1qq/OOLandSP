#include <iostream>
using namespace std;

// Класс для решения уравнения ax + b = 0.
class Solution{
  private:
    double a, b; // Коэффициенты уравнения.
  public:
    // Конструктор задаёт коэффициенты; по умолчанию a = 1, b = 0.
    Solution(double a = 1, double b = 0){
      this->a = a;
      this->b = b;
    }

    // Перегруженный унарный минус возвращает корень уравнения.
    double operator-(){
      if (a == 0){
        if (b == 0){
          // Равенство 0 = 0 выполняется при любом x.
          throw "Бесконечно много решений";
        }
        // При a = 0 и b != 0 равенство невозможно.
        throw "Решений нет";
      }
      return -b / a; // Единственный корень при a != 0.
    }
};

int main(){
  // Создаём объекты для уравнений с разными коэффициентами.
  Solution equation(-2, 6);
  Solution equation1(0, 4);
  Solution equation2(-3, 0);

  // В try решаем уравнение, в catch обрабатываем исключение.
  try{
    cout << "\nУравнение: -2x + 6 = 0";
    double x = -equation; // Вызов operator-() для объекта equation.
    cout << "\nx = " << x << endl;
  }catch(const char *message){
    // Перехватываем строковое сообщение, переданное через throw.
    cout << message << endl;
  }

  try{
    cout << "\nУравнение: 0x + 4 = 0\n";
    double x = -equation1;
    cout << "\nx = " << x << endl;
  }catch(const char *message){
    cout << message << endl;
  }
  
  try{
    cout << "\nУравнение: -3x + 0 = 0";
    double x = -equation2;
    cout << "\nx = " << x << endl;
  }catch(const char *message){
    cout << message << endl;
  }

  return 0;
}
