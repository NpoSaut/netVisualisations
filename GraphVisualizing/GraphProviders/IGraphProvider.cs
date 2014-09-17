using System;
using System.Collections.Generic;
using GraphVisualizing.Model;

namespace GraphVisualizing.GraphProviders
{
    public interface IGraphProvider<TX, TY> where TX : IComparable<TX>
    {
        IEnumerable<GraphSegmentElement<TX, TY>> GetGraphSegments(Segment<TX> OnSegment);
    }
}
