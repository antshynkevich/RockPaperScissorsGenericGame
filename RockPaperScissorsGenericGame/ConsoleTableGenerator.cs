using ConsoleTables;

namespace RockPaperScissorsGenericGame;

internal static class ConsoleTableGenerator
{
    public static void PrintHelp(int len, string[] args, int[,] matrix) 
    {
        var firstRaw = new string[len + 1];
        firstRaw[0] = "PC v | User ->";
        for (int i = 0; i < len; i++)
        {
            firstRaw[i + 1] = args[i];
        }

        var table = new ConsoleTable(firstRaw);
        for (int i = 0; i < len; i++)
        {
            var row = new string[len + 1];
            row[0] = args[i];
            for (int j = 1; j <= len; j++)
            {
                string result = matrix[i, j - 1] switch
                {
                    0 => "Lose",
                    1 => "Draw",
                    2 => "Win",
                    _ => "Something went wrong"
                };

                row[j] = result;
            }

            table.AddRow(row.ToArray());
        }

        table.Write();
    }
}
