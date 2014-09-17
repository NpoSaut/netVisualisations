using System;
using System.Windows;
using System.Windows.Media;

namespace GraphVisualizing.Model
{
    public class LineSegment<TX, TY> : GraphSegmentElement<TX, TY> where TX : IComparable<TX>
    {
        private readonly Pen _pen;

        public LineSegment(Segment<TX> Segment, TY Y1, TY Y2, Pen Pen) : base(Segment)
        {
            this.Y1 = Y1;
            this.Y2 = Y2;
            _pen = Pen;
        }

        public TY Y1 { get; private set; }
        public TY Y2 { get; private set; }

        /// <summary>Отрисовывает объект в указанном контексте рисования</summary>
        /// <param name="dc">Контекст рисования</param>
        /// <param name="Projector">Индекс масштаба рисования</param>
        protected override void Draw(DrawingContext dc, IProjector<TX, TY> Projector)
        {
            Point p1 = Projector.Project(Segment.Start, Y1);
            Point p2 = Projector.Project(Segment.End, Y2);
            dc.DrawLine(_pen, p1, p2);
        }
    }
}
