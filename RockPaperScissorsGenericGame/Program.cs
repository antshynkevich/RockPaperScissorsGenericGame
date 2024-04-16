namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            InputValidation.CheckArguments(ref args);
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

            var userMoveValue = InputValidation.GetUserMove(args.Length + 1);
            if (userMoveValue == -1)
            {
                ConsoleOutput.PrintHelp(args.Length, args, matrixService.Matrix);
                continue;
            }

            if (userMoveValue == -2)
            {
                InputValidation.UserArgsInput(ref args);
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
}
