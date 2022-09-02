using DTO.Domain;
using MimeKit;

namespace MailClient.Builders
{
    public interface IMimeMessageBuilder
    {
        IMimeMessageBuilder SetMailConfig(MailDto config);
        MimeMessage Build();
    }
}