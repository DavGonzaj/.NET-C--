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
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano");
PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");
// Execute Assignment Tasks here!

// Query 1: Find the first eruption that is in Chile and print the result.
List<Eruption> chileEruptions = eruptions.Where(c => c.Location == "Chile").ToList();
if (chileEruptions.Count > 0)
{
    Console.WriteLine("First eruption in Chile: ");
    Console.WriteLine(chileEruptions.First().ToString());
}

// Query 2: Find the first eruption from the "Hawaiian Is" location and print it. If none is found, print "No Hawaiian Is Eruption found."
List<Eruption> hawaiianEruptions = eruptions.Where(c => c.Location == "Hawaiian Is").ToList();
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
List<Eruption> greenlandEruptions = eruptions.Where(c => c.Location == "Greenland").ToList();
if (greenlandEruptions.Count > 0)
{
    Console.WriteLine("First eruption in Greenland: ");
    Console.WriteLine(greenlandEruptions.First().ToString());
}
else
{
    Console.WriteLine("No Greenland Eruption found.");
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
