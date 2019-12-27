namespace NumberGame
{
    /// <summary>
    /// Determines logic of work with game board
    /// </summary>
    public interface ICells
    {
        /// <summary>
        /// Count of rows on board
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Count of columns on board
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Get cell on that coordinates
        /// </summary>
        /// <param name="row">Index of row</param>
        /// <param name="column">Index of column</param>
        Cell At(int row, int column);
        /// <summary>
        /// Find horizontal neighbor for cell
        /// </summary>
        /// <param name="cell">Coordinates of start cell</param>
        (bool, CellTuple) FindHorizontalFor((uint, uint) cell);
        /// <summary>
        /// Find vertical neighbor for cell
        /// </summary>
        /// <param name="cell">Coordinates of start cell</param>
        (bool, CellTuple) FindVerticalFor((uint, uint) cell);
    }
}