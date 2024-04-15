namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        CheckArguments(ref args);
        var movesService = new GameMoveService(args);
        // var moves = movesService.GetMoves();
        Console.WriteLine("Available moves:");
        movesService.PrintAllMoves();
        Console.WriteLine("0 - exit\r\n? - help\r\n");
        Console.Write("Enter your move: ");
        var userMove = GetUserMove();
        var matrixService = new MovesMatrix(args.Length);
        ConsoleOutput.PrintHelp(args.Length, args, matrixService.Matrix);
    }

    private static int GetUserMove()
    {
        int userMove = int.Parse(Console.ReadLine());
        return userMove;
    }

    private static void CheckArguments(ref string[] args)
    {
        if (args.Length < 3)
        {
            ConsoleOutput.PrintWarning("The number of arguments cannot less than three. " +
                "You must add at least three arguments.");
            Console.Write("Here is an example of how you can run this programm from the command line next time: ");
            ConsoleOutput.PrintHint("dotnet run Rock Spoke Paper Lizard Scissors");
            Console.WriteLine("Enter your parametes using one string and space sign ' ' as a separator");
            var secondInput = Console.ReadLine();
            var secondArgs = secondInput?.Split(' ') ?? [];
            CheckArguments(ref secondArgs);
            args = secondArgs;
            return;
        }

        if (args.Length % 2 == 1) return;
        else
        {
            ConsoleOutput.PrintWarning("The number of arguments cannot be even. " +
                "Use an odd number of arguments, such as 3, 5, 7, or even 21.");
            ConsoleOutput.PrintHint("I'll make your input shorter and remove one item. So, don't worry!");
            args = args[..^1];
            return;
        }
    }
}
