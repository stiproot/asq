using DTO.Notification;
using System.Collections.Generic;

namespace MailClient.Providers
{
    public static class MailSubjectProvider
    {
        private static readonly IDictionary<NotificationTypeEnu, string> _subjectMappings
            = new Dictionary<NotificationTypeEnu, string>()
            {
                { NotificationTypeEnu.WELCOME, "Welcome to ASQ :)" },
                { NotificationTypeEnu.EMAIL_CONFIRMATION, "Email Confirmation" },
                { NotificationTypeEnu.MEETING_REGISTRATION, "New ASQ Meeting Participant" },
                { NotificationTypeEnu.MEETING_CREATION, "ASQ Meeting Successfully Created" }
            };

        public static string GetSubject(NotificationTypeEnu typeEnu)
            => _subjectMappings[typeEnu];
    }
}