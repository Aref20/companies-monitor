namespace CompaniesMonitor.Shared.Mail
{
    public interface IEmailSender
    { 
        void SendEmail(Message message);
    }
}
