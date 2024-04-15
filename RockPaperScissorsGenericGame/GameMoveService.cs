namespace RockPaperScissorsGenericGame;

internal class GameMoveService
{
    private readonly GameMove[] _moves;

    public GameMoveService(string[] userInputArgs)
    {
        _moves = new GameMove[userInputArgs.Length];
        for (int i = 0; i < userInputArgs.Length; i++)
        {
            _moves[i] = new GameMove(userInputArgs[i], i + 1);
        }
    }

    public GameMove[] GetMoves()
    {
        return _moves;
    }

    public void PrintAllMoves()
    {
        foreach (var gameMove in _moves)
        {
            Console.WriteLine(gameMove);
        }
    }
}
