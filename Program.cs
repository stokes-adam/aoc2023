var output = File.ReadAllLines("input.txt")
    .Select(line =>
    {
        var keyRes = line.Split(":");

        return new
        {
            Id = int.Parse(keyRes.First().Split(" ").Last()),
            Sets = keyRes.Last()
                .Split(";", StringSplitOptions.TrimEntries)
                .Select(game => game.Split(",", StringSplitOptions.TrimEntries)
                    .Select(contentsString =>
                    {
                        var contents = contentsString.Split(" ");

                        return new
                        {
                            Count = int.Parse(contents.First()),
                            Color = contents.Last()
                        };
                    }))
        };
    })
    .Select(line =>
    {
        var largestFound = new Dictionary<string, int>();

        foreach (var sets in line.Sets)
        {
            foreach (var set in sets)
            {
                if (set.Count > largestFound.GetValueOrDefault(set.Color))
                    largestFound[set.Color] = set.Count;
            }
        }

        return new
        {
            line.Id,
            MinimumValues = largestFound
        };
    })
    .Select(game => game.MinimumValues.Aggregate(1, (acc, x) => acc * x.Value))
    .Sum();
    
    
Console.WriteLine(output);