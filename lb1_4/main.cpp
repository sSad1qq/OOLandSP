#include <iostream>
using namespace std;

class Solution{
  private:
    double a, b;
  public:
    Solution(double a = 1, double b = 0){
      this->a = a;
      this->b = b;
    }

    double operator-(){
      if (a == 0){
        if (b == 0){
          throw "Бесконечно много решений";
        }
        throw "Решений нет";
      }
      return -b / a;
    }
};

int main(){
  Solution equation(-2, 6);

  try{
    double x = -equation;
    cout << "x = " << x << endl;
  }catch(const char *message){
    cout << message << endl;
  }

  return 0;
}
