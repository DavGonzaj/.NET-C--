List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46, "Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
// Example Query - Prints all Stratovolcano eruptions
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(e => e.Type == "Stratovolcano");
PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");
// Execute Assignment Tasks here!

// Query 1: Find the first eruption that is in Chile and print the result.
List<Eruption> chileEruptions = eruptions.Where(e => e.Location == "Chile").ToList();
if (chileEruptions.Count > 0)
{
    Console.WriteLine("First eruption in Chile: ");
    Console.WriteLine(chileEruptions.First().ToString());
}

// Query 2: Find the first eruption from the "Hawaiian Is" location and print it. If none is found, print "No Hawaiian Is Eruption found."
List<Eruption> hawaiianEruptions = eruptions.Where(e => e.Location == "Hawaiian Is").ToList();
if (hawaiianEruptions.Count > 0)
{
    Console.WriteLine("First eruption in Hawaiian Is: ");
    Console.WriteLine(hawaiianEruptions.First().ToString());
}
else
{
    Console.WriteLine("No Hawaiian Is Eruption found.");
}

// Query 3: Find the first eruption from the "Greenland" location and print it. If none is found, print "No Greenland Eruption found."
List<Eruption> greenlandEruptions = eruptions.Where(e => e.Location == "Greenland").ToList();
if (greenlandEruptions.Count > 0)
{
    Console.WriteLine("First eruption in Greenland: ");
    Console.WriteLine(greenlandEruptions.First().ToString());
}
else
{
    Console.WriteLine("No Greenland Eruption found.");
}

// Query 4: Find the first eruption that is after the year 1900 AND in "New Zealand", then print it.
List<Eruption> nzEruptions = eruptions.Where(e => e.Year > 1900 && e.Location == "New Zealand").ToList();
if (nzEruptions.Count > 0)
{
    Console.WriteLine("First eruption after 1900 in New Zealand: ");
    Console.WriteLine(nzEruptions.First().ToString());
}
else
{
    Console.WriteLine("No New Zealand eruption found after 1900.");
}

// Query 5: Find all eruptions where the volcano's elevation is over 2000m and print them.
List<Eruption> highElevationEruptions = eruptions.Where(e => e.ElevationInMeters > 2000).ToList();
PrintEach(highElevationEruptions, "Eruptions with elevation over 2000m:");

// Query 6: Find all eruptions where the volcano's name starts with "L" and print them. Also print the number of eruptions found.
List<Eruption> lEruptions = eruptions.Where(e => e.Volcano.StartsWith("L")).ToList();
PrintEach(lEruptions, "Eruptions with volcano names starting with 'L':");

int lEruptionsCount = lEruptions.Count;
Console.WriteLine($"Number of eruptions found: {lEruptionsCount}");

// Query 7: Find the highest elevation, and print only that integer.
int highestElevation = eruptions.Max(e => e.ElevationInMeters);
Console.WriteLine($"Highest elevation: {highestElevation}");

// Query 8: Use the highest elevation variable to find and print the name of the Volcano with that elevation.
List<Eruption> highestElevationVolcanoes = eruptions.Where(e => e.ElevationInMeters == highestElevation).ToList();
if (highestElevationVolcanoes.Count > 0)
{
    Console.WriteLine($"Volcano(s) with the highest elevation ({highestElevation}m):");
    foreach (Eruption volcano in highestElevationVolcanoes)
    {
        Console.WriteLine(volcano.Volcano);
    }
}
else
{
    Console.WriteLine("No volcano found with the highest elevation.");
}

// Query 9: Print all Volcano names alphabetically.
List<string> sortedVolcanoNames = eruptions.Select(e => e.Volcano).OrderBy(name => name).ToList();

Console.WriteLine("Volcano names alphabetically:");
foreach (string volcanoName in sortedVolcanoNames)
{
    Console.WriteLine(volcanoName);
}

// Query 10: Print the sum of all the elevations of the volcanoes combined.
int totalElevation = eruptions.Sum(e => e.ElevationInMeters);
Console.WriteLine($"Sum of all elevations: {totalElevation}");

// Query 11: Print whether any volcanoes erupted in the year 2000.
bool anyEruptionIn2000 = eruptions.Any(e => e.Year == 2000);
Console.WriteLine($"Did any volcanoes erupt in 2000? {(anyEruptionIn2000 ? "Yes" : "No")}");

// Query 12: Find all stratovolcanoes and print just the first three.
List<Eruption> stratovolcanoes = eruptions.Where(e => e.Type == "Stratovolcano").Take(3).ToList();
Console.WriteLine("First three stratovolcanoes:");
foreach (Eruption volcano in stratovolcanoes)
{
    Console.WriteLine(volcano.ToString());
}

// Query 13: Print all eruptions that happened before the year 1000 CE alphabetically by Volcano name.
List<Eruption> eruptionsBefore1000 = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano).ToList();
Console.WriteLine("Eruptions before the year 1000 CE alphabetically by Volcano name:");
foreach (Eruption eruption in eruptionsBefore1000)
{
    Console.WriteLine(eruption.ToString());
}

// Query 14: Print only the volcano names for eruptions that happened before the year 1000 CE alphabetically by Volcano name.
List<string> volcanoNamesBefore1000 = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano).Select(e => e.Volcano).ToList();

Console.WriteLine("Volcano names for eruptions before the year 1000 CE alphabetically by Volcano name:");
foreach (string volcanoName in volcanoNamesBefore1000)
{
    Console.WriteLine(volcanoName);
}


// Helper method to print each item in a List or IEnumerable. This should remain at the bottom of your class!
static void PrintEach(IEnumerable<Eruption> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (Eruption item in items)
    {
        Console.WriteLine(item.ToString());
    }
}
