namespace NumberGame
{
    public struct Cell
    {
        public static Cell Create(uint value)
        {
            return new Cell(value, false);
        }

        public static Cell CreateClosed()
        {
            return new Cell(uint.MaxValue, true);
        }

        public uint Value { get; }
        public bool Closed { get; private set; }

        private Cell(uint value, bool closed) : this()
        {
            Value = value;
            Closed = closed;
        }

        public void Close()
        {
            Closed = true;
        }
    }
}
