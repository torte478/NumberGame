namespace NumberGame
{
    /// <summary>
    /// Game field cell
    /// </summary>
    public struct Cell
    {
        private uint? value;
        private bool? closed;
        private bool? empty;

        /// <summary>
        /// Value of cell
        /// </summary>
        public uint Value { get { return value ?? uint.MaxValue; } }
        /// <summary>
        /// True, when call was closed
        /// </summary>
        public bool Closed { get { return closed ?? true; } private set { closed = value; } }
        /// <summary>
        /// True, when is empty yet
        /// </summary>
        public bool Empty { get { return empty ?? true; } }

        /// <summary>
        /// Game field cell
        /// </summary>
        /// <param name="value">value of cell</param>
        public Cell(uint value) : this()
        {
            this.value = value;
            this.closed = false;
            this.empty = false;
        }

        /// <summary>
        /// Change type of cell to 'closed'
        /// </summary>
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
