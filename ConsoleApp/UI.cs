using System;
using System.Threading;
using NumberGame;

namespace ConsoleApp
{
    internal sealed class UI
    {
        private const ushort SleepDuration = 500;

        private IGame game;

        public UI(IGame game)
        {
            this.game = game;
        }

        public void Start()
        {
            while (true)
            {
                DrawGame();
                Sleep();
                NextStep();
            }
        }

        private void DrawGame()
        {
            var cells = game.ToCells();
            //TODO: next
            Console.Clear();
            Console.WriteLine(cells.ToString());
        }

        private void NextStep()
        {
            game = game.Next();
        }

        private static void Sleep()
        {
            Thread.Sleep(SleepDuration);
        }
    }
}