namespace NumberGame
{
    public interface IAlgorithm
    {
        (bool, CellTuple) Resolve();
    }
}