using System;
using GraphVisualizing.Projection;

namespace GraphVisualizing.Controls
{
    public class TimeGraphView : GraphViewBase<DateTime, Double>
    {
        public TimeGraphView()
            : base(new ScaleShiftProjector<DateTime, double>(
                       new DateTimeToDoubleConverter(),
                       new DoubleToDoubleConverter())) { }
    }

    public class DateTimeToDoubleConverter : IDoubleConverter<DateTime>
    {
        private static DateTime t0 = new DateTime(1980, 1, 1);

        public double GetDoubleValue(DateTime Value) { return (Value - t0).TotalSeconds; }
        public DateTime GetTypedValue(double DoubleValue) { return t0.AddSeconds(DoubleValue); }
    }
}
