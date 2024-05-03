namespace RockPaperScissorsGenericGame;

internal static class InputValidation
{
    internal enum ErrorCode
    {
        Correct,
        LessThanThree,
        EvenAndLong,
        ContainsRepeatingAndLong,
        ContainsRepeatingAndShort
    }

    internal static UserMoveInput GetUserMove(int maxValue)
    {
        Console.Write("Enter your move: ");
        var userMoveInput = new UserMoveInput();
        var input = Console.ReadLine();
        int number = 0;

        if (input == "?" || input == "*" || input == "0" || int.TryParse(input, out number))
        {
            userMoveInput.MoveNumber = number;
            if (number > 0 && number <= maxValue) return userMoveInput;
            if (input == "?") userMoveInput.IsHelpCommand = true;
            if (input == "*") userMoveInput.IsRestartCommand = true;
            if (input == "0") userMoveInput.IsExitCommand = true;
            return userMoveInput;
        }

        ConsoleOutput.PrintError("Your input was incorrect. The input must be a number and cannot be an empty string.");
        Console.WriteLine("Please, enter integer number or '?' sign to get a reference table with moves.");
        ConsoleOutput.PrintHint("Available moves are 1 to " + maxValue);
        return GetUserMove(maxValue);
    }

    internal static void CheckArguments(ref string[] args)
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
            case ErrorCode.ContainsRepeatingAndLong:
                ConsoleOutput.PrintError("The array of argumets cannot have repeating values. Use an odd number of unique arguments.");
                ConsoleOutput.PrintHint("Fortunatelly, the array of arguments was long enough, " +
                    "so we deleted all repeating strings and made it shorter.");
                var newArray = args.Distinct().ToArray();
                args = newArray[..^((newArray.Length + 1) % 2)];
                                return;
            case ErrorCode.ContainsRepeatingAndShort:
                ConsoleOutput.PrintError("The array of argumets cannot have repeating values. Use an odd number of unique arguments");
                Console.Write("Here is an example of how you can run this programm from the command line next time: ");
                ConsoleOutput.PrintHint("dotnet run Rock Spoke Paper Lizard Scissors");
                UserArgsInput(ref args);
                return;
        }
    }

    internal static void UserArgsInput(ref string[] args)
    {
        Console.WriteLine("Enter your parametes using one string and space sign ' ' as a separator. " +
            "Unfortunatelly, you cannot use spacebar during the user input");
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

        int countWithoutRepeanting = args.Distinct().Count();
        if (args.Length != countWithoutRepeanting)
        {
            if (countWithoutRepeanting >= 3)
            {
                return ErrorCode.ContainsRepeatingAndLong;
            }
            else
            {
                return ErrorCode.ContainsRepeatingAndShort;
            }
        }

        return args.Length % 2 == 1 ? ErrorCode.Correct : ErrorCode.EvenAndLong;
    }
}
