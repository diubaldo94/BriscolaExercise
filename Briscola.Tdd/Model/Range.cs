namespace Briscola.Tdd.Model
{
    public class Range
    {
        public Range(int min, int max)
        {
            Min = min;
            Max = max; 
        }

        public int Min { get; }

        public int Max { get; }
    }
}