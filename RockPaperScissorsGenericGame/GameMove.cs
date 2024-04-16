namespace RockPaperScissorsGenericGame;

internal class GameMove
{
    public string MoveName { get; }
    public int MoveIndex { get; }

    public GameMove(string name, int index)
    {
        MoveName = name;
        MoveIndex = index;
    }

    public override string ToString()
    {
        return $"{MoveIndex} - {MoveName}";
    }
}
