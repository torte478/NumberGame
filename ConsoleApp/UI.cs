using System;
using System.Threading;
using NumberGame;

namespace ConsoleApp
{
    /// <summary>
    /// Console GUI for bot work demonstration
    /// </summary>
    internal sealed class UI
    {
        private readonly int speed;
        private IGame game;

        /// <summary>
        /// Console GUI for bot work demonstration
        /// </summary>
        /// <param name="speed">Speed of demonstration</param>
        /// <param name="game">instance of game</param>
        public UI(int speed, IGame game)
        {
            this.speed = speed;
            this.game = game;
        }

        /// <summary>
        /// Start demonstration
        /// </summary>
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