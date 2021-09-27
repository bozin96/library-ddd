
namespace Library.Application.Dtos.Patrons
{
    public class PatronResponse
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public PatronResponse()
        {

        }

        public PatronResponse(string firstName, string middleName, string lastName, string email, string type)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Type = type;
        }
    }
}
