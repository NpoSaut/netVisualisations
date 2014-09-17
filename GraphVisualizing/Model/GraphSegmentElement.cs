using System;

namespace GraphVisualizing.Model
{
    public abstract class GraphSegmentElement<TX, TY> : GraphElement<TX, TY> where TX : IComparable<TX>
    {
        protected GraphSegmentElement(Segment<TX> Segment) { this.Segment = Segment; }
        public Segment<TX> Segment { get; private set; }

        /// <summary>Проверяет, попадает ли этот элемент в указанную области видимости</summary>
        /// <param name="Segment">Область видимости</param>
        /// <returns>True, если объект может оказаться виден в указанной области</returns>
        public override bool TestVisual(Segment<TX> Segment) { return this.Segment.IsIntersects(Segment); }
    }
}
