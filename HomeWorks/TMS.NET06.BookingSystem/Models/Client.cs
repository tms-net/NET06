namespace TMS.NET06.BookingSystem
{
    public class Client
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public ContactInfo ContactInformation { get; set; }
    }

    public class ContactInfo
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookId { get; set; }
    }
}