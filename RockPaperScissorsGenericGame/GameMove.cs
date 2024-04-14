namespace RockPaperScissorsGenericGame;

internal class GameMove
{
    public string MoveName { get; set; }
    public int MoveIndex { get; set; }

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
