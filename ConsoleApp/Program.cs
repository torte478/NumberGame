using NumberGame;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.CreateDefault();
            var ui = new UI(game);

            ui.Start();
        }
    }
}
