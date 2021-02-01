using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork5
{
    //Павел Егоров
    //1. Написать приложение, считающее в раздельных потоках:
    //    a.факториал числа N, которое вводится с клавиатуру;
    //    b.сумму целых чисел до N, которое также вводится с клавиатуры.
    class Program
    {
        static void Main(string[] args)
        {
            List<Thread> ThList = new List<Thread>();
            Thread th1 = new Thread(Factorial);
            ThList.Add(th1);
            Thread th2 = new Thread(Sum);
            ThList.Add(th2);

            Console.WriteLine("Введите целое число: ");
            int number = Convert.ToInt32(Console.ReadLine());

            foreach (var item in ThList)
            {
                item.Start(number);
            }
            Console.ReadLine();
        }

        static void Factorial(object o)
        {
            int n = (int)o;
            int sum = 1;

            for (int i = 1; i <= n; i++)
            {
                Thread.Sleep(1000);
                sum *= i;
                Console.WriteLine($"{i}; Факториал:{sum}; Thread:{Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"Факториал числа {n} равен: {sum}");
        }

        static void Sum(object o)
        {
            int n = (int)o;
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                Thread.Sleep(100);
                sum += i;
                Console.WriteLine($"{i}; Сумма:{sum}; Thread:{Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"Сумма чисел {n} равны: {sum}");
        }
    }
}
