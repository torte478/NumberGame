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
                NextStep();
            }
        }

        private void DrawGame()
        {
            var cells = game.ToCells();
            
            Console.Clear();
            for (var i = 0; i < cells.Length; ++i)
            {
                for (var j = 0; j < cells[i].Length; ++j)
                    Console.Write(cells[i][j]);
                Console.WriteLine();
            }
        }

        private void NextStep()
        {
            game = game.Next();
        }
    }
}