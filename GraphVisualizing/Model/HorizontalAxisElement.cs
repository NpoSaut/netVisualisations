using System;
using System.Windows;
using System.Windows.Media;

namespace GraphVisualizing.Model
{
    public class HorizontalAxisElement<TX, TY> : GraphElement<TX, TY> where TX : IComparable<TX>
    {
        public HorizontalAxisElement(Pen Pen, TY Y)
        {
            this.Pen = Pen;
            this.Y = Y;
        }

        public TY Y { get; private set; }

        public Pen Pen { get; private set; }

        /// <summary>Отрисовывает объект в указанном контексте рисования</summary>
        /// <param name="dc">Контекст рисования</param>
        /// <param name="Projector">Индекс масштаба рисования</param>
        protected override void Draw(DrawingContext dc, IProjector<TX, TY> Projector)
        {
            dc.DrawLine(Pen,
                        new Point(-1000, Projector.Project(default(TX), Y).Y),
                        new Point(1000, Projector.Project(default(TX), Y).Y));
        }

        /// <summary>Проверяет, попадает ли этот элемент в указанную области видимости</summary>
        /// <param name="Segment">Область видимости</param>
        /// <returns>True, если объект может оказаться виден в указанной области</returns>
        public override bool TestVisual(Segment<TX> Segment) { throw new NotImplementedException(); }
    }
}
