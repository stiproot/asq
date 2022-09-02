using DTO.Domain;
using System.Collections.Generic;
using System;

namespace DTO.Events
{
    public class SendMailEvent : BaseEvent
    {
        public Guid tracking_id{ get; set; }
        public IEnumerable<MailDto> mails_to_send{ get; set; }
    }
}