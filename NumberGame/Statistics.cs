namespace NumberGame
{
    /// <summary>
    /// Statistics of bot running
    /// </summary>
    public struct Statistics
    {
        /// <summary>
        /// Number of completed iterations
        /// </summary>
        public uint Iterations { get; set; }
        /// <summary>
        /// Total number of field cells
        /// </summary>
        public uint Total { get; set; }
        /// <summary>
        /// Number of closed cells
        /// </summary>
        public uint Closed { get; set; }
        /// <summary>
        /// Number of filled and removed rows
        /// </summary>
        public uint Deletions { get; set; }
    }
}
