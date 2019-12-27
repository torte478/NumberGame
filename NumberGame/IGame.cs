namespace NumberGame
{
    /// <summary>
    /// Determines of general game logic
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Make next step
        /// </summary>
        void Next();
        /// <summary>
        /// Get cells of game field
        /// </summary>
        ICells ToCells();
        /// <summary>
        /// Get statistics of current game
        /// </summary>
        Statistics ToStatistics();
    }
}
