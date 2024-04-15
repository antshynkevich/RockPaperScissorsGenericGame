namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        CheckArguments(args);
        var movesService = new GameMoveService(args);
        // var moves = movesService.GetMoves();
        Console.WriteLine("Available moves:");
        movesService.PrintAllMoves();
        Console.WriteLine("0 - exit\r\n? - help\r\n");
        Console.Write("Enter your move: ");
        var userMove = GetUserMove();
        var matrixService = new MovesMatrix(args.Length);
        ConsoleTableGenerator.PrintHelp(args.Length, args, matrixService.Matrix);
    }

    private static int GetUserMove()
    {
        int userMove = int.Parse(Console.ReadLine());
        return userMove;
    }

    private static void CheckArguments(string[] args)
    {
        try
        {
            ConsoleInput.CheckInputLength(args.Length);
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
