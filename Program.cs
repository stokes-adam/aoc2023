var lines = File.ReadAllLines("input.txt");

// per number buffer
var numberBuffer = string.Empty;
var coordBuffer = new List<(int x, int y)>();
var isValidNumber = false;

// value
var coordsToNumbers = new Dictionary<string, HashSet<int>>();

// find
for (var x = 0; x < lines.Length; x++)
{
    for (var y = 0; y < lines[x].Length; y++)
    {
        var currentChar = lines[x][y];

        var endOfNumber = !char.IsDigit(currentChar);
        
        if (endOfNumber)
        {
            AddAndClear();
            continue;
        }

        numberBuffer += currentChar;

        var positions = new (int xSeeker, int ySeeker)[]
        {
            (x - 1, y - 1),
            (x - 1, y    ),
            (x - 1, y + 1),
            (x    , y - 1),
            (x    , y + 1),
            (x + 1, y - 1),
            (x + 1, y    ),
            (x + 1, y + 1),
        };

        foreach (var (xSeeker, ySeeker) in positions)
        {
            if (!Validate(xSeeker, ySeeker)) continue;
            
            coordBuffer.Add((xSeeker, ySeeker));
            isValidNumber = true;
        }
    }

    AddAndClear();
}

bool Validate(int xSeeker, int ySeeker)
{
    if (xSeeker > lines[0].Length - 1 || xSeeker < 0) return false;
    if (ySeeker > lines   .Length - 1 || ySeeker < 0) return false;

    return lines[xSeeker][ySeeker] == '*';
}

void AddAndClear()
{
    if (isValidNumber)
    {
        var number = int.Parse(numberBuffer);
        
        foreach (var (x, y) in coordBuffer)
        {
            var key = x + "," + y;
            
            if (!coordsToNumbers.ContainsKey(key))
                coordsToNumbers[key] = new();
            
            coordsToNumbers[key].Add(number);
        }
    }

    ResetBuffers();
}

void ResetBuffers()
{
    numberBuffer = string.Empty;
    coordBuffer = new();
    isValidNumber = false;
}
