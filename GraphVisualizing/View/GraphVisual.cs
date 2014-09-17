using System.Windows.Media;
using GraphVisualizing.Model;

namespace GraphVisualizing.View
{
    public class GraphVisual : DrawingVisual
    {
        public GraphVisual(IMouseEventReceiver MouseEventReceiver, int ZIndex = 0)
        {
            this.MouseEventReceiver = MouseEventReceiver;
            this.ZIndex = ZIndex;
        }

        public IMouseEventReceiver MouseEventReceiver { get; set; }

        /// <summary>Z-индекс визуального элемента</summary>
        public int ZIndex { get; private set; }
    }
}
