namespace AllProject.Server.Services.Interfaces
{
    public interface IEmailService
    {

        public Task<bool> SendEmail(string to, string subJect, string message);
    }
}
