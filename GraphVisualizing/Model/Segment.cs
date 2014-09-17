using System;

namespace GraphVisualizing.Model
{
    public class Segment<TX> where TX : IComparable<TX>
    {
        public Segment(TX Start, TX End)
        {
            this.End = End;
            this.Start = Start;
        }

        public TX Start { get; private set; }
        public TX End { get; private set; }

        public bool IsIntersects(Segment<TX> Other)
        {
            return (Start.CompareTo(Other.Start) >= 0 && Start.CompareTo(Other.End) <= 0) ||
                   (End.CompareTo(Other.Start) >= 0 && End.CompareTo(Other.End) <= 0);
        }

        /// <summary>Возвращает строку, которая представляет текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString() { return string.Format("[{0}; {1}]", Start, End); }
    }
}
