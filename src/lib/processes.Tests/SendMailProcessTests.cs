using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using managers.Resource;
using processes.Factory;
using DTO.Events;

namespace processes.Tests
{
    public class SendMailProcessTests
    {
        private const bool _runSendMailProcess = false;
        //private readonly IKafkaResourceManager _kafkaResourceManager;
        private readonly IProcessFactory _processFactory;

        public SendMailProcessTests(
            IProcessFactory processFactory
            //IKafkaResourceManager kafkaResourceManager
        )
        {
            this._processFactory = processFactory;
            //this._kafkaResourceManager = kafkaResourceManager;
        }

        //[Fact]
        //public void SendMailTest()
        //{
            //try
            //{
                //if(!_runSendMailProcess) return;

                //Task t = RunSendMailProcess();
                //t.Wait();
            //}
            //catch(Exception ex)
            //{
                //throw;
            //}
        //}

        //public async Task RunSendMailProcess()
        //{
            //// arrange
            //var message = await this._kafkaResourceManager.ReadMailEvent();
            //if(message == null)
            //{
                //throw new Exception("Consumption result from mail topic is null");
            //}

            ////throw new Exception(message);

            //var @event = JsonSerializer.Deserialize<SendMailEvent>(message);
            //if(@event == null)
            //{
                //throw new Exception("Config is null after deserializing");
            //}

            //var process = _processFactory.Create(Process.ProcessTypeEnu.SendMailProcess);
            //if(process == null)
            //{
                //throw new Exception("Process is null");
            //}
            
            //process.SetEvent(@event).Init();

            //// act
            //await process.Execute();
        //}
    }
}
