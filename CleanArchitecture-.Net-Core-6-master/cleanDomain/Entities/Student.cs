namespace cleanDomain.Entities
{
    public class Student : EntityBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }

        public Student()
        {
        }

        public Student(string firstName, string lastName, int age, string email, int sexId)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
            if(sexId == 1)
                SexId = Sex.Male.Id;
            else if(sexId == 2)
                SexId = Sex.Female.Id;
            else
                SexId = Sex.NotSpecified.Id;
        }

        public int SetSex(int sexId)
        {
            int id = 3;
            if (sexId == 1)
                id = Sex.Male.Id;
            else if(sexId == 2)
                id = Sex.Female.Id;
            else
                id = Sex.NotSpecified.Id;
            
            return id;
        }
    }
}