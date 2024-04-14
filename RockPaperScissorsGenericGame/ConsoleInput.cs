namespace RockPaperScissorsGenericGame;

internal class ConsoleInput
{
    public void CheckInputLength(int inputLength)
    {
        if (inputLength == 0)
        {
            throw new ArgumentException("The number of arguments cannot be zero. You must add at least three arguments.");
        }

        if (inputLength < 3)
        {
            throw new ArgumentException("The number of arguments cannot be less than three. You must add at least three arguments.");
        }

        if (inputLength % 2 == 0)
        {
            throw new ArgumentException("The number of arguments cannot be even. Use an odd number of arguments, such as 3, 5, 7, or 21.");
        }
    }
}
