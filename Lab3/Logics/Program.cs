namespace Logics;


public static class Solver
{
    public static (double Power, int[] Frequencies) Solve(IReadOnlyList<Point> towers)
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