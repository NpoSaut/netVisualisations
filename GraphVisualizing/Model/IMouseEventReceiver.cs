using System.Windows.Input;

namespace GraphVisualizing.Model
{
    public interface IMouseEventReceiver
    {
        void OnMouseClick(MouseButtonEventArgs ChangedButton);
        void OnMouseMove(MouseEventArgs MouseEventArgs);
        void OnMouseEnter(MouseEventArgs MouseEventArgs);
        void OnMouseLeave(MouseEventArgs MouseEventArgs);
    }
}
