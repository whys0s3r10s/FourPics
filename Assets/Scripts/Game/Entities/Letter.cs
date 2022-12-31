namespace FourPics
{
    public class Letter
    {
        public char Value { get; }

        public int Index { get; set; }

        public int? AttachedIndex { get; set; }

        public bool Removed { get; set; }

        public bool Locked { get; set; }

        public bool Attached => AttachedIndex != null;

        public Letter(char value, int? attachedIndex = null, bool removed = false, bool locked = false)
        {
            Value = value;
            AttachedIndex = attachedIndex;
            Removed = removed;
            Locked = locked;
        }
    }
}