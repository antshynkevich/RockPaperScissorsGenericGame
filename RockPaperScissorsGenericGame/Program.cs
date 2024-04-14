namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        CheckArguments(args);

        var moves = new List<GameMove>();
        for (int i = 0; i < args.Length; i++)
        {
            moves.Add(new GameMove(args[i], i + 1));
        }

        Console.WriteLine("Available moves:");
        foreach (var gameMove in moves)
        {
            Console.WriteLine(gameMove);
        }

        Console.WriteLine("0 - exit\r\n? - help\r\n");
    }

    private static void CheckArguments(string[] args)
    {
        var input = new ConsoleInput();
        try
        {
            input.CheckInputLength(args.Length);
        }
        catch (Exception exp)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exp.Message);
            Console.ResetColor();
            Console.Write("Here is an example of how you can run this programm from the command line: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("dotnet run Rock Spoke Paper Lizard Scissors");
            Console.ResetColor();
        }
    }
}
