namespace Labs;


public static class Lab1
{
    public static string Solve(string text)
    {
        var lines = text
            .ReplaceLineEndings("\n")
            .Split("\n")
            .AsEnumerable()
            .GetEnumerator();
        var input = Parse(lines);
        var result = IsSimilar(input.FirstCube, input.SecondCube)
            ? "YES"
            : "NO";
        return result.ToString();
    }


    private static Lab1Input Parse(IEnumerator<string> lines)
    {
        return new()
        {
            FirstCube = ParseCube("First cube"),
            SecondCube = ParseCube("Second cube")
        };


        Cube ParseCube(string name)
        {
            if (!lines.MoveNext())
            {
                throw new InvalidOperationException(
                    $"{name}: the input data does not contain the row of colors."
                );
            }

            var parts = lines.Current.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 6)
            {
                throw new InvalidOperationException(
                    $"{name}: expected 6 colors, but got {parts.Length}."
                );
            }

            var colors = parts.Select((p, i) => int.TryParse(p, out var color)
                ? color
                : throw new InvalidOperationException(
                    $"{name}: expected number as color №{i}."
                )
            )
                .ToArray();

            const int
                Front = 0,
                Back = 1,
                Top = 2,
                Bottom = 3,
                Left = 4,
                Right = 5;

            return new()
            {
                Front = colors[Front],
                Back = colors[Back],
                Top = colors[Top],
                Bottom = colors[Bottom],
                Left = colors[Left],
                Right = colors[Right]
            };
        }
    }


    private static bool IsSimilar(Cube firstCube, Cube secondCube)
    {
        var viewed = new HashSet<Cube>();
        return Calculate(firstCube);

        bool Calculate(Cube cube) =>
            viewed.Add(cube) && (
                cube == secondCube
                || Calculate(cube.RotatePositiveX())
                || Calculate(cube.RotateNegativeX())
                || Calculate(cube.RotatePositiveY())
                || Calculate(cube.RotateNegativeY())
                || Calculate(cube.RotatePositiveY())
                || Calculate(cube.RotateNegativeY())
            );
    }
}