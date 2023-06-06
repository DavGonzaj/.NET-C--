//1. Iterate and print values
static void PrintList(List<string> MyList)
{
    foreach (string item in MyList)
    {
        Console.WriteLine(item);
    }
}
List<string> TestStringList = new List<string>() { "Harry", "Steve", "Carla", "Jeanne" };
PrintList(TestStringList);


//2. Print Sum
static void SumOfNumbers(List<int> IntList)
{
    int sum = 0;
    foreach (int number in IntList)
    {
        sum += number;
    }
    Console.WriteLine("The sum is: " + sum);
}
List<int> TestIntList = new List<int>() { 2, 7, 12, 9, 3 };
// You should get back 33 in this example
SumOfNumbers(TestIntList);


//3. Find Max
static int FindMax(List<int> IntList)
{
    int max = IntList[0];
    for (int i = 1; i < IntList.Count; i++)
    {
        if (IntList[i] > max)
        {
            max = IntList[i];
        }
    }
    return max;
}
List<int> TestIntList2 = new List<int>() { -9, 12, 10, 3, 17, 5 };
// You should get back 17 in this example
FindMax(TestIntList2);
Console.WriteLine(FindMax(TestIntList2));


//4. Square the Values
static List<int> SquareValues(List<int> IntList)
{
    List<int> squaredList = new List<int>();
    foreach (int number in IntList)
    {
        int squaredValue = number * number;
        squaredList.Add(squaredValue);
    }
    return squaredList;
}
List<int> TestIntList3 = new List<int>() { 1, 2, 3, 4, 5 };
// You should get back [1,4,9,16,25], think about how you will show that this worked
SquareValues(TestIntList3);
List<int> squaredValuesList = SquareValues(TestIntList3);
foreach (int value in squaredValuesList)
{
    Console.WriteLine(value);
}


//5. Replace Negative Numbers with 0
static int[] NonNegatives(int[] IntArray)
{
    for (int i = 0; i < IntArray.Length; i++)
    {
        if (IntArray[i] < 0)
        {
            IntArray[i] = 0;
        }
    }
    return IntArray;
}
int[] TestIntArray = new int[] { -1, 2, 3, -4, 5 };
// You should get back [0,2,3,0,5], think about how you will show that this worked
NonNegatives(TestIntArray);
int[] resultArray = NonNegatives(TestIntArray);
foreach (int value in resultArray)
{
    Console.WriteLine(value);
}


//6. Print Dictionary
static void PrintDictionary(Dictionary<string, string> MyDictionary)
{
    foreach (KeyValuePair<string, string> pair in MyDictionary)
    {
        Console.WriteLine(pair.Key + ": " + pair.Value);
    }
}
Dictionary<string, string> TestDict = new Dictionary<string, string>();
TestDict.Add("HeroName", "Iron Man");
TestDict.Add("RealName", "Tony Stark");
TestDict.Add("Powers", "Money and intelligence");
PrintDictionary(TestDict);


//7. Find Key
static bool FindKey(Dictionary<string, string> MyDictionary, string SearchTerm)
{
    return MyDictionary.ContainsKey(SearchTerm);
}
// Use the TestDict from the earlier example or make your own
// This should print true
Console.WriteLine(FindKey(TestDict, "RealName"));
// This should print false
Console.WriteLine(FindKey(TestDict, "Name"));


//8. Generate a Dictionary
// Ex: Given ["Julie", "Harold", "James", "Monica"] and [6,12,7,10], return a dictionary
// {
//	"Julie": 6,
//	"Harold": 12,
//	"James": 7,
//	"Monica": 10
// } 
static Dictionary<string, int> GenerateDictionary(List<string> Names, List<int> Numbers)
{
    Dictionary<string, int> resultDict = new Dictionary<string, int>();

    for (int i = 0; i < Names.Count; i++)
    {
        string name = Names[i];
        int number = Numbers[i];
        resultDict[name] = number;
    }

    return resultDict;
}
// We've shown several examples of how to set your tests up properly, it's your turn to set it up!
// Your test code here

List<string> namesList = new List<string>() { "Julie", "Harold", "James", "Monica" };
List<int> numbersList = new List<int>() { 6, 12, 7, 10 };

Dictionary<string, int> generatedDict = GenerateDictionary(namesList, numbersList);

foreach (KeyValuePair<string, int> pair in generatedDict)
{
    Console.WriteLine(pair.Key + ": " + pair.Value);
}