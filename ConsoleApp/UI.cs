using System;
using System.Threading;
using NumberGame;

namespace ConsoleApp
{
    internal sealed class UI
    {
        private readonly int speed;
        private IGame game;

        public UI(int speed, IGame game)
        {
            this.speed = speed;
            this.game = game;
        }

        public void Start()
        {
            while (true)
            {
                DrawGame();
                Thread.Sleep(speed);
                game.Next();
            }
        }

        private void DrawGame()
        {
            var cells = game.ToCells();
            
            Console.Clear();
            for (var i = 0; i < cells.Height; ++i)
            {
                for (var j = 0; j < cells.Width; ++j)
                    Console.Write(cells.At(i, j));
                Console.WriteLine();
            }
            Console.WriteLine("===================");
            Console.WriteLine(game);
        }
    }
}