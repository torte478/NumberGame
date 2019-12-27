namespace NumberGame
{
    public interface ILogic // TODO : refactor that shit
    {
        (bool, CellTuple) FindHorizontalFor((uint, uint) first);
        (bool, CellTuple) FindVerticalFor((uint, uint) first);
    }
}