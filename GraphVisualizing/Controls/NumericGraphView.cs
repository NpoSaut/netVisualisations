using GraphVisualizing.Projection;

namespace GraphVisualizing.Controls
{
    public class NumericGraphView : GraphViewBase<double, double>
    {
        public NumericGraphView()
            : base(new ScaleShiftProjector<double, double>(
                       new DoubleToDoubleConverter(), new DoubleToDoubleConverter())) { }
    }

    public class DoubleToDoubleConverter : IDoubleConverter<double>
    {
        public double GetDoubleValue(double Value) { return Value; }
        public double GetTypedValue(double DoubleValue) { return DoubleValue; }
    }
}
