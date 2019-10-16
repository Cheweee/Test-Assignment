using System.Runtime.Serialization;

namespace Test.Data.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }
    }

    public class InstructorGetOptions
    {
        public int? Id { get; set; }
        
        public string Search { get; set; }
    }
}