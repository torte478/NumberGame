namespace NumberGame
{
    public interface IGame
    {
        void Next();
        ICells ToCells();
        Statistics ToStatistics();
    }
}
