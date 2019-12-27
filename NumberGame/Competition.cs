using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGame
{
    public sealed class Competition
    {
        private Dictionary<string, IGame> games;
        private readonly int iterations;

        public Competition(Dictionary<string, IGame> games, int iterations)
        {
            this.games = games;
            this.iterations = iterations;
        }

        public Dictionary<string, Statistics> Run()
        {
            var result = new Dictionary<string, Statistics>();

            foreach (var game in games)
            {
                var current = game.Value;
                for (var i = 0; i < iterations; ++i)
                    current = current.Next();

                result.Add(game.Key, current.ToStatistics());
            }

            return result;
        }
    }
}
