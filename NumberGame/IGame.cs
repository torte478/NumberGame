namespace NumberGame
{
    public interface IGame
    {
        IGame Next();
        Cell[][] ToCells();
    }
}
