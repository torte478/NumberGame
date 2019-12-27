using NumberGame;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.CreateDefault(
                        next: (cells) =>
                                    new FirstStepAlgorithm(
                                    //new RandomAlgorithm(
                                        (_) => new Logic(_)
                                        , cells)
                                    .Resolve());
            var ui = new UI(500, game);

            ui.Start();
        }
    }
}
