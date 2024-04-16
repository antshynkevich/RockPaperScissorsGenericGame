namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        CheckArguments(ref args);
        var movesService = new GameMoveService(args);
        var matrixService = new MovesMatrix(args.Length);
        var hmacService = new HmacCalculatorService();

        var computerMove = movesService.GetMoveByIndex(new Random().Next(1, args.Length + 1));
        hmacService.CalculateHmacForMessage(computerMove.MoveName);
        ConsoleOutput.PrintCodes("HMAC: " + hmacService.GetHmacAsString());
        Console.WriteLine("Available moves:");
        movesService.PrintAllMoves();
        Console.WriteLine("0 - exit\r\n? - help\r\n");
        var userMoveValue = GetUserMove(args.Length + 1);
        if (userMoveValue == -1)
        {
            ConsoleOutput.PrintHelp(args.Length, args, matrixService.Matrix);
            return;
        }

        if (userMoveValue == 0) return;
        var userMove = movesService.GetMoveByIndex(userMoveValue);
        Console.WriteLine($"Your move: {userMove.MoveName}\r\nComputer move: {computerMove.MoveName}");
        var moveIndex = matrixService.MoveRelationIndex(userMove, computerMove);
        switch (moveIndex)
        {
            case 0:
                Console.WriteLine("You lose!");
                break;
            case 1:
                Console.WriteLine("It's a draw! OMG!");
                break;
            case 2:
                Console.WriteLine("You WON! What a player!");
                break;
            default:
                ConsoleOutput.PrintWarning("Something went wrong");
                break;
        }

        ConsoleOutput.PrintCodes($"Secret key: {hmacService.GetSecretKeyAsString()}");
    }

    private static int GetUserMove(int maxValue)
    {
        Console.Write("Enter your move: ");
        var input = Console.ReadLine();
        int number = 0;
        if (input == "?" || int.TryParse(input, out number))
        {
            if (input == "?") return -1;
            if (number == 0) return 0;
            if (number > 0 && number <= maxValue) return number;
        }

        ConsoleOutput.PrintWarning("Your input was incorrect. This is not a number or empty string.");
        Console.WriteLine("Please, inter integer number or '?' sign to get a reference table with moves.");
        ConsoleOutput.PrintHint("Available moves are 1 to " + maxValue);
        return GetUserMove(maxValue);
    }


    private static void CheckArguments(ref string[] args)
    {
        ErrorCode code = GetErrorCode(args);
        switch (code)
        {
            case ErrorCode.Correct:
                return;
            case ErrorCode.LessThanThree:
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

            case ErrorCode.EvenAndLong:
                ConsoleOutput.PrintWarning("The number of arguments cannot be even. " +
                        "Use an odd number of arguments, such as 3, 5, 7, or even 21.");
                ConsoleOutput.PrintHint("I'll make your input shorter and remove one item. So, don't worry!");
                args = args[..^1];
                return; 
        }
    }

    internal static ErrorCode GetErrorCode(string[] args)
    {
        if (args.Length < 3)
        {
            return ErrorCode.LessThanThree;
        }

        return args.Length % 2 == 1 ? ErrorCode.Correct : ErrorCode.EvenAndLong;
    }
}
