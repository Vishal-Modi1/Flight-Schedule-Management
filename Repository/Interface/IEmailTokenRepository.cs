using DataModels.Models;


namespace Repository.Interface
{
    public interface IEmailTokenRepository
    {
        EmailToken Create(EmailToken emailTokens);
    }
}
