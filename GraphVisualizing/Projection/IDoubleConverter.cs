namespace GraphVisualizing
{
    public interface IDoubleConverter<TValue>
    {
        double GetDoubleValue(TValue Value);
        TValue GetTypedValue(double DoubleValue);
    }
}