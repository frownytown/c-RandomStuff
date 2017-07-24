using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    /// <summary>
    /// This program takes the arguments #rows #columns data, row by row operator(I, D, T)
    /// It then generates a matrix based on the data, and performs some operation on it based on the operator given
    /// </summary>
    class MatrixManipulation
    {
        static void Main(string[] args)
        {
            // Checks to make sure the arguements given are correct or it throws an exception and exits
            try
            {
                Execute(args);
            }
            catch
            {
                Console.WriteLine("Something went wrong, Please make sure you use valid data!");
                Console.WriteLine("Format is <#rows> <#columns> <data, row by row> <operator(I, D, T)>");
            }
            
        }

        private static void Execute(string[] args)
        {
            // Breaks input arguments into different parts so that it can parse them successfully
            int parsedInt = 0;
            // stores operator as a character
            char operate = 'a';
            // stores matrix data values in a list for now
            List<int> parameters = new List<int>();
            foreach (string i in args)
            {
                if (Int32.TryParse(i, out parsedInt))
                {
                    parameters.Add(parsedInt);
                }
                else
                {
                    operate = i.ToCharArray()[0];
                }

            }
            // remove the matrix dimension arguments from list so that list is just data points
            int rows = parameters[0];
            int columns = parameters[1];
            parameters.RemoveRange(0, 2);
            int[,] matrixData = BuildMatrix(rows, columns, parameters);
            matrixData = PerformOperation(matrixData, operate);
            PrintingLogic(matrixData);
            // So that program stays open
            Console.ReadKey();
        }

        public static int[,] BuildMatrix(int rows, int columns, List<int> data)
        {
            // checks to make sure matrix is the right size for the data provided and then creates it based on data in list
            if (rows * columns == data.Count)
            {
                int[,] matrix = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {

                    for (int j = 0; j < columns; j++)
                    {
                        // iterate through the list adding the first value to the matrix and then removing it from the list
                        matrix[i, j] = data.First(); 
                        data.Remove(data.First());
                    }
                }
                return matrix;
            }
            else
            {
                throw new Exception("Inputs are the wrong size!");
            }
        }
        public static int[,] Transpose(int[,] matrix)
            // method to transpose the matrix 
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            int[,] transposedMatrix = new int[column, row];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }
            return transposedMatrix;
        }
        public static int[,] PerformOperation(int[,] matrix, char operate)
        {
            if (operate == 'I') // Increase all elements in matrix by 1
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {

                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] += 1;
                    }
                }
            }
            else if (operate == 'D') // Decrease all elements in matrix by 1
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {

                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] -= 1;
                    }
                }
            }
            else if (operate == 'T') // Transpose this matrix ROW1 = Column1, vice versa
            {
                matrix = Transpose(matrix); 
            }
            return matrix;
        }
        public static void PrintingLogic(int[,] matrix)
        {
            // prints out matrix so that I could verify it is correct
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }
    }
}
