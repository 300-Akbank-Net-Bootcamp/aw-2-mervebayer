using Vb.Data.Entity;

namespace Vb.Api.Dtos
{
    public class AccountDto
    {
        public int CustomerId { get; set; }
        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }

    }
}