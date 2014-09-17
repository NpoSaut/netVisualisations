using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphVisualizing.Model;
using GraphVisualizing.View;

namespace GraphVisualizing.Controls
{
    /// <summary>Панель - хост для визуальных элементов</summary>
    public abstract class GraphVisualHost<TX, TY> : Panel
    {
        public IProjector<TX, TY> Projector { get; private set; }

        /// <summary>Визуальные элементы карты</summary>
        private readonly VisualCollection _visuals;

        protected GraphVisualHost(IProjector<TX, TY> Projector)
        {
            this.Projector = Projector;
            _visuals = new VisualCollection(this);
        }

        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        protected override Visual GetVisualChild(int index) { return _visuals[index]; }

        /// <summary>Добавляет визуальный элемент на карту</summary>
        /// <param name="v">Визуальный элемент</param>
        protected void AddVisual(GraphVisual v)
        {
            int index;
            for (index = 0; index < _visuals.Count; index++)
                if (((GraphVisual)_visuals[index]).ZIndex > v.ZIndex) break;

            _visuals.Insert(index, v);
        }

        /// <summary>Удаляет визуальный элемент с карты</summary>
        /// <param name="v"></param>
        protected void DeleteVisual(GraphVisual v) { _visuals.Remove(v); }

        /// <summary>Проверяет попадание мыши по элементу карты</summary>
        public GraphVisual HitVisual(Point point)
        {
            return VisualTreeHelper.HitTest(this, point).VisualHit as GraphVisual;
        }

        private IMouseEventReceiver SafeGetMouseEventReceiver(GraphVisual Visual) { return Visual != null ? Visual.MouseEventReceiver : null; }

        private IMouseEventReceiver _mouseMoveOn;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var newMouseMoveOnElement = SafeGetMouseEventReceiver(HitVisual(e.GetPosition(this)));
            if (!Equals(_mouseMoveOn, newMouseMoveOnElement))
            {
                if (_mouseMoveOn != null) _mouseMoveOn.OnMouseLeave(e);
                if (newMouseMoveOnElement != null) newMouseMoveOnElement.OnMouseEnter(e);
            }
            if (_mouseMoveOn != null) _mouseMoveOn.OnMouseMove(e);
            _mouseMoveOn = newMouseMoveOnElement;
            base.OnMouseMove(e);
        }

        private MouseButton? _clickButton;
        private IMouseEventReceiver _mouseDownOn;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            _clickButton = e.ChangedButton;
            _mouseDownOn = SafeGetMouseEventReceiver(HitVisual(e.GetPosition(this)));
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_clickButton == e.ChangedButton)
            {
                var mouseUpOnElement = SafeGetMouseEventReceiver(HitVisual(e.GetPosition(this)));
                if (mouseUpOnElement != null && Equals(_mouseDownOn, mouseUpOnElement))
                {
                    mouseUpOnElement.OnMouseClick(e);
                }
            }
            _clickButton = null;
            base.OnMouseUp(e);
        }
    }
}