namespace Labs;


internal class Point(int x, int y)
{
    public int X { get; } = x;

    public int Y { get; } = y;


    public double DistanceTo(Point other)
    {
        int dx = X - other.X;
        int dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}