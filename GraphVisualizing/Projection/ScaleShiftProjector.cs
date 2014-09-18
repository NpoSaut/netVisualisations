using System.Windows;

namespace GraphVisualizing.Projection
{
    public class ScaleShiftProjector<TX, TY> : IProjector<TX, TY>
    {
        private readonly IDoubleConverter<TX> _xConverter;
        private readonly IDoubleConverter<TY> _yConverter;

        public ScaleShiftProjector(IDoubleConverter<TX> XConverter, IDoubleConverter<TY> YConverter)
        {
            _xConverter = XConverter;
            _yConverter = YConverter;
        }

        public TX X0 { get; set; }
        public TY Y0 { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }

        public Point Project(TX x, TY y)
        {
            return new Point(
                ScaleX * (_xConverter.GetDoubleValue(x) - _xConverter.GetDoubleValue(X0)),
                -ScaleY * (_yConverter.GetDoubleValue(y) - _yConverter.GetDoubleValue(Y0)));
        }


        public TX GetX(double ScreenX)
        {
            double onScreenX0 = _xConverter.GetDoubleValue(X0);
            double shiftedScaledValue = ScreenX / ScaleX + onScreenX0;
            return _xConverter.GetTypedValue(shiftedScaledValue);
        }

        public TY GetY(double ScreenY) { return _yConverter.GetTypedValue((ScreenY + _yConverter.GetDoubleValue(Y0)) / ScaleY); }
    }
}
