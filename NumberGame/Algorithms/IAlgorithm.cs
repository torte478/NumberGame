namespace NumberGame.Algorithms
{
    /// <summary>
    /// Determines search algorithm of available steps
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Find available step
        /// </summary>
        /// <returns>'true' and cuple of cell coordinates when step exists
        /// and 'false' and garbage on other case</returns>
        (bool, CellTuple) Resolve();
    }
}