using System.Collections.Generic;
using System.Linq;
namespace Sudoku
{
    public class Sudoku
    {
        private int[][] sudokuData;
        public Sudoku(int[][] sudokuData)
        {
            this.sudokuData = sudokuData;
        }

        public bool IsValid()
        {
            int length = sudokuData[0].Length;

            for (int i = 1; i < sudokuData.Length; i++)
            {
                if (length != sudokuData[i].Length)
                {
                    return false;
                }
            }

            List<int> numbers = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                numbers.Add(i);
            }

            for (int i = 0; i < sudokuData.Length; i++)
            {
                if (!sudokuData[i].OrderBy(x => x).SequenceEqual(numbers))
                {
                    return false;
                }
            }

            List<int> tempNumbers = new List<int>();
            for (int i = 0; i < sudokuData.Length; i++)
            {
                tempNumbers = new List<int>(numbers);
                for (int j = 0; j < sudokuData.Length; j++)
                {
                    if (sudokuData[j][i] != tempNumbers[sudokuData[j][i] - 1])
                    {
                        return false;
                    }
                    else
                    {
                        tempNumbers[sudokuData[j][i] - 1] = 0;
                    }
                }
                
            }

            return true;
        }
    }
}
