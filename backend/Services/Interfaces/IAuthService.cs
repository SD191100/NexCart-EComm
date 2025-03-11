namespace NexCart.Services.Interfaces
{
    public interface IAuthService
    {
        public void Register(string email, string password, string role, string firstName, string lastName,string? contactNumber, string? companyName, string? gstNumber);
        public void Register(string email, string password, string role, string firstName, string lastName);
        //public void RegisterSeller(string email, string password, string role, string firstName, string lastName, string contactNumber, string companyName, string gstNumber);
        string Authenticate(string email, string password);
    }
}

