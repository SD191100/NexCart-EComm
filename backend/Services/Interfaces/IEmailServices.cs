namespace NexCart.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string toEmail, string subject, string body);
    }
}

