using NumberGame;
using NumberGame.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var games = new Dictionary<string, IGame>()
            {
                { "HorOriented", Game.CreateDefault((cells) => new FirstStepAlgorithm(true, Logic.Create, cells).Resolve()) },
                { "VertOriented", Game.CreateDefault((cells) => new FirstStepAlgorithm(false, Logic.Create, cells).Resolve()) },
                { "Random1", Game.CreateDefault((cells) => new RandomAlgorithm(Logic.Create, cells).Resolve()) },
                { "Random2", Game.CreateDefault((cells) => new RandomAlgorithm(Logic.Create, cells).Resolve()) },
                { "Random3", Game.CreateDefault((cells) => new RandomAlgorithm(Logic.Create, cells).Resolve()) },
            };

            var competion = new Competition(games, 1000);
            var result = competion.Run();
            WriteResult(result);

            //var game = Game.CreateDefault(
            //            next: (cells) =>
            //                        new FirstStepAlgorithm(false,
            //                        //new RandomAlgorithm(
            //                            (_) => new Logic(_)
            //                            , cells)
            //                        .Resolve());
            //var ui = new UI(500, game);

            //ui.Start();
        }

        private static void WriteResult(Dictionary<string, Statistics> result)
        {
            var lines = result
                        .Select(kv => (key: kv.Key, value: kv.Value, percent: kv.Value.Closed * 100.0 / kv.Value.Total))
                        .OrderByDescending(item => item.percent)
                        .Select(item => $"{item.key} : " +
                                        $"it = {item.value.Iterations} " +
                                        $"del = {item.value.Deletions} " +
                                        $"{item.value.Closed}/{item.value.Total} " +
                                        $"({item.percent}%)")
                        .ToArray();

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
