namespace Vb.Api.Dtos
{
    public class CustomerDto
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}