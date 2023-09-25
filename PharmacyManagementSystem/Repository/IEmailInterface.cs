namespace PharmacyManagementSystem.Repository
{
    public interface IEmailInterface
    {
        public void SendEmail(string toEmail, string aubject, string body);
    }
}
