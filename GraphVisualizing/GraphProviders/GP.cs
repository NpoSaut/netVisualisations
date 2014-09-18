using System;
using System.Collections.Generic;
using System.Windows.Media;
using GraphVisualizing.Model;

namespace GraphVisualizing.GraphProviders
{
    public class GP<TX, TY> : IGraphProvider<TX, TY> where TX : IComparable<TX>
    {
        public GP(Func<TX, TY> F, Pen GraphPen)
        {
            this.GraphPen = GraphPen;
            f = F;
        }

        public Func<TX, TY> f { get; set; }

        public Pen GraphPen { get; set; }

        public IEnumerable<GraphSegmentElement<TX, TY>> GetGraphSegments(Segment<TX> OnSegment)
        {
            return new[]
                   {
                       new LineSegment<TX, TY>(OnSegment, f(OnSegment.Start), f(OnSegment.End), GraphPen)
                   };
        }
    }
}
