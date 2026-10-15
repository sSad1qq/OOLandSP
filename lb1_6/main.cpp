#include <iostream>
#include <string> // Класс string для работы со строками
using namespace std;

class Solution {
public:
  string line; // Исходная строка
  Solution() { this->line = "This is  example string   for  testing"; }
  ~Solution() {};
  // Замена каждой последовательности пробелов одной запятой
  void edit_line() {
    string result; // Пустая строка для накопления результата
    // length() возвращает длину строки; индексы символов начинаются с нуля
    for (size_t i = 0; i < this->line.length(); ++i) {
      // Операция [] предоставляет доступ к символу по индексу
      if (this->line[i] != ' ') {
        result += this->line[i]; // += добавляет символ в конец строки
      } else {
        // Проверяем границу и пропускаем пробелы до последнего в группе
        while (i + 1 < this->line.length() && this->line[i + 1] == ' ') {
          ++i;
        }
        result += ','; // Одна запятая вместо всей группы пробелов
      }
    }
    this->line = result; // Присваивание заменяет исходную строку результатом
  }
};

int main() {
  Solution my_string = Solution();
  cout << "\nИсходная строка: " << my_string.line;
  my_string.edit_line();
  cout << "\nИзмененная строка: " << my_string.line;
  return 0;
}
