using Library.Core.SyncedAggregate;

namespace Library.Application.Dtos.Patrons
{
    public class PatronAddRequest : BaseRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public PatronType Type { get; set; }

        public PatronAddRequest()
        {

        }
    }
}
