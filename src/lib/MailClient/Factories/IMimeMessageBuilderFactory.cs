using MimeKit;
using MailClient.Builders;

namespace MailClient.Factories
{
    public interface IMimeMessageBuilderFactory
    {
        IMimeMessageBuilder Create();
    }
}