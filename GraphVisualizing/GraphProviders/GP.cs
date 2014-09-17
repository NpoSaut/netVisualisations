using System;
using System.Collections.Generic;
using System.Windows.Media;
using GraphVisualizing.Model;

namespace GraphVisualizing.GraphProviders
{
    public class GP : IGraphProvider<double, double>
    {
        public GP(Func<double, double> F, Pen GraphPen)
        {
            this.GraphPen = GraphPen;
            f = F;
        }

        public Func<Double, Double> f { get; set; }

        public Pen GraphPen { get; set; }

        public IEnumerable<GraphSegmentElement<double, double>> GetGraphSegments(Segment<double> OnSegment)
        {
            return new[]
                   {
                       new LineSegment<double, double>(OnSegment, f(OnSegment.Start), f(OnSegment.End), GraphPen)
                   };
        }
    }
}
