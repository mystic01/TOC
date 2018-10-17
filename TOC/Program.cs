using System;
using System.Collections.Generic;
using System.Threading;

namespace TOC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadLine();

            var tocModel = new TocModel(new List<int> { 5,5,5,5,5, 5, 5, 5, 5, 5 });

            for (int i = 0; i < 2000; i++)
            {
                Console.Clear();
                tocModel.Tick();
                tocModel.Output();
                Thread.Sleep(10);
            }

            Console.WriteLine($"Finish Goods : {tocModel.FinishedGoodsAmount}");

            Console.ReadLine();
        }
    }
}