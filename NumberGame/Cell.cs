namespace NumberGame
{
    public struct Cell
    {
        public uint Value { get; }
        public bool Closed { get; }

        public Cell(uint value) : this()
        {
            Value = value;
        }
    }
}
