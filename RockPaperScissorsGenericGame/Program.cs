using static RockPaperScissorsGenericGame.MovesMatrix;

namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            InputValidation.CheckArguments(ref args);
            var movesService = new GameMoveService(args);
            var hmacService = new HmacCalculatorService();

            Console.WriteLine("\r\n–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––");
            var computerMove = movesService.GetMoveByIndex(new Random().Next(1, args.Length + 1));
            hmacService.CalculateHmacForMessage(computerMove.MoveName);
            ConsoleOutput.PrintCodes("HMAC: " + hmacService.GetHmacAsString());

            Console.WriteLine("Available moves:");
            movesService.PrintAllMoves();
            Console.WriteLine("* - take user arguments from the console\r\n? - help\r\n0 - exit\r\n");

            var userMoveInput = InputValidation.GetUserMove(args.Length + 1);
            if (userMoveInput.IsHelpCommand)
            {
                var matrixService = new MovesMatrix(args.Length);
                ConsoleOutput.PrintHelp(args.Length, args, matrixService.Matrix);
                continue;
            }

            if (userMoveInput.IsRestartCommand)
            {
                InputValidation.UserArgsInput(ref args);
                continue;
            }

            if (userMoveInput.IsExitCommand) break;

            var userMove = movesService.GetMoveByIndex(userMoveInput.MoveNumber);
            Console.WriteLine($"Your move: {userMove.MoveName}\r\nComputer move: {computerMove.MoveName}");
            var moveIndex = MovesMatrix.GetGameMoveRelationIndex(userMove, computerMove, args.Length);
            switch (moveIndex)
            {
                case GameMoveRelationships.Loss:
                    ConsoleOutput.PrintError("You lose!");
                    break;
                case GameMoveRelationships.Draw:
                    ConsoleOutput.PrintCodes("It's a draw! OMG!");
                    break;
                case GameMoveRelationships.Win:
                    ConsoleOutput.PrintHint("You WON! What a player!");
                    break;
                default:
                    ConsoleOutput.PrintError("Something went wrong");
                    break;
            }

            ConsoleOutput.PrintCodes($"Secret key: {hmacService.GetSecretKeyAsString()}");
            Console.WriteLine("–––––––––––––––––––––––––––Well played!––––––––––––––––––––––––––");
        }

        ConsoleOutput.PrintHint("Goodbye!");
    }
}
