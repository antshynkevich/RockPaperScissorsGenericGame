namespace RockPaperScissorsGenericGame;

internal class UserMoveInput
{
    public int MoveNumber { get; set; }
    public bool IsHelpCommand { get; set; }
    public bool IsExitCommand { get; set; }
    public bool IsRestartCommand { get; set; }
}
