using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using GraphVisualizing.View;

namespace GraphVisualizing.Model
{
    public abstract class GraphElement<TX, TY> : IMouseEventReceiver where TX : IComparable<TX>
    {
        private static GuidelineSet _screenGuidelineSet;

        static GraphElement()
        {
            _screenGuidelineSet = new GuidelineSet(
                Enumerable.Range(0, 40000).Select(x => (double)x).ToArray(),
                Enumerable.Range(0, 40000).Select(y => (double)y).ToArray());
        }

        /// <summary>Z-индекс элемента на карте</summary>
        /// <remarks>Меньшее значения индекса соответствуют нижним слоям на карте</remarks>
        protected virtual int ZIndex
        {
            get { return 0; }
        }

        /// <summary>Визуальный элемент, изображающий данный элемент карты</summary>
        internal GraphVisual AttachedVisual { get; set; }

        /// <summary>Событие, сигнализирующее о том, что элемент запросил изменение своего визуального отображения</summary>
        internal event EventHandler ChangeVisualRequested;

        /// <summary>Отправляет запрос на смену своего визуального отображения</summary>
        protected void RequestChangeVisual()
        {
            EventHandler handler = ChangeVisualRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>Отрисовывает объект в указанном контексте рисования</summary>
        /// <param name="dc">Контекст рисования</param>
        /// <param name="Projector">Индекс масштаба рисования</param>
        protected abstract void Draw(DrawingContext dc, IProjector<TX, TY> Projector);

        /// <summary>Получает визуальный элемент для этого элемента карты</summary>
        public GraphVisual GetVisual(IProjector<TX, TY> Projector)
        {
            var res = new GraphVisual(this, ZIndex);
            using (DrawingContext dc = res.RenderOpen())
            {
                Draw(dc, Projector);
            }
            return res;
        }

        /// <summary>Проверяет, попадает ли этот элемент в указанную области видимости</summary>
        /// <param name="Segment">Область видимости</param>
        /// <returns>True, если объект может оказаться виден в указанной области</returns>
        public abstract bool TestVisual(Segment<TX> Segment);

        public Boolean IsMouseOver { get; private set; }

        public static GuidelineSet ScreenGuidelineSet
        {
            get { return _screenGuidelineSet; }
        }

        public virtual void OnMouseClick(MouseButtonEventArgs ChangedButton) { }
        public virtual void OnMouseMove(MouseEventArgs MouseEventArgs) { }
        public virtual void OnMouseEnter(MouseEventArgs MouseEventArgs) { IsMouseOver = true; }
        public virtual void OnMouseLeave(MouseEventArgs MouseEventArgs) { IsMouseOver = false; }
    }
}