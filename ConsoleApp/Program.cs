using System;
using System.Collections.Generic;
using System.Linq;
using NumberGame;
using NumberGame.Algorithms;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Print '0' for exit");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1: Show game demo");
                Console.WriteLine("2: Start bot competition");

                var command = ReadCommand();
                if (command.Item1)
                {
                    var value = command.Item2;
                    if (value == 0)
                        break;
                    if (value == 1)
                        ShowGameDemo();
                    else if (value == 2)
                        StartBotCompetition();
                }
            }
        }

        private static void StartBotCompetition()
        {
            var games = new Dictionary<string, IGame>()
            {
                { "HorOriented", CreateHorizontalOriented() },
                { "VertOriented", CreateVerticalOriented() },
                { "Random1", CreateRandom() },
                { "Random2", CreateRandom() },
                { "Random3", CreateRandom() },
            };

            int iterations = 10;
            while (true)
            {
                Console.WriteLine("Print iterations count");
                var command = ReadCommand();
                if (command.Item1)
                {
                    if (command.Item2 == 0) return;
                    iterations = Math.Max(command.Item2, iterations);
                    break;
                }
            }

            var competion = new Competition(games, iterations);
            var result = competion.Run();
            WriteResult(result);
        }

        private static void ShowGameDemo()
        {
            Console.WriteLine("Print bot type");
            Console.WriteLine("1: Horizontal oriented");
            Console.WriteLine("2: Vertical oriented");
            Console.WriteLine("3: Random steps");

            IGame game = null;
            while (true)
            {
                var command = ReadCommand();
                if (command.Item1)
                {
                    var value = command.Item2;
                    if (value == 0) return;

                    if (value == 1)
                        game = CreateHorizontalOriented();
                    else if (value == 2)
                        game = CreateVerticalOriented();
                    else if (value == 3)
                        game = CreateRandom();

                    if (value >= 1 && value <= 3)
                        break;
                }
            }

            Console.WriteLine("Print speed <=3000");
            var speed = 3000;
            while (true)
            {
                Console.WriteLine("Print iterations count");
                var command = ReadCommand();
                if (command.Item1)
                {
                    if (command.Item2 == 0) return;
                    speed = Math.Min(command.Item2, speed);
                    break;
                }
            }
            Console.Clear();

            var ui = new UI(speed, game);

            ui.Start();
        }

        private static (bool, int) ReadCommand()
        {
            Console.Write(":> ");
            var line = Console.ReadLine();
            if (int.TryParse(line, out int command))
                return (true, command);
            return (false, -1);
        }

        private static void WriteResult(Dictionary<string, Statistics> result)
        {
            var lines = result
                        .Select(kv => (
                                    key: kv.Key, 
                                    value: kv.Value, 
                                    percent: kv.Value.Closed * 100.0 / kv.Value.Total))
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

        private static IGame CreateVerticalOriented()
        {
            return Game.CreateDefault((cells) => new FirstStepAlgorithm(false, Logic.Create, cells).Resolve());
        }

        private static IGame CreateHorizontalOriented()
        {
            return Game.CreateDefault((cells) => new FirstStepAlgorithm(true, Logic.Create, cells).Resolve());
        }

        private static IGame CreateRandom()
        {
            return Game.CreateDefault((cells) => new RandomAlgorithm(Logic.Create, cells).Resolve());
        }
    }
}
