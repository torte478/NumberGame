namespace NumberGame
{
    public struct Cell
    {
        private uint? value;
        private bool? closed;
        private bool? empty;

        public uint Value { get { return value ?? uint.MaxValue; } }
        public bool Closed { get { return closed ?? true; } private set { closed = value; } }
        public bool Empty { get { return empty ?? true; } }

        public Cell(uint value) : this()
        {
            this.value = value;
            this.closed = false;
            this.empty = false;
        }

        public void Close()
        {
            closed = true;
        }

        public override string ToString()
        {
            return Closed
                   ? (Empty ? "_" : "#")
                   : Value.ToString();
        }
    }
}
