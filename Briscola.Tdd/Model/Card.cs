namespace Briscola.Tdd.Model
{
    public struct Card
    {
        public Card(string seed, int value)
        {
            Seed = seed;
            Value = value;
        }

        public string Seed { get; }
        public int Value { get; }
    }
}