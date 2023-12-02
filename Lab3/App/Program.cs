using Logics;

namespace App;


internal class Program
{
    private static void Main()
    {
        try
        {
            var lines = File
                .ReadAllLines("INPUT.TXT")
                .AsEnumerable()
                .GetEnumerator();
            var input = Parse(lines);
            var (power, frequencies) = Solver.Solve(input);
            File.WriteAllLines("OUTPUT.TXT", [
                power.ToString(),
            string.Join(' ', frequencies)
            ]);
        }
        catch (Exception exception)
        {
            File.WriteAllLines("OUTPUT.TXT", [exception.Message]);
        }
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
}
