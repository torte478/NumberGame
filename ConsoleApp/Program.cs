using NumberGame;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.CreateDefault(
                        next: (_) => new CellTuple());
            var ui = new UI(game);

            ui.Start();
        }
    }
}
