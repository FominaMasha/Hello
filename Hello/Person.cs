namespace Hello
{
    public class Person
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }

        public Person(string surname, string name, string company, string phone)
        {
            Surname = surname;
            Name = name;
            Company = company;
            Phone = phone;
        }
    }
}
