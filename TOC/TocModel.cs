using System;
using System.Collections.Generic;
using System.Linq;

namespace TOC
{
    internal class TocModel
    {
        private readonly Station[] _stations;
        private readonly Queue<Goods>[] _waitQueue;
        private Goods _lastFinishedGoods;
        private int _timeStamp;
        private int _totalTakeTime = 0;
        private int _totalFinishedCount = 0;

        public TocModel(List<int> times)
        {
            _stations = new Station[times.Count];
            for (int i = 0; i < times.Count; i++)
                _stations[i] = new Station(times[i]);

            _waitQueue = new Queue<Goods>[times.Count + 1];
            for (int i = 0; i < times.Count + 1; i++)
                _waitQueue[i] = new Queue<Goods>();

            _timeStamp = 0;
        }

        public int FinishedGoodsAmount
        {
            get { return _totalFinishedCount; }
        }

        public void Tick()
        {
            _timeStamp++;

            MakeAllStationsWork();
            TryToPushGoodsIntoNextStationQueue();
            TryToPushGoodsIntoFirstQueue();
            TryToPushOneGoodsFormQueueIntoStation();
            UpdateStatisticData();
        }

        private void MakeAllStationsWork()
        {
            foreach (var station in _stations)
                station.WorkingTimePlusOne();
        }

        private void UpdateStatisticData()
        {
            if (_waitQueue[_waitQueue.Length - 1].Any())
            {
                _lastFinishedGoods = _waitQueue[_waitQueue.Length - 1].Dequeue();
                _lastFinishedGoods.EndTimeStamp = _timeStamp;

                var takeTime = _lastFinishedGoods.EndTimeStamp - _lastFinishedGoods.StartTimeStamp;
                _totalTakeTime += takeTime;
                _totalFinishedCount++;
            }
        }

        private void TryToPushOneGoodsFormQueueIntoStation()
        {
            for (int i = _stations.Length - 1; i >= 0; i--)
            {
                if (_stations[i].Goods == null && _waitQueue[i].Any())
                {
                    _stations[i].Goods = _waitQueue[i].Dequeue();
                }
            }
        }

        private void TryToPushGoodsIntoFirstQueue()
        {
            if (_waitQueue[0].Count == 0)
                _waitQueue[0].Enqueue(new Goods(_timeStamp));
        }

        private void TryToPushGoodsIntoNextStationQueue()
        {
            for (int i = _stations.Length - 1; i >= 0; i--)
            {
                if (_stations[i].Goods != null && _stations[i].WorkingTime == _stations[i].Time)
                {
                    _waitQueue[i + 1].Enqueue(_stations[i].Goods);
                    _stations[i].Goods = null;
                }
            }
        }

        public void Output()
        {
            OutputStationNumber();
            OutputStationWorkingTime();
            OutputGoodsStatus();

            Console.WriteLine("===============================");

            OutputStatisticData();

            if (_timeStamp % 2 == 0)
                Console.WriteLine("*");
            else
                Console.WriteLine(" ");
        }

        private void OutputStatisticData()
        {
            var takeTime = "NONE";
            if (_lastFinishedGoods != null)
                takeTime = (_lastFinishedGoods.EndTimeStamp - _lastFinishedGoods.StartTimeStamp).ToString();
            Console.WriteLine($"Total Working Time of the Last Goods: {takeTime}");
            if (_totalFinishedCount > 0)
            {
                Console.WriteLine($"Finished Goods:{_totalFinishedCount}");
                Console.WriteLine($"Average Working Time: {_totalTakeTime / _totalFinishedCount}");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void OutputGoodsStatus()
        {
            //Goods .     .         .
            Console.Write($"{"Goods",10}");
            for (int i = 0; i < _stations.Length; i++)
            {
                Console.Write(" ");
                if (_waitQueue[i].Any())
                    Console.Write($"{_waitQueue[i].Count,2}");
                else
                    Console.Write("  ");
                if (_stations[i].Goods != null)
                    Console.Write($"{".",2}");
                else
                    Console.Write("  ");
            }
            Console.WriteLine();
        }

        private void OutputStationWorkingTime()
        {
            //Time   3    5    2    3
            Console.Write($"{"TIME",10}");
            foreach (var station in _stations)
                Console.Write($"{station.Time,5}");
            Console.WriteLine();
        }

        private void OutputStationNumber()
        {
            //NO.    1    2    3    4
            Console.Write($"{"No.",10}");
            for (int i = 0; i < _stations.Length; i++)
                Console.Write($"{i,5}");
            Console.WriteLine();
        }
    }
}