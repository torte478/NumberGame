namespace NumberGame.Algorithms
{
    public interface IAlgorithm
    {
        (bool, CellTuple) Resolve();
    }
}