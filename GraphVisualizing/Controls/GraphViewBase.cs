using System;
using System.Collections.Generic;
using System.Windows.Media;
using GraphVisualizing.GraphProviders;
using GraphVisualizing.Model;

namespace GraphVisualizing.Controls
{
    public abstract class GraphViewBase<TX, TY> : GraphVisualHost<TX, TY> where TX : IComparable<TX>
    {
        private readonly List<GraphElement<TX, TY>> _elements = new List<GraphElement<TX, TY>>();
        protected GraphViewBase(IProjector<TX, TY> Projector) : base(Projector) { }

        public IGraphProvider<TX, TY> GraphProvider { get; set; }

        public void Refresh()
        {
            foreach (var element in _elements)
                HideElement(element);
            _elements.Clear();

            _elements.Add(new HorizontalAxisElement<TX, TY>(new Pen(Brushes.BlueViolet, 1), default(TY)));

            // Refreshing Graphs
            for (int i = 0; i < ActualWidth; i++)
            {
                IEnumerable<GraphSegmentElement<TX, TY>> graphSegments =
                    GraphProvider.GetGraphSegments(new Segment<TX>(Projector.GetX(i), Projector.GetX(i + 1)));
                foreach (var graphSegmentElement in graphSegments)
                    _elements.Add(graphSegmentElement);
            }

            foreach (var element in _elements)
                VisualizeElement(element);
        }

        /// <summary>Выводит визуальное представление элемента на карту</summary>
        /// <param name="Element">Элемент для визуализации</param>
        private void VisualizeElement(GraphElement<TX, TY> Element)
        {
            Element.AttachedVisual = Element.GetVisual(Projector);
            Element.ChangeVisualRequested += OnGraphElementChangeVisualRequested;
            AddVisual(Element.AttachedVisual);
        }

        /// <summary>Скрывает визуальное представление с карты</summary>
        /// <param name="Element">Элемент для сокрытия</param>
        private void HideElement(GraphElement<TX, TY> Element)
        {
            DeleteVisual(Element.AttachedVisual);
            Element.ChangeVisualRequested -= OnGraphElementChangeVisualRequested;
            Element.AttachedVisual = null;
        }

        /// <summary>Выполняет действия по перерисовке визуального отображения элемента карты</summary>
        private void OnGraphElementChangeVisualRequested(object Sender, EventArgs Args)
        {
            var element = (GraphElement<TX, TY>)Sender;
            HideElement(element);
            VisualizeElement(element);
        }
    }
}
