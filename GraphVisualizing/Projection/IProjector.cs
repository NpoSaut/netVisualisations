using System;
using System.Windows;

namespace GraphVisualizing
{
    public interface IProjector<TX, TY>
    {
        Point Project(TX x, TY y);
        TX GetX(Double ScreenX);
        TY GetY(Double ScreenY);
    }
}
