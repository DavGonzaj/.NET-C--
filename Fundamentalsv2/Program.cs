// #Three Basic Arrays

// @Create an integer array with the values 0 through 9 inside.

int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


// Create a string array with the names "Tim", "Martin", "Nikki", and "Sara".
string[] names = { "Tim", "Martin", "Nikki", "Sara" };


// Create a boolean array of length 10. Then fill it with alternating true/false values, starting with true. (Tip: do this using logic! Do not hard-code the values in!)
bool[] boolArray = new bool[10];
bool value = true;

for (int i = 0; i < boolArray.Length; i++)
{
    boolArray[i] = value;
    value = !value;
}

// List of Flavors

// Create a string List of ice cream flavors that holds at least 5 different flavors. (Feel free to add more than 5!)
List<string> iceCreamFlavors = new List<string>()
{
    "Lemon","Chocolate","Strawberry","Vanilla","Coffee"
};

// Output the length of the List after you added the flavors.
Console.WriteLine(iceCreamFlavors.Count);


//Output the third flavor in the List.
Console.WriteLine(iceCreamFlavors[2]);


//Now remove the third flavor using its index location.
iceCreamFlavors.RemoveAt(2);
Console.WriteLine(iceCreamFlavors[2]);

//Output the length of the List again. It should now be one fewer.
Console.WriteLine(iceCreamFlavors.Count);


//User Dictionary

// Create a dictionary that will store string keys and string values.
Dictionary<string, string> nameFlavorDictionary = new Dictionary<string, string>();
Random random = new Random();
// Add key/value pairs to the dictionary where:
// Each key is a name from your names array (this can be done by hand or using logic)
// Each value is a randomly selected flavor from your flavors List (remember Random from earlier?)
foreach (string name in names)
{
    int randomIndex = random.Next(iceCreamFlavors.Count);
    string randomFlavor = iceCreamFlavors[randomIndex];
    nameFlavorDictionary.Add(name, randomFlavor);
}
// Loop through the dictionary and print out each user's name and their associated ice cream flavor.
// Printing the name-flavor pairs
foreach (KeyValuePair<string, string> pair in nameFlavorDictionary)
{
    Console.WriteLine(pair.Key + ": " + pair.Value);
}