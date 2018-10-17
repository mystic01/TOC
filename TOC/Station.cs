using System;

namespace TOC
{
    internal class Station
    {
        private readonly int _initialTimeSetting;
        private int _time;
        private Goods _goods;
        private static Random _rand = new Random();

        public Station(int time)
        {
            _initialTimeSetting = time;
            _time = GetShiftingTime(_initialTimeSetting);
            WorkingTime = 0;
        }

        private int GetShiftingTime(int initialTimeSetting)
        {
            var randomNum = _rand.Next(1, 100);
            if (randomNum <= 5)
                return initialTimeSetting - 2;
            else if (randomNum > 5 && randomNum <= 20)
                return initialTimeSetting - 1;
            else if (randomNum > 20 && randomNum <= 80)
                return initialTimeSetting;
            else if (randomNum > 81 && randomNum <= 95)
                return initialTimeSetting + 1;
            else
                return initialTimeSetting + 2;
        }

        public Goods Goods
        {
            get { return _goods; }
            set
            {
                if (value != null)
                {
                    WorkingTime = 0;
                    _time = GetShiftingTime(_initialTimeSetting);
                }
                _goods = value;
            }
        }

        public int Time
        {
            get { return _time; }
        }

        public int WorkingTime { get; private set; }

        public void WorkingTimePlusOne()
        {
            if (_goods != null)
                WorkingTime += 1;
        }
    }
}