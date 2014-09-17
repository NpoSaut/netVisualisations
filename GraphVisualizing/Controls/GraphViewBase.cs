using System;
using System.Collections.Generic;
using GraphVisualizing.GraphProviders;
using GraphVisualizing.Model;

namespace GraphVisualizing.Controls
{
    public abstract class GraphViewBase<TX, TY> : GraphVisualHost<TX, TY> where TX : IComparable<TX>
    {
        protected GraphViewBase(IProjector<TX, TY> Projector) : base(Projector) { }

        public IGraphProvider<TX, TY> GraphProvider { get; set; }

        public void Refresh()
        {
            for (int i = 0; i < ActualWidth; i++)
            {
                IEnumerable<GraphSegmentElement<TX, TY>> elements = GraphProvider.GetGraphSegments(new Segment<TX>(Projector.GetX(i), Projector.GetX(i + 1)));
                foreach (var element in elements)
                {
                    VisualizeElement(element);
                }
            }
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
