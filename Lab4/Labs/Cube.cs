namespace Labs;


internal record class Cube
{
    public required int Back { get; init; }

    public required int Front { get; init; }

    public required int Right { get; init; }

    public required int Left { get; init; }

    public required int Top { get; init; }

    public required int Bottom { get; init; }


    public Cube RotatePositiveX() => this with
    {
        Right = Bottom,
        Left = Top,
        Top = Right,
        Bottom = Left
    };


    public Cube RotateNegativeX() => this with
    {
        Right = Top,
        Left = Bottom,
        Top = Left,
        Bottom = Right
    };


    public Cube RotatePositiveY() => this with
    {
        Back = Top,
        Front = Bottom,
        Top = Front,
        Bottom = Back
    };


    public Cube RotateNegativeY() => this with
    {
        Back = Bottom,
        Front = Top,
        Top = Back,
        Bottom = Front
    };


    public Cube RotatePositiveZ() => this with
    {
        Left = Front,
        Back = Left,
        Right = Back,
        Front = Right
    };


    public Cube RotateNegativeZ() => this with
    {
        Left = Front,
        Back = Right,
        Right = Front,
        Front = Right
    };
}