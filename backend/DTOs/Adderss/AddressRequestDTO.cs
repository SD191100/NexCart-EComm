namespace NexCart.DTOs.Adderss
{
    public class AddressRequestDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
    }
}
