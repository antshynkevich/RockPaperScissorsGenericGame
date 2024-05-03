namespace RockPaperScissorsGenericGame;

internal class MovesMatrix
{
    private readonly int[,] _matrix;
    public MovesMatrix(int length)
    {
        _matrix = MovesMatrixCalulation(length);
    }

    public int[,] Matrix => _matrix;

    /// <summary>
    /// This method calculates a matrix that is responsible for the relationships of moves. '2' means win, '1' means draw and '0' means loss.
    /// </summary>
    /// <param name="n">The length value of the parameter array.</param>
    /// <returns>The matrix with the relationships of moves.</returns>
    private static int[,] MovesMatrixCalulation(int n)
    {
        var firstMatrixRow = new int[n];
        firstMatrixRow[0] = 1;
        for (int i = 1; i < n; i++)
        {
            firstMatrixRow[i] = i <= (n / 2) ? 2 : 0;
        }

        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            matrix[0, i] = firstMatrixRow[i];
        }

        for (int i = 1; i < n; i++)
        {
            // Move the last element to the start of the new row
            matrix[i, 0] = matrix[i - 1, n - 1];

            // Shift elements to the right of the new row
            for (int j = n - 1; j > 0; j--)
                matrix[i, j] = matrix[i - 1, j - 1];
        }

        return matrix;
    }

    public static GameMoveRelationships GetGameMoveRelationIndex(GameMove userMove, GameMove computerMove, int numberOfMoves)
    {
        if (userMove.MoveIndex == computerMove.MoveIndex) return GameMoveRelationships.Draw;
        return ((userMove.MoveIndex + numberOfMoves - computerMove.MoveIndex) % numberOfMoves > numberOfMoves / 2)
            ? GameMoveRelationships.Loss : GameMoveRelationships.Win;
    }

    public enum GameMoveRelationships
    {
        Draw,
        Win,
        Loss
    }
}
