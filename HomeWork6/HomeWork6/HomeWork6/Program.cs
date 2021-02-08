using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6
{
    class Program
    {
        static void Main(string[] args)
        {
            void Run()
            {
                Console.WriteLine("Создание двух матриц 100 на 100 из случайных чисел от 0 до 10.");
                Matrix m1 = new Matrix(100, 100);
                m1.Randomize(0, 10);

                Matrix m2 = new Matrix(100, 100);
                m2.Randomize(0, 10);
                Console.WriteLine();

                Console.Write("Процесс основного потока: ");
                Stopwatch sw = Stopwatch.StartNew();
                Matrix result1 = m1 * m2;
                sw.Stop();
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms");                
                Console.WriteLine();

                Console.Write("Процесс параллельного потока: ");
                sw.Reset();
                sw.Start();
                Matrix result2 = m1.MulParallel(m2);
                sw.Stop();
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
                Console.WriteLine();

                Console.Write("Процесс с задачей: ");
                sw.Reset();
                sw.Start();
                Matrix result3 = m1.MulTask(m2);
                sw.Stop();
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
                Console.WriteLine();

                Console.Write("Процесс Async/Await: ");
                sw.Reset();
                sw.Start();
                Task<Matrix> task = Task.Factory.StartNew(() => m1.MulAsync(m2)).Result;
                Matrix result4 = task.Result;
                sw.Stop();
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
                Console.WriteLine();
            }

            Run();
            Console.ReadKey();
        }
    }
    public class Matrix
    {
        private readonly int[,] _data;
        public Matrix(int rows, int cols)
        {
            _data = new int[rows, cols];
        }
        public void Randomize(int min, int max)
        {
            Random random = new Random();
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    _data[row, col] = random.Next(min, max);
                }
            }
        }
        public int Rows => _data.GetUpperBound(0) + 1;
        public int Cols => _data.GetUpperBound(1) + 1;
        public static Matrix operator *(Matrix m1, Matrix m2) => m1.Mul(m2);
        public int this[int row, int col]
        {
            get => _data[row, col];
            set => _data[row, col] = value;
        }
        public Matrix Mul(Matrix other)
        {
            Matrix result = new Matrix(this.Rows, other.Cols);
            for (int row = 0; row < result.Rows; row++)
            {
                for (int col = 0; col < result.Cols; col++)
                {
                    result[row, col] = MulRowCol(this, row, other, col);
                }
            }
            return result;
        }
        private static int MulRowCol(Matrix m1, int row, Matrix m2, int col)
        {
            int result = 0;
            for (int i = 0; i < m1.Cols; i++)
            {
                result += m1[row, i] * m2[i, col];
            }
            return result;
        }
        public Matrix MulParallel(Matrix other)
        {
            Matrix result = new Matrix(this.Rows, other.Cols);
            for (int row = 0; row < result.Rows; row++)
            {
                for (int col = 0; col < result.Cols; col++)
                {
                    result[row, col] = MulParallelRowCol(this, row, other, col);
                }
            }
            return result;
        }
        private static int MulParallelRowCol(Matrix m1, int row, Matrix m2, int col)
        {
            object lo = new object();
            int result = 0;
            ParallelLoopResult plr = Parallel.For(0, m1.Cols, (i) =>
            {
                lock (lo)
                {
                    result += m1[row, i] * m2[i, col];
                }
            });
            while (!plr.IsCompleted) ;
            return result;
        }
        public Matrix MulTask(Matrix other)
        {
            Matrix result = new Matrix(this.Rows, other.Cols);
            for (int row = 0; row < result.Rows; row++)
            {
                for (int col = 0; col < result.Cols; col++)
                {
                    Task<int> task = Task.Factory.StartNew(() => MulRowCol(this, row, other, col));
                    result[row, col] = task.Result;
                }
            }
            return result;
        }
        public async Task<Matrix> MulAsync(Matrix other)
        {
            Matrix result = new Matrix(this.Rows, other.Cols);
            for (int row = 0; row < result.Rows; row++)
            {
                for (int col = 0; col < result.Cols; col++)
                {
                    result[row, col] = await Task.Run(() => MulRowCol(this, row, other, col));
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            if (obj is Matrix)
            {
                Matrix other = obj as Matrix;
                if ((this.Rows != other.Rows) || (this.Cols != other.Cols))
                    return false;

                for (int row = 0; row < Rows; row++)
                {
                    for (int col = 0; col < Cols; col++)
                    {
                        if (this[row, col] != other[row, col])
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 20;
        }
    }
}
