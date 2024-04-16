namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            CheckArguments(ref args);
            var movesService = new GameMoveService(args);
            var matrixService = new MovesMatrix(args.Length);
            var hmacService = new HmacCalculatorService();

            Console.WriteLine("\r\n–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––");
            var computerMove = movesService.GetMoveByIndex(new Random().Next(1, args.Length + 1));
            hmacService.CalculateHmacForMessage(computerMove.MoveName);
            ConsoleOutput.PrintCodes("HMAC: " + hmacService.GetHmacAsString());

            Console.WriteLine("Available moves:");
            movesService.PrintAllMoves();
            Console.WriteLine("* - take user arguments from the console\r\n? - help\r\n0 - exit\r\n");

            var userMoveValue = GetUserMove(args.Length + 1);
            if (userMoveValue == -1)
            {
                ConsoleOutput.PrintHelp(args.Length, args, matrixService.Matrix);
                continue;
            }
            if (userMoveValue == -2)
            {
                UserArgsInput(ref args);
                continue;
            }

            if (userMoveValue == 0) break;
            var userMove = movesService.GetMoveByIndex(userMoveValue);
            Console.WriteLine($"Your move: {userMove.MoveName}\r\nComputer move: {computerMove.MoveName}");
            var moveIndex = matrixService.MoveRelationIndex(userMove, computerMove);
            switch (moveIndex)
            {
                case 0:
                    ConsoleOutput.PrintError("You lose!");
                    break;
                case 1:
                    ConsoleOutput.PrintCodes("It's a draw! OMG!");
                    break;
                case 2:
                    ConsoleOutput.PrintHint("You WON! What a player!");
                    break;
                default:
                    ConsoleOutput.PrintError("Something went wrong");
                    break;
            }

            ConsoleOutput.PrintCodes($"Secret key: {hmacService.GetSecretKeyAsString()}");
            Console.WriteLine("–––––––––––––––––––––––––––Well played!––––––––––––––––––––––––––");
        }

        ConsoleOutput.PrintHint("Good buy!");
    }
    private static int GetUserMove(int maxValue)
    {
        Console.Write("Enter your move: ");
        var input = Console.ReadLine();
        int number = 0;
        var flag = input == "?" || input == "*";
        if (flag || int.TryParse(input, out number))
        {
            if (input == "?") return -1;
            if (input == "*") return -2;
            if (number == 0) return 0;
            if (number > 0 && number <= maxValue) return number;
        }

        ConsoleOutput.PrintError("Your input was incorrect. The input must be a number and cannot be an empty string.");
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
                ConsoleOutput.PrintError("The number of arguments cannot less than three. " +
                        "You must add at least three arguments.");
                Console.Write("Here is an example of how you can run this programm from the command line next time: ");
                ConsoleOutput.PrintHint("dotnet run Rock Spoke Paper Lizard Scissors");
                UserArgsInput(ref args);
                return;

            case ErrorCode.EvenAndLong:
                ConsoleOutput.PrintError("The number of arguments cannot be even. " +
                        "Use an odd number of arguments, such as 3, 5, 7, or 21 for example.");
                ConsoleOutput.PrintHint("I'll make your input shorter and remove one item. So, don't worry!");
                args = args[..^1];
                return; 
        }
    }

    private static void UserArgsInput(ref string[] args)
    {
        Console.WriteLine("Enter your parametes using one string and space sign ' ' as a separator." +
            "Unfortunatelly, you cannot use spacebar in your ");
        var secondInput = Console.ReadLine();
        var secondArgs = secondInput?.Split(' ') ?? [];
        CheckArguments(ref secondArgs);
        args = secondArgs;
        return;
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
