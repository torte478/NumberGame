namespace NumberGame
{
    public interface ICells
    {
        int Height { get; }
        int Width { get; }
        Cell At(int row, int column);
        (bool, CellTuple) FindHorizontalFor((uint, uint) first);
        (bool, CellTuple) FindVerticalFor((uint, uint) first);
    }
}