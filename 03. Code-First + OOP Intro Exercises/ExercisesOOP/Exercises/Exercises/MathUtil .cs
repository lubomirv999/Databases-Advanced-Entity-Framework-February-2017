using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class MathUtil
    {
        //Task 6
        //Methods
        public static int Sum(int firstNum, int secondNum)
        {
            return firstNum + secondNum;
        }

        public static int Substract(int firstNum, int secondNum)
        {
            return firstNum - secondNum;
        }

        public static int Multiply(int firstNum, int secondNum)
        {
            return firstNum * secondNum;
        }

        public static int Devide(int firstNum, int secondNum)
        {
            return firstNum / secondNum;
        }

        public static int Percentage(int total, int percentage)
        {
            return Devide(Multiply(total, percentage), 100);
        }
    }
}
