
using System;
using System.Numerics;

class Program
{
    
    static Complex Determinant(Complex a, Complex b, Complex c, Complex d)
    {
        return a * d - b * c;
    }

    
    static void Solve2x2(Complex a, Complex b, Complex c, Complex d, Complex e, Complex f)
    {
        
        Complex detMain = Determinant(a, b, c, d);

        if (detMain == Complex.Zero)
        {
            Console.WriteLine("Система не имеет единого решения (детерминант равен нулю).");
            return;
        }

        
        Complex detX = Determinant(e, b, f, d);
        Complex detY = Determinant(a, e, c, f);

        
        Complex x = detX / detMain;
        Complex y = detY / detMain;

        Console.WriteLine($"Решение: X = {x}, Y = {y}");
    }

    
    static Complex Determinant(Complex[,] matrix)
    {
        int n = matrix.GetLength(0);

        if (n == 1)
            return matrix[0, 0];

        Complex det = Complex.Zero;
        Complex[,] subMatrix = new Complex[n - 1, n - 1];

        for (int i = 0; i < n; i++)
        {
            
            int subRow = 0;
            for (int row = 1; row < n; row++)
            {
                int subCol = 0;
                for (int col = 0; col < n; col++)
                {
                    if (col != i)
                    {
                        subMatrix[subRow, subCol] = matrix[row, col];
                        subCol++;
                    }
                }
                subRow++;
            }

            
            Complex sign = (i % 2 == 0) ? Complex.One : -Complex.One;
            det += sign * matrix[0, i] * Determinant(subMatrix);
        }

        return det;
    }

    
    static void SolveNxN(Complex[,] matrix, Complex[] rightHandSide)
    {
        int n = matrix.GetLength(0);

        
        Complex detMain = Determinant(matrix);

        if (detMain == Complex.Zero)
        {
            Console.WriteLine("Система не имеет единого решения (детерминант равен нулю).");
            return;
        }

        
        Complex[] solution = new Complex[n];

        for (int i = 0; i < n; i++)
        {
            Complex[,] tempMatrix = (Complex[,])matrix.Clone();
            for (int j = 0; j < n; j++)
            {
                tempMatrix[j, i] = rightHandSide[j];
            }
            solution[i] = Determinant(tempMatrix) / detMain;
        }

        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"X{i + 1} = {solution[i]}");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введите размерность матрицы n:");
        int n = int.Parse(Console.ReadLine());

        Complex[,] matrix = new Complex[n, n];
        Complex[] rightHandSide = new Complex[n];

        Console.WriteLine("Введите элементы матрицы и правые части:");

        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введите {i + 1}-ю строку матрицы (реальная и мнимая части через пробел):");
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = ParseComplex();
            }
        }

        
        Console.WriteLine("Введите правые части системы (реальная и мнимая части через пробел):");
        for (int i = 0; i < n; i++)
        {
            rightHandSide[i] = ParseComplex();
        }

        SolveNxN(matrix, rightHandSide);
    }

    
    static Complex ParseComplex()
    {
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');
        double real = double.Parse(parts[0]);
        double imag = double.Parse(parts[1]);
        return new Complex(real, imag);
    }
}

