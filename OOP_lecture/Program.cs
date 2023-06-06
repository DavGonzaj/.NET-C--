Student studentOne = new Student("Dan", "Wartsbaugh", "C#", 9001);
Person personOne = new Person("Max", "Rauchman");
Person personTwo = new Person();
{
    FirstName = "Zach",
    LastName = "Louk"
};
Student studentTwo = new Student("Tillman", "Pugh", "C#", 56);
Student studentThree = new Student("Zach", "Louk", "C#", 2);

Console.WriteLine(personOne.FullName());
Console.WriteLine(personTwo.FullName());
Console.WriteLine(studentOne.FullName());
Console.WriteLine(studentOne.CurrentStack);
Console.WriteLine(studentOne.StudentId);

List<Person> personList = new List<Person>()
{
    personOne,
    personTwo,
    studentOne
};

foreach (Person person in personList)
{
    Console.WriteLine(person.FullName());
}


List<Student> studentList = new List<Student>();
studentList.Add(studentOne);
studentList.Add(studentTwo);
studentList.Add(studentThree);

Lecture myLecture = new Lecture("C# OOP", 2, personOne, studentList);

Console.WriteLine("hello");
