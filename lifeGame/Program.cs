using System;
using System.IO;

namespace lifeGame
{
	class Program
	{
		static void Main(string[] args)
		{
			string txt = File.ReadAllText("/Users/anastasia/Projects/lifeGame/input.txt");
			string[] lines = txt.Split('\n');
			string[] first_line = lines[0].Split(' ');
			int n = Convert.ToInt32(first_line[0]); // число строк
			int m = Convert.ToInt32(first_line[1]); // число столбцов 
			int k = Convert.ToInt32(first_line[2]); // число поколений
			bool[,] cells = new bool[n + 2, m + 2];
			bool[,] temp_cells = new bool[n + 2, m + 2];

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (lines[i][j - 1] == '*')
                    {
                        cells[i, j] = true;
                    }
                    else
                    {
                        cells[i, j] = false;
                    }
                }
            }

            int s = 0;
            while (s < k)
            {
                for (int j = 1; j <= m; j++)
                {
                    cells[0, j] = cells[n, j];
                    cells[n + 1, j] = cells[1, j];
                }
                for (int i = 1; i <= n; i++)
                {
                    cells[i, 0] = cells[i, m];
                    cells[i, m + 1] = cells[i, 1];
                }
                cells[0, 0] = cells[n, m];
                cells[0, m + 1] = cells[n, 1];
                cells[n + 1, 0] = cells[1, m];
                cells[n + 1, m + 1] = cells[1, 1];

                for (int i = 0; i < n + 2; i++)
                {
                    for (int j = 0; j < m + 2; j++)
                    {
                        temp_cells[i, j] = cells[i, j];
                    }
                }

                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= m; j++)
                    {
                        int num = 0;
                        if (cells[i - 1, j] == true) num++;
                        if (cells[i + 1, j] == true) num++;
                        if (cells[i, j + 1] == true) num++;
                        if (cells[i, j - 1] == true) num++;
                        if (cells[i - 1, j - 1] == true) num++;
                        if (cells[i + 1, j - 1] == true) num++;
                        if (cells[i - 1, j + 1] == true) num++;
                        if (cells[i + 1, j + 1] == true) num++;
                        if (num == 3 && cells[i, j] == false) temp_cells[i, j] = true;
                        else if (cells[i, j] == true && (num < 2 || num > 3)) temp_cells[i, j] = false;
                    }
                }
                s++;
                for (int i = 0; i <= n + 1; i++)
                {
                    for (int j = 0; j <= m + 1; j++)
                    {
                        cells[i, j] = temp_cells[i, j];
                    }
                }
            }

            string answer = "";
            System.IO.StreamWriter stream = new System.IO.StreamWriter("/Users/anastasia/Projects/lifeGame/output.txt");
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (cells[i, j] == true) answer += '*';
                    else answer += ".";
                }
                answer += "\n";
            }
            stream.Write(answer);
            stream.Close();
        }
    }
}