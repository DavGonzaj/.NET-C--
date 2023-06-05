void OneToTwoFityFive()
{
    for (int i = 1; i <= 255; i++)
    {
        Console.WriteLine(i);
    }
}

OneToTwoFityFive();









void FizzBuzz()
{
    for (int i = 1; i <= 100; i++)
    {
        bool isDivisibleBy3 = i % 3 == 0;
        bool isDivisibleBy5 = i % 5 == 0;
        bool isDivisibleBy3And5 = isDivisibleBy3 && isDivisibleBy5;
        if (isDivisibleBy3And5)
        {
            Console.WriteLine("FizzBuzz");
        }
        else if (isDivisibleBy3)
        {
            Console.WriteLine("Fizz");
        }
        else if (isDivisibleBy5)
        {
            Console.WriteLine("Buzz");
        }
        else
        {
            Console.WriteLine(i);
        }
    }
}

FizzBuzz();