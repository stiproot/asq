using DTO.Notification;
using System.Collections.Generic;
using System.IO;
using System;

namespace MailClient.Providers
{
    public class MailTemplateProvider : IMailTemplateProvider
    {
        private const string _templateDirName = "MailTemplates";
        private readonly IMailSettingProvider _settings;

        private readonly IDictionary<NotificationTypeEnu, string> _templateFileNameMappings
            = new Dictionary<NotificationTypeEnu, string>()
            {
                { NotificationTypeEnu.WELCOME, "welcome-template.html" },
                { NotificationTypeEnu.EMAIL_CONFIRMATION, "email-confirmation-template.html" },
                { NotificationTypeEnu.MEETING_REGISTRATION, "meeting-participation-template.html" },
                { NotificationTypeEnu.MEETING_CREATION, "meeting-creation-template.html" }
            };

        public MailTemplateProvider(
            IMailSettingProvider settings
        )
        { 
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private string PathToFile(string fileName)
            => Path.Combine(this._settings.GetTemplateDirPath(), fileName);

        private string ReadContentFromFile(string fileName)
        {
            var path = this.PathToFile(fileName);
            if(!File.Exists(path))
            {
                throw new Exception($"Template not found with provided path - {path}");
            }

            return File.ReadAllText(path);
        }

        public string GetTemplateContents(NotificationTypeEnu typeEnu)
            => this.ReadContentFromFile(this._templateFileNameMappings[typeEnu]);
    }
}