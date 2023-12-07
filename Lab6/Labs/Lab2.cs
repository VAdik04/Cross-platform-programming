namespace Labs;


public static class Lab2
{
    public static string Solve(string text)
    {
        var lines = text
            .ReplaceLineEndings("\n")
            .Split("\n")
            .AsEnumerable()
            .GetEnumerator();
        var input = Parse(lines);
        var result = FindMaxRectangleSize(input);
        return result.ToString();
    }


    private static bool[,] Parse(IEnumerator<string> lines)
    {
        if (!lines.MoveNext())
        {
            throw new InvalidOperationException(
                "The input data does not have a dimension line."
            );
        }

        var sizeParts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (!(
            sizeParts is [var nText, var mText, var kText]
            && int.TryParse(nText, out var n)
            && n > 0
            && int.TryParse(mText, out var m)
            && m > 0
            && int.TryParse(kText, out var k)
            && k > 0
        ))
        {
            throw new InvalidOperationException(
                "Invalid size line."
            );
        }

        bool[,] field = new bool[n, m];

        for (int i = 0; i < k; i++)
        {
            if (!lines.MoveNext())
            {
                throw new InvalidOperationException(
                    $"Ship №{i + 1}: the line is missing."
                );
            }
            var shipParts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!(
                shipParts is [var topText, var leftText, var bottomText, var rightText]
                && int.TryParse(topText, out var top)
                && top > 0 && top <= n
                && int.TryParse(leftText, out var left)
                && left > 0 && left <= m
                && int.TryParse(bottomText, out var bottom)
                && bottom > 0 && bottom <= n && bottom >= top
                 && int.TryParse(rightText, out var right)
                && right > 0 && right <= m && right >= left
            ))
            {
                throw new InvalidOperationException(
                    $"Ship №{i + 1}: invalid data."
                );
            }

            for (int row = top; row <= bottom; ++row)
            {
                for (int column = left; column <= right; ++column)
                {
                    if (field[row - 1, column - 1])
                    {
                        throw new InvalidOperationException(
                            $"Ship №{i + 1}: is on an occupied cell ({row}, {column})."
                        );
                    }
                    field[row - 1, column - 1] = true;
                }
            }
        }

        return field;
    }


    private static int FindMaxRectangleSize(bool[,] field)
    {
        var n = field.GetLength(0);
        var m = field.GetLength(1);


        const int None = -1;

        var maxWidths = new int[n, m];
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < m; ++j)
            {
                maxWidths[i, j] = None;
            }
        }
        for (int i = 0; i < n; ++i)
        {
            GetMaxWidth(i, 0);
        }


        var maxSize = 0;
        for (var j = 0; j < m; ++j)
        {
            for (var i = 0; i < n; ++i)
            {
                if (maxWidths[i, j] == 0)
                {
                    continue;
                }

                var currentWidth = maxWidths[i, j];
                var currentStartI = i;
                var currentI = i + 1;
                while (currentI < n)
                {
                    if (maxWidths[currentI, j] >= currentWidth)
                    {
                        ++currentI;
                        continue;
                    }

                    var currentSize = currentWidth * (currentI - currentStartI);
                    if (currentSize > maxSize)
                    {
                        maxSize = currentSize;
                    }
                    if (maxWidths[currentI, j] == 0)
                    {
                        break;
                    }

                    currentWidth = maxWidths[currentI, j];
                    currentStartI = currentI;
                    ++currentI;
                }

                var lastSize = currentWidth * (currentI - currentStartI);
                if (lastSize > maxSize)
                {
                    maxSize = lastSize;
                }
            }
        }


        return maxSize;


        int GetMaxWidth(int i, int j)
        {
            if (j == m)
            {
                return 0;
            }
            if (maxWidths[i, j] != None)
            {
                return maxWidths[i, j];
            }
            if (!EmptyCellForShip(i, j))
            {
                return maxWidths[i, j] = 0;
            }
            return maxWidths[i, j] = 1 + GetMaxWidth(i, j + 1);
        }


        bool EmptyCellForShip(int i, int j)
        {
            if (!(i >= 0 && i < n && j >= 0 && j < m))
            {
                return false;
            }
            for (int di = -1; di <= 1; ++di)
            {
                for (int dj = -1; dj <= 1; ++dj)
                {
                    int nearI = i + di;
                    int nearJ = j + dj;
                    if (
                        nearI >= 0 && nearI < n
                        && nearJ >= 0 && nearJ < m
                        && field[nearI, nearJ]
                    )
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}