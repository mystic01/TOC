namespace TOC
{
    internal class Goods
    {
        public Goods(int timeStamp)
        {
            StartTimeStamp = timeStamp;
        }

        public int StartTimeStamp { get; private set; }
        public int EndTimeStamp { get; set; }
    }
}