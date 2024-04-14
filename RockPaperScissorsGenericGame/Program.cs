namespace RockPaperScissorsGenericGame;

internal static class Program
{
    static void Main(string[] args)
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
