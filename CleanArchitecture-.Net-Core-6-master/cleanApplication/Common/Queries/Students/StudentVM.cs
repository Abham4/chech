using cleanDomain.Enumerations;

namespace cleanApplication.Common.Queries.Students
{
    public class StudentVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}