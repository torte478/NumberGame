using System.Collections.Generic;

namespace NumberGame
{
    /// <summary>
    /// Implementation of different bot analytics
    /// </summary>
    public sealed class Competition
    {
        private Dictionary<string, IGame> games;
        private readonly int iterations;

        /// <summary>
        /// Implementation of different bot analytics
        /// </summary>
        /// <param name="games">Collection of bots</param>
        /// <param name="iterations">Number of iteration for each bot</param>
        public Competition(Dictionary<string, IGame> games, int iterations)
        {
            this.games = games;
            this.iterations = iterations;
        }

        /// <summary>
        /// Start analysys
        /// </summary>
        /// <returns>Collection of results</returns>
        public Dictionary<string, Statistics> Run()
        {
            var result = new Dictionary<string, Statistics>();

            foreach (var game in games)
            {
                var current = game.Value;
                for (var i = 0; i < iterations; ++i)
                    current.Next();

                result.Add(game.Key, current.ToStatistics());
            }

            return result;
        }
    }
}
