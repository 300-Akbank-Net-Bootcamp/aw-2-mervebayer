namespace Vb.Api.Dtos
{
    public class ContactDto
    {
        public int CustomerId { get; set; }
        public string ContactType { get; set; }
        public string Information { get; set; }
        public bool IsDefault { get; set; }
    }
}