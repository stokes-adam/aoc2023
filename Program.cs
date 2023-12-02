var numbers = new Dictionary<string, string>
{
    ["one"] = "1",
    ["two"] = "2",
    ["three"] = "3",
    ["four"] = "4",
    ["five"] = "5",
    ["six"] = "6",
    ["seven"] = "7",
    ["eight"] = "8",
    ["nine"] = "9"
};

var output = File.ReadAllLines("input.txt")
    .Select(line =>
    {
        var wordBuilder = line;
        var numberBuffer = string.Empty;

        foreach (var c in line)
        {
            if (char.IsDigit(c))
            {
                numberBuffer += c;
                wordBuilder = wordBuilder[1..];
                continue;
            }

            foreach (var (word, number) in numbers)
            {
                if (wordBuilder.StartsWith(word))
                {
                    numberBuffer += number;
                }
            }

            Console.WriteLine(new { line, c, wordBuilder, numberBuffer });

            if (wordBuilder.Length == 0) continue;

            wordBuilder = wordBuilder[1..];
        }

        return numberBuffer;
    })
    .Select(charArray => $"{charArray.First()}{charArray.Last()}")
    .Select(twonums =>
    {
        Console.WriteLine("two nums: " + twonums);
        return twonums;
    })
    .Select(int.Parse)
    .Sum();

Console.WriteLine(output);
