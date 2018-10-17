using System;
using System.Collections.Generic;
using System.Threading;

namespace TOC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("按下<Enter>鍵開始");
            Console.ReadLine();

            var tocModel = new TocModel(new List<int> { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 });

            for (int i = 0; i < 500; i++)
            {
                Console.Clear();
                tocModel.Tick();
                tocModel.Output();
                Console.WriteLine($"{"TIME:",5} {i}");
                Thread.Sleep(100);
            }

            Console.WriteLine("結束");
            //Console.WriteLine($"Finish Goods : {tocModel.FinishedGoodsAmount}");
            Console.ReadLine();
        }
    }
}