using MuseumBack.Models.Trainer;
using System;

namespace AITesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = Console.ReadLine();
            AITrainer.Train(path);
            Console.ReadKey();
        }
    }
}
