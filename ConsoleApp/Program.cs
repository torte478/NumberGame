using NumberGame;
using NumberGame.Algorithms;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.CreateDefault(
                        next: (cells) =>
                                    new FirstStepAlgorithm(false,
                                    //new RandomAlgorithm(
                                        (_) => new Logic(_)
                                        , cells)
                                    .Resolve());
            var ui = new UI(500, game);

            ui.Start();
        }
    }
}
