var lines = File.ReadAllLines("input.txt");

var xLastIndex = lines[0].Length - 1;
var yLastIndex = lines.Length - 1;

var validNumbers = new List<int>();

var numberBuffer = string.Empty;
var isValidNumber = false;

for (var x = 0; x < lines.Length; x++)
{
    for (var y = 0; y < lines[x].Length; y++)
    {
        var ch = lines[x][y];
        
        if (!char.IsDigit(ch))
        {
            if (isValidNumber)
                validNumbers.Add(int.Parse(numberBuffer));
            
            numberBuffer = string.Empty;
            isValidNumber = false;
            continue;
        }

        numberBuffer += ch;

        var isValidCoordinate =
            (x > 0          && y > 0 &&          lines[x - 1][y - 1].IsValidatorSymbol()) || // check top left
            (x > 0          &&                   lines[x - 1][y    ].IsValidatorSymbol()) || // check top
            (x > 0          && y < yLastIndex && lines[x - 1][y + 1].IsValidatorSymbol()) || // check top right
            (                  y > 0          && lines[x    ][y - 1].IsValidatorSymbol()) || // check left
            (                  y < yLastIndex && lines[x    ][y + 1].IsValidatorSymbol()) || // check right
            (x < xLastIndex && y > 0 &&          lines[x + 1][y - 1].IsValidatorSymbol()) || // check bottom left
            (x < xLastIndex &&                   lines[x + 1][y    ].IsValidatorSymbol()) || // check bottom
            (x < xLastIndex && y < yLastIndex && lines[x + 1][y + 1].IsValidatorSymbol()); // check bottom right

        if (isValidCoordinate)
            isValidNumber = true;
    }
}

var output = validNumbers.Sum();

Console.WriteLine(output);


static class ArrayExtensions
{
    public static bool IsValidatorSymbol(this char c) => !(char.IsDigit(c) || c == '.');
}
