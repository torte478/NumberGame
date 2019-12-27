namespace NumberGame
{
    /// <summary>
    /// Pair of cell coordinates
    /// </summary>
    public struct CellTuple
    {
        /// <summary>
        /// Coordinates of first cell
        /// </summary>
        public (uint, uint) First { get; set; }
        /// <summary>
        /// Coordinates of second cell
        /// </summary>
        public (uint, uint) Second{ get; set; }
    }
}
