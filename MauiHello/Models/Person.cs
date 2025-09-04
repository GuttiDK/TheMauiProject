namespace MauiHello.Models
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }

        public Person() { }

        public Person(string name, string address, int age)
        {
            Name = name;
            Address = address;
            Age = age;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address}, Age: {Age}";
        }
    }
}