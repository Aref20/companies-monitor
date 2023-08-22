namespace MSGCompaniesMonitor.Mail
{
    public interface IEmailSender
    { 
        void SendEmail(Message message);
    }
}
