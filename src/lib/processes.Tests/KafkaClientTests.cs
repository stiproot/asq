//using DTO.Events;
//using DTO.Notification;
//using managers.Resource;
//using Xunit;
//using System.Text.Json;
//using System;

//namespace processes.Tests
//{
    //public class KafkaClientTests
    //{
        //private const bool _pushToQ = false;
        //private const bool _readFromTestMailQ = false;
        //private const bool _readFromMailQ = false;
        //private readonly IKafkaResourceManager _kafkaResourceManager;

        //public KafkaClientTests(IKafkaResourceManager kafkaResourceManager) => this._kafkaResourceManager = kafkaResourceManager;

        //[Fact]
        //public async void TestPushToMailQueue()
        //{
            //if(!_pushToQ) return;

            //var configStr = "{\"ToEmailAddress\":\"stipcich.simon@gmail.com\",\"ToUsername\":\"simon\",\"ToName\":\"simon\",\"ToSurname\":\"stipcich\",\"MailContentType\":2,\"ToUniqueId\":\"bd60aa54-5246-4c3b-ade9-edc22b08afc5\",\"ParticipantUsername\":null,\"ParticipantName\":null,\"ParticipantSurname\":null,\"ExtMeetingStartUrl\":null,\"MeetingId\":0,\"MeetingTimezone\":null,\"MeetingStartDatetime\":null}";

            //var config = JsonSerializer.Deserialize<NotificationConfig>(configStr);

            ////var config = new NotificationConfig
            ////{
                ////ToEmailAddress = "simon@asq.properties",
                ////ToName = "simon",
                ////ToSurname = "stipcich",
                ////ToUsername = "stipsmoosh"
            ////};

            //var @event = new SendMailEvent
            //{
                //tracking_id = Guid.NewGuid(),
                //mail_config = config
            //};

            //await this._kafkaResourceManager.PushMailTestEvent(@event);
        //}

        //[Fact]
        //public async void TestReadFromTestMailQueue()
        //{
            //if(!_readFromTestMailQ) return;

            //var message = await this._kafkaResourceManager.ReadTestMailEvent();
            //Assert.NotNull(message);
        //}

        //[Fact]
        //public async void TestReadFromMailQueue()
        //{
            //if(!_readFromMailQ) return;

            //var message = await this._kafkaResourceManager.ReadMailEvent();
            //throw new Exception(message);
            //Assert.NotNull(message);
        //}
    //}
//}
