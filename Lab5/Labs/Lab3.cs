namespace Labs;


public static class Lab3
{
    public static string Solve(string text)
    {
        var lines = text
            .ReplaceLineEndings("\n")
            .Split("\n")
            .AsEnumerable()
            .GetEnumerator();
        var input = Parse(lines);
        var (power, frequencies) = Solve(input.ToList());
        return string.Join(
            Environment.NewLine,
            power,
            string.Join(' ', frequencies)
        );
    }


    private static Point[] Parse(IEnumerator<string> lines)
    {
        if (!lines.MoveNext())
        {
            throw new InvalidOperationException(
                "The input data does not have a dimension line."
            );
        }

        if (!(
            int.TryParse(lines.Current, out var numberTransmitters)
            && numberTransmitters >= 1
        ))
        {
            throw new InvalidOperationException(
                "Invalid number of transmitters."
            );
        }

        var points = new Point[numberTransmitters];

        for (int i = 0; i < numberTransmitters; ++i)
        {
            if (!lines.MoveNext())
            {
                throw new InvalidOperationException(
                    $"Transmitter №{i + 1}: the line is missing."
                );
            }
            var pointParts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!(
                pointParts is [var xText, var yText]
                && int.TryParse(xText, out var x)
                && int.TryParse(yText, out var y)
            ))
            {
                throw new InvalidOperationException(
                    $"Transmitter №{i + 1}: invalid data."
                );
            }

            points[i] = new(x, y);
        }

        return points;
    }


    private static (double Power, int[] Frequencies) Solve(IReadOnlyList<Point> towers)
    {
        var size = towers.Count;

        int[] resultFrequencies = null!;
        var left = 0D;
        var right = 1e9;

        while (right - left > 1e-8)
        {
            const int
                NoneFrequency = 0,
                FirstFrequency = 1,
                SecondFrequency = 2;

            var middle = (left + right) / 2D;
            var frequencies = new int[size];
            var indices = new Stack<int>();
            var good = true;

            for (var i = 0; good && i < size; ++i)
            {
                if (frequencies[i] != NoneFrequency)
                {
                    continue;
                }

                indices.Push(i);
                frequencies[i] = FirstFrequency;

                while (good && indices.TryPop(out var currentI))
                {
                    for (var j = 0; j < size; ++j)
                    {
                        if (
                            currentI == j
                            || towers[currentI].DistanceTo(towers[j]) >= middle
                        )
                        {
                            continue;
                        }

                        if (frequencies[j] == NoneFrequency)
                        {
                            frequencies[j] = frequencies[currentI] == FirstFrequency
                                ? SecondFrequency
                                : FirstFrequency;
                            indices.Push(j);
                        }
                        else if (frequencies[currentI] == frequencies[j])
                        {
                            good = false;
                            break;
                        }
                    }
                }
            }

            if (good)
            {
                left = middle;
                resultFrequencies = frequencies;
            }
            else
            {
                right = middle;
            }
        }

        return (left / 2D, resultFrequencies);
    }
}